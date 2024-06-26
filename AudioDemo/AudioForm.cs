using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.SDK.Platform;
using VideoOS.Platform.UI;

namespace AudioDemo
{
	/// <summary>
	/// This sample shows how to play live from an audio source, e.g. a microphone to a camera or video server.
	/// </summary>
    public partial class AudioForm : Form
    {
		private AudioPlayer _audioPlayer;
    	private Item _selectedMic;

        private static readonly Guid IntegrationId = new Guid("D9771C97-9D24-4D45-89E4-0166AE434915");
        private const string IntegrationName = "Audio Demo";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        public AudioForm()
        {
            InitializeComponent();

        	_audioPlayer = new AudioPlayer();
            _audioPlayer.ConnectResponseEvent += new ConnectResponseEventHandler(_audioPlayer_ConnectResponseEvent);
        }

        /// <summary>
        /// Handles the Click event of the _connectButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnConnectClick(object sender, EventArgs e)
        {
            _audioDevicesGroupBox.Enabled = false;

        	Uri uri = new Uri(_serverUrlTextBox.Text);

        	VideoOS.Platform.SDK.Environment.RemoveAllServers();
        	VideoOS.Platform.SDK.Environment.AddServer(_secureOnlyCheckBox.Checked, uri, System.Net.CredentialCache.DefaultNetworkCredentials);

			try
			{
				toolStripStatusLabel1.Text = "Connecting ...";
				Refresh();
				VideoOS.Platform.SDK.Environment.Login(uri, IntegrationId, IntegrationName, Version, ManufacturerName, true);
				toolStripStatusLabel1.Text = "Connected";
				Refresh();
				_audioDevicesGroupBox.Enabled = true;
				_connectButton.Enabled = false;
			}
			catch (ServerNotFoundMIPException snfe)
			{
				toolStripStatusLabel1.Text = "Server not found: " + snfe.Message;

			}
			catch (InvalidCredentialsMIPException ice)
			{
				toolStripStatusLabel1.Text = "Invalid credentials for: " + ice.Message;
			}
			catch (Exception)
			{
				toolStripStatusLabel1.Text = "Internal error connecting to: " + uri.DnsSafeHost;
			}
        }

        /// <summary>
        /// Handles the CheckedChanged event of the _muteCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnMuteCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            _audioPlayer.Mute = _muteCheckBox.Checked;
        }

        private void OnVolumeTrackBarValueChanged(object sender, EventArgs e)
        {
            _audioPlayer.Volume = _volumeTrackBar.Value / 10.0;
        }

		/// <summary>
		/// Ask user to select a microphone. When selected initialize the AudioPlayer and start live.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSelectMic(object sender, EventArgs e)
		{
			if (_selectedMic != null)
			{
				_audioPlayer.Disconnect();
			}

            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
            itemPicker.KindsFilter = new List<Guid>() { Kind.Microphone };
            itemPicker.SelectionMode = SelectionModeOptions.AutoCloseOnSelect;
            itemPicker.Items = Configuration.Instance.GetItems(ItemHierarchy.UserDefined);

            if (itemPicker.ShowDialog().Value)
            {
                _selectedMic = itemPicker.SelectedItems.First();
                buttonSelect.Text = _selectedMic.Name;

                _audioPlayer.MicrophoneFQID = _selectedMic.FQID;
                _audioPlayer.Initialize();
                _audioPlayer.Connect();
            }
		}

        private void _audioPlayer_ConnectResponseEvent(object sender, ConnectResponseEventEventArgs e)
        {
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            if (_selectedMic != null)
            {
                _audioPlayer.Disconnect();
            }
            _audioPlayer.Close();
        }
    }
}