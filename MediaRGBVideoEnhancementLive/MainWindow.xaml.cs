using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using VideoOS.Platform;
using VideoOS.Platform.Live;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace MediaRGBVideoEnhancementLive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow, INotifyPropertyChanged
    {
        private Item _selectedCamera;
        private BitmapLiveSource _bitmapLiveSource;
        private int _counter = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        private ToolkitRGBEnhancement.RGBHandling.Transform _transform;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            _transform = new ToolkitRGBEnhancement.RGBHandling.Transform();
            Transformation_ValueChanged(null, null);
        }

        private bool CameraSelected => _selectedCamera != null;

        private bool _stopped = true;
        private bool Stopped
        {
            get => _stopped;
            set
            {
                _stopped = value;
                OnPropertyChanged(nameof(RestartEnabled));
                OnPropertyChanged(nameof(StopEnabled));
            }
        }

        public bool RestartEnabled => CameraSelected && Stopped;
        public bool StopEnabled => CameraSelected && !Stopped;

        public string CameraName => CameraSelected ? _selectedCamera.Name : "Select camera...";

        private string _frameCountText = "...";
        public string FrameCountText 
        {
            get => _frameCountText;
            private set
            {
                _frameCountText = value;
                OnPropertyChanged(nameof(FrameCountText));
            }
        }

        private BitmapImage _imageSource = null;
        public BitmapImage VideoImage
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(VideoImage));
            }
        }

        private void UpdateItem(Item cameraItem)
        {
            _selectedCamera = cameraItem;
            OnPropertyChanged(nameof(RestartEnabled));
            OnPropertyChanged(nameof(StopEnabled));
            OnPropertyChanged(nameof(CameraName));
        }

        private void SelectCameraButton_Click(object sender, RoutedEventArgs e)
        {
            if (CameraSelected)
            {
                TerminateVideo();
            }

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                UpdateItem(itemPicker.SelectedItems.First());

                Stopped = false;

                InitializeVideo();
            }
        }

        #region User actions
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Stopped = true;
            TerminateVideo();

            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Stopped = false;

            InitializeVideo();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Stopped = true;

            TerminateVideo();
        }

        private void Transformation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_transform != null)
            {
                _transform.SetVectors((int)(_RScrollBar.Value * _exposeSlider.Value), (int)(_GScrollBar.Value * _exposeSlider.Value), (int)(_BScrollBar.Value * _exposeSlider.Value), (int)_offsetSlider.Value);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Bitmap handling
        private void BitmapLiveSourceLiveContentEvent(object sender, EventArgs e)
        {
            try
            {
                LiveContentEventArgs args = e as LiveContentEventArgs;
                if (args != null)
                {
                    if (args.LiveContent != null)
                    {
                        LiveSourceBitmapContent bitmapContent = args.LiveContent as LiveSourceBitmapContent;
                        if (bitmapContent != null)
                        {
                            FrameCountText = _counter++.ToString();
                            if (Stopped)
                            {
                                bitmapContent.Dispose();
                            }
                            else
                            {
                                // The following code does these functions:
                                //    Get a IntPtr to the start of the GBR bitmap
                                //    Transform via sample transformation (To be replaced with your C++ code)
                                //    Create a Bitmap with the result
                                //    Create a new Bitmap scaled to visible area on screen
                                //    Assign new Bitmap into PictureBox
                                //    Dispose first Bitmap
                                //
                                // The transformation is therefore done on the original image, but if the transformation is
                                // keeping to the same size, then this would be much more effective if the resize was done first,
                                // and the transformation afterwards.
                                // Scaling can be done by setting the Width and Height on the 

                                int width = bitmapContent.GetPlaneWidth(0);
                                int height = bitmapContent.GetPlaneHeight(0);
                                int stride = bitmapContent.GetPlaneStride(0);

                                // When using RGB / BGR bitmaps, they have all bytes continues in memory.  The PlanePointer(0) is used for all planes:
                                IntPtr plane0 = bitmapContent.GetPlanePointer(0);

                                IntPtr newPlane0 = _transform.Perform(plane0, stride, width, height);        // Make the sample transformation / color change

                                var myImage = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, newPlane0);
                                var rightSizedBitmap = myImage;
                                if (width != _enhancedImageFrame.ActualWidth || height != _enhancedImageFrame.ActualHeight)
                                {
                                    // We need to resize to the displayed area
                                    rightSizedBitmap = new Bitmap(myImage, (int)_enhancedImageFrame.ActualWidth, (int)_enhancedImageFrame.ActualHeight);
                                }

                                VideoImage = ToBitmapImage(rightSizedBitmap);

                                myImage.Dispose();
                                bitmapContent.Dispose();
                                _transform.Release(newPlane0);
                            }
                        }
                    }
                    else if (args.Exception != null)
                    {
                        // Handle any exceptions occurred inside toolkit or on the communication to the VMS

                        Bitmap bitmap = new Bitmap(320, 240);
                        Graphics g = Graphics.FromImage(bitmap);
                        g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                        if (args.Exception is CommunicationMIPException)
                        {
                            g.DrawString("Connection lost to server ...", new Font(System.Drawing.FontFamily.GenericMonospace, 12),
                                         Brushes.White, new PointF(20, (float)_enhancedImageFrame.ActualHeight / 2 - 20));
                        }
                        else
                        {
                            g.DrawString(args.Exception.Message, new Font(System.Drawing.FontFamily.GenericMonospace, 12),
                                         Brushes.White, new PointF(20, (float)_enhancedImageFrame.ActualHeight / 2 - 20));
                        }
                        g.Dispose();
                        VideoImage = ToBitmapImage(bitmap);
                        bitmap.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("BitmapLiveSourceLiveContentEvent", ex);
            }
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        #endregion

        #region private methods

        private void InitializeVideo()
        {
            _imageViewer.CameraFQID = _selectedCamera.FQID;
            _imageViewer.MaintainImageAspectRatio = true;
            _imageViewer.Initialize();
            _imageViewer.Connect();
            _imageViewer.Selected = true;

            _bitmapLiveSource = new BitmapLiveSource(_selectedCamera, BitmapFormat.BGR24);
            _bitmapLiveSource.LiveContentEvent += new EventHandler(BitmapLiveSourceLiveContentEvent);
            try
            {
                _bitmapLiveSource.Width = (int)_enhancedImageFrame.ActualWidth;
                _bitmapLiveSource.Height = (int)_enhancedImageFrame.ActualHeight;
                _bitmapLiveSource.SetKeepAspectRatio(true, true);
                _bitmapLiveSource.Init();
                _bitmapLiveSource.LiveModeStart = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to recording server:" + ex.Message);
            }
        }

        private void TerminateVideo()
        {
            if (_bitmapLiveSource != null)
            {
                _bitmapLiveSource.LiveContentEvent -= new EventHandler(BitmapLiveSourceLiveContentEvent);
                _bitmapLiveSource.LiveModeStart = false;
                _bitmapLiveSource.Close();
                _bitmapLiveSource = null;
            }
            _imageViewer.Disconnect();
            _imageViewer.Close();
        }

        private void _imageEnhanced_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_enhancedImageFrame.ActualWidth != 0 && _bitmapLiveSource != null)
            {
                _bitmapLiveSource.Width = (int)_enhancedImageFrame.ActualWidth;
                _bitmapLiveSource.Height = (int)_enhancedImageFrame.ActualHeight;
                _bitmapLiveSource.SetWidthHeight();
            }
        }
        #endregion
    }
}
