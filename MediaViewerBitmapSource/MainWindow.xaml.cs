using System.Collections.Generic;
using System;
using System.Windows;
using VideoOS.Platform.Client;
using VideoOS.Platform.UI;
using VideoOS.Platform;
using VideoOS.Platform.UI.Controls;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediaViewerBitmapSource
{
    /// <summary>
    /// The key class shown here is the BitmapSource, that handles changes between live and playback -
    /// as well as handle all playback commands.
    /// </summary>
    public partial class MainWindow : VideoOSWindow, INotifyPropertyChanged
    {
        private bool _loopingActive = false;
        private Item _newlySelectedItem;
        private PlaybackController _playbackController;
        private VideoOS.Platform.Client.BitmapSource _bitmapSource;

        private BitmapImage _imageSource = null;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool PlaybackEnabled => _newlySelectedItem != null && _playbackRadioButton.IsChecked.Value;
        public bool VideoModeRadioButtonsEnabled => _newlySelectedItem != null;

        private int _loopProgress = 0;
        public int LoopProgress {
            get => _loopProgress;
            set { 
                _loopProgress = value;
                OnPropertyChanged(nameof(LoopProgress));
            } 
        }

        #region initialize
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            InitVideoOSControls();
        }

        private void InitVideoOSControls()
        {
            // In this sample we create a specific PlaybackController.
            // All commands to this controller needs to be sent via messages with the destination as _playbackFQID.
            // All message Indications coming from this controller will have sender as _playbackController.

            FQID playbackControllerFQID = ClientControl.Instance.GeneratePlaybackController();
            _playbackUserControl.Init(playbackControllerFQID);
            _playbackController = ClientControl.Instance.GetPlaybackController(playbackControllerFQID);

            _bitmapSource = new VideoOS.Platform.Client.BitmapSource();
            _bitmapSource.PlaybackFQID = playbackControllerFQID;
            _bitmapSource.NewBitmapEvent += bitmapSource_NewBitmapEvent;
            _bitmapSource.Selected = true;
        }

        private void InitBitmap()
        {
            if (_bitmapSource != null)
            {
                _bitmapSource.Init();
            }
        }

        #endregion

        #region handling images
        private void bitmapSource_NewBitmapEvent(Bitmap bitmap)
        {
            if(!CheckAccess())
            {
                Bitmap copyOfBitmap = new Bitmap(bitmap);
                bitmap.Dispose();
                Dispatcher.BeginInvoke(new Action(() => bitmapSource_NewBitmapEvent(copyOfBitmap)));
            }
            else
            {
                lock (this)
                {
                    try
                    {
                        ImageSource = BitmapToImageSource(bitmap);
                        bitmap.Dispose();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        #endregion

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResetControls();

                ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
                {
                    KindsFilter = new List<Guid> { Kind.Camera },
                    SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                    Items = Configuration.Instance.GetItems()
                };

                if (itemPicker.ShowDialog().Value)
                {
                    _newlySelectedItem = itemPicker.SelectedItems.First();
                    CameraButton.Content = _newlySelectedItem.Name;

                    _bitmapSource.Item = _newlySelectedItem;
                    InitBitmap();

                    _playbackUserControl.SetCameras(new List<FQID>() { _newlySelectedItem.FQID });
                    _playbackUserControl.SetEnabled(true);
                }
                else
                {
                    _newlySelectedItem = null;
                }
                OnPropertyChanged(nameof(PlaybackEnabled));
                OnPropertyChanged(nameof(VideoModeRadioButtonsEnabled));
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Camera select", ex);
            }
        }

        private void playbackRadioButton_Changed(object sender, RoutedEventArgs e)
        {
            if(_bitmapSource == null)
            {
                return;
            }
            
            if (_playbackRadioButton.IsChecked.Value)
            {
                _bitmapSource.LiveStop();
            }
            else
            {
                _playbackController.PlaybackMode = PlaybackController.PlaybackModeType.Stop;
                StopLooping();
                _bitmapSource.LiveStart();
            }
            OnPropertyChanged(nameof(PlaybackEnabled));
        }

        #region Looping
        private void LoopButton_Click(object sender, RoutedEventArgs e)
        {
            if(!_loopingActive)
            {
                StartLooping();
            }
            else
            {
                StopLooping();
            }
        }

        private void StartLooping()
        {
            if (_loopingActive)
            {
                return;
            }

            _loopingActive = true;
            _loopButton.Content = "Stop looping";
            DateTime start = _playbackController.PlaybackTime;
            DateTime end = _playbackController.PlaybackTime + TimeSpan.FromSeconds(20);

            _playbackController.SequenceProgressChanged += new EventHandler<PlaybackController.ProgressChangedEventArgs>(playbackController_SequenceProgressChanged);
            _playbackController.SetSequence(start, end);
            _playbackController.PlaybackMode = PlaybackController.PlaybackModeType.Forward;
            _playbackController.PlaybackSpeed = 5.0F;
        }

        private void StopLooping() 
        { 
            if (!_loopingActive)
            {
                return;
            }

            _loopingActive = false;
            _loopButton.Content = "Start looping";
            _playbackController.SequenceProgressChanged -= new EventHandler<PlaybackController.ProgressChangedEventArgs>(playbackController_SequenceProgressChanged);
            _playbackController.SetSequence(DateTime.MinValue, DateTime.MinValue);
            _playbackController.PlaybackMode = PlaybackController.PlaybackModeType.Stop;
            _playbackController.PlaybackSpeed = 1.0F;
            LoopProgress = 0;
        }

        void playbackController_SequenceProgressChanged(object sender, PlaybackController.ProgressChangedEventArgs e)
        {
            LoopProgress = Convert.ToInt32(e.Progress * 100);
        }
        #endregion

        #region Closing
        private void ResetControls()
        {
            StopLooping();
            CloseBitmap();
            _playbackRadioButton.IsChecked = true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            VideoOS.Platform.SDK.Environment.Logout();
            StopLooping();
            if (_bitmapSource != null)
            {
                CloseBitmap();
                _bitmapSource.NewBitmapEvent -= bitmapSource_NewBitmapEvent;
                _bitmapSource = null;
            }

            if (_playbackUserControl != null)
            {
                _playbackUserControl.Close();
                _playbackUserControl = null;
            }

            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        private void CloseBitmap()
        {
            if (_bitmapSource != null)
            {
                _bitmapSource.Close();
            }
        }

        #endregion

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
