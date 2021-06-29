using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace VideoViewer2Playback
{
	public partial class MainForm : Form
	{
		#region private fields

		private Item _selectItem1;
		private ImageViewerControl _imageViewerControl1;
		private Item _selectItem2;
		private ImageViewerControl _imageViewerControl2;
		private double _speed1 = 0.0;
		private double _speed2 = 0.0;
		private bool mode2InLive = true;
		private Item _related1Microphone;
        private Item _related1Speaker;
		private Item _related2Microphone;
        private Item _related2Speaker;

        private AudioPlayerControl _audioPlayerControl1Microphone;
        private AudioPlayerControl _audioPlayerControl1Speaker;

        private AudioPlayerControl _audioPlayerControl2Microphone;
        private AudioPlayerControl _audioPlayerControl2Speaker;

        private FQID _playbackControllerFQID;

		#endregion

		#region construction and close

		public MainForm()
		{
			InitializeComponent();

			EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler,
			                                             new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));
			EnvironmentManager.Instance.RegisterReceiver(PlaybackIndicationHandler,
														 new MessageIdFilter(MessageId.SmartClient.PlaybackIndication));

			// Create audio players 
			_audioPlayerControl1Microphone = ClientControl.Instance.GenerateAudioPlayerControl();
			Controls.Add(_audioPlayerControl1Microphone);

            _audioPlayerControl1Speaker = ClientControl.Instance.GenerateAudioPlayerControl();
            Controls.Add(_audioPlayerControl1Speaker);

            _audioPlayerControl2Microphone = ClientControl.Instance.GenerateAudioPlayerControl();
			Controls.Add(_audioPlayerControl2Microphone);

            _audioPlayerControl2Speaker = ClientControl.Instance.GenerateAudioPlayerControl();
            Controls.Add(_audioPlayerControl2Speaker);
        }

        private void OnClose(object sender, EventArgs e)
		{
            if (_playbackControllerFQID != null)
            {
                ClientControl.Instance.ReleasePlaybackController(_playbackControllerFQID);
                _playbackControllerFQID = null;
            }
			Close();
		}
		#endregion

		#region Time changed event handler

		private object PlaybackTimeChangedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
		{
			DateTime time = (DateTime)message.Data;
			// The built-in PlaybackController has FQID==null
			if (sender == null)
				textBoxTime.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();

			// The PlaybackController on the right hand side has a specific FQID
			if (sender!=null && _playbackControllerFQID!=null && sender.ObjectId==_playbackControllerFQID.ObjectId)
				textBoxTime2.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();
			return null;
		}

		private object PlaybackIndicationHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
		{
			if ((sender==null && _playbackControllerFQID==null) ||  _playbackControllerFQID.EqualGuids(sender))		// If message from right hand side control
			{
				if (message.Data is PlaybackCommandData)
				{
					PlaybackCommandData data = (PlaybackCommandData) message.Data;
					if (data.Command == PlaybackData.PlayStop)
					{
						_speed2 = 0;
					}
				}
			}
			return null;
		}

		#endregion

		#region Playback Click handling - Left hand camera
		private void OnSelect1Click(object sender, EventArgs e)
		{
			if (_imageViewerControl1 != null)
			{
				_imageViewerControl1.Disconnect();
			}
			_audioPlayerControl1Microphone.Initialize();
            _audioPlayerControl1Speaker.Initialize();

			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems());
			if (form.ShowDialog() == DialogResult.OK)
			{
				_selectItem1 = form.SelectedItem;
				buttonSelect1.Text = _selectItem1.Name;

				_imageViewerControl1 = ClientControl.Instance.GenerateImageViewerControl();
				_imageViewerControl1.Dock = DockStyle.Fill;
				_imageViewerControl1.ClickEvent += new EventHandler(ImageViewerControl1Click);
				panel1.Controls.Clear();
				panel1.Controls.Add(_imageViewerControl1);
				_imageViewerControl1.CameraFQID = _selectItem1.FQID;
				_imageViewerControl1.Initialize();
				_imageViewerControl1.Connect();
				_imageViewerControl1.Selected = true;
				checkBoxZoom1.Enabled = true;
				_imageViewerControl1.EnableMouseControlledPtz = true;
				_imageViewerControl1.EnableMousePtzEmbeddedHandler = true;

                checkBoxAudio.Enabled = false;
                List<Item> related = _selectItem1.GetRelated();

                // Test for any related microphones and speakers, and pick out the first one
                _related1Microphone = null;
                _related1Speaker = null;

                if (related!=null)
				{
					foreach (Item item in related)
						if (item.FQID.Kind == Kind.Microphone)
						{
							_related1Microphone = item;
							_audioPlayerControl1Microphone.MicrophoneFQID = _related1Microphone.FQID;
							break;
						}
                    foreach (Item item in related)
                        if (item.FQID.Kind == Kind.Speaker)
                        {
                            _related1Speaker = item;
                            _audioPlayerControl1Speaker.SpeakerFQID = _related1Speaker.FQID;
                            break;
                        }
                    checkBoxAudio.Enabled = _related1Microphone != null || _related1Speaker != null;
                    checkBoxAudio.Checked = checkBoxAudio.Enabled;
                }
            }
		}

		void ImageViewerControl1Click(object sender, EventArgs e)
		{
			if (_imageViewerControl2 != null)
				_imageViewerControl2.Selected = false;
			_imageViewerControl1.Selected = true;
		}


		private void OnStop1Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
			                                        	MessageId.SmartClient.PlaybackCommand, 
														new PlaybackCommandData() { Command=PlaybackData.PlayStop}));
			EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
			buttonMode.Text = "Current mode: Playback";
			_speed1 = 0.0;
		}

		private void OnMode1Click(object sender, EventArgs e)
		{
			if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
			{
				EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
				buttonMode.Text = "Current mode: Playback";
			}
			else
			{
				EnvironmentManager.Instance.Mode = Mode.ClientLive;
				buttonMode.Text = "Current mode: Live";
			}
			buttonReverse.Enabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
			buttonForward.Enabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
			buttonStop.Enabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
		}

		private void OnReverse1Click(object sender, EventArgs e)
		{
			if (_speed1 == 0.0)
				_speed1 = 1.0;
			else
				_speed1 *= 2;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand, 
														new PlaybackCommandData() { Command=PlaybackData.PlayReverse, Speed=_speed1}));
		}

		private void OnForward1Click(object sender, EventArgs e)
		{
			if (_speed1 == 0.0)
				_speed1 = 1.0;
			else
				_speed1 *= 2;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand, 
														new PlaybackCommandData() { Command=PlaybackData.PlayForward, Speed = _speed1}));
		}

		private void OnAudio1CheckChanged(object sender, EventArgs e)
		{
			if (checkBoxAudio.Checked)
			{
                if (_related1Microphone != null)
                {
                    _audioPlayerControl1Microphone.Connect();
                    if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
                        _audioPlayerControl1Microphone.StartLive();
                    else
                    {
                        _audioPlayerControl1Microphone.StartBrowse();
                    }
                }
                if (_related1Speaker != null)
                {
                    _audioPlayerControl1Speaker.Connect();
                    if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
                        _audioPlayerControl1Speaker.StartLive();
                    else
                    {
                        _audioPlayerControl1Speaker.StartBrowse();
                    }
                }
            }
            else
			{
				_audioPlayerControl1Microphone.Disconnect();
                _audioPlayerControl1Speaker.Disconnect();
            }
		}

		private void OnZoom1CheckChanged(object sender, EventArgs e)
		{
			_imageViewerControl1.EnableDigitalZoom = checkBoxZoom1.Checked;
		}

		#endregion

		#region Playback Click handling - Right hand camera

		private void OnSelect2Click(object sender, EventArgs e)
		{
			if (_imageViewerControl2 != null)
			{
				_imageViewerControl2.Disconnect();
			}
			_audioPlayerControl2Microphone.Initialize();
            _audioPlayerControl2Speaker.Initialize();

            ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems());
			if (form.ShowDialog() == DialogResult.OK)
			{
				_selectItem2 = form.SelectedItem;
				buttonSelect2.Text = _selectItem2.Name;

				// Make sure we have a separate playback controller for right hand side camera
				if (_playbackControllerFQID == null)
					_playbackControllerFQID = ClientControl.Instance.GeneratePlaybackController();
				_imageViewerControl2 = ClientControl.Instance.GenerateImageViewerControl();
				_imageViewerControl2.PlaybackControllerFQID = _playbackControllerFQID;
				_imageViewerControl2.Dock = DockStyle.Fill;
				_imageViewerControl2.ClickEvent += new EventHandler(ImageViewerControl2Click);
				panel2.Controls.Clear();
				panel2.Controls.Add(_imageViewerControl2);
				_imageViewerControl2.CameraFQID = _selectItem2.FQID;
				_imageViewerControl2.Initialize();
				_imageViewerControl2.Connect();
				_imageViewerControl2.Selected = true;
				checkBoxZoom2.Enabled = true;


				// Always start out in live
				mode2InLive = true;
				EnvironmentManager.Instance.SendMessage(
					new VideoOS.Platform.Messaging.Message(MessageId.System.ModeChangeCommand, Mode.ClientLive),
					_playbackControllerFQID);

                // Test for any related microphones and speakers, and pick out the first one
                _related2Microphone = null;
                _related2Speaker = null;

                checkBoxAudio2.Enabled = false;
				List<Item> related = _selectItem2.GetRelated();
				if (related != null)
				{
					foreach (Item item in related)
						if (item.FQID.Kind == Kind.Microphone)
						{
							_related2Microphone = item;
							_audioPlayerControl2Microphone.MicrophoneFQID = _related2Microphone.FQID;
							_audioPlayerControl2Microphone.PlaybackControllerFQID = _playbackControllerFQID;
							break;
						}
                    foreach (Item item in related)
                        if (item.FQID.Kind == Kind.Speaker)
                        {
                            _related2Speaker = item;
                            _audioPlayerControl2Speaker.SpeakerFQID = _related2Speaker.FQID;
                            _audioPlayerControl2Speaker.PlaybackControllerFQID = _playbackControllerFQID;
                            break;
                        }
                    checkBoxAudio2.Enabled = _related2Microphone != null || _related2Speaker != null;
                    checkBoxAudio2.Checked = checkBoxAudio2.Enabled;
                }

            }
		}

		void ImageViewerControl2Click(object sender, EventArgs e)
		{
			if (_imageViewerControl1 != null)
				_imageViewerControl1.Selected = false;
			_imageViewerControl2.Selected = true;
		}


		private void OnStop2Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PlayStop }), 
														_playbackControllerFQID);
			
			mode2InLive = false;
			buttonMode2.Text = "Current mode: Playback";
			_speed2 = 0.0;
		}

		private void OnMode2Click(object sender, EventArgs e)
		{
			mode2InLive = !mode2InLive;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.System.ModeChangeCommand, mode2InLive ? Mode.ClientLive : Mode.ClientPlayback),
				_playbackControllerFQID);
			if (mode2InLive == false)
			{
				buttonMode2.Text = "Current mode: Playback";
			}
			else
			{
				buttonMode2.Text = "Current mode: Live";
			}
			buttonReverse2.Enabled = !mode2InLive;
			buttonForward2.Enabled = !mode2InLive;
			buttonStop2.Enabled = !mode2InLive;
			radioButtonNoskip.Enabled = !mode2InLive;
			radioButtonSkip.Enabled = !mode2InLive;
			radioButtonStopAtEnd.Enabled = !mode2InLive;
		}

		private void OnReverse2Click(object sender, EventArgs e)
		{
			if (_speed2 == 0.0)
				_speed2 = 1.0;
			else
				_speed2 *= 2;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed2 }), 
														_playbackControllerFQID);
		}

		private void OnForward2Click(object sender, EventArgs e)
		{
			if (_speed2 == 0.0)
				_speed2 = 1.0;
			else
				_speed2 *= 2;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed2 }), 
														_playbackControllerFQID);
		}

		private void OnAudio2CheckChanged(object sender, EventArgs e)
		{
			if (checkBoxAudio2.Checked)
			{
				_audioPlayerControl2Microphone.Connect();
                _audioPlayerControl2Speaker.Connect();
                if (mode2InLive)
                {
                    _audioPlayerControl2Microphone.StartLive();
                    _audioPlayerControl2Speaker.StartLive();
                }
                else
                {
                    _audioPlayerControl2Microphone.StartBrowse();
                    _audioPlayerControl2Speaker.StartBrowse();
                }
            }
			else
			{
				_audioPlayerControl2Microphone.Disconnect();
                _audioPlayerControl2Speaker.Disconnect();
            }
        }

		private void OnSkipModeChanged(object sender, EventArgs e)
		{
			PlaybackSkipModeData mode = PlaybackSkipModeData.Noskip;
			if (radioButtonSkip.Checked)
				mode = PlaybackSkipModeData.Skip;
			if (radioButtonStopAtEnd.Checked)
				mode = PlaybackSkipModeData.StopAtSequenceEnd;

			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackSkipModeCommand, mode),
														_playbackControllerFQID);
		}

		private void OnZoom2CheckChanged(object sender, EventArgs e)
		{
			_imageViewerControl2.EnableDigitalZoom = checkBoxZoom2.Checked;
		}

        #endregion

    }
}
