using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace VideoViewer
{
	public partial class MainForm : Form
	{
		private Item _selectItem1;
		private ImageViewerControl _imageViewerControl1;
		private Item _selectItem2;
		private ImageViewerControl _imageViewerControl2;
		private double _speed = 0.0;

		public MainForm()
		{
			InitializeComponent();

			EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler,
			                                             new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));
		}


		#region ImageViewer 1 select

		private void buttonSelect1_Click(object sender, EventArgs e)
		{
			if (_imageViewerControl1!=null)
			{
				_imageViewerControl1.Disconnect();
				_imageViewerControl1.Close();
				_imageViewerControl1.Dispose();
				_imageViewerControl1 = null;
			}

			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems());
			if (form.ShowDialog()==DialogResult.OK)
			{
				_selectItem1 = form.SelectedItem;
				buttonSelect1.Text = _selectItem1.Name;

				_imageViewerControl1 = ClientControl.Instance.GenerateImageViewerControl();
				_imageViewerControl1.Dock = DockStyle.Fill;
				_imageViewerControl1.ClickEvent += new EventHandler(_imageViewerControl1_ClickEvent);
				panel1.Controls.Clear();
				panel1.Controls.Add(_imageViewerControl1);
				_imageViewerControl1.CameraFQID = _selectItem1.FQID;
				// Lets enable/disable the header based on the tick mark.  Could also disable LiveIndicator or CameraName.
				_imageViewerControl1.EnableVisibleHeader = checkBoxHeader.Checked;
			    _imageViewerControl1.EnableVisibleLiveIndicator = EnvironmentManager.Instance.Mode == Mode.ClientLive;
				_imageViewerControl1.EnableMousePtzEmbeddedHandler = true;
				_imageViewerControl1.MaintainImageAspectRatio = true;
				_imageViewerControl1.ImageOrPaintInfoChanged += ImageOrPaintChangedHandler;

			    _imageViewerControl1.EnableRecordedImageDisplayedEvent = true;
                _imageViewerControl1.RecordedImageReceivedEvent += _imageViewerControl1_RecordedImageReceivedEvent;

			    _imageViewerControl1.EnableVisibleTimeStamp = true;
                _imageViewerControl1.AdaptiveStreaming = checkBoxAdaptiveStreaming.Checked;

                _imageViewerControl1.Initialize();
				_imageViewerControl1.Connect();
				_imageViewerControl1.Selected = true;

				_imageViewerControl1.EnableDigitalZoom = checkBoxDigitalZoom.Checked;
			}
		}

        void _imageViewerControl1_RecordedImageReceivedEvent(object sender, RecordedImageReceivedEventArgs e)
        {
        }

		void ImageOrPaintChangedHandler(object sender, EventArgs e)
		{
			Debug.WriteLine("ImageSize:"+_imageViewerControl1.ImageSize.Width+"x"+_imageViewerControl1.ImageSize.Height+", PaintSIze:"+
				_imageViewerControl1.PaintSize.Width+"x"+_imageViewerControl1.PaintSize.Height+
				", PaintLocation:" + _imageViewerControl1.PaintLocation.X + "-" + _imageViewerControl1.PaintLocation.Y);
		}

		void _imageViewerControl1_ClickEvent(object sender, EventArgs e)
		{
			if (_imageViewerControl2 != null)
				_imageViewerControl2.Selected = false;
			_imageViewerControl1.Selected = true;
		}
		#endregion

		#region ImageViewer 2 select

		private void buttonSelect2_Click(object sender, EventArgs e)
		{
			if (_imageViewerControl2 != null)
			{
				_imageViewerControl2.Disconnect();
				_imageViewerControl2.Close();
				_imageViewerControl2.Dispose();
				_imageViewerControl2 = null;
			}

			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems());
			if (form.ShowDialog() == DialogResult.OK)
			{
				_selectItem2 = form.SelectedItem;
				buttonSelect2.Text = _selectItem2.Name;

				_imageViewerControl2 = ClientControl.Instance.GenerateImageViewerControl();
				_imageViewerControl2.Dock = DockStyle.Fill;
				_imageViewerControl2.ClickEvent += new EventHandler(_imageViewerControl2_ClickEvent);
				panel2.Controls.Clear();
				panel2.Controls.Add(_imageViewerControl2);
				_imageViewerControl2.CameraFQID = _selectItem2.FQID;
				_imageViewerControl2.EnableVisibleHeader = checkBoxHeader.Checked;
				_imageViewerControl2.EnableMousePtzEmbeddedHandler = true;
                _imageViewerControl2.AdaptiveStreaming = checkBoxAdaptiveStreaming.Checked;
                _imageViewerControl2.Initialize();
				_imageViewerControl2.Connect();
				_imageViewerControl2.Selected = true;

				_imageViewerControl2.EnableDigitalZoom = checkBoxDigitalZoom.Checked;
			}
		}

		void _imageViewerControl2_ClickEvent(object sender, EventArgs e)
		{
			if (_imageViewerControl1 != null)
				_imageViewerControl1.Selected = false;
			_imageViewerControl2.Selected = true;
		}
		#endregion

		#region Time changed event handler

        private void HandleTimeChanged(DateTime time)
        {
            textBoxTime.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();

            _imageViewerControl1.Invalidate();
        }

        private object PlaybackTimeChangedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
		{
			DateTime time = ((DateTime)message.Data).ToLocalTime();
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => HandleTimeChanged(time)));
            }
            else
                HandleTimeChanged(time);
			return null;
		}

		#endregion

		#region Playback Click handling

		private void checkBoxDigitalZoom_CheckedChanged(object sender, EventArgs e)
		{
			if (_imageViewerControl1!=null)
				_imageViewerControl1.EnableDigitalZoom = checkBoxDigitalZoom.Checked;
			if (_imageViewerControl2 != null)
				_imageViewerControl2.EnableDigitalZoom = checkBoxDigitalZoom.Checked;
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
			                                        	MessageId.SmartClient.PlaybackCommand, 
														new PlaybackCommandData() { Command=PlaybackData.PlayStop}));
			EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            buttonMode.Text = "Current mode: Playback";
			_speed = 0.0;
		}

		private void OnModeClick(object sender, EventArgs e)
		{
			if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
			{
                if (_imageViewerControl1 != null)
                    _imageViewerControl1.EnableVisibleLiveIndicator = false;
                EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
				buttonMode.Text = "Current mode: Playback";
            }
			else
			{
                if (_imageViewerControl1 != null)
                    _imageViewerControl1.EnableVisibleLiveIndicator = true;
                EnvironmentManager.Instance.Mode = Mode.ClientLive;
				buttonMode.Text = "Current mode: Live";
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
														new PlaybackCommandData() { Command=PlaybackData.PlayReverse, Speed=_speed}));
		}

		private void buttonForward_Click(object sender, EventArgs e)
		{
			if (_speed == 0.0)
				_speed = 1.0;
			else
				_speed *= 2;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand, 
														new PlaybackCommandData() { Command=PlaybackData.PlayForward, Speed = _speed}));
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.Begin }));
		}

		private void buttonEnd_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.End }));
		}

		private void OnPrevSequence(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PreviousSequence}));
		}

		private void OnNextSequence(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.NextSequence }));
		}

		private void OnPreviousFrame(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.Previous }));
		}

		private void OnNextFrame(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.Next }));
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

		private void OnStartRecording2(object sender, EventArgs e)
		{
			if (_selectItem2 != null)
				EnvironmentManager.Instance.SendMessage(
					new VideoOS.Platform.Messaging.Message(MessageId.Control.StartRecordingCommand), _selectItem2.FQID);
		}

		private void OnStopRecording2(object sender, EventArgs e)
		{
			if (_selectItem2 != null)
				EnvironmentManager.Instance.SendMessage(
					new VideoOS.Platform.Messaging.Message(MessageId.Control.StopRecordingCommand), _selectItem2.FQID);
		}

		private void OnClose(object sender, EventArgs e)
		{
			Close();
		}

        private string _selectedStoragePath = "";
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    _selectedStoragePath = folderBrowserDialog1.SelectedPath;
                    if (System.IO.File.Exists(System.IO.Path.Combine(_selectedStoragePath, "cache.xml")) ||
                        System.IO.File.Exists(System.IO.Path.Combine(_selectedStoragePath, "archives_cache.xml")))
                    {
                        var uri = new Uri("file:\\" + _selectedStoragePath);
                        VideoOS.Platform.SDK.Environment.AddServer(false, uri, System.Net.CredentialCache.DefaultNetworkCredentials);

                        VideoOS.Platform.SDK.Environment.LoadConfiguration(uri);
                    }
                    else
                    {
                        MessageBox.Show("No cache.xml or archives_cache.xml file were found in selected folder.");
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Folder select", ex);
            }

        }

        private void buttonLiftMask_Click(object sender, EventArgs e)
        {
            Configuration.Instance.ServerFQID.ServerId.UserContext.SetPrivacyMaskLifted(!Configuration.Instance.ServerFQID.ServerId.UserContext.PrivacyMaskLifted);
        }

        private void checkBoxAdaptiveStreaming_CheckedChanged(object sender, EventArgs e)
        {
            if (_imageViewerControl1 != null)
            {
                _imageViewerControl1.AdaptiveStreaming = checkBoxAdaptiveStreaming.Checked;
            }
        }
    }
}
