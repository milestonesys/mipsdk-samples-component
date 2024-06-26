using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VideoOS.Platform;
using VideoOS.Platform.Live;
using VideoOS.Platform.SDK.OAuth;
using VideoOS.Platform.UI;

namespace MultiUserEnvironment
{
    /// <summary>
    /// Interaction logic for UserContextControl.xaml
    /// </summary>
    public partial class UserContextControl : UserControl, INotifyPropertyChanged
    {
        private UserContext _userContext;
        private Item _selectedItem;
        private JPEGLiveSource _jpegLiveSource;

        public ObservableCollection<Item> CamerasUserContext { get; } = new ObservableCollection<Item>();

        private ConfigurationMonitor _configurationMonitor;
        public ConfigurationMonitor ConfigurationMonitor { get => _configurationMonitor; set => _configurationMonitor = value; }
        public Uri ServerUri { get; set; }
        public string AuthTypeGroupName { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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

        public UserContextControl()
        {
            DataContext = this;
            InitializeComponent();
        }

        public void FillCameraList()
        {
            if (_userContext == null)
            {
                return;
            }

            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(() => { FillCameraList(); }));
            }
            else
            {
                SearchResult searchResult;
                IEnumerable<Item> allCameras = _userContext.Configuration.GetItemsBySearch(Kind.Camera.ToString(), 100, 5, out searchResult).Where(i => i.FQID.FolderType == FolderType.No);
                foreach (Item item in allCameras)
                {
                    if (item.Enabled)
                        CamerasUserContext.Add(item);
                }
            }
        }

        public void CloseJpegLiveSource()
        {
            if (_jpegLiveSource != null)
            {
                _jpegLiveSource.LiveContentEvent += new EventHandler(JpegLiveSourceLiveNotificationEvent);
                _jpegLiveSource.Close();
                _jpegLiveSource = null;
            }
        }

        private void OnLogon(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                var useOAuth = _useOAuthTokenCheckBox.IsChecked ?? false;
                var isAdUser = _negotiateAuthTypeRadioButton.IsChecked ?? false;
                if (useOAuth)
                {
                    MipTokenCache tokenCache = IdpHelper.GetTokenCache(ServerUri, _userNameBox.Text, _passwordBox.Password, isAdUser);
                    _userContext = VideoOS.Platform.SDK.MultiUserEnvironment.CreateUserContext(tokenCache);
                }
                else
                {
                    _userContext = VideoOS.Platform.SDK.MultiUserEnvironment.CreateUserContext(_userNameBox.Text, _passwordBox.Password, isAdUser);
                }

                VideoOS.Platform.SDK.MultiUserEnvironment.LoginUserContext(_userContext);

                _selectedCameraButton.IsEnabled = true;
                FillCameraList();

                _configurationMonitor.AddUserContext(_userContext);

                _onLogoutButton.IsEnabled = true;
                _onLogonButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Logon User 1", ex);
            }
            Cursor = Cursors.Arrow;
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            CloseJpegLiveSource();
            VideoOS.Platform.SDK.MultiUserEnvironment.Logout(_userContext);
            VideoOS.Platform.SDK.MultiUserEnvironment.RemoveUserContext(_userContext);
            _configurationMonitor.RemoveUserContext(_userContext);
            _userContext = null;
            VideoImage = null;
            CamerasUserContext.Clear();
            _onLogoutButton.IsEnabled = false;
            _onLogonButton.IsEnabled = true;
            _selectedCameraButton.IsEnabled = false;
            _selectedCameraButton.Content = "Select camera...";
        }

        private void OnSelectCamera(object sender, RoutedEventArgs e)
        {
            // Ask user to select a camera

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
            itemPicker.SelectionMode = SelectionModeOptions.AutoCloseOnSelect;
            itemPicker.Items = _userContext.Configuration.GetItemsByKind(Kind.Camera);

            List<Item> system = _userContext.Configuration.GetItemsByKind(Kind.Camera, ItemHierarchy.SystemDefined);
            List<Item> user = _userContext.Configuration.GetItemsByKind(Kind.Camera, ItemHierarchy.UserDefined);
            var systemItems = system.All(i => i.FQID.FolderType == FolderType.No);
            var userItems = user.All(i => i.FQID.FolderType == FolderType.No);

            if (itemPicker.ShowDialog().Value)
            {
                CloseJpegLiveSource();

                _selectedItem = itemPicker.SelectedItems.First();
                _selectedCameraButton.Content = _selectedItem.Name;

                _jpegLiveSource = new JPEGLiveSource(_selectedItem);
                try
                {
                    _jpegLiveSource.Width = (int)_imageFrame.ActualWidth;
                    _jpegLiveSource.Height = (int)_imageFrame.ActualHeight;
                    _jpegLiveSource.LiveModeStart = true;
                    _jpegLiveSource.Init();
                    _jpegLiveSource.LiveContentEvent += new EventHandler(JpegLiveSourceLiveNotificationEvent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not Init:" + ex.Message);
                    _jpegLiveSource = null;
                }
            }
        }

        /// <summary>
        /// This event is called when JPEG is available or some exception has occurred
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JpegLiveSourceLiveNotificationEvent(object sender, EventArgs e)
        {
            if (!CheckAccess())
            {
                // Make sure we execute on the UI thread before updating UI Controls
                Dispatcher.BeginInvoke(new EventHandler(JpegLiveSourceLiveNotificationEvent), new[] { sender, e });
            }
            else
            {
                LiveContentEventArgs args = e as LiveContentEventArgs;
                if (args != null)
                {
                    if (args.LiveContent != null)
                    {
                        // Display the received JPEG

                        int width = args.LiveContent.Width;
                        int height = args.LiveContent.Height;

                        MemoryStream ms = new MemoryStream(args.LiveContent.Content);
                        Bitmap myImage = new Bitmap(ms);
                        var rightSizedBitmap = myImage;
                        if (myImage.Width != _imageFrame.ActualWidth || myImage.Height != _imageFrame.ActualHeight)
                        {
                            rightSizedBitmap = new Bitmap(myImage, (int)_imageFrame.ActualWidth, (int)_imageFrame.ActualHeight);
                        }

                        VideoImage = ToBitmapImage(rightSizedBitmap);

                        myImage.Dispose();
                        ms.Close();
                        ms.Dispose();
                        args.LiveContent.Dispose();
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
                                            Brushes.White, new PointF(20, (float)_imageFrame.ActualHeight / 2 - 20));
                        }
                        else
                        {
                            g.DrawString(args.Exception.Message, new Font(System.Drawing.FontFamily.GenericMonospace, 12),
                                            Brushes.White, new PointF(20, (float)_imageFrame.ActualHeight / 2 - 20));
                        }
                        g.Dispose();
                        VideoImage = ToBitmapImage(bitmap);
                        bitmap.Dispose();
                    }
                }
            }
        }

        private static BitmapImage ToBitmapImage(Bitmap bitmap)
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
    }
}
