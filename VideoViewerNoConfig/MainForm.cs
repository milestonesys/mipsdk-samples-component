using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.SDK;
using VideoOS.Platform.UI;

namespace VideoViewer
{
	public partial class MainForm : Form
	{
	    private UserContext _userContext;
		private Item _selectItem1;
		private ImageViewerControl _imageViewerControl1;
        private AudioPlayerControl _audioPlayerControl1;
		private double _speed = 0.0;
	    private FQID _playbackFQID;
        private Mode _myMode = Mode.ClientPlayback;

		public MainForm()
		{
			InitializeComponent();

            _playbackFQID = VideoOS.Platform.ClientControl.Instance.GeneratePlaybackController();

			EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler,
			                                             new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));

            String[] cameras = Directory.GetFiles("C:\\CameraXml\\");       //We store camera xml here!!

		    comboBox1.Items.Add("Select a camera ...");
		    foreach (String name in cameras)
		    {
		        comboBox1.Items.Add(name);
		    }
		    comboBox1.SelectedIndex = 0;
       
            String[] microphones = Directory.GetFiles("C:\\MicrophoneXml\\");       //We store microphone xml here!!

            comboBoxAudio.Items.Add("Select a microphone ...");
            foreach (String name in microphones)
            {
                comboBoxAudio.Items.Add(name);
            }
            comboBoxAudio.SelectedIndex = 0;
        }


		#region ImageViewer select

        private void OnCameraSelect(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                return;

			if (_imageViewerControl1!=null)
			{
				_imageViewerControl1.Disconnect();
				_imageViewerControl1.Close();
				_imageViewerControl1.Dispose();
				_imageViewerControl1 = null;
			}

            /*
            if (_userContext != null)
            {
                VideoOS.Platform.SDK.MultiEnvironment.Logout(_userContext);
                _userContext = null;
            }*/

            try
            {
                if (_userContext == null)
                {
                    _userContext = VideoOS.Platform.SDK.MultiEnvironment.CreateSingleServerUserContext(textBoxUser.Text,
                        textBoxPassword.Text, checkBoxAd.Checked, new UriBuilder(textBoxServer.Text).Uri);
                    VideoOS.Platform.SDK.MultiEnvironment.LoginUserContext(_userContext, false, false);
                }

                if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex > comboBox1.Items.Count)
                    return;
                string name = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                string xml = File.ReadAllText(name);

                _imageViewerControl1 = ClientControl.Instance.GenerateImageViewerControl();
                _imageViewerControl1.Dock = DockStyle.Fill;
                _imageViewerControl1.ClickEvent += new EventHandler(_imageViewerControl1_ClickEvent);
                panel1.Controls.Clear();
                panel1.Controls.Add(_imageViewerControl1);

                //_imageViewerControl1.CameraFQID = _selectItem1.FQID;  --- this is replaced with below line:
                _imageViewerControl1.SetCameraXml(
                    _userContext.Configuration.ServerFQID.ServerId.UserContext, xml);
                FQID cameraFQID = _imageViewerControl1.CameraFQID;
                _selectItem1 = _userContext.Configuration.GetItem(cameraFQID);

                System.Collections.Generic.List<Item> related = _selectItem1.GetRelated();

                // Lets enable/disable the header based on the tick mark.  Could also disable LiveIndicator or CameraName.
                _imageViewerControl1.EnableVisibleHeader = checkBoxHeader.Checked;
                _imageViewerControl1.EnableMousePtzEmbeddedHandler = true;
                _imageViewerControl1.MaintainImageAspectRatio = true;
                _imageViewerControl1.SetVideoQuality(0, 1);
                _imageViewerControl1.ImageOrPaintInfoChanged += ImageOrPaintChangedHandler;
                _imageViewerControl1.Initialize();
                _imageViewerControl1.Connect();
                _imageViewerControl1.Selected = true;
                _imageViewerControl1.PlaybackControllerFQID = _playbackFQID;
                _imageViewerControl1.ConnectResponseEvent += _imageViewerControl1_ConnectResponseEvent;
                _imageViewerControl1.EnableDigitalZoom = checkBoxDigitalZoom.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to login - " + ex.Message);
            }
        }

        void _imageViewerControl1_ConnectResponseEvent(object sender, ConnectResponseEventEventArgs e)
        {
            //We like to start in Live mode
            SetMode(Mode.ClientLive);
        }

	    private void SetMode(Mode newMode)
	    {
	        if (_myMode != newMode)
	        {
	            _myMode = newMode;
			    EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
			                                        	MessageId.System.ModeChangeCommand, newMode), _playbackFQID);
	        }
	    }

		void ImageOrPaintChangedHandler(object sender, EventArgs e)
		{
			Debug.WriteLine("ImageSize:"+_imageViewerControl1.ImageSize.Width+"x"+_imageViewerControl1.ImageSize.Height+", PaintSIze:"+
				_imageViewerControl1.PaintSize.Width+"x"+_imageViewerControl1.PaintSize.Height+
				", PaintLocation:" + _imageViewerControl1.PaintLocation.X + "-" + _imageViewerControl1.PaintLocation.Y);
		}

		void _imageViewerControl1_ClickEvent(object sender, EventArgs e)
		{
			_imageViewerControl1.Selected = true;
		}
		#endregion

		#region Time changed event handler

		private object PlaybackTimeChangedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
		{
			DateTime time = ((DateTime)message.Data).ToLocalTime();
			textBoxTime.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();

            if (_imageViewerControl1 != null)
    			_imageViewerControl1.Invalidate();
			return null;
		}

		#endregion

		#region Playback Click handling

		private void checkBoxDigitalZoom_CheckedChanged(object sender, EventArgs e)
		{
			if (_imageViewerControl1!=null)
				_imageViewerControl1.EnableDigitalZoom = checkBoxDigitalZoom.Checked;
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
			                                        	MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayStop }), _playbackFQID);
			//EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            SetMode(Mode.ClientPlayback);

			buttonMode.Text = "Current mode: Playback";
			_speed = 0.0;
		}

		private void OnModeClick(object sender, EventArgs e)
		{
            if (_myMode == Mode.ClientLive)
			{
				buttonMode.Text = "Current mode: Playback";
                SetMode(Mode.ClientPlayback);
            }
			else
			{
				buttonMode.Text = "Current mode: Live";
                SetMode(Mode.ClientLive);
            }
		}

		private void buttonReverse_Click(object sender, EventArgs e)
		{
			if (_speed == 0.0)
				_speed = 1.0;
			else
				_speed *= 2;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed }), _playbackFQID);
		}

		private void buttonForward_Click(object sender, EventArgs e)
		{
			if (_speed == 0.0)
				_speed = 1.0;
			else
				_speed *= 2;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed }), _playbackFQID);
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.Begin }),_playbackFQID);
		}

		private void buttonEnd_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.End }), _playbackFQID);
		}

		private void OnPrevSequence(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PreviousSequence }), _playbackFQID);
		}

		private void OnNextSequence(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.NextSequence }), _playbackFQID);
		}

		private void OnPreviousFrame(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.Previous }), _playbackFQID);
		}

		private void OnNextFrame(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.Next }), _playbackFQID);
		}

		#endregion

		private void OnStartRecording1(object sender, EventArgs e)
		{
			if (_selectItem1 != null)
				EnvironmentManager.Instance.SendMessage(
					new VideoOS.Platform.Messaging.Message(MessageId.Control.StartRecordingCommand), _selectItem1.FQID);
		}

		private void OnStopRecording1(object sender, EventArgs e)
		{
			if (_selectItem1 != null)
				EnvironmentManager.Instance.SendMessage(
					new VideoOS.Platform.Messaging.Message(MessageId.Control.StopRecordingCommand), _selectItem1.FQID);
		}

		private void OnClose(object sender, EventArgs e)
		{
            if (_playbackFQID != null)
            {
                ClientControl.Instance.ReleasePlaybackController(_playbackFQID);
                _playbackFQID = null;
            }
            Close();
		}

        private void OnMicrophoneSelected(object sender, EventArgs e)
        {
            if (comboBoxAudio.SelectedIndex == 0)
                return;

			if (_audioPlayerControl1!=null)
			{
                _audioPlayerControl1.Disconnect();
                _audioPlayerControl1.Close();
                _audioPlayerControl1.Dispose();
                _audioPlayerControl1 = null;
			}

            /*
            if (_userContext != null)
            {
                VideoOS.Platform.SDK.MultiEnvironment.Logout(_userContext);
                _userContext = null;
            }*/

            try
            {
                if (_userContext == null)
                {
                    _userContext = VideoOS.Platform.SDK.MultiEnvironment.CreateSingleServerUserContext(textBoxUser.Text,
                        textBoxPassword.Text, checkBoxAd.Checked, new UriBuilder(textBoxServer.Text).Uri);
                    VideoOS.Platform.SDK.MultiEnvironment.LoginUserContext(_userContext, false, false);
                }

                if (comboBoxAudio.SelectedIndex == 0 || comboBoxAudio.SelectedIndex > comboBoxAudio.Items.Count)
                    return;
                string name = comboBoxAudio.Items[comboBoxAudio.SelectedIndex].ToString();
                string xml = File.ReadAllText(name);

                _audioPlayerControl1 = ClientControl.Instance.GenerateAudioPlayerControl();

                panel2.Controls.Clear();
                panel2.Controls.Add(_audioPlayerControl1);

                //_imageViewerControl1.CameraFQID = _selectItem1.FQID;  --- this is replaced with below line:
                _audioPlayerControl1.SetAudioXml(
                    _userContext.Configuration.ServerFQID.ServerId.UserContext, xml);

                //FQID cameraFQID = _audioPlayerControl1.CameraFQID;
                //_selectItem1 = _userContext.Configuration.GetItem(cameraFQID);


                _audioPlayerControl1.Initialize();
                _audioPlayerControl1.Connect();
                _audioPlayerControl1.PlaybackControllerFQID = _playbackFQID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to login - " + ex.Message);
            }
        }


	}
}
