using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace VideoViewer2WayAudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow
    {
        private OutgoingSpeakerController _outgoingSpeakerController = null;
        private AudioPlayer _audioPlayer;
        private AudioStreamPlayer _audioStreamPlayer;
        private System.IO.FileStream _soundFileStream = null;

        private Item _relatedMicrophone;
        private Item _relatedSpeaker;
        private Item _selectCameraItem;

        private bool _fromPCMic = true;
        private bool _fromPCMicInitialized = false;
        private double _speed = 0.0;
        private string _currentPlaybackMode = "";

        #region Constructor, close and init

        public MainWindow()
        {
            InitializeComponent();

            EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler, new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));

            //Initialize dialog components
            _rb_pc_mic.IsChecked = _fromPCMic;
            _rb_from_file.IsChecked = !_fromPCMic;

            _checkBoxSpeaker.IsChecked = true;
            _checkBoxAudioMicrophone.IsChecked = true;

            _buttonMode.IsEnabled = false;
            _comboBoxSampleRate.SelectedItem = ((string[])_comboBoxSampleRate.ItemsSource)[0];

            // Create audio players and initialize
            _audioPlayer = new AudioPlayer();

            _outgoingSpeakerController = new OutgoingSpeakerController();
            _outgoingSpeakerController.UIDispatcher = Dispatcher;
            _outgoingSpeakerController.OutgoingAudioErrorTextEvent += AudioController_OutgoingAudioErrorTextEvent;
            _outgoingSpeakerController.OutgoingAudioTransmitEnableStateChangedEvent += AudioController_OutgoingAudioTransmitEnableStateChangedEvent;
            _outgoingSpeakerController.OutgoingAudioMeterEvent += AudioController_OutgoingAudioMeterEvent;
            _outgoingSpeakerController.DoRetryConnect = true;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (_outgoingSpeakerController != null)
            {
                _outgoingSpeakerController.OutgoingAudioErrorTextEvent -= AudioController_OutgoingAudioErrorTextEvent;
                _outgoingSpeakerController.OutgoingAudioTransmitEnableStateChangedEvent -= AudioController_OutgoingAudioTransmitEnableStateChangedEvent;
                _outgoingSpeakerController.OutgoingAudioMeterEvent -= AudioController_OutgoingAudioMeterEvent;
                _outgoingSpeakerController.Close(0);
            }

            Close();
        }

        private void InitializePCMicToSpeaker()
        {
            if (!_fromPCMicInitialized && _relatedSpeaker != null)
            {
                _outgoingSpeakerController.Close(0);
                var audioRecorder = ClientControl.Instance.GeneratePcAudioRecorder();
                audioRecorder.OutgoingAudioWaveFormatSamplesPerSecond = Int32.Parse(_comboBoxSampleRate.SelectedItem.ToString());
                _outgoingSpeakerController.Init(audioRecorder);
                _outgoingSpeakerController.Connect(_relatedSpeaker.FQID, 0);
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
                _audioStreamPlayer.ProgressCompleteEvent += AudioStreamPlayer_ProgressCompleteEvent;
                _outgoingSpeakerController.Init(_audioStreamPlayer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization of speaker failed. Cause: " + ex.Message);
                return;
            }
            _progressBarMeter.Visibility = Visibility.Visible;
            _outgoingSpeakerController.Connect(_relatedSpeaker.FQID, 0);
            _outgoingSpeakerController.TransmitStart();
        }

        private void AudioStreamPlayer_ProgressCompleteEvent(object sender, EventArgs e)
        {
            _outgoingSpeakerController.TransmitStop();
            _audioStreamPlayer.Free();
            _audioStreamPlayer.ProgressCompleteEvent -= AudioStreamPlayer_ProgressCompleteEvent;
            _audioStreamPlayer = null;
        }

        #endregion

        #region Time changed event handler

        private object PlaybackTimeChangedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
        {
            Dispatcher.Invoke(() => 
            {
                DateTime time = (DateTime)message.Data;
                // The built-in PlaybackController has FQID==null
                if (sender == null)
                    _textBoxTime.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();
            });
            return null;
        }


        #endregion

        #region AudioController eventhandlers

        private void AudioController_OutgoingAudioErrorTextEvent(object sender, EventArgs e)
        {
            _errorInputControl.Message = _outgoingSpeakerController.OutgoingAudioErrorText;
        }

        private void AudioController_OutgoingAudioTransmitEnableStateChangedEvent(object sender, EventArgs e)
        {
            bool enabled = _outgoingSpeakerController.OutgoingAudioTransmitEnableState;
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    _buttonTalk.IsEnabled = enabled;
                    if (enabled == false)
                        ButtonTalk_MouseLeftButtonUp(this, null);
                    // Ensure re-connect will not continue after button has been disabled }));
                }));
            }
        }

        /// <summary>
        /// Signals that the outgoing audio transmit enable state has changed
        /// </summary>
        private void AudioController_OutgoingAudioMeterEvent(object sender, OutgoingSpeakerController.OutgoingAudioMeterEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(() => { UpdateAudioProgress(e.OutgoingAudioMeter); }));
            }
            else
            {
                UpdateAudioProgress(e.OutgoingAudioMeter);
            }
        }

        private void UpdateAudioProgress(int outgoingAudioMeter)
        {
            _progressBarMeter.Value = outgoingAudioMeter;
            if (_audioStreamPlayer != null)
                _textBoxProgress.Text = Math.Round(_audioStreamPlayer.Progress, 2).ToString();
        }

        #endregion

        #region User Click handling
        
        private void ButtonSelectCamera_Click(object sender, RoutedEventArgs e)
        {
            
            _imageViewerWpfControl.Disconnect();
            
            _audioPlayer.Disconnect();
            _audioPlayer.Close();

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                _selectCameraItem = itemPicker.SelectedItems.First();
                _buttonSelect.Content = _selectCameraItem.Name;

                _imageViewerWpfControl.CameraFQID = _selectCameraItem.FQID;
                _imageViewerWpfControl.Initialize();
                _imageViewerWpfControl.Connect();
                _imageViewerWpfControl.Selected = true;
                _buttonMode.IsEnabled = true;

                // Test for any related microphones, and pick out the first one
                _relatedMicrophone = null;
                _checkBoxAudioMicrophone.IsEnabled = false;
                _checkBoxSpeaker.IsEnabled = false;
                List<Item> related = _selectCameraItem.GetRelated();

                _outgoingSpeakerController.Close(0);
                _fromPCMicInitialized = false;
                _outgoingSpeakerController.OutgoingAudioErrorText = "";

                if (related != null)
                {
                    foreach (Item item in related)
                    {
                        if (item.FQID.Kind == Kind.Microphone)
                        {
                            _relatedMicrophone = item;
                            _checkBoxAudioMicrophone.IsEnabled = true;
                            _audioPlayer.MicrophoneFQID = _relatedMicrophone.FQID;
                            _audioPlayer.Initialize();
                            _checkBoxAudioMicrophone.IsChecked = true;
                        }
                        if (item.FQID.Kind == Kind.Speaker)
                        {
                            _relatedSpeaker = item;
                            _checkBoxSpeaker.IsEnabled = true;
                            _checkBoxSpeaker.IsChecked = true;
                            _buttonTalk.IsEnabled = true;
                            _rb_pc_mic.IsChecked = _fromPCMic;
                            _rb_from_file.IsChecked = !_fromPCMic;

                            if (_rb_pc_mic.IsChecked.Value && _relatedSpeaker != null)
                                Dispatcher.BeginInvoke(new Action(InitializePCMicToSpeaker));

                        }
                    }
                }

                UpdateCheckBoxSpeaker();
                UpdateCheckBoxAudioMicrophone();
            }
        }

        private void ImageViewerWpfControl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _imageViewerWpfControl.Selected = true;
        }


        private void CheckBoxAudioMicrophone_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAudioMicrophone();
        }

        private void CheckBoxAudioMicrophone_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAudioMicrophone();
        }

        private void UpdateCheckBoxAudioMicrophone()
        {
            if (_audioPlayer == null)
                return;
            if (_checkBoxAudioMicrophone.IsEnabled && _checkBoxAudioMicrophone.IsChecked.Value)
            {
                _audioPlayer.Connect();
            }
            else
            {
                _audioPlayer.Disconnect();
            }
        }

        private void CheckBoxSpeaker_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxSpeaker();
        }

        private void CheckBoxSpeaker_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxSpeaker();
        }

        private void UpdateCheckBoxSpeaker()
        {
            if (!_checkBoxSpeaker.IsEnabled || !_checkBoxSpeaker.IsChecked.Value)
            {
                if (_outgoingSpeakerController != null)
                    _outgoingSpeakerController.Close(0);
                _fromPCMicInitialized = false; //Need to be reinitialized
                _progressBarMeter.Visibility = Visibility.Hidden;
                _buttonTalk.IsEnabled = false;	//TEST
                _button_Select_WAV_File.IsEnabled = false;
                _rb_from_file.IsEnabled = false;
                _rb_pc_mic.IsEnabled = false;
                _comboBoxSampleRate.IsEnabled = false;
                _textBlockSampleRate.IsEnabled = false;
            }
            else
            {
                _buttonTalk.IsEnabled = _rb_pc_mic.IsChecked.Value;	//TEST
                _button_Select_WAV_File.IsEnabled = _rb_from_file.IsChecked.Value;
                _rb_from_file.IsEnabled = true;
                _rb_pc_mic.IsEnabled = true;
                _progressBarMeter.Visibility = Visibility.Hidden;
                if (_rb_pc_mic.IsChecked.Value && _relatedSpeaker != null)
                    Dispatcher.BeginInvoke(new Action(InitializePCMicToSpeaker));
                _comboBoxSampleRate.IsEnabled = true;
                _textBlockSampleRate.IsEnabled = true;
            }
        }

        private void ButtonTalk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _progressBarMeter.Visibility = Visibility.Visible;
            if (!_fromPCMicInitialized)
                return;

            _outgoingSpeakerController.TransmitStart();
        }

        private void ButtonTalk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_fromPCMicInitialized)
                return;
            _outgoingSpeakerController.TransmitStop();
            _progressBarMeter.Visibility = Visibility.Hidden;
        }

        private void ComboBoxSampleRate_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            // we changed recording sample rate, we need to reinitialize outgoing speaker
            _fromPCMicInitialized = false;
            if (_rb_pc_mic.IsChecked.Value && _relatedSpeaker != null)
                Dispatcher.BeginInvoke(new Action(InitializePCMicToSpeaker));
        }

        private void Button_Select_WAV_File_Click(object sender, RoutedEventArgs e)
        {
            //Show file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "wav";
            openFileDialog.Filter = "WAV sound files (*.wav)|*.wav";
            
            if (!openFileDialog.ShowDialog().Value)
                return; //Do nothing

            string filename = openFileDialog.FileName;
            if (!filename.ToLower().EndsWith(".wav"))
            {
                MessageBox.Show("Only WAV files can be used");
                return;
            }

            //Open a file stream
            try
            {
                if (_soundFileStream != null)
                {
                    _soundFileStream.Close();
                    _soundFileStream = null;
                }
                _soundFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the file: " + ex.Message);
                return;
            }

            try
            {
                Dispatcher.BeginInvoke(new Action(() => { InitialiseAudioStreamToSpeaker(_soundFileStream, true); }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to play file: " + ex.Message);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _buttonTalk.IsEnabled = _rb_pc_mic.IsChecked.Value;
            _button_Select_WAV_File.IsEnabled = _rb_from_file.IsChecked.Value;
            _comboBoxSampleRate.IsEnabled = !_rb_from_file.IsChecked.Value;
            _textBlockSampleRate.IsEnabled = !_rb_from_file.IsChecked.Value;

            if (_rb_pc_mic.IsChecked.Value)
                Dispatcher.BeginInvoke(new Action(InitializePCMicToSpeaker));
        }

        private void ButtonMode_Click(object sender, RoutedEventArgs e)
        {
            if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
            {
                EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
                _buttonMode.Content = "Current mode: Playback";
                _currentPlaybackMode = "";
            }
            else
            {
                EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                    MessageId.SmartClient.PlaybackCommand,
                    new PlaybackCommandData { Command = PlaybackData.PlayStop }));
                _speed = 0.0;
                _currentPlaybackMode = "";

                EnvironmentManager.Instance.Mode = Mode.ClientLive;
                _buttonMode.Content = "Current mode: Live";
            }
            _buttonReverse.IsEnabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
            _buttonForward.IsEnabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
            _buttonStop.IsEnabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
        }

        private void ButtonReverse_Click(object sender, RoutedEventArgs e)
        {
            if (_speed == 0.0 || _currentPlaybackMode != PlaybackData.PlayReverse)
                _speed = 1.0;
            else
                _speed *= 2;
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed }));
            _currentPlaybackMode = PlaybackData.PlayReverse;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
            EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            _buttonMode.Content = "Current mode: Playback";
            _speed = 0.0;
            _currentPlaybackMode = "";
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            if (_speed == 0.0 || _currentPlaybackMode != PlaybackData.PlayForward)
                _speed = 1.0;
            else
                _speed *= 2;
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed }));
            _currentPlaybackMode = PlaybackData.PlayForward;
        }

        #endregion
    }
}