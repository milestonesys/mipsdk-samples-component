using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;

namespace MatrixServer
{
	public partial class MatrixViewItem : UserControl
	{
		private ImageViewerControl _imageViewerControl;
		private Item _item;

		public MatrixViewItem()
		{
			InitializeComponent();
		}


		public Item CameraItem
		{
			get { return _item; }
			set
			{
				if (value != null || _item != null && IsHandleCreated)
				{
					// Get to UI thread
					Invoke(new MethodInvoker(delegate()
						                         {
							                         PerformDisconnect();
							                         _item = value;
							                         if (value != null)
							                         {
								                         PerformConnect();
							                         }
						                         }));
				}
			}
		}

		private void PerformDisconnect()
		{
			if (_imageViewerControl != null)
			{
				this.Controls.Clear();
				_imageViewerControl.Disconnect();
				_imageViewerControl.Close();
				_imageViewerControl.Dispose();
				_imageViewerControl = null;
			}

		}

		private void PerformConnect()
		{
			_imageViewerControl = ClientControl.Instance.GenerateImageViewerControl();
			_imageViewerControl.Dock = DockStyle.Fill;
			this.Controls.Add(_imageViewerControl);

			_imageViewerControl.CameraFQID = _item.FQID;
			_imageViewerControl.EnableVisibleHeader = true;
			_imageViewerControl.EnableMousePtzEmbeddedHandler = true;
			_imageViewerControl.Initialize();
			_imageViewerControl.Connect();
			_imageViewerControl.Selected = true;
			_imageViewerControl.EnableDigitalZoom = true;
		}
	}
}
