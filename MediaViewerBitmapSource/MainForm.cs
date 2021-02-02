using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.SDK.UI.LoginDialog;
using VideoOS.Platform.UI;

namespace MediaViewerBitmapSource
{
    /// <summary>
    /// The key class shown here is the BitmapSource, that handles changes between live and playback -
    /// as well as handle all playback commands.
    /// </summary>
    public partial class MainForm : Form
    {
        #region private fields

        private static readonly Guid IntegrationId = new Guid("FE72F0F2-1EDE-4823-B9EF-5E3C28EB6774");
        private const string IntegrationName = "Media Viewer BitmapSource";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";
        private FQID _playbackFQID;
        private Item _newlySelectedItem;
        private PlaybackController _playbackController;
        private PlaybackUserControl _playbackUserControl;

        //private PlaybackTimeInformationData _currentTimeInformation;
        private BitmapSource _bitmapSource;

        private bool _loggedOn;

        #endregion

        #region construction & close

        public MainForm()
        {
            InitializeComponent();

        }

        private void OnLoad(object sender, EventArgs e)
        {
            // In this sample we create a specific PlaybackController.
            // All commands to this controller needs to be sent via messages with the destination as _playbackFQID.
            // All message Indications coming from this controller will have sender as _playbackController.

            _playbackFQID = null; // use default playbackController instead of: ClientControl.Instance.GeneratePlaybackController();

            _playbackUserControl = ClientControl.Instance.GeneratePlaybackUserControl();
            _playbackUserControl.Visible = true;
            _playbackUserControl.Dock = DockStyle.Fill;
            panelPlayback.Controls.Add(_playbackUserControl);
            _playbackUserControl.BringToFront();
            _playbackUserControl.Init(_playbackFQID);

            _playbackController = ClientControl.Instance.GetPlaybackController(_playbackFQID);
            _bitmapSource = new BitmapSource();
            _bitmapSource.PlaybackFQID = _playbackFQID;
            _bitmapSource.NewBitmapEvent += _bitmapSource_NewBitmapEvent;
            _bitmapSource.Selected = true;


            EnvironmentManager.Instance.TraceFunctionCalls = true;
        }
        private void OnClose(object sender, EventArgs e)
        {
            VideoOS.Platform.SDK.Environment.Logout();
            if (_bitmapSource != null)
            {
                CloseBitmap();
                _bitmapSource.NewBitmapEvent -= _bitmapSource_NewBitmapEvent;
                _bitmapSource = null;
            }

            if (_playbackUserControl != null)
            {
                _playbackUserControl.Close();
                _playbackUserControl = null;
            }
            this.Close();
        }


        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void InitBitmap()
        {
            if (_bitmapSource != null)
            {
                _bitmapSource.Init();
            }
        }

        private void CloseCurrent()
        {
            CloseBitmap();

            pictureBox.Image = new Bitmap(100, 60);
            buttonCamera.Text = "Select camera...";
            buttonCamera.Enabled = false;
            _loggedOn = false;
        }

        private void CloseBitmap()
        {
            if (_bitmapSource != null)
            {
                _bitmapSource.Close();
            }
        }

        #endregion

        #region Source selection

        #region login to a server

        private void LoginClick(object sender, EventArgs e)
        {
            try
            {
                if (!_loggedOn)
                {
                    DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
                    loginForm.ShowDialog();
                    if (Connected == false)
                        return;
                    _loggedOn = true;
                    buttonCamera.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Folder select", ex);
            }
        }

        #endregion

        #region Select camera

        private void OnSelectCamera(object sender, EventArgs e)
        {
            try
            {
                CloseCurrent();

                ItemPickerForm form = new ItemPickerForm();
                form.KindFilter = Kind.Camera;
                form.AutoAccept = true;
                form.Init(Configuration.Instance.GetItems(ItemHierarchy.Both));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _newlySelectedItem = form.SelectedItem;
                    buttonCamera.Text = _newlySelectedItem.Name;
                    buttonSelectServer.Enabled = false;
                    _bitmapSource.Item = _newlySelectedItem;
                    InitBitmap();

                    _playbackUserControl.SetCameras(new List<FQID>() { _newlySelectedItem.FQID });
                    _playbackUserControl.SetEnabled(true);  // Refresh the TimeLine 

                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Camera select", ex);
            }
        }

        private static bool Connected = false;
        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }
        #endregion

        #endregion

        #region Private methods

        // We save current bitmap, for redisplay during ReSize
        private Bitmap _currentOriginalBitmap = null;
        void _bitmapSource_NewBitmapEvent(Bitmap bitmap)
        {

            if (InvokeRequired)
            {
                if (_currentOriginalBitmap != null)
                    _currentOriginalBitmap.Dispose();
                _currentOriginalBitmap = new Bitmap(bitmap);
                bitmap.Dispose();

                this.BeginInvoke(new MethodInvoker(() => _bitmapSource_NewBitmapEvent(_currentOriginalBitmap)));
            }
            else
            {
                lock (this)
                {
                    try
                    {
                        pictureBox.Image = new Bitmap(bitmap, pictureBox.Width, pictureBox.Height);
                        this.Refresh();
                    }
                    catch (InvalidOperationException ioe)
                    {
                        System.Diagnostics.Debug.WriteLine("ioe " + ioe.Message);
                    }
                    catch (ArgumentException ae)
                    {
                        System.Diagnostics.Debug.WriteLine("ae " + ae.Message);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("other " + ex.Message);
                    }
                }
            }
        }

        private void OnReSizePictureBox(object sender, EventArgs e)
        {
            try
            {
                lock (this)
                {
                    if (_currentOriginalBitmap != null && pictureBox.Width != 0 && pictureBox.Height != 0)
                    {
                        // We will not set the Image while Resizing (as it generates more resize events) - but do it afterwards via BeginInvoke
                        BeginInvoke(
                            new MethodInvoker(
                                () => pictureBox.Image = new Bitmap(_currentOriginalBitmap, pictureBox.Width, pictureBox.Height)));
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("OnReSizePictureBox", ex);
            }
        }

        private void buttonLoop_Click(object sender, EventArgs e)
        {
            DateTime start = _playbackController.PlaybackTime;
            DateTime end = _playbackController.PlaybackTime + TimeSpan.FromSeconds(20);

            _playbackController.SequenceProgressChanged += new EventHandler<PlaybackController.ProgressChangedEventArgs>(_playbackController_SequenceProgressChanged);
            _playbackController.SetSequence(start, end);
            _playbackController.PlaybackMode = PlaybackController.PlaybackModeType.Forward;
            _playbackController.PlaybackSpeed = 5.0F;
        }

        void _playbackController_SequenceProgressChanged(object sender, PlaybackController.ProgressChangedEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                try
                {
                    progressBar1.Value = Convert.ToInt32(e.Progress * 100);
                }
                catch (Exception ex)
                {
                    EnvironmentManager.Instance.ExceptionDialog("_playbackController_SequenceProgressChanged", ex);
                }
            }));
        }
        private void buttonLoopStop_Click(object sender, EventArgs e)
        {
            _playbackController.SequenceProgressChanged -= new EventHandler<PlaybackController.ProgressChangedEventArgs>(_playbackController_SequenceProgressChanged);
            _playbackController.SetSequence(DateTime.MinValue,DateTime.MinValue);
            _playbackController.PlaybackMode = PlaybackController.PlaybackModeType.Stop;
            _playbackController.PlaybackSpeed = 1.0F;
        }


        #endregion

        private void OnModeChange(object sender, EventArgs e)
        {
            if (radioLive.Checked)
            {
                _playbackController.PlaybackMode = PlaybackController.PlaybackModeType.Stop;
                _bitmapSource.LiveStart();
                panelPlayback.Visible = false;
                panelLoop.Visible = false;
            }
            else
            {
                _bitmapSource.LiveStop();
                panelPlayback.Visible = true;
                panelLoop.Visible = true;
            }
        }


    }
}
