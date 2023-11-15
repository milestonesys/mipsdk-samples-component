using System;
using System.Linq;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace LogOnServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow
    {
        private Item _selectedItem;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Select_Camera(object sender, RoutedEventArgs e)
        {
            if (_imageViewer.IsInitialized)
            {
                _imageViewer.Disconnect();
                _imageViewer.Close();
            }

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
            itemPicker.SelectionMode = SelectionModeOptions.AutoCloseOnSelect;
            itemPicker.Items = Configuration.Instance.GetItemsByKind(Kind.Camera);

            if (itemPicker.ShowDialog().Value)
            {
                _selectedItem = itemPicker.SelectedItems.First();
                selectCameraButton.Content = _selectedItem.Name;

                _imageViewer.CameraFQID = _selectedItem.FQID;
                _imageViewer.EnableVisibleHeader = true;
                _imageViewer.Initialize();
                _imageViewer.Connect();
                _imageViewer.Selected = true;
            }
        }

        private void OnStartRecording(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
                EnvironmentManager.Instance.SendMessage(
                    new VideoOS.Platform.Messaging.Message(MessageId.Control.StartRecordingCommand), _selectedItem.FQID);
            LogResourceHandler.LogStart(_selectedItem);
        }

        private void OnStopRecording(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
                EnvironmentManager.Instance.SendMessage(
                    new VideoOS.Platform.Messaging.Message(MessageId.Control.StopRecordingCommand), _selectedItem.FQID);
            LogResourceHandler.LogStop(_selectedItem);
        }

        private void OnGranted(object sender, EventArgs e)
        {
            LogResourceHandler.CardSwiped(true);

            LogResourceHandler.CardSwiped(false);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            VideoOS.Platform.SDK.Environment.Logout();
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }
    }
}
