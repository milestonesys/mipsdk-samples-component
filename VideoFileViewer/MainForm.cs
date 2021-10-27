using System;
using System.IO;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.UI;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.Client;
using System.Collections.Generic;

namespace VideoFileViewer
{
    public partial class MainForm : Form
    {
        private ImageViewerControl _imageViewerControl;
        private PlaybackUserControl _playbackUserControl;
        private Item _camera;
        private string _selectedStoragePath;

        private static readonly Guid IntegrationId = new Guid("6F636F95-59C7-40FF-BBBF-F3C6D9EB549E");
        private const string IntegrationName = "Video File Viewer";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        public MainForm()
        {
            InitializeComponent();
            //This sample only uses PlayBackMode...
            EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _playbackUserControl = ClientControl.Instance.GeneratePlaybackUserControl();
            _playbackUserControl.Visible = true;
            _playbackUserControl.Dock = DockStyle.Fill;
            _panelPlayback.Controls.Add(_playbackUserControl);
            _playbackUserControl.BringToFront();
            _playbackUserControl.Init(null);
        }

        private void OnClose(object sender, EventArgs e)
        {
            if (_playbackUserControl != null)
            {
                _playbackUserControl.Close();
                _playbackUserControl = null;
            }

            if (_imageViewerControl != null)
            {
                _imageViewerControl.Disconnect();
                _imageViewerControl.Close();
                _panelImageViewer.Controls.Remove(_imageViewerControl);
                _imageViewerControl.Dispose();
                _imageViewerControl = null;
            }

            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                                                        MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.Begin }));
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                                                        MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = (double)1.0 }));
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                                                        MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
        }

        private void buttonDB_Click(object sender, EventArgs e)
        {
            // - OPEN an export in database format 
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
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
                                buttonSelectCamera.Enabled = true;
                            }
                            catch (NotAuthorizedMIPException)
                            {
                                PasswordForm form = new PasswordForm();
                                DialogResult result = form.ShowDialog();
                                if (result == DialogResult.Cancel)
                                    return;
                                if (result == DialogResult.OK)
                                    password = form.Password;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No cache.xml file was found in the selected folder.");
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

        private void buttonSelectCamera_Click(object sender, EventArgs e)
        {
            if (_imageViewerControl != null)
            {
                _imageViewerControl.Disconnect();
                _imageViewerControl.Close();
                _panelImageViewer.Controls.Remove(_imageViewerControl);
                _imageViewerControl.Dispose();
                _imageViewerControl = null;
            }
            // Select Camera
            ItemPickerForm form = new ItemPickerForm();
            form.KindFilter = Kind.Camera;
            form.AutoAccept = true;
            form.Init(Configuration.Instance.GetItems(ItemHierarchy.Both));

            // Set Camera to ImageViewerControl
            if (form.ShowDialog() == DialogResult.OK)
            {
                _camera = form.SelectedItem;
                buttonSelectCamera.Text = _camera.Name;

                _imageViewerControl = ClientControl.Instance.GenerateImageViewerControl();
                _imageViewerControl.Dock = DockStyle.Fill;
                _panelImageViewer.Controls.Add(_imageViewerControl);
                _imageViewerControl.CameraFQID = _camera.FQID;
                _imageViewerControl.Initialize();
                _imageViewerControl.Connect();
                _imageViewerControl.Selected = true;

                _playbackUserControl.SetCameras(new List<FQID>() { _camera.FQID });
                _playbackUserControl.SetEnabled(true);  // Refresh the TimeLine 
                EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            }
        }
    }
}
