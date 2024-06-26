using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Data.HardwareDeviceDefinitions;
using VideoOS.Platform.Data.MediaDataHelpers;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.Metadata;
using Rectangle = System.Drawing.Rectangle;

namespace CameraMetadataProvider
{
    public partial class MainForm : Form
    {
        private MediaProviderChannel _videoProvider;
        private readonly MediaProviderService _mediaProviderService;
        private MetadataProviderChannel _metadataProviderChannel;
        private MediaProviderChannel _audioProvider;
        private double _altitude = 0;
        private readonly AudioRecorderToMediaData _audioRecorderToMediaData;

        public MainForm()
        {
            InitializeComponent();

            var hardwareDefinition = new HardwareDefinition(
                PhysicalAddress.Parse("001122334455"),
                "VideoAndMetadataProvider")
            {
                Firmware = "v10",
                CameraDevices =
                {
                    new CameraDeviceDefinition
                    {
                        Codec = MediaContent.Jpeg,
                        CodecSet = { MediaContent.Jpeg },
                        Fps = 8,
                        FpsRange = new Tuple<double, double>(1, 30),
                        Quality = 75,
                        QualityRange = new Tuple<int, int>(1, 100),
                        Resolution = "800x600",
                        ResolutionSet = { "320x240", "640x480", "800x600", "1024x768" }
                    }
                },
                MetadataDevices = {  MetadataDeviceDefintion.CreateGpsDevice() },
                MicrophoneDevices = { new PcmMicrophoneDeviceDefinition() },
            };

            // Open the HTTP Service
            _mediaProviderService = new MediaProviderService();	
            _mediaProviderService.Init(52123, "password", hardwareDefinition);

            // create pcm in wrapper
            _audioRecorderToMediaData = new AudioRecorderToMediaData(ClientControl.Instance.GeneratePcAudioRecorder());
            _audioRecorderToMediaData.ErrorHandler.OnError += ErrorHandlerOnError;
        }

        #region User Click handling
        private void ButtonConnectClick(object sender, EventArgs e)
        {
            // Create a provider to handle camera channel 1
            _videoProvider = _mediaProviderService.CreateMediaProvider(1);
            _videoProvider.SessionOpening += MediaProviderSessionOpening;
            _videoProvider.GetMediaData += MediaProviderGetMediaData;
            _videoProvider.SessionClosed += MediaProviderSessionClosed;
            _videoProvider.HandlePTZCommandEvent += MediaProviderHandlePTZCommandEvent;

            // Create a provider to handle metadata channel 1
            _metadataProviderChannel = _mediaProviderService.CreateMetadataProvider(1);
            _metadataProviderChannel.SessionOpening += MediaProviderSessionOpening;
            _metadataProviderChannel.SessionClosed += MediaProviderSessionClosed;

            // Create a provider to handle metadata channel 1
            _audioProvider = _mediaProviderService.CreateMediaProvider(1, MediaContent.Pcm);
            _audioProvider.GetMediaData += _audioRecorderToMediaData.GetMediaData;
            _audioProvider.SessionOpening += MediaProviderSessionOpening;
            _audioProvider.SessionClosed += MediaProviderSessionClosed;

            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
        }

        object MediaProviderHandlePTZCommandEvent(string controlCommandMessageId, object controlCommandData)
        {
            switch (controlCommandMessageId)
            {
                case MessageId.Control.PTZCenterCommand:
                    ShowCommand(controlCommandMessageId);
                    // Handle
                    break;
                case MessageId.Control.PTZMoveCommand:
                    var moveCommand = controlCommandData as string;
                    ShowCommand(controlCommandMessageId+ Environment.NewLine+moveCommand);
                    // Handle
                    break;
                case MessageId.Control.PTZMoveStartCommand:
                    var startMoveCommand = (PTZMoveStartCommandData)controlCommandData;
                    ShowCommand(controlCommandMessageId+Environment.NewLine+
                        "Pan="+startMoveCommand.Pan+
                        "Tilt="+startMoveCommand.Tilt+
                        "Zoom="+startMoveCommand.Zoom+
                        "Speed="+startMoveCommand.Speed);
                    // Handle
                    break;
                case MessageId.Control.PTZMoveStopCommand:
                    ShowCommand(controlCommandMessageId);
                    // Handle
                    break;
                case MessageId.Control.PTZRectangleCommand:
                    var rectangleCommand = (PTZRectangleCommandData)controlCommandData;
                    ShowCommand(controlCommandMessageId+Environment.NewLine+
                        "Top=" + rectangleCommand.Top +
                        "Left=" + rectangleCommand.Left +
                        "Right=" + rectangleCommand.Right +
                        "Bottom=" + rectangleCommand.Bottom + 
                        "RefWidth=" + rectangleCommand.RefWidth + 
                        "RefHeight=" + rectangleCommand.RefHeight 
                        );
                    // Handle
                    break;
                case MessageId.Control.PTZMoveAbsoluteCommand:
                    var absoluteCommand = (PTZMoveAbsoluteCommandData)controlCommandData;
                    ShowCommand(controlCommandMessageId+Environment.NewLine+
                        "Pan=" + absoluteCommand.Pan +
                        "Tilt=" + absoluteCommand.Tilt +
                        "Zoom=" + absoluteCommand.Zoom +
                        "Speed=" + absoluteCommand.Speed);
                    // Handle
                    break;
                case MessageId.Control.PTZAUXCommand:
                    var data = (PTZAUXCommandData)controlCommandData;
                    ShowCommand(controlCommandMessageId + Environment.NewLine + "AUX=" + data.AuxNumber + ", " +
                                ((data.On) ? "On" : "Off"));
                    break;
                case MessageId.Control.LensCommand:
                    ShowCommand(controlCommandMessageId + Environment.NewLine + "Command=" + (string)controlCommandData);
                    break;

                case MessageId.Control.PTZGetAbsoluteRequest:
                    ShowCommand(controlCommandMessageId);
                    // Handle
                    return new PTZGetAbsoluteRequestData { Pan = 0.1, Tilt = 0.2, Zoom = 0.3 };
                default:
                    ShowCommand(controlCommandMessageId+" - Not handled");
                    break;
            }
            return null;
        }

        private delegate void AddCommandLineDelegate(string commandLine);
        private void AddCommandLine(string commandLine)
        {
            lstPTZCommands.Items.Insert(0, commandLine);
            lstPTZCommands.Refresh();
        }

        private void ShowCommand(string info)
        {
            Invoke(new AddCommandLineDelegate(AddCommandLine), info);
        }

        private void ButtonDisconnectClick(object sender, EventArgs e)
        {
            if (_videoProvider != null)
                _videoProvider.Disconnect();
            _mediaProviderService.RemoveMediaProvider(_videoProvider);

            if (_metadataProviderChannel != null)
                _metadataProviderChannel.Disconnect();
            _mediaProviderService.RemoveMetadataProvider(_metadataProviderChannel);

            if (_audioProvider != null)
                _audioProvider.Disconnect();
            _mediaProviderService.RemoveMediaProvider(_audioProvider);

            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            textBoxSessionCount.Text = @"0";
            SetIdleMode(true);
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            if (_videoProvider != null)
            {
                _videoProvider.Disconnect();
                _videoProvider.SessionOpening -= MediaProviderSessionOpening;
                _videoProvider.GetMediaData -= MediaProviderGetMediaData;
                _videoProvider.SessionClosed -= MediaProviderSessionClosed;
            }
            
            if (_metadataProviderChannel != null)
			{
                _metadataProviderChannel.Disconnect();
                _metadataProviderChannel.SessionOpening -= MediaProviderSessionOpening;
                _metadataProviderChannel.SessionClosed -= MediaProviderSessionClosed;
            }

            if (_audioProvider != null)
            {
                _audioProvider.Disconnect();
                _audioProvider.SessionOpening -= MediaProviderSessionOpening;
                _audioProvider.GetMediaData -= _audioRecorderToMediaData.GetMediaData;
                _audioProvider.SessionClosed -= MediaProviderSessionClosed;
            }

            _audioRecorderToMediaData.Close();

            if (_mediaProviderService != null)
                _mediaProviderService.Close();

            Application.Exit();
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            labelQuality.Text = "" + hScrollBarQuality.Value;
        }

        #endregion

        #region Callback methods from VideoProvider - Channel 1

        /// <summary>
        /// This method is called when a new session is about to be created.
        /// This method should validate the requested session parameters, and change if needed.
        /// If unable to handle more sessions for this channel, you can throw an Exception.
        /// </summary>
        /// <param name="session"></param>
        void MediaProviderSessionOpening(MediaProviderSession session)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MediaProviderChannel.SessionOpeningDelegate(MediaProviderSessionOpening), session);
            }
            else
            {
                // Here we can check what the server side has configured, and adjust to what make sense for us.
                int maxFPS = 15;
                if (comboBoxFPS.SelectedItem!=null)
                {
                    Int32.TryParse(comboBoxFPS.SelectedItem.ToString(), out maxFPS);
                }
                if (session.FPS > maxFPS)
                    session.FPS = maxFPS;
                // Resolution, Quality, etc can also be modified
                if (session.Quality > hScrollBarQuality.Value)
                    session.Quality = hScrollBarQuality.Value;

                textBoxSessionCount.Text = "" + _videoProvider.ActiveSessions;
            }
        }

        /// <summary>
        /// This is called when one of the sessions are closed.
        /// In this sample we just update the active session counter for this channel.
        /// </summary>
        /// <param name="session"></param>
        void MediaProviderSessionClosed(MediaProviderSession session)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MediaProviderChannel.SessionClosedDelegate(MediaProviderSessionClosed), session);
            }
            else
            {
                textBoxSessionCount.Text = "" + _videoProvider.ActiveSessions;
            }
        }

        /// <summary>
        /// For this sample, we provide JPEG frames.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        MediaData MediaProviderGetMediaData(MediaProviderSession session)
        {
            if (InvokeRequired)
            {
                // Get on the UI thread before executing the below code
                return Invoke(new MediaProviderChannel.GetMediaDataDelegate(MediaProviderGetMediaData), session) as MediaData;
            }

            var random = new Random(DateTime.UtcNow.Millisecond);

            var metadata = new MetadataStream
            {
                NavigationalData = new NavigationalData
                {
                    Altitude = _altitude,
                    Latitude = random.Next(-90, 90),
                    Longitude = random.Next(-180, 180)
                }
            };

            _altitude += 0.01;

            _metadataProviderChannel.QueueMetadata(metadata, DateTime.UtcNow);
            
            var bitmap = new Bitmap(panelTimeDisplay.Width, panelTimeDisplay.Height);
            panelTimeDisplay.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), panelTimeDisplay.Size));
            var mediaData = new MediaData();
            using (var ms = new MemoryStream())
            {
                var qualityParam = new EncoderParameter(Encoder.Quality, session.Quality);
                bitmap.Save(ms, GetJpegEncoder(), new EncoderParameters(1) { Param = new[] { qualityParam } });
                ms.Seek(0, SeekOrigin.Begin);
                mediaData.Bytes = new byte[ms.Length];
                ms.Read(mediaData.Bytes, 0, (int) ms.Length);
            }
            mediaData.DateTime = DateTime.UtcNow;
            return mediaData;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Get the .Net JPEG Encoder
        /// </summary>
        /// <returns></returns>
        private ImageCodecInfo GetJpegEncoder()
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            textBoxTime.Text = DateTime.Now.ToString("hh.mm.ss:fff");
        }

        #endregion

        private void OnIdle(object sender, EventArgs e)
        {
            if (_videoProvider!=null)
            {
                SetIdleMode(!_videoProvider.InIdleMode);
            }
        }

        private void SetIdleMode(bool inIdleMode)
        {
            _videoProvider.InIdleMode = inIdleMode;
            if (inIdleMode)
            {
                timer1.Stop();
                buttonIdle.Text = "Start sending Video";
            }
            else
            {
                timer1.Start();
                buttonIdle.Text = "Go into Idle Mode";
            }
        }

        #region Audio handling

        private void OnButtonPushToTalkMouseDown(object sender, MouseEventArgs e)
        {
            _audioProvider.InIdleMode = _audioRecorderToMediaData.InIdle = false;
        }

        private void OnButtonPushToTalkMouseUp(object sender, MouseEventArgs e)
        {
            _audioProvider.InIdleMode = _audioRecorderToMediaData.InIdle = true;
        }

        private void ErrorHandlerOnError(Exception exception)
        {
            Console.WriteLine("ErrorHandlerOnError");
        }

        #endregion
    }
}
