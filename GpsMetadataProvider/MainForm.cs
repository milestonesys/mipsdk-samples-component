using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using VideoOS.Platform.Data;
using VideoOS.Platform.Metadata;

namespace GpsMetadataProvider
{
    public partial class MainForm : Form
    {
        private MetadataProviderChannel _metadataProviderChannel;
        private MediaProviderService _metadataProviderService;
        private readonly MetadataSerializer _metadataSerializer = new MetadataSerializer();

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
                    MetadataDevices = { MetadataDeviceDefintion.CreateGpsDevice() }
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
            var random = new Random(DateTime.UtcNow.Millisecond);

            var metadata = new MetadataStream
            {
                NavigationalData = new NavigationalData
                {
                    Altitude = random.NextDouble(),
                    Latitude = random.Next(-90, 90),
                    Longitude = random.Next(-180, 180)
                }
            };

            var result = _metadataProviderChannel.QueueMetadata(metadata, DateTime.UtcNow);
            if (result == false)
                Trace.WriteLine(string.Format("{0}: Failed to write to channel", DateTime.UtcNow));

            textMetadata.Text = _metadataSerializer.WriteMetadataXml(metadata);
            textBoxTime.Text = DateTime.Now.ToString("HH.mm.ss:fff");
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
    }
}
