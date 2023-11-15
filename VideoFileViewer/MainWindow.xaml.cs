using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace VideoFileViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow
    {
        private static readonly Guid IntegrationId = new Guid("6F636F95-59C7-40FF-BBBF-F3C6D9EB549E");
        private const string IntegrationName = "Video File Viewer";
        private const string Version = "2.0";
        private const string ManufacturerName = "Sample Manufacturer";

        private Item _camera;
        private string _selectedStoragePath;

        public MainWindow()
        {
            InitializeComponent();

            _playbackWpfUserControl.Init(null);
        }

        private void ButtonMedia_Click(object sender, RoutedEventArgs e)
        {
            // - OPEN an export in database format 
            try
            {
                using (var folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog())
                {
                    if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _selectedStoragePath = folderBrowserDialog1.SelectedPath;
                        if (File.Exists(Path.Combine(_selectedStoragePath, "cache.xml")))
                        {
                            bool done = false;
                            string password = "";
                            while (!done)
                            {
                                VideoOS.Platform.SDK.Environment.RemoveAllServers();
                                Uri uri = new Uri("file:\\" + _selectedStoragePath);
                                VideoOS.Platform.SDK.Environment.AddServer(false, uri, new System.Net.NetworkCredential("", password));

                                try
                                {
                                    VideoOS.Platform.SDK.Environment.Login(uri, IntegrationId, IntegrationName, Version, ManufacturerName);

                                    VideoOS.Platform.SDK.Environment.LoadConfiguration(uri);
                                    done = true;
                                    _buttonSelectCamera.IsEnabled = true;
                                }
                                catch (NotAuthorizedMIPException)
                                {
                                    PasswordWindow window = new PasswordWindow();
                                    bool? result = window.ShowDialog();
                                    if (!result.Value)
                                        return;
                                    if (result.Value)
                                        password = window.Password;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No cache.xml file was found in the selected folder.");
                        }
                    }
                }
            }
            catch (NotAuthorizedMIPException ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Not authorized", ex);
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Folder select", ex);
            }
        }

        private void ButtonSelectCamera_Click(object sender, RoutedEventArgs e)
        {
            _imageViewerWpfControl.Disconnect();
            _imageViewerWpfControl.Close();

            // Select Camera
            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems(ItemHierarchy.Both)
            };

            if (itemPicker.ShowDialog().Value)
            {
                _camera = itemPicker.SelectedItems.First();
                _buttonSelectCamera.Content = _camera.Name;

                _imageViewerWpfControl.CameraFQID = _camera.FQID;
                _imageViewerWpfControl.Initialize();
                _imageViewerWpfControl.Connect();
                _imageViewerWpfControl.Selected = true;
          
                _playbackWpfUserControl.SetCameras(new List<FQID>() { _camera.FQID });
                _playbackWpfUserControl.SetEnabled(true);
                EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            }
        }
        
        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = (double)1.0 }));
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.PostMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.Begin }));
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            _playbackWpfUserControl.Close();

            _imageViewerWpfControl.Disconnect();
            _imageViewerWpfControl.Close();
            _imageViewerWpfControl.Dispose();

            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }
    }
}