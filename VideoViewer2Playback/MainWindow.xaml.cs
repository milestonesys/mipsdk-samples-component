using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace VideoViewer2Playback
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow
    {
        private Item _selectItem1;
        private Item _selectItem2;
        private Item _related1Microphone;
        private Item _related1Speaker;
        private Item _related2Microphone;
        private Item _related2Speaker;

        private double _speed1 = 0.0;
        private double _speed2 = 0.0;
        private bool _mode2InLive = true;

        private AudioPlayer _audioPlayer1Microphone;
        private AudioPlayer _audioPlayer1Speaker;

        private AudioPlayer _audioPlayer2Microphone;
        private AudioPlayer _audioPlayer2Speaker;

        private FQID _playbackControllerFQID;

        #region Constructor and close

        public MainWindow()
        {
            InitializeComponent();
            
            EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler, new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));
            EnvironmentManager.Instance.RegisterReceiver(PlaybackIndicationHandler, new MessageIdFilter(MessageId.SmartClient.PlaybackIndication));

            // Create audio players 
            _audioPlayer1Microphone = new AudioPlayer();

            _audioPlayer1Speaker = new AudioPlayer();

            _audioPlayer2Microphone = new AudioPlayer();

            _audioPlayer2Speaker = new AudioPlayer();

            _radioButtonSkip.IsChecked = true;
        }
        
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
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
            Dispatcher.Invoke(() => 
            { 
                DateTime time = (DateTime)message.Data;
                // The built-in PlaybackController has FQID==null
                if (sender == null)
                    _textBoxTime1.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();

                // The PlaybackController on the right hand side has a specific FQID
                if (sender != null && _playbackControllerFQID != null && sender.ObjectId == _playbackControllerFQID.ObjectId)
                    _textBoxTime2.Text = time.ToShortDateString() + "  " + time.ToLongTimeString();
            });
            return null;
        }

        private object PlaybackIndicationHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
        {
            if ((sender == null && _playbackControllerFQID == null) || _playbackControllerFQID.EqualGuids(sender))      // If message from right hand side control
            {
                if (message.Data is PlaybackCommandData)
                {
                    PlaybackCommandData data = (PlaybackCommandData)message.Data;
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

        private void ButtonSelect1_Click(object sender, RoutedEventArgs e)
        {
            _imageViewerWpfControl1.Disconnect();
            
            _audioPlayer1Microphone.Initialize();
            _audioPlayer1Speaker.Initialize();

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                _selectItem1 = itemPicker.SelectedItems.First();
                _buttonSelect1.Content = _selectItem1.Name;

                _imageViewerWpfControl1.CameraFQID = _selectItem1.FQID;
                _imageViewerWpfControl1.Initialize();
                _imageViewerWpfControl1.Connect();
                _imageViewerWpfControl1.Selected = true;
                
                _checkBoxZoom1.IsEnabled = true;
                _checkBoxAudio1.IsEnabled = false;

                List<Item> related = _selectItem1.GetRelated();

                // Test for any related microphones and speakers, and pick out the first one
                _related1Microphone = null;
                _related1Speaker = null;

                if (related != null)
                {
                    foreach (Item item in related)
                    {
                        if (item.FQID.Kind == Kind.Microphone)
                        {
                            _related1Microphone = item;
                            _audioPlayer1Microphone.MicrophoneFQID = _related1Microphone.FQID;
                            break;
                        }
                    }

                    foreach (Item item in related)
                    {
                        if (item.FQID.Kind == Kind.Speaker)
                        {
                            _related1Speaker = item;
                            _audioPlayer1Speaker.SpeakerFQID = _related1Speaker.FQID;
                            break;
                        }
                    }

                    _checkBoxAudio1.IsEnabled = _related1Microphone != null || _related1Speaker != null;
                    _checkBoxAudio1.IsChecked = _checkBoxAudio1.IsEnabled;
                }
            }
        }
        
        private void ImageViewerWpfControl1_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _imageViewerWpfControl2.Selected = false;
            _imageViewerWpfControl1.Selected = true;
        }
        
        private void CheckBoxZoom1_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxZoom1();
        }

        private void CheckBoxZoom1_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxZoom1();
        }

        private void UpdateCheckBoxZoom1()
        {
            _imageViewerWpfControl1.EnableDigitalZoom = _checkBoxZoom1.IsChecked.Value;
        }

        private void CheckBoxAudio1_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAudio1();
        }

        private void CheckBoxAudio1_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAudio1();
        }

        private void UpdateCheckBoxAudio1()
        {
            if (_checkBoxAudio1.IsChecked.Value)
            {
                if (_related1Microphone != null)
                {
                    _audioPlayer1Microphone.Connect();
                }
                if (_related1Speaker != null)
                {
                    _audioPlayer1Speaker.Connect();
                }
            }
            else
            {
                _audioPlayer1Microphone.Disconnect();
                _audioPlayer1Speaker.Disconnect();
            }
        }

        private void ButtonMode1_Click(object sender, RoutedEventArgs e)
        {
            if (EnvironmentManager.Instance.Mode == Mode.ClientLive)
            {
                EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
                _buttonMode1.Content = "Current mode: Playback";
            }
            else
            {
                EnvironmentManager.Instance.Mode = Mode.ClientLive;
                _buttonMode1.Content = "Current mode: Live";
            }
            _buttonReverse1.IsEnabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
            _buttonForward1.IsEnabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
            _buttonStop1.IsEnabled = EnvironmentManager.Instance.Mode == Mode.ClientPlayback;
        }

        private void ButtonReverse1_Click(object sender, RoutedEventArgs e)
        {
            if (_speed1 == 0.0)
                _speed1 = 1.0;
            else
                _speed1 *= 2;
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed1 }));
        }

        private void ButtonStop1_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
            EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
            _buttonMode1.Content = "Current mode: Playback";
            _speed1 = 0.0;
        }

        private void ButtonForward1_Click(object sender, RoutedEventArgs e)
        {
            if (_speed1 == 0.0)
                _speed1 = 1.0;
            else
                _speed1 *= 2;
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                MessageId.SmartClient.PlaybackCommand,
                new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed1 }));
        }

        #endregion

        #region Playback Click handling - Right hand camera
        
        private void ButtonSelect2_Click(object sender, RoutedEventArgs e)
        {
            _imageViewerWpfControl2.Disconnect();
            
            _audioPlayer2Microphone.Initialize();
            _audioPlayer2Speaker.Initialize();

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                _selectItem2 = itemPicker.SelectedItems.First();
                _buttonSelect2.Content = _selectItem2.Name;

                // Make sure we have a separate playback controller for right hand side camera
                if (_playbackControllerFQID == null)
                    _playbackControllerFQID = ClientControl.Instance.GeneratePlaybackController();

                _imageViewerWpfControl2.PlaybackControllerFQID = _playbackControllerFQID;
                _imageViewerWpfControl2.CameraFQID = _selectItem2.FQID;
                _imageViewerWpfControl2.Initialize();
                _imageViewerWpfControl2.Connect();
                _imageViewerWpfControl2.Selected = true;

                _checkBoxZoom2.IsEnabled = true;
                
                // Always start out in live
                _mode2InLive = true;
                EnvironmentManager.Instance.SendMessage(
                    new VideoOS.Platform.Messaging.Message(MessageId.System.ModeChangeCommand, Mode.ClientLive),
                    _playbackControllerFQID);

                // Test for any related microphones and speakers, and pick out the first one
                _related2Microphone = null;
                _related2Speaker = null;

                _checkBoxAudio2.IsEnabled = false;
                List<Item> related = _selectItem2.GetRelated();
                if (related != null)
                {
                    foreach (Item item in related)
                        if (item.FQID.Kind == Kind.Microphone)
                        {
                            _related2Microphone = item;
                            _audioPlayer2Microphone.MicrophoneFQID = _related2Microphone.FQID;
                            _audioPlayer2Microphone.PlaybackControllerFQID = _playbackControllerFQID;
                            break;
                        }
                    foreach (Item item in related)
                        if (item.FQID.Kind == Kind.Speaker)
                        {
                            _related2Speaker = item;
                            _audioPlayer2Speaker.SpeakerFQID = _related2Speaker.FQID;
                            _audioPlayer2Speaker.PlaybackControllerFQID = _playbackControllerFQID;
                            break;
                        }
                    _checkBoxAudio2.IsEnabled = _related2Microphone != null || _related2Speaker != null;
                    _checkBoxAudio2.IsChecked = _checkBoxAudio2.IsEnabled;
                }
            }
        }

        private void ImageViewerWpfControl2_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _imageViewerWpfControl1.Selected = false;
            _imageViewerWpfControl2.Selected = true;
        }

        private void CheckBoxZoom2_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxZoom2();
        }

        private void CheckBoxZoom2_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxZoom2();
        }

        private void UpdateCheckBoxZoom2()
        {
            _imageViewerWpfControl2.EnableDigitalZoom = _checkBoxZoom2.IsChecked.Value;
        }

        private void CheckBoxAudio2_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAudio2();
        }

        private void CheckBoxAudio2_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxAudio2();
        }

        private void UpdateCheckBoxAudio2()
        {
            if (_checkBoxAudio2.IsChecked.Value)
            {
                _audioPlayer2Microphone.Connect();
                _audioPlayer2Speaker.Connect();
            }
            else
            {
                _audioPlayer2Microphone.Disconnect();
                _audioPlayer2Speaker.Disconnect();
            }
        }

        private void SkipMode_OnChecked(object sender, RoutedEventArgs e)
        {
            PlaybackSkipModeData mode = PlaybackSkipModeData.Noskip;
            if (_radioButtonSkip.IsChecked.Value)
                mode = PlaybackSkipModeData.Skip;
            if (_radioButtonStopAtEnd.IsChecked.Value)
                mode = PlaybackSkipModeData.StopAtSequenceEnd;

            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                    MessageId.SmartClient.PlaybackSkipModeCommand, mode),
                _playbackControllerFQID);
        }

        private void ButtonMode2_Click(object sender, RoutedEventArgs e)
        {
            _mode2InLive = !_mode2InLive;
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.System.ModeChangeCommand, _mode2InLive ? Mode.ClientLive : Mode.ClientPlayback),
                _playbackControllerFQID);
            if (_mode2InLive == false)
            {
                _buttonMode2.Content = "Current mode: Playback";
            }
            else
            {
                _buttonMode2.Content = "Current mode: Live";
            }
            _buttonReverse2.IsEnabled = !_mode2InLive;
            _buttonForward2.IsEnabled = !_mode2InLive;
            _buttonStop2.IsEnabled = !_mode2InLive;
            _radioButtonNoskip.IsEnabled = !_mode2InLive;
            _radioButtonSkip.IsEnabled = !_mode2InLive;
            _radioButtonStopAtEnd.IsEnabled = !_mode2InLive;
        }

        private void ButtonReverse2_Click(object sender, RoutedEventArgs e)
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

        private void ButtonStop2_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
                    MessageId.SmartClient.PlaybackCommand,
                    new PlaybackCommandData() { Command = PlaybackData.PlayStop }),
                _playbackControllerFQID);

            _mode2InLive = false;
            _buttonMode2.Content = "Current mode: Playback";
            _speed2 = 0.0;
        }

        private void ButtonForward2_Click(object sender, RoutedEventArgs e)
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

        #endregion
    }
}