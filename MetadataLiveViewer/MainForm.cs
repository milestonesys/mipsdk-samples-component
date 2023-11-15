using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Live;
using VideoOS.Platform.UI;

namespace MetadataLiveViewer
{
	public partial class MainForm : Form
	{
		#region private fields

		private Item _selectItem1;
		private MetadataLiveSource _metadataLiveSource;
        private int _count;

		#endregion

		#region construction and close

		public MainForm()
		{
			InitializeComponent();
		}

		private void OnClose(object sender, EventArgs e)
		{
			if (_metadataLiveSource != null)
				_metadataLiveSource.Close();
			Close();
		}
		#endregion


		#region Live Click handling 
		private void OnSelect1Click(object sender, EventArgs e)
		{
			if (_metadataLiveSource != null)
			{
				// Close any current displayed Metadata Live Source
				_metadataLiveSource.LiveContentEvent -= OnLiveContentEvent;
				_metadataLiveSource.LiveStatusEvent -= OnLiveStatusEvent;
				_metadataLiveSource.Close();
				_metadataLiveSource = null;
			}

			ClearAllFlags();
			ResetSelections();

			ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
			{
				KindsFilter = new List<Guid> { Kind.Metadata },
				SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
				Items = Configuration.Instance.GetItems(),
            };

			if (itemPicker.ShowDialog().Value)
			{
                _selectItem1 = itemPicker.SelectedItems.First();
                deviceSelectButton.Text = _selectItem1.Name;

                _metadataLiveSource = new MetadataLiveSource(_selectItem1);
                try
                {
                    _metadataLiveSource.LiveModeStart = true;
                    _metadataLiveSource.Init();
                    _metadataLiveSource.LiveContentEvent += OnLiveContentEvent;
                    _metadataLiveSource.LiveStatusEvent += OnLiveStatusEvent;
                    _metadataLiveSource.ErrorEvent += OnErrorEvent;

                    _count = 0;
                    labelCount.Text = _count.ToString(CultureInfo.InvariantCulture);
                    buttonPause.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Could not Init:" + ex.Message);
                    _metadataLiveSource = null;
                }
            }
			else
			{
                _selectItem1 = null;
                deviceSelectButton.Text = @"Select Metadata device ...";
                labelCount.Text = "0";
                labelSize.Text = "";
                buttonPause.Enabled = false;
            }
		}

        /// <summary>
        /// This event is called when some exception has occurred
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="exception"></param>
	    private void OnErrorEvent(MetadataLiveSource sender, Exception exception)
	    {
            if (InvokeRequired)
            {
                // Make sure we execute on the UI thread before updating UI Controls
                BeginInvoke(new Action(() => OnErrorEvent(sender, exception)));
            }
            else
            {
                // Display the error
                labelSize.Text = @"Error!";

                if (exception is CommunicationMIPException)
                {
                    textBoxMetadataOutput.Text = @"Connection lost to server ...";
                }
                else
                {
                    textBoxMetadataOutput.Text = exception.ToString();
                }
            }
	    }

	    /// <summary>
		/// This event is called when metadata is available
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        void OnLiveContentEvent(MetadataLiveSource sender, MetadataLiveContent e)
        {
            if (InvokeRequired)
            {
                // Make sure we execute on the UI thread before updating UI Controls
                BeginInvoke(new Action(() => OnLiveContentEvent(sender, e)));
            }
            else
            {
                if (e.Content != null)
                {
                    // Display the received metadata
                    var metadataXml = e.Content.GetMetadataString();
                    textBoxMetadataOutput.Text = metadataXml;
                    labelSize.Text = metadataXml.Length.ToString(CultureInfo.InvariantCulture);
                    _count++;
                    labelCount.Text = "" + _count;
                }
            }
        }

		/// <summary>
		/// This event is called when a Live status package has been received.
		/// </summary>
		/// <param name="sender"></param>
        /// <param name="args"></param>
        void OnLiveStatusEvent(object sender, LiveStatusEventArgs args)
		{
			if (InvokeRequired)
			{
                BeginInvoke(new EventHandler<LiveStatusEventArgs>(OnLiveStatusEvent), new[] { sender, args });
			}
			else
			{
				if (args != null)
				{
					if ((args.ChangedStatusFlags & StatusFlags.Motion) != 0)
						checkBoxMotion.Checked = (args.CurrentStatusFlags & StatusFlags.Motion) != 0;

					if ((args.ChangedStatusFlags & StatusFlags.Notification) != 0)
						checkBoxNotification.Checked = (args.CurrentStatusFlags & StatusFlags.Notification) != 0;

					if ((args.ChangedStatusFlags & StatusFlags.CameraConnectionLost) != 0)
						checkBoxOffline.Checked = (args.CurrentStatusFlags & StatusFlags.CameraConnectionLost) != 0;

					if ((args.ChangedStatusFlags & StatusFlags.Recording) != 0)
						checkBoxRec.Checked = (args.CurrentStatusFlags & StatusFlags.Recording) != 0;

					if ((args.ChangedStatusFlags & StatusFlags.LiveFeed) != 0)
						checkBoxLiveFeed.Checked = (args.CurrentStatusFlags & StatusFlags.LiveFeed) != 0;

					if ((args.ChangedStatusFlags & StatusFlags.ClientLiveStopped) != 0)
						checkBoxClientLive.Checked = (args.CurrentStatusFlags & StatusFlags.ClientLiveStopped) != 0;

					if ((args.ChangedStatusFlags & StatusFlags.DatabaseFail) != 0)
						checkBoxDBFail.Checked = (args.CurrentStatusFlags & StatusFlags.DatabaseFail) != 0;

					if ((args.ChangedStatusFlags & StatusFlags.DiskFull) != 0)
						checkBoxDiskFull.Checked = (args.CurrentStatusFlags & StatusFlags.DiskFull) != 0;

					Debug.WriteLine("LiveStatus: motion=" + checkBoxMotion.Checked + ", Notification=" + checkBoxNotification.Checked +
					                ", Offline=" + checkBoxOffline.Checked + ", Recording=" + checkBoxRec.Checked);

					if (checkBoxLiveFeed.Checked==false)
					{
						ClearAllFlags();
					}
				}
			}
		}

		private void ClearAllFlags()
		{
			checkBoxMotion.Checked = false;
			checkBoxNotification.Checked = false;
			checkBoxOffline.Checked = false;
			checkBoxRec.Checked = false;
			checkBoxClientLive.Checked = false;
			checkBoxDBFail.Checked = false;
			checkBoxDiskFull.Checked = false;
			checkBoxLiveFeed.Checked = false;
		}

		private void ResetSelections()
		{
			checkBoxClientLive.Checked = false;
			buttonPause.Enabled = false;
			buttonPause.Text = "Pause";
		}
        #endregion

		private void OnClick(object sender, EventArgs e)
		{
			if (_metadataLiveSource != null)
			{
				if (_metadataLiveSource.LiveModeStart)				//TODO review when tool kit return msg is OK.
				{
					_metadataLiveSource.LiveModeStart = false;
					buttonPause.Text = "Start";
				}
				else
				{
					_metadataLiveSource.LiveModeStart = true;
					buttonPause.Text = "Pause";
				}
			}
		}
	}
}
