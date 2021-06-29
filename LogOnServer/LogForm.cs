using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace LogOnServer
{
	public partial class LogForm : Form
	{
		private Item _selectItem1;
		private ImageViewerControl _imageViewerControl1;

		public LogForm()
		{
			InitializeComponent();
		}

		private void buttonSelect1_Click(object sender, EventArgs e)
		{
			if (_imageViewerControl1 != null)
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
			if (form.ShowDialog() == DialogResult.OK)
			{
				_selectItem1 = form.SelectedItem;
				buttonSelect1.Text = _selectItem1.Name;

				_imageViewerControl1 = ClientControl.Instance.GenerateImageViewerControl();
				_imageViewerControl1.Dock = DockStyle.Fill;
				panel1.Controls.Clear();
				panel1.Controls.Add(_imageViewerControl1);
				_imageViewerControl1.CameraFQID = _selectItem1.FQID;
				_imageViewerControl1.Initialize();
				_imageViewerControl1.Connect();
				_imageViewerControl1.Selected = true;
			}
		}

		private void OnStartRecording1(object sender, EventArgs e)
		{
			if (_selectItem1 != null)
				EnvironmentManager.Instance.SendMessage(
					new VideoOS.Platform.Messaging.Message(MessageId.Control.StartRecordingCommand), _selectItem1.FQID);
			LogResourceHandler.LogStart(_selectItem1);
		}

		private void OnStopRecording1(object sender, EventArgs e)
		{
			if (_selectItem1 != null)
				EnvironmentManager.Instance.SendMessage(
					new VideoOS.Platform.Messaging.Message(MessageId.Control.StopRecordingCommand), _selectItem1.FQID);
			LogResourceHandler.LogStop(_selectItem1);
		}

		private void OnGranted(object sender, EventArgs e)
		{
			LogResourceHandler.CardSwiped(true);

			LogResourceHandler.CardSwiped(false);
		}

		private void OnClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				VideoOS.Platform.SDK.Environment.Logout();
				VideoOS.Platform.SDK.Environment.RemoveAllServers();
			}
			catch (Exception)
			{
			}
		}

	}
}
