using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.Platform.Data;
using VideoOS.Platform.Metadata;
using Appearance = VideoOS.Platform.Metadata.Appearance;

namespace BoundingBoxMetadataProvider
{
    public partial class MainForm : Form
    {
        // Constants used for describing the size and movement of the bounding box.
        private const float TopLimit = 0.75f;
        private const float RightLimit = 0.75f;
        private const float BottomLimit = -0.75f;
        private const float LeftLimit = -0.75f;
        private const float BoundingBoxSize = 0.5f;
        private const float StepSize = 0.1f;
        private const float MaxTextSize = 0.1f;
        private const float MinTextSize = 0.05f;

        private const uint MinLineThicknes = 1;
        private const uint MaxLineThicknes = 5;

        private readonly TimeSpan _timeBetweenMetadata = TimeSpan.FromSeconds(0.5);

        private MetadataProviderChannel _metadataProviderChannel;
        private MediaProviderService _metadataProviderService;
        private readonly MetadataSerializer _metadataSerializer = new MetadataSerializer();

        private CancellationTokenSource _cts;
        private Task _senderTask;
        private MovementDirected _currentDirection;

        private static readonly IList<DisplayColor> Colors = CreateColors().ToList();
        private static readonly IList<string> Texts = CreateTextValues().ToList();
        private static readonly IList<string> FontFamilys = new List<string> { "Helvetica", "Courier New", "Times New Roman" }; 

        private static IEnumerable<DisplayColor> CreateColors()
        {
            yield return DisplayColor.ParseArgbString("#FFF0F8FF");
            yield return DisplayColor.ParseArgbString("#BBF5F5DC");
            yield return DisplayColor.ParseArgbString("#FFA52A2A");
            yield return DisplayColor.ParseArgbString("#887FFF00");
            yield return DisplayColor.ParseArgbString("#FFDC143C");
            yield return DisplayColor.ParseArgbString("#66BDB76B");
            yield return DisplayColor.ParseArgbString("#FFFF8C00");
            yield return DisplayColor.ParseArgbString("#44808080");
            yield return DisplayColor.ParseArgbString("#FFF0F888");
            yield return DisplayColor.ParseArgbString("#226B8E23");
            yield return null;
        }

        private static IEnumerable<string> CreateTextValues()
        {
            yield return "A square";
            yield return "A text\nwith several\nline breaks";
            yield return "A sentence that is very long and contains lots of text. And now even more text. Wait, there is more!";
        }

        private enum MovementDirected
        {
            Up, Down, Left, Right
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonConnectClick(object sender, EventArgs e)
        {
            // Open the HTTP Service
            if (_metadataProviderService == null)
            {
                var hardwareDefinition = new HardwareDefinition(
                    PhysicalAddress.Parse("001122334455"),
                    "MetadataProvider")
                {
                    Firmware = "v10",
                    MetadataDevices = { MetadataDeviceDefintion.CreateBoundingBoxDevice() }
                };

                _metadataProviderService = new MediaProviderService();
                _metadataProviderService.Init(52123, "password", hardwareDefinition);
            }

            // Create a provider to handle channel 1
            _metadataProviderChannel = _metadataProviderService.CreateMetadataProvider(1);
            _metadataProviderChannel.SessionOpening += MetadataProviderSessionOpening;
            _metadataProviderChannel.SessionClosed += MetadataProviderSessionClosed;
            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
            buttonSendData.Enabled = true;
        }

        private void ButtonDisconnectClick(object sender, EventArgs e)
        {
            StopMetadata();

            if (_metadataProviderChannel != null)
            {
                _metadataProviderChannel.Disconnect();
                textBoxSessionCount.Text = @"0";
            }

            _metadataProviderService.RemoveMetadataProvider(_metadataProviderChannel);
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            buttonSendData.Enabled = false;
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
            
            if (_metadataProviderChannel != null)
            {
                _metadataProviderChannel.Disconnect();
                _metadataProviderChannel.SessionOpening -= MetadataProviderSessionOpening;
                _metadataProviderChannel.SessionClosed -= MetadataProviderSessionClosed;
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
                buttonSendData.Text = @"Start Metadata";
            }
        }

        private void StartMetadata()
        {
            _cts = new CancellationTokenSource();
            _senderTask =
                Task.Factory.StartNew(SendData, _cts.Token, TaskCreationOptions.LongRunning)
                    .ContinueWith(DisplayException, TaskContinuationOptions.OnlyOnFaulted);
            buttonSendData.Text = @"Stop Metadata";
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
                textBoxSessionCount.Text = "" + _metadataProviderChannel.ActiveSessions;
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
                textBoxSessionCount.Text = "" + _metadataProviderChannel.ActiveSessions;
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
                Right = LeftLimit + BoundingBoxSize,
                LineColor = Colors[0],
                FillColor = Colors[Colors.Count / 2],
                LineDisplayPixelThickness = 1
            };

            var description = new DisplayText
            {
                Value = Texts[0],
                FontFamily = FontFamilys[0],
                IsBold = false,
                IsItalic = false,
                Color = Colors[0],
                Size = MinTextSize,
                CenterX = 0,
                CenterY = 0
            };

            const int staticObjectId = 2;
            var staticObject = new OnvifObject(staticObjectId)
            {
                Appearance = new Appearance
                {
                    Shape = new Shape
                    {
                        BoundingBox = new Rectangle
                        {
                            Bottom = -BoundingBoxSize / 2,
                            Left = -BoundingBoxSize / 2,
                            Top = BoundingBoxSize / 2,
                            Right = BoundingBoxSize / 2
                        }
                    },
                    Description = new DisplayText
                    {
                        Value = "This is just text"
                    }
                }
            };

            _currentDirection = MovementDirected.Up;

            while (token.IsCancellationRequested == false)
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
                                                },
                                                Description = description
                                            }
                                        },
                                        staticObject
                                    }
                                }
                            }
                        }
                    }
                };

                var result = _metadataProviderChannel.QueueMetadata(metadata, DateTime.UtcNow);
                if (result == false)
                    Trace.WriteLine(string.Format("{0}: Failed to write to channel", DateTime.UtcNow));
                else
                {
                    MoveBoundingBox(boundingBox);
                    UpdateLineColor(boundingBox);
                    UpdateFillColor(boundingBox);
                    UpdateLineThickness(boundingBox);

                    UpdateTextValue(description);
                    UpdateFontFamily(description);
                    UpdateTextDecoration(description);
                    UpdateTextColor(description);
                    UpdateTextSize(description);
                    UpdateTextPosition(description, boundingBox);
                    
                    Invoke((MethodInvoker)(() =>
                    {
                        textMetadata.Text = _metadataSerializer.WriteMetadataXml(metadata);
                        textBoxTime.Text = DateTime.Now.ToString("HH.mm.ss:fff");
                    }));
                }

                Thread.Sleep(_timeBetweenMetadata);
            }
        }

        private void UpdateLineThickness(Rectangle boundingBox)
        {
            if (boundingBox.LineDisplayPixelThickness.HasValue == false)
            {
                boundingBox.LineDisplayPixelThickness = MinLineThicknes;
            }

            var nextThickness = boundingBox.LineDisplayPixelThickness.Value % MaxLineThicknes + 1;
            boundingBox.LineDisplayPixelThickness = nextThickness;
        }

        private void UpdateFillColor(Rectangle boundingBox)
        {
            var nextColor = GetNextColor(boundingBox.FillColor);
            boundingBox.FillColor = nextColor;
        }

        private void UpdateLineColor(Rectangle boundingBox)
        {
            var nextColor = GetNextColor(boundingBox.LineColor);
            boundingBox.LineColor = nextColor;
        }

        private DisplayColor GetNextColor(DisplayColor lineColor)
        {
            var index = Colors.IndexOf(lineColor);
            return index == Colors.Count - 1 ? Colors[0] : Colors[index + 1];
        }

        private void UpdateFontFamily(DisplayText description)
        {
            var index = FontFamilys.IndexOf(description.FontFamily);
            description.FontFamily = index == FontFamilys.Count - 1 ? FontFamilys[0] : FontFamilys[index + 1];
        }

        private void UpdateTextDecoration(DisplayText description)
        {
            if (!description.IsBold && !description.IsItalic)
            {
                description.IsBold = false;
                description.IsItalic = true;
            }
            else if (!description.IsBold && description.IsItalic)
            {
                description.IsBold = true;
                description.IsItalic = true;
            }
            else if (description.IsBold && description.IsItalic)
            {
                description.IsBold = true;
                description.IsItalic = false;
            }
            else
            {
                description.IsBold = false;
                description.IsItalic = false;
            }
        }

        private void UpdateTextColor(DisplayText description)
        {
            var nextColor = GetNextColor(description.Color);
            description.Color = nextColor;
        }

        private void UpdateTextSize(DisplayText description)
        {
            var newSize = description.Size + 0.01f;
            description.Size = newSize > MaxTextSize ? MinTextSize : newSize;
        }

        private void UpdateTextPosition(DisplayText description, Rectangle boundingBox)
        {
            if (_currentDirection == MovementDirected.Up || _currentDirection == MovementDirected.Right)
            {
                description.CenterX = boundingBox.Left + (boundingBox.Right - boundingBox.Left) / 2 ;
                description.CenterY = boundingBox.Top + description.Size;
            }
            if (_currentDirection == MovementDirected.Down || _currentDirection == MovementDirected.Left)
            {
                description.CenterX = boundingBox.Left + (boundingBox.Right - boundingBox.Left) / 2;
                description.CenterY = boundingBox.Bottom - description.Size;
            }
        }

        private void UpdateTextValue(DisplayText description)
        {
            var index = Texts.IndexOf(description.Value);
            description.Value = index == Texts.Count - 1 ? Texts[0] : Texts[index + 1];
        }

        private void MoveBoundingBox(Rectangle boundingBox)
        {
            switch (_currentDirection)
            {
                case MovementDirected.Up:
                    boundingBox.Top += StepSize;
                    boundingBox.Bottom += StepSize;
                    if (boundingBox.Top >= TopLimit)
                        _currentDirection = MovementDirected.Right;
                    break;
                case MovementDirected.Right:
                    boundingBox.Right += StepSize;
                    boundingBox.Left += StepSize;
                    if (boundingBox.Right >= RightLimit)
                        _currentDirection = MovementDirected.Down;
                    break;
                case MovementDirected.Down:
                    boundingBox.Top -= StepSize;
                    boundingBox.Bottom -= StepSize;
                    if (boundingBox.Bottom <= BottomLimit)
                        _currentDirection = MovementDirected.Left;
                    break;
                case MovementDirected.Left:
                    boundingBox.Right -= StepSize;
                    boundingBox.Left -= StepSize;
                    if (boundingBox.Left <= LeftLimit)
                        _currentDirection = MovementDirected.Up;
                    break;
            }
        }
    }
}
