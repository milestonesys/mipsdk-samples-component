using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.Platform.Data;
using VideoOS.Platform.Metadata;
using Appearance = VideoOS.Platform.Metadata.Appearance;

namespace MultiChannelMetadataProvider
{
    public partial class MainForm : Form
    {
        // Constants used for describing the size and movement of the bounding box.
        private const float TopLimit = 0.75f;
        private const float RightLimit = 0.75f;
        private const float BottomLimit = -0.75f;
        private const float LeftLimit = -0.75f;
        private const float BoundingBoxSize = 0.5f;
        private const float StepSize = 0.05f;
        private readonly TimeSpan _timeBetweenMetadata = TimeSpan.FromSeconds(0.2);

        private readonly Random _random = new Random(DateTime.UtcNow.Millisecond);

        private MetadataProviderChannel _boundingBoxProviderChannel;
        private MetadataProviderChannel _gpsProviderChannel;
        private MetadataProviderChannel _nonStandardProviderChannel;
        private MediaProviderService _metadataProviderService;
        private readonly MetadataSerializer _metadataSerializer = new MetadataSerializer();

        private CancellationTokenSource _cts;
        private Task _senderTask;
        private MovementDirected _currentDirection;

        private enum MovementDirected
        {
            Up, Down, Left, Right
        }

        public MainForm()
        {
            InitializeComponent();
            sourceComboBox.SelectedIndex = 0;
        }

        private void ButtonConnectClick(object sender, EventArgs e)
        {
            if (_metadataProviderService != null)
                return;

            // Open the HTTP Service
            var hardwareDefinition = new HardwareDefinition(
                    PhysicalAddress.Parse("001122334466"),
                    "MetadataProvider")
                {
                    Firmware = "v10",
                    MetadataDevices =
                    {
                        MetadataDeviceDefintion.CreateBoundingBoxDevice(),
                        MetadataDeviceDefintion.CreateGpsDevice(),
                        CreateNonStandardDevice()
                    }
                };

            _metadataProviderService = new MediaProviderService();

            // Create a provider to handle channel 1
            _boundingBoxProviderChannel = _metadataProviderService.CreateMetadataProvider(1);
            _boundingBoxProviderChannel.SessionOpening += MetadataProviderSessionOpening;
            _boundingBoxProviderChannel.SessionClosed += MetadataProviderSessionClosed;
            _boundingBoxProviderChannel.ParametersChanged += MetadataProviderParametersChanged;
            //_boundingBoxProviderChannel.ParameterDictionary["ValidTime"] = "10";      // We can override the default, if it make sense


            // Create a provider to handle channel 2
            _gpsProviderChannel = _metadataProviderService.CreateMetadataProvider(2);
            _gpsProviderChannel.SessionOpening += MetadataProviderSessionOpening;
            _gpsProviderChannel.SessionClosed += MetadataProviderSessionClosed;
            _gpsProviderChannel.ParametersChanged += MetadataProviderParametersChanged;
            //_gpsProviderChannel.ParameterDictionary["ValidTime"] = "8";               // We can override the default, if it make sense

            // Create a provider to handle channel 3
            _nonStandardProviderChannel = _metadataProviderService.CreateMetadataProvider(3);
            _nonStandardProviderChannel.SessionOpening += MetadataProviderSessionOpening;
            _nonStandardProviderChannel.SessionClosed += MetadataProviderSessionClosed;
            _nonStandardProviderChannel.ParametersChanged += MetadataProviderParametersChanged;

            //_nonStandardProviderChannel.ParameterDictionary["Frequency"] = "5";       // We can override the default, if it make sense
            //_nonStandardProviderChannel.ParameterDictionary["Sensitivity"] = "2";



            _metadataProviderService.Init(52123, "password", hardwareDefinition);

            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
            buttonSendData.Enabled = true;
        }

        private MetadataDeviceDefintion CreateNonStandardDevice()
        {
            return new MetadataDeviceDefintion
            {
                MetadataTypes =
                {
                    new MetadataTypeDefinition
                    {
                        Name = "PEOPLE_COUNT",
                        DisplayName = "People Count",
                        Id = new Guid("531EF2C1-87FC-4019-9B02-F37D9A1803FE"),
                        DisplayId = new Guid("E3B1D126-5459-44C4-9178-CD4FF785429D"),
                        Capabilities =
                        {
                            new DataCapability
                            {
                                Name = "Frequency",
                                TypeName = "FrequencyType",
                                TypeDisplayName = "Frequency (Hz)",
                                TypeDisplayId = new Guid("CC0BAA51-E5D1-481F-9CAE-B454C9723969"),
                                TypeId = new Guid("3E56D4CF-606C-4F4A-A33E-A888A1343EEA"),
                                Minimum = 1,
                                Maximum = 10,
                                Default = 2,
                                Resolution = 1
                            },
                            new EnumCapability
                            {
                                Name = "Sensitivity",
                                TypeName = "SensitivityType",
                                TypeDisplayName = "Sensitivity",
                                TypeDisplayId = new Guid("FBA4AB76-D3F5-4DE8-870C-AA3C621B5033"),
                                TypeId = new Guid("FBA4AB76-D3F5-4DE8-870C-AA3C621B5033"),
                                DefaultValue = "Medium",
                                EnumValues =
                                {
                                    new EnumCapabilityValue
                                    {
                                        Name = "Low",
                                        DisplayName = "Low",
                                        Id = new Guid("2D3AAC2E-E108-4C8F-8997-2AB0629B763F"),
                                        DisplayId = new Guid("8274B1A9-B126-4BD8-A74F-C55EEFE323B8"),
                                        Value = "1"
                                    },
                                    new EnumCapabilityValue
                                    {
                                        Name = "Medium",
                                        DisplayName = "Medium",
                                        Id = new Guid("8F9B8D55-2557-493D-9A25-D524580E8F8C"),
                                        DisplayId = new Guid("A8B5A5DB-D606-4590-9CA2-CABEB10FC674"),
                                        Value = "2"
                                    },
                                    new EnumCapabilityValue
                                    {
                                        Name = "High",
                                        DisplayName = "High",
                                        Id = new Guid("D4669253-CBE1-400B-8E2C-DA188C307380"),
                                        DisplayId = new Guid("D4669253-CBE1-400B-8E2C-DA188C307380"),
                                        Value = "3"
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private void ButtonDisconnectClick(object sender, EventArgs e)
        {
            StopMetadata();

            if (_boundingBoxProviderChannel != null)
            {
                _boundingBoxProviderChannel.Disconnect();
                _metadataProviderService.RemoveMetadataProvider(_boundingBoxProviderChannel);
                textBoxSessionCount.Text = @"0";
            }

            
            if (_gpsProviderChannel != null)
            {
                _gpsProviderChannel.Disconnect();
                _metadataProviderService.RemoveMetadataProvider(_gpsProviderChannel);
                textBoxSessionCount.Text = @"0";
            }

            if (_nonStandardProviderChannel != null)
            {
                _nonStandardProviderChannel.Disconnect();
                _metadataProviderService.RemoveMetadataProvider(_nonStandardProviderChannel);
                textBoxSessionCount.Text = @"0";
            }
            
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            buttonSendData.Enabled = false;

            _metadataProviderService.Close();       // New 2017R3
            _metadataProviderService = null;        // New 2017R3
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
            
            if (_boundingBoxProviderChannel != null)
            {
                _boundingBoxProviderChannel.Disconnect();
                _boundingBoxProviderChannel.SessionOpening -= MetadataProviderSessionOpening;
                _boundingBoxProviderChannel.SessionClosed -= MetadataProviderSessionClosed;
                _boundingBoxProviderChannel.ParametersChanged -= MetadataProviderParametersChanged;
            }

            if (_gpsProviderChannel != null)
            {
                _gpsProviderChannel.Disconnect();
                _gpsProviderChannel.SessionOpening -= MetadataProviderSessionOpening;
                _gpsProviderChannel.SessionClosed -= MetadataProviderSessionClosed;
                _gpsProviderChannel.ParametersChanged -= MetadataProviderParametersChanged;
            }

            if (_nonStandardProviderChannel != null)
            {
                _nonStandardProviderChannel.Disconnect();
                _nonStandardProviderChannel.SessionOpening -= MetadataProviderSessionOpening;
                _nonStandardProviderChannel.SessionClosed -= MetadataProviderSessionClosed;
                _nonStandardProviderChannel.ParametersChanged -= MetadataProviderParametersChanged;
            }
            
            if (_metadataProviderService != null)
                _metadataProviderService.Close();

            Application.Exit();
        }

        private void buttonSendData_Click(object sender, EventArgs e)
        {
            if (_senderTask == null)
            {
                StartMetadata();
            }
            else
            {
                StopMetadata();
            }
        }

        private void StopMetadata()
        {
            if (_senderTask != null)
            {
                _cts.Cancel();
                _senderTask = null;
                buttonSendData.Text = @"Start all metadata";
            }
        }

        private void StartMetadata()
        {
            _cts = new CancellationTokenSource();
            _senderTask =
                Task.Factory.StartNew(SendData, _cts.Token, TaskCreationOptions.LongRunning)
                    .ContinueWith(DisplayException, TaskContinuationOptions.OnlyOnFaulted);
            buttonSendData.Text = @"Stop all metadata";
        }

        private static void DisplayException(Task task)
        {
            Debug.Assert(task.Exception != null, "task.Exception != null");
            MessageBox.Show(task.Exception.InnerException.Message, @"Exception when sending metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// This method is called when a new session is about to be created.
        /// This method should validate the requested session parameters, and change if needed.
        /// If unable to handle more sessions for this channel, you can throw an Exception.
        /// </summary>
        /// <param name="session"></param>
        void MetadataProviderSessionOpening(MediaProviderSession session)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<MediaProviderSession>(MetadataProviderSessionOpening), session);
            }
            else
            {
                textBoxSessionCount.Text = (_boundingBoxProviderChannel.ActiveSessions /*+ _gpsProviderChannel.ActiveSessions + _nonStandardProviderChannel.ActiveSessions*/).ToString();
                if (sourceComboBox.SelectedIndex == 0)
                    DumpProperties(_boundingBoxProviderChannel.ServerParameterDictionary);
                if (sourceComboBox.SelectedIndex == 1)
                    DumpProperties(_gpsProviderChannel.ServerParameterDictionary);
                if (sourceComboBox.SelectedIndex == 2)
                    DumpProperties(_nonStandardProviderChannel.ServerParameterDictionary);

            }
        }

        /// <summary>
        /// This is called when one of the sessions are closed.
        /// In this sample we just update the active session counter for this channel.
        /// </summary>
        /// <param name="session"></param>
        void MetadataProviderSessionClosed(MediaProviderSession session)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<MediaProviderSession>(MetadataProviderSessionClosed), session);
            }
            else
            {
                textBoxSessionCount.Text = (_boundingBoxProviderChannel.ActiveSessions + _gpsProviderChannel.ActiveSessions + _nonStandardProviderChannel.ActiveSessions).ToString();
            }
        }

        void MetadataProviderParametersChanged(MediaProviderSession  session, MetadataProviderChannel channel)
        {
            //TODO - we should stored the changed parameters on disk, to ensure we have them for next application execution

            DumpIfChannelIsShown(channel.Channel, new Dictionary<string, string>(channel.ServerParameterDictionary));            
        }

        private void DumpIfChannelIsShown(int channel, Dictionary<string, string> properties)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    DumpIfChannelIsShown(channel, properties);
                }));
            }
            else
            {
                if ((sourceComboBox.SelectedIndex + 1) == channel)
                {
                    DumpProperties(properties);
                }
            }
        }

        private void DumpProperties(Dictionary<string, string> properties)
        {
            listBoxProperties.Items.Clear();
            listBoxProperties.Items.Add("Set " + DateTime.Now);
            foreach (string key in properties.Keys)
            {
                listBoxProperties.Items.Add(key + "=" + properties[key]);
            }
        }

        void SendData(object obj)
        {
            var token = (CancellationToken) obj;
            var dummyCenterOfGravity = new Vector { X = 0f, Y = 0f };
            var boundingBox = new Rectangle
            {
                Bottom = BottomLimit,
                Left = LeftLimit,
                Top = BottomLimit + BoundingBoxSize,
                Right = LeftLimit + BoundingBoxSize
            };
            _currentDirection = MovementDirected.Up;

            double gpsAltitiude = 0.0;

            while (token.IsCancellationRequested == false)
            {
                SendBoundingBoxData(boundingBox, dummyCenterOfGravity);
                SendGpsData(ref gpsAltitiude);
                SendNonStandardData();

                Thread.Sleep(_timeBetweenMetadata);
            }
        }

        private readonly string _peopleCountXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>" + Environment.NewLine +
                                                  @"<tt:MetadataStream xmlns:tt=""http://www.onvif.org/ver10/schema"">" + Environment.NewLine +
                                                  @"  <tt:VideoAnalytics>" + Environment.NewLine +
                                                  @"    <tt:Extension>" + Environment.NewLine +
                                                  @"      <PeopleCount>" + Environment.NewLine +
                                                  @"        <Id>{0}</Id>" + Environment.NewLine +
                                                  @"        <Count>{1}</Count>" + Environment.NewLine +
                                                  @"      </PeopleCount>" + Environment.NewLine +
                                                  @"    </tt:Extension>" + Environment.NewLine +
                                                  @"  </tt:VideoAnalytics>" + Environment.NewLine +
                                                  @"</tt:MetadataStream>";

        private void SendNonStandardData()
        {
            var peopleCount = _random.Next(0, 100);

            var metadata = string.Format(_peopleCountXml, Guid.NewGuid(), peopleCount);
            
            var result = _nonStandardProviderChannel.QueueMetadata(metadata, DateTime.UtcNow);
            if (result == false)
                Trace.WriteLine(string.Format("{0}: Failed to write to non-standard channel", DateTime.UtcNow));
            else
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    if (sourceComboBox.SelectedIndex == 2)
                    {
                        DisplayMetadata(metadata);
                    }
                }));
            }
        }

        private void SendGpsData(ref double gpsAltitiude)
        {
            gpsAltitiude += 0.01;
            
            var metadata = new MetadataStream
            {
                NavigationalData = new NavigationalData
                {
                    Altitude = gpsAltitiude,
                    Latitude = _random.Next(-90, 90),
                    Longitude = _random.Next(-180, 180)
                }
            };
            
            var result = _gpsProviderChannel.QueueMetadata(metadata, DateTime.UtcNow);
            if (result == false)
                Trace.WriteLine(string.Format("{0}: Failed to write to GPS channel", DateTime.UtcNow));
            else
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    if (sourceComboBox.SelectedIndex == 1)
                    {
                        DisplayMetadata(metadata);
                    }
                }));
            }
        }

        private void SendBoundingBoxData(Rectangle boundingBox, Vector dummyCenterOfGravity)
        {
            const int objectId = 1;

            var metadata = new MetadataStream
            {
                VideoAnalyticsItems =
                {
                    new VideoAnalytics
                    {
                        Frames =
                        {
                            new Frame(DateTime.UtcNow)
                            {
                                Objects =
                                {
                                    new OnvifObject(objectId)
                                    {
                                        Appearance = new Appearance
                                        {
                                            Shape = new Shape
                                            {
                                                BoundingBox = boundingBox,
                                                CenterOfGravity = dummyCenterOfGravity
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var result = _boundingBoxProviderChannel.QueueMetadata(metadata, DateTime.UtcNow);
            if (result == false)
                Trace.WriteLine(string.Format("{0}: Failed to write to bounding box channel", DateTime.UtcNow));
            else
            {
                MoveBoundingBox(boundingBox);
                BeginInvoke(new MethodInvoker(() =>
                {
                    if (sourceComboBox.SelectedIndex == 0)
                    {
                        DisplayMetadata(metadata);
                    }
                }));
            }
        }

        private void DisplayMetadata(MetadataStream metadata)
        {
            DisplayMetadata(_metadataSerializer.WriteMetadataXml(metadata));
        }

        private void DisplayMetadata(string metadata)
        {
            Invoke((MethodInvoker)(() =>
            {
                textMetadata.Text = metadata;
                textBoxTime.Text = DateTime.Now.ToString("HH.mm.ss:fff");
            }));
        }

        private void MoveBoundingBox(Rectangle boundingBox)
        {
            switch (_currentDirection)
            {
                case MovementDirected.Up:
                    boundingBox.Top += StepSize;
                    boundingBox.Bottom += StepSize;
                    if (boundingBox.Top > TopLimit)
                        _currentDirection = MovementDirected.Right;
                    break;
                case MovementDirected.Right:
                    boundingBox.Right += StepSize;
                    boundingBox.Left += StepSize;
                    if (boundingBox.Right > RightLimit)
                        _currentDirection = MovementDirected.Down;
                    break;
                case MovementDirected.Down:
                    boundingBox.Top -= StepSize;
                    boundingBox.Bottom -= StepSize;
                    if (boundingBox.Bottom < BottomLimit)
                        _currentDirection = MovementDirected.Left;
                    break;
                case MovementDirected.Left:
                    boundingBox.Right -= StepSize;
                    boundingBox.Left -= StepSize;
                    if (boundingBox.Left < LeftLimit)
                        _currentDirection = MovementDirected.Up;
                    break;
            }
        }

        private void DisplayIndexChanged(object sender, EventArgs e)
        {
            if (_boundingBoxProviderChannel != null && _gpsProviderChannel != null && _nonStandardProviderChannel != null)
            {
                if (sourceComboBox.SelectedIndex == 0)
                    DumpProperties(_boundingBoxProviderChannel.ServerParameterDictionary);
                if (sourceComboBox.SelectedIndex == 1)
                    DumpProperties(_gpsProviderChannel.ServerParameterDictionary);
                if (sourceComboBox.SelectedIndex == 2)
                    DumpProperties(_nonStandardProviderChannel.ServerParameterDictionary);
            }
        }
    }
}
