using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VideoOS.Platform;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace VideoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow
    {
        private Item _selectItem1;
        private Item _selectItem2;
        private double _speed = 0.0;

        public MainWindow()
        {
            InitializeComponent();

            EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler,
                new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));

            checkBoxHeader.IsChecked = true;
        }

        #region ImageViewer 1 select

        private void Button_Select1_Click(object sender, RoutedEventArgs e)
        {
            _imageViewerWpfControl1.Disconnect();
            _imageViewerWpfControl1.Close();

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                _selectItem1 = itemPicker.SelectedItems.First();
                buttonSelect1.Content = _selectItem1.Name;
                
                _imageViewerWpfControl1.CameraFQID = _selectItem1.FQID;
                // Lets enable/disable the header based on the tick mark. Could also disable LiveIndicator or CameraName.
                _imageViewerWpfControl1.EnableVisibleHeader = checkBoxHeader.IsChecked.Value;
                _imageViewerWpfControl1.EnableVisibleLiveIndicator = EnvironmentManager.Instance.Mode == Mode.ClientLive;
                _imageViewerWpfControl1.AdaptiveStreaming = checkBoxAdaptiveStreaming.IsChecked.Value;

                _imageViewerWpfControl1.Initialize();
                _imageViewerWpfControl1.Connect();
                _imageViewerWpfControl1.Selected = true;

                _imageViewerWpfControl1.EnableDigitalZoom = checkBoxDigitalZoom.IsChecked.Value;
            }
        }

        private void ImageViewerWpfControl1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _imageViewerWpfControl2.Selected = false;
            _imageViewerWpfControl1.Selected = true;
        }

        void ImageOrPaintChangedHandler(object sender, EventArgs e)
        {
            Debug.WriteLine("ImageSize:" + _imageViewerWpfControl1.ImageSize.Width + "x" + _imageViewerWpfControl1.ImageSize.Height + ", PaintSIze:" +
                            _imageViewerWpfControl1.PaintSize.Width + "x" + _imageViewerWpfControl1.PaintSize.Height +
                            ", PaintLocation:" + _imageViewerWpfControl1.PaintLocation.X + "-" + _imageViewerWpfControl1.PaintLocation.Y);
        }
        #endregion

        #region ImageViewer 1 Record start/stop

        private void ButtonStartRecording1_Click(object sender, RoutedEventArgs e)
        {
            if (_selectItem1 != null)
                EnvironmentManager.Instance.PostMessage(
                    new VideoOS.Platform.Messaging.Message(MessageId.Control.StartRecordingCommand), _selectItem1.FQID);
        }

        private void ButtonStopRecording1_Click(object sender, RoutedEventArgs e)
        {
            if (_selectItem1 != null)
                EnvironmentManager.Instance.PostMessage(
                    new VideoOS.Platform.Messaging.Message(MessageId.Control.StopRecordingCommand), _selectItem1.FQID);
        }
        #endregion

        #region ImageViewer 2 select

        private void Button_Select2_Click(object sender, RoutedEventArgs e)
        {
            _imageViewerWpfControl2.Disconnect();
            _imageViewerWpfControl2.Close();

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                _selectItem2 = itemPicker.SelectedItems.First();
                buttonSelect2.Content = _selectItem2.Name;
                
                _imageViewerWpfControl2.CameraFQID = _selectItem2.FQID;
                _imageViewerWpfControl2.EnableVisibleHeader = checkBoxHeader.IsChecked.Value;
                _imageViewerWpfControl2.AdaptiveStreaming = checkBoxAdaptiveStreaming.IsChecked.Value;

                _imageViewerWpfControl2.Initialize();
                _imageViewerWpfControl2.Connect();
                _imageViewerWpfControl2.Selected = true;

                _imageViewerWpfControl2.EnableDigitalZoom = checkBoxDigitalZoom.IsChecked.Value;
            }
        }

        private void ImageViewerWpfControl2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _imageViewerWpfControl1.Selected = false;
            _imageViewerWpfControl2.Selected = true;
        }
        #endregion

        #region ImageViewer 2 Record start/stop

        private void ButtonStartRecording2_Click(object sender, RoutedEventArgs e)
        {
            if (_selectItem2 != null)
                EnvironmentManager.Instance.PostMessage(
                    new VideoOS.Platform.Messaging.Message(MessageId.Control.StartRecordingCommand), _selectItem2.FQID);
        }

        private void ButtonStopRecording2_Click(object sender, RoutedEventArgs e)
        {
            if (_selectItem2 != null)
                EnvironmentManager.Instance.PostMessage(
                    new VideoOS.Platform.Messaging.Message(MessageId.Control.StopRecordingCommand), _selectItem2.FQID);
        }
        #endregion

        #region Time changed event handler

        private void HandleTimeChanged(DateTime time)
        {
            textBoxTime.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();
        }

        private object PlaybackTimeChangedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
        {
            DateTime time = ((DateTime)message.Data).ToLocalTime();
            if (Dispatcher.CheckAccess())
                HandleTimeChanged(time);
            else
                Dispatcher.BeginInvoke(new Action(() => HandleTimeChanged(time)));

            return null;
        }

        #endregion

        #region Checkboxes
        
        private void checkBoxHeader_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxHeader();
        }

        private void CheckBoxHeader_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxHeader();
        }

        private void checkBoxDigitalZoom_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxDigitalZoom();
        }

        private void CheckBoxDigitalZoom_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxDigitalZoom();
        }

        private void checkBoxAdaptiveStreaming_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAdaptiveStreaming();
        }
        
        private void CheckBoxAdaptiveStreaming_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAdaptiveStreaming();
        }

        private void UpdateCheckBoxHeader()
        {
            _imageViewerWpfControl1.EnableVisibleHeader = checkBoxHeader.IsChecked.Value;
            _imageViewerWpfControl2.EnableVisibleHeader = checkBoxHeader.IsChecked.Value;
        }

        private void UpdateCheckBoxDigitalZoom()
        {
            _imageViewerWpfControl1.EnableDigitalZoom = checkBoxDigitalZoom.IsChecked.Value;
            _imageViewerWpfControl2.EnableDigitalZoom = checkBoxDigitalZoom.IsChecked.Value;
        }

        private void UpdateCheckBoxAdaptiveStreaming()
        {
            _imageViewerWpfControl1.AdaptiveStreaming = checkBoxAdaptiveStreaming.IsChecked.Value;
        }

        #endregion

        #region Playback click handling

        private void ButtonPreviousSequence_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PreviousSequence }));
        }

        private void ButtonDBStart_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.Begin }));
        }

        private void ButtonPreviousFrame_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.Previous }));
        }

        private void buttonMode_Click(object sender, RoutedEventArgs e)
        {
            if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
            {
                _imageViewerWpfControl1.EnableVisibleLiveIndicator = false;
                EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
                buttonMode.Content = "Current mode: Playback";
            }
            else
            {
                _imageViewerWpfControl1.EnableVisibleLiveIndicator = true;
                EnvironmentManager.Instance.Mode = Mode.ClientLive;
                buttonMode.Content = "Current mode: Live";
            }
        }

        private void ButtonReverse_Click(object sender, RoutedEventArgs e)
        {
            if (_speed == 0.0)
                _speed = 1.0;
            else
                _speed *= 2;
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed }));
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
            EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            buttonMode.Content = "Current mode: Playback";
            _speed = 0.0;
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            if (_speed == 0.0)
                _speed = 1.0;
            else
                _speed *= 2;
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed }));
        }

        private void ButtonNextSequence_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.NextSequence }));
        }

        private void ButtonDBEnd_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.End }));
        }

        private void ButtonextFrame_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.Next }));
        }
        #endregion

        private string _selectedStoragePath = "";
        private void ButtonOpenDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog())
                {
                    if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _selectedStoragePath = folderBrowserDialog1.SelectedPath;
                        if (System.IO.File.Exists(System.IO.Path.Combine(_selectedStoragePath, "cache.xml")))
                        {
                            var uri = new Uri("file:\\" + _selectedStoragePath);
                            VideoOS.Platform.SDK.Environment.AddServer(false, uri,
                                System.Net.CredentialCache.DefaultNetworkCredentials);

                            VideoOS.Platform.SDK.Environment.LoadConfiguration(uri);
                        }
                        else
                        {
                            MessageBox.Show("No cache.xml file was found in the selected folder.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Folder select", ex);
            }
        }

        private void ButtonLiftMask_Click(object sender, RoutedEventArgs e)
        {
            Configuration.Instance.ServerFQID.ServerId.UserContext.SetPrivacyMaskLifted(!Configuration.Instance.ServerFQID.ServerId.UserContext.PrivacyMaskLifted);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}