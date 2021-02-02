using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Live;
using VideoOS.Platform.Client;
using VideoOS.Platform.UI;

namespace MediaRGBVideoEnhancementLive
{
	public partial class MainForm : Form
	{
		private Item _selectItem;
		private ImageViewerControl _imageViewerControl;
		private BitmapLiveSource _bitmapLiveSource;
		private bool _stopped = true;
		private int _counter = 0;
		private ToolkitRGBEnhancement.RGBHandling.Transform transform;

		public MainForm()
		{
			InitializeComponent();
			transform = new ToolkitRGBEnhancement.RGBHandling.Transform();
			OnScrollChange(null, null);

		}


		#region Camera selection

		private void OnSelect(object sender, EventArgs e)
		{
			if (_imageViewerControl!=null || _bitmapLiveSource!=null)
			{
				TerminateVideo();
			}

			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems());
			if (form.ShowDialog()==DialogResult.OK)
			{
				_selectItem = form.SelectedItem;
				buttonSelect1.Text = _selectItem.Name;

				_stopped = false;

				InitializeVideo();

				buttonStop.Enabled = true;
			}
		}

		#endregion

		#region Bitmap handling

		void BitmapLiveSourceLiveContentEvent(object sender, EventArgs e)
		{
			try
			{
				if (this.InvokeRequired)
				{
					// Make sure we execute on the UI thread before updating UI Controls
					BeginInvoke(new EventHandler(BitmapLiveSourceLiveContentEvent), new[] {sender, e});
				}
				else
				{
					LiveContentEventArgs args = e as LiveContentEventArgs;
					if (args != null)
					{
						if (args.LiveContent != null)
						{
							LiveSourceBitmapContent bitmapContent = args.LiveContent as LiveSourceBitmapContent;
							if (bitmapContent != null)
							{
								labelCount.Text = ""+(_counter++);
								if (_stopped)
								{
									bitmapContent.Dispose();
								}
								else
								{
									// The following code does these functions:
									//    Get a IntPtr to the start of the GBR bitmap
									//    Transform via sample transformation (To be replaced with your C++ code)
									//    Create a Bitmap with the result
									//    Create a new Bitmap scaled to visible area on screen
									//    Assign new Bitmap into PictureBox
									//    Dispose first Bitmap
									//
									// The transformation is therefore done on the original image, but if the transformation is
									// keeping to the same size, then this would be much more effective if the resize was done first,
									// and the transformation afterwards.
									// Scaling can be done by setting the Width and Height on the 

									int width = bitmapContent.GetPlaneWidth(0);
									int height = bitmapContent.GetPlaneHeight(0);
									int stride = bitmapContent.GetPlaneStride(0);

									// When using RGB / BGR bitmaps, they have all bytes continues in memory.  The PlanePointer(0) is used for all planes:
									IntPtr plane0 = bitmapContent.GetPlanePointer(0);

									IntPtr newPlane0 = transform.Perform(plane0, stride, width, height);		// Make the sample transformation / color change

									Image myImage = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, newPlane0);

									// We need to resize to the displayed area
									pictureBoxEnhanced.Image = new Bitmap(myImage, pictureBoxEnhanced.Width, pictureBoxEnhanced.Height);

									myImage.Dispose();
									bitmapContent.Dispose();
									transform.Release(newPlane0);
								}
							}
						}
						else if (args.Exception != null)
						{
							// Handle any exceptions occurred inside toolkit or on the communication to the VMS

                            Bitmap bitmap = new Bitmap(320, 240);
                            Graphics g = Graphics.FromImage(bitmap);
                            g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                            if (args.Exception is CommunicationMIPException)
                            {
                                g.DrawString("Connection lost to server ...", new Font(FontFamily.GenericMonospace, 12),
                                             Brushes.White, new PointF(20, pictureBoxEnhanced.Height/2 - 20));
                            } else
                            {
                                g.DrawString(args.Exception.Message, new Font(FontFamily.GenericMonospace, 12),
                                             Brushes.White, new PointF(20, pictureBoxEnhanced.Height / 2 - 20));                                
                            }
						    g.Dispose();
                            pictureBoxEnhanced.Image = new Bitmap(bitmap, pictureBoxEnhanced.Size);
                            bitmap.Dispose();
						}

					}
				}
			} catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("BitmapLiveSourceLiveContentEvent", ex);
			}
		}

		#endregion

		#region User actions

		private void OnClose(object sender, EventArgs e)
		{
			_stopped = true;
			TerminateVideo();
			
			Close();
		}

		private void OnStop(object sender, EventArgs e)
		{
			_stopped = true;
			buttonRestart.Enabled = _stopped;
			buttonStop.Enabled = !_stopped;

			TerminateVideo();
		}

		private void OnRestart(object sender, EventArgs e)
		{
			_stopped = false;
			buttonRestart.Enabled = _stopped;
			buttonStop.Enabled = !_stopped;

			InitializeVideo();
		}

		private void OnScrollChange(object sender, ScrollEventArgs e)
		{
			transform.SetVectors(vScrollBarR.Value * hScrollBarExpose.Value, vScrollBarG.Value * hScrollBarExpose.Value, vScrollBarB.Value * hScrollBarExpose.Value, hScrollBarOffset.Value);
		}

		#endregion

		#region private methods

		private void InitializeVideo()
		{
			_imageViewerControl = ClientControl.Instance.GenerateImageViewerControl();
			_imageViewerControl.Dock = DockStyle.Fill;
			panel1.Controls.Clear();
			panel1.Controls.Add(_imageViewerControl);
			_imageViewerControl.CameraFQID = _selectItem.FQID;
			_imageViewerControl.MaintainImageAspectRatio = true;
			_imageViewerControl.Initialize();
			_imageViewerControl.Connect();
			_imageViewerControl.Selected = true;

			_bitmapLiveSource = new BitmapLiveSource(_selectItem, BitmapFormat.BGR24);
			_bitmapLiveSource.LiveContentEvent += new EventHandler(BitmapLiveSourceLiveContentEvent);
			try
			{
				_bitmapLiveSource.Width = pictureBoxEnhanced.Width;
				_bitmapLiveSource.Height = pictureBoxEnhanced.Height;
				_bitmapLiveSource.SetKeepAspectRatio(true, true);
				_bitmapLiveSource.Init();
				_bitmapLiveSource.LiveModeStart = true;
			} catch (Exception ex)
			{
				MessageBox.Show("Unable to connect to recording server:"+ex.Message);
			}

		}

		private void TerminateVideo()
		{
			if (_bitmapLiveSource != null)
			{
				_bitmapLiveSource.LiveContentEvent -= new EventHandler(BitmapLiveSourceLiveContentEvent);
				_bitmapLiveSource.LiveModeStart = false;
				_bitmapLiveSource.Close();
				_bitmapLiveSource = null;
			}
			if (_imageViewerControl != null)
			{
				_imageViewerControl.Disconnect();
				_imageViewerControl.Close();
				_imageViewerControl = null;
			}
		}

		private void OnResizePictureBox(object sender, EventArgs e)
		{
            if (pictureBoxEnhanced.Width != 0 && _bitmapLiveSource!=null)
			{
				_bitmapLiveSource.Width = pictureBoxEnhanced.Width;
				_bitmapLiveSource.Height = pictureBoxEnhanced.Height;
				_bitmapLiveSource.SetWidthHeight();
			}
		}
		#endregion
	}
}
