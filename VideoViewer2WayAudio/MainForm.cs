using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Data;
using VideoOS.Platform.Login;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace VideoViewer2WayAudio
{
    public partial class MainForm : Form
    {
        #region private fields

        private Item _selectItem1;
        private ImageViewerControl _imageViewerControl1;
        private double _speed1 = 0.0;
        private string _currentPlaybackMode = "";
        private Item _related1Microphone;
        private Item _related1Speaker=null;
        private AudioPlayerControl _audioPlayerControl1;
        private OutgoingSpeakerController _outgoingSpeakerController = null;
        private AudioStreamPlayer _audioStreamPlayer;

        private bool _fromPCMic = true;

        private bool _fromPCMicInitialized = false;

        private System.IO.FileStream soundFileStream = null;

        #endregion

        #region construction and close

        public MainForm()
        {
            InitializeComponent();

            EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler,
                                                         new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));
            //Initialize dialog components
            checkBoxAudio.Enabled = false;
            checkBoxAudio.Checked = true;
            checkBoxSpeaker.Enabled = false;
            progressBarMeter.Visible = false;
            checkBoxSpeaker.Checked = true;
            rb_pc_mic.Checked = _fromPCMic;
            rb_from_file.Checked = !_fromPCMic;
            onCheckedChanged_Speaker(this.checkBoxSpeaker, null);
            openFileDialog1.DefaultExt = "wav";
            openFileDialog1.Filter = "WAV sound files (*.wav)|*.wav";
            buttonMode.Enabled = false;
            comboBoxSampleRate.SelectedIndex = 0;

            // Create audio players and initialize
            _audioPlayerControl1 = ClientControl.Instance.GenerateAudioPlayerControl();
            Controls.Add(_audioPlayerControl1);

            _outgoingSpeakerController = new OutgoingSpeakerController();
            _outgoingSpeakerController.UIHandle = this;
            _outgoingSpeakerController.OutgoingAudioErrorTextEvent += new EventHandler(AudioController_OutgoingAudioErrorTextEvent);
            _outgoingSpeakerController.OutgoingAudioTransmitEnableStateChangedEvent += new EventHandler(AudioController_OutgoingAudioTransmitEnableStateChangedEvent);
            _outgoingSpeakerController.OutgoingAudioMeterEvent += new OutgoingSpeakerController.OutgoingAudioMeterEventHandler(AudioController_OutgoingAudioMeterEvent);
            _outgoingSpeakerController.DoRetryConnect = true;
        }



        private void InitializePCMicToSpeaker()
        {
            if (!_fromPCMicInitialized)
            {
                _outgoingSpeakerController.Close(0);
                var audioRecorder = ClientControl.Instance.GeneratePcAudioRecorder();
                audioRecorder.OutgoingAudioWaveFormatSamplesPerSecond = Int32.Parse(comboBoxSampleRate.SelectedItem.ToString());
                _outgoingSpeakerController.Init(audioRecorder);
                _outgoingSpeakerController.Connect(_related1Speaker.FQID,0);
                _fromPCMicInitialized = true;
            }
        }


        private void InitialiseAudioStreamToSpeaker(System.IO.Stream stream, bool initFromFile)
        {
            _fromPCMicInitialized = false; //If you shift to mic later, then I have to reinitialize the _outgoingSpeakerController
            _outgoingSpeakerController.Close(0);
            try
            {
                _audioStreamPlayer = new AudioStreamPlayer(stream, initFromFile);
                _audioStreamPlayer.ProgressCompleteEvent += new EventHandler(audioStreamPlayer_ProgressCompleteEvent);
                _outgoingSpeakerController.Init(_audioStreamPlayer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization of speaker failed. Cause: "+ex.Message);
                return;
            }
            progressBarMeter.Visible = true;
            _outgoingSpeakerController.Connect(_related1Speaker.FQID,0);
            _outgoingSpeakerController.TransmitStart();
        }

        void audioStreamPlayer_ProgressCompleteEvent(object sender, EventArgs e)
        {
            _outgoingSpeakerController.TransmitStop();
            _audioStreamPlayer.Free();
            _audioStreamPlayer.ProgressCompleteEvent -= audioStreamPlayer_ProgressCompleteEvent;
            _audioStreamPlayer = null;
        }

        private void OnClose(object sender, EventArgs e)
        {
            if (_outgoingSpeakerController != null)
            {
                _outgoingSpeakerController.OutgoingAudioErrorTextEvent -= new EventHandler(AudioController_OutgoingAudioErrorTextEvent);
                _outgoingSpeakerController.OutgoingAudioTransmitEnableStateChangedEvent -= new EventHandler(AudioController_OutgoingAudioTransmitEnableStateChangedEvent);
                _outgoingSpeakerController.OutgoingAudioMeterEvent -= new OutgoingSpeakerController.OutgoingAudioMeterEventHandler(AudioController_OutgoingAudioMeterEvent);
                _outgoingSpeakerController.Close(0);
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

            return null;
        }


        #endregion

        #region User Click handling
        private void OnSelect1Click(object sender, EventArgs e)
        {
            if (_imageViewerControl1 != null)
            {
                _imageViewerControl1.Disconnect();
            }

            _audioPlayerControl1.Disconnect();   
            _audioPlayerControl1.Close();

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
                buttonMode.Enabled = true;

                // Test for any related microphones, and pick out the first one
                _related1Microphone = null;
                checkBoxAudio.Enabled = false;
                checkBoxSpeaker.Enabled = false;
                List<Item> related = _selectItem1.GetRelated();

                _outgoingSpeakerController.Close(0);
                _fromPCMicInitialized = false;
                _outgoingSpeakerController.OutgoingAudioErrorText = "";

                if (related != null)
                {
                    foreach (Item item in related)
                    {
                        if (item.FQID.Kind == Kind.Microphone)
                        {
                            _related1Microphone = item;
                            checkBoxAudio.Enabled = true;
                            _audioPlayerControl1.MicrophoneFQID = _related1Microphone.FQID;
                            _audioPlayerControl1.Initialize();
                            checkBoxAudio.Checked = true;
                        }
                        if (item.FQID.Kind == Kind.Speaker)
                        {
                            _related1Speaker = item;
                            checkBoxSpeaker.Enabled = true;
                            checkBoxSpeaker.Checked = true;
                            this.buttonTalk.Enabled = true;	
                            this.rb_pc_mic.Checked = _fromPCMic;
                            this.rb_from_file.Checked = !_fromPCMic;

                            if (this.rb_pc_mic.Checked && _related1Speaker != null)
                                BeginInvoke(new MethodInvoker(delegate() { InitializePCMicToSpeaker(); }));

                        }
                    }
                }
                onCheckedChanged_Speaker(this.checkBoxSpeaker, null);
                OnAudio1CheckChanged(this.checkBoxAudio, null);
            }
        }

        void ImageViewerControl1Click(object sender, EventArgs e)
        {
            _imageViewerControl1.Selected = true;
        }


        private void OnStop1Click(object sender, EventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                                                        MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
            EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            buttonMode.Text = "Current mode: Playback";
            _speed1 = 0.0;
            _currentPlaybackMode = "";
        }

        private void OnMode1Click(object sender, EventArgs e)
        {
            if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
            {
                EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
                buttonMode.Text = "Current mode: Playback";
                _currentPlaybackMode = "";
            }
            else
            {
                EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                                                            MessageId.SmartClient.PlaybackCommand,
                                                            new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
                _speed1 = 0.0;
                _currentPlaybackMode = "";

                EnvironmentManager.Instance.Mode = Mode.ClientLive;
                buttonMode.Text = "Current mode: Live";
            }
            buttonReverse.Enabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
            buttonForward.Enabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
            buttonStop.Enabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;


        }

        private void OnReverse1Click(object sender, EventArgs e)
        {
            if (_speed1 == 0.0 || _currentPlaybackMode != PlaybackData.PlayReverse)
                _speed1 = 1.0;
            else
                _speed1 *= 2;
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                                                        MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed1 }));
            _currentPlaybackMode = PlaybackData.PlayReverse;
        }

        private void OnForward1Click(object sender, EventArgs e)
        {
            if (_speed1 == 0.0 || _currentPlaybackMode != PlaybackData.PlayForward)
                _speed1 = 1.0;
            else
                _speed1 *= 2;
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                                                        MessageId.SmartClient.PlaybackCommand,
                                                        new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed1 }));
            _currentPlaybackMode = PlaybackData.PlayForward;
        }

        private void OnAudio1CheckChanged(object sender, EventArgs e)
        {
            if (_audioPlayerControl1 == null)
                return;
            if (checkBoxAudio.Enabled && checkBoxAudio.Checked)
            {
                _audioPlayerControl1.Connect();
                
                if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
                    _audioPlayerControl1.StartLive();
                else
                {
                    _audioPlayerControl1.StartBrowse();
                }
            }
            else
            {
                _audioPlayerControl1.Disconnect();
            }
        }
        #endregion


        void AudioController_OutgoingAudioErrorTextEvent(object sender, EventArgs e)
        {
            errorProviderSpeaker.SetError(buttonTalk, _outgoingSpeakerController.OutgoingAudioErrorText);
        }

        private void OnMouseDownTalk(object sender, MouseEventArgs e)
        {
            progressBarMeter.Visible = true;
            if (!_fromPCMicInitialized)
                return;

            _outgoingSpeakerController.TransmitStart();
        }

        private void OnMouseUpTalk(object sender, MouseEventArgs e)
        {
            if (!_fromPCMicInitialized)
                return;
            _outgoingSpeakerController.TransmitStop();
            progressBarMeter.Visible = false;
        }

        private void AudioController_OutgoingAudioTransmitEnableStateChangedEvent(object sender, EventArgs e)
        {
            bool enabled = _outgoingSpeakerController.OutgoingAudioTransmitEnableState;
            if (this.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate()
                {
                    this.buttonTalk.Enabled = enabled;
                    if (enabled == false)
                        OnMouseUpTalk(this, null);
                    // Ensure re-connect will not continue after button has been disabled }));
                }));
            }
            else
            {
                //this.buttonTalk.Enabled = enabled;
                //if (enabled == false)
                //	OnMouseUpTalk(this, null);			// Ensure re-connect will not continue after button has been disabled
            }
        }

        /// <summary>
        /// Signals that the outgoing audio transmit enable state has changed
        /// </summary>
        void AudioController_OutgoingAudioMeterEvent(object sender, OutgoingSpeakerController.OutgoingAudioMeterEventArgs e)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate() { this.progressBarMeter.Value = e.OutgoingAudioMeter; }));
            }
            else
            {
                this.progressBarMeter.Value = e.OutgoingAudioMeter;
                if (_audioStreamPlayer!=null)
                    textBox1.Text = _audioStreamPlayer.Progress.ToString();
            }
        }

        private void onCheckedChanged_Radio_button(object sender, EventArgs e)
        {
            if (sender == this.rb_from_file)
                this.rb_pc_mic.Checked = !this.rb_from_file.Checked;
            else
                this.rb_from_file.Checked = !this.rb_pc_mic.Checked;
            this.buttonTalk.Enabled = this.rb_pc_mic.Checked;
            this.btn_Select_WAV_File.Enabled = this.rb_from_file.Checked;
            this.comboBoxSampleRate.Enabled = !this.rb_from_file.Checked;
            this.labelSampleRate.Enabled = !this.rb_from_file.Checked;

            if (this.rb_pc_mic.Checked)
                BeginInvoke(new MethodInvoker(delegate() { InitializePCMicToSpeaker(); }));

        }



        private void onBtnClick_Select_WAV_File(object sender, EventArgs e)
        {
            //Show file dialog
            openFileDialog1.DefaultExt = "wav";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return; //Do nothing

            string filename = openFileDialog1.FileName;
            if (!filename.ToLower().EndsWith(".wav"))
            {
                MessageBox.Show("Only WAV files can be used");
                return;
            }

            //Open a file stream
            try {
                if (soundFileStream != null)
                {
                    soundFileStream.Close();
                    soundFileStream = null;
                }
                soundFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the file: "+ex.Message);
                return;
            }

            try
            {
                BeginInvoke(new MethodInvoker(delegate() { InitialiseAudioStreamToSpeaker(soundFileStream, true); }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to play file: "+ex.Message);

                return;
            }
        }

        private void onCheckedChanged_Speaker(object sender, EventArgs e)
        {
            if (!this.checkBoxSpeaker.Enabled || !this.checkBoxSpeaker.Checked)
            {
                if (_outgoingSpeakerController != null)
                    _outgoingSpeakerController.Close(0);
                _fromPCMicInitialized = false; //Need to be reinitialized
                progressBarMeter.Visible = false;
                this.buttonTalk.Enabled = false;	//TEST
                this.btn_Select_WAV_File.Enabled = false;
                this.rb_from_file.Enabled = false;
                this.rb_pc_mic.Enabled = false;
                this.comboBoxSampleRate.Enabled = false;
                labelSampleRate.Enabled = false;
            }
            else
            {
                this.buttonTalk.Enabled = this.rb_pc_mic.Checked;	//TEST
                this.btn_Select_WAV_File.Enabled = this.rb_from_file.Checked;
                this.rb_from_file.Enabled = true;
                this.rb_pc_mic.Enabled = true;
                progressBarMeter.Visible = false;
                if (this.rb_pc_mic.Checked && _related1Speaker != null)
                    BeginInvoke(new MethodInvoker(delegate() { InitializePCMicToSpeaker(); }));
                this.comboBoxSampleRate.Enabled = true;
                labelSampleRate.Enabled = true;
            }
        }

        private void comboBoxSampleRate_SelectedValueChanged(object sender, EventArgs e)
        {
            // we changed recording sample rate, we need to reinitialize outgoing speaker
            _fromPCMicInitialized = false;
            if (this.rb_pc_mic.Checked && _related1Speaker != null)
                BeginInvoke(new MethodInvoker(delegate () { InitializePCMicToSpeaker(); }));
        }
    }

}
