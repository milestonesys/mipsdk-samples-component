using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Live;
using VideoOS.Platform.UI;
using VideoOS.Platform.Util.AdaptiveStreaming;

namespace MediaLiveViewer
{
	public partial class MainForm : Form
	{
		#region private fields

		private Item _selectItem1;
		private JPEGLiveSource _jpegLiveSource;

		private int _count = 0;

		#endregion

		#region construction and close

		public MainForm()
		{
			InitializeComponent();
			comboBoxResolution.SelectedIndex = 1;
			comboBoxCompression.SelectedIndex = 0;
            comboBoxStreamSelection.SelectedIndex = 0;
		}

		private void OnClose(object sender, EventArgs e)
		{
			if (_jpegLiveSource != null)
				_jpegLiveSource.Close();
			Close();
		}
		#endregion


		#region Live Click handling 
		private void OnSelect1Click(object sender, EventArgs e)
		{
			if (_jpegLiveSource != null)
			{
				// Close any current displayed JPEG Live Source
				_jpegLiveSource.LiveContentEvent -= JpegLiveSource1LiveNotificationEvent;
				_jpegLiveSource.LiveStatusEvent -= JpegLiveStatusNotificationEvent;
				_jpegLiveSource.Close();
				_jpegLiveSource = null;
				pictureBox1.Image = new Bitmap(1, 1);
			}
            /*
			checkBoxAspect.Checked = false;
			checkBoxFill.Checked = false;
			checkBoxKeyFramesOnly.Checked = false;
			checkBoxCrop.Checked = false;
            */
			ClearAllFlags();
			ResetSelections();

			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems());

			// Ask user to select a camera
			if (form.ShowDialog() == DialogResult.OK)		
			{
				_selectItem1 = form.SelectedItem;
				buttonSelect1.Text = _selectItem1.Name;

				_jpegLiveSource = new JPEGLiveSource(_selectItem1);
				try
				{
					SetResolution();
					_jpegLiveSource.LiveModeStart = true;
					_jpegLiveSource.SetKeepAspectRatio(checkBoxAspect.Checked, checkBoxFill.Checked);
					checkBoxAspect.Enabled = false;
					checkBoxFill.Enabled = false;
					checkBoxKeyFramesOnly.Enabled = false;
					comboBoxResolution.Enabled = !checkBoxAspect.Checked;	// Only allow resolution change, if filling available space
				    _jpegLiveSource.Width = pictureBox1.Width;
				    _jpegLiveSource.Height = pictureBox1.Height;
					_jpegLiveSource.KeyFramesOnly = checkBoxKeyFramesOnly.Checked;
                    SetStreamType(pictureBox1.Width, pictureBox1.Height);
                    _jpegLiveSource.Init();
					_jpegLiveSource.LiveContentEvent += JpegLiveSource1LiveNotificationEvent;
					_jpegLiveSource.LiveStatusEvent += JpegLiveStatusNotificationEvent;

					labelCount.Text = "0";
					buttonPause.Enabled = true;
                    buttonLift.Enabled = true;
					_count = 0;

				} catch (Exception ex)
				{
					MessageBox.Show("Could not Init:" + ex.Message);
					_jpegLiveSource = null;
				}
			} else
			{
				_selectItem1 = null;
				buttonSelect1.Text = "Select Camera ...";
				labelCount.Text = "0";
				labelSize.Text = "";
				labelResolution.Text = "";
				buttonPause.Enabled = false;
                checkBoxAspect.Enabled = true;
                checkBoxFill.Enabled = true;
                checkBoxKeyFramesOnly.Enabled = true;
                buttonLift.Enabled = false;
            }
        }

	    private bool OnMainThread = false;
		/// <summary>
		/// This event is called when JPEG is available or some exception has occurred
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void JpegLiveSource1LiveNotificationEvent(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
			    if (OnMainThread)
			    {
                    LiveContentEventArgs args = e as LiveContentEventArgs;
			        if (args != null && args.LiveContent != null)
			        {
                        // UI thread is too busy - discard this frame from display
			            args.LiveContent.Dispose();
			        }
			        return;
			    }
			    OnMainThread = true;
			    // Make sure we execute on the UI thread before updating UI Controls
				BeginInvoke(new EventHandler(JpegLiveSource1LiveNotificationEvent), new[] { sender, e });
			}
			else
			{
				LiveContentEventArgs args = e as LiveContentEventArgs;
				if (args != null)
				{
					if (args.LiveContent != null)		
					{
						// Display the received JPEG
						labelSize.Text = ""+args.LiveContent.Content.Length;

						int width = args.LiveContent.Width;
						int height = args.LiveContent.Height;

						MemoryStream ms = new MemoryStream(args.LiveContent.Content);
						Bitmap newBitmap = new Bitmap(ms);
						labelResolution.Text = "" + width + "x" + height;
						if (pictureBox1.Size.Width != 0 && pictureBox1.Size.Height != 0)
						{
							if (!checkBoxAspect.Checked && (newBitmap.Width != pictureBox1.Width || newBitmap.Height != pictureBox1.Height))
							{
								pictureBox1.Image = new Bitmap(newBitmap, pictureBox1.Size);
							}
							else
							{
								pictureBox1.Image = newBitmap;
							}
						}
						if (args.LiveContent.CroppingDefined)
						{
							labelCropRect.Text = "" + args.LiveContent.CropWidth + "x" + args.LiveContent.CropHeight;
						} else
						{
							labelCropRect.Text = "--";
						}
						textBoxDecodingStatus.Text = args.LiveContent.HardwareDecodingStatus;

						ms.Close();
						ms.Dispose();

						_count++;
						labelCount.Text = "" + _count;

						args.LiveContent.Dispose();
					} else if (args.Exception != null)
					{
						// Handle any exceptions occurred inside toolkit or on the communication to the VMS

					    Bitmap bitmap = new Bitmap(320, 240);
                        Graphics g = Graphics.FromImage(bitmap);
                        g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                        g.DrawString("Connection lost to server ...", new Font(FontFamily.GenericMonospace, 12), Brushes.White, new PointF(20, pictureBox1.Height/2 - 20));
					    g.Dispose();
                        pictureBox1.Image = new Bitmap(bitmap, pictureBox1.Size);
					    bitmap.Dispose();
					}

				}
                OnMainThread = false;
			}
		}

		/// <summary>
		/// This event is called when a Live status package has been received.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void JpegLiveStatusNotificationEvent(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				BeginInvoke(new EventHandler(JpegLiveStatusNotificationEvent), new[] { sender, e });
			}
			else
			{
				LiveStatusEventArgs args = e as LiveStatusEventArgs;
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
			//checkBoxOffline.Checked = false;
			checkBoxRec.Checked = false;
			checkBoxClientLive.Checked = false;
			checkBoxDBFail.Checked = false;
			checkBoxDiskFull.Checked = false;
			checkBoxLiveFeed.Checked = false;
		}

		private void ResetSelections()
		{
			comboBoxResolution.SelectedIndex = 1;
			comboBoxCompression.SelectedIndex = 0;
			checkBoxClientLive.Checked = false;
			buttonPause.Enabled = false;
			buttonPause.Text = "Pause";
		}

		private void OnResolutionChanged(object sender, EventArgs e)
		{
			if (_jpegLiveSource != null)
			{
				// _jpegLiveSource.LiveModeStart = false;
				SetResolution();
				// _jpegLiveSource.LiveModeStart = true;
			}
		}

		private void SetResolution()
		{
            int width = 0, height = 0;
			switch (comboBoxResolution.SelectedIndex)
			{
				case 0:
					width = 160;
					height = 120;
					break;
				case 1:
					width = 320;
					height = 240;
					break;
				case 2:
					width = 640;
					height = 480;
					break;
				case 3:
					width = 1024;
					height = 780;
					break;
                case 4:
                    width = 1920;
                    height = 1080;
                    break;
            }
            _jpegLiveSource.Width = width;
            _jpegLiveSource.Height = height;
            _jpegLiveSource.SetWidthHeight();

            SetStreamType(width, height);
        }

        private void SetStreamType(int width, int height)
        {
            if (null == _jpegLiveSource)
                return;

            switch (comboBoxStreamSelection.SelectedIndex)
            {
                case 0:
                    _jpegLiveSource.StreamSelectionParams.StreamSelectionType = StreamSelectionType.DefaultStream;
                    break;

                case 1:
                    _jpegLiveSource.StreamSelectionParams.StreamSelectionType = StreamSelectionType.MaximalResolution;
                    break;

                case 2:
                    _jpegLiveSource.StreamSelectionParams.SetStreamAdaptiveResolution(width, height);
                    _jpegLiveSource.StreamSelectionParams.StreamSelectionType = StreamSelectionType.AdaptiveToResolution;
                    break;
            }
        }

        #endregion

        private void OnClick(object sender, EventArgs e)
		{
			if (_jpegLiveSource != null)
			{
				if (_jpegLiveSource.LiveModeStart)				//TODO review when tool kit return msg is OK.
				{
					_jpegLiveSource.LiveModeStart = false;
					buttonPause.Text = "Start";
				}
				else
				{
					_jpegLiveSource.LiveModeStart = true;
					buttonPause.Text = "Pause";
				}
			}
		}

        private void OnQualityChanged(object sender, EventArgs e)
		{
			if (_jpegLiveSource!=null)
			{
				int newQuality;
                if (Int32.TryParse(comboBoxCompression.SelectedItem.ToString(), out newQuality))
                    _jpegLiveSource.Compression = newQuality;
			}
		}

		private void OnCropChanged(object sender, EventArgs e)
		{
			if (_jpegLiveSource!=null)
			{
				if (checkBoxCrop.Checked)
				{
					_jpegLiveSource.Width = 80;
					_jpegLiveSource.Height = 80;
					_jpegLiveSource.SetWidthHeight();
					_jpegLiveSource.SetCropRectangle(100, 100, 80, 80);
				}
				else
				{
					SetResolution();
					_jpegLiveSource.SetCropRectangle(0.0, 0.0, 1.0, 1.0);
				}
			}
		}

		private void OnResizePictureBox(object sender, EventArgs e)
		{
		    if (_jpegLiveSource != null)
		    {
		        _jpegLiveSource.Width = pictureBox1.Width;
		        _jpegLiveSource.Height = pictureBox1.Height;
		        _jpegLiveSource.SetWidthHeight();
		    }

            SetStreamType(pictureBox1.Width, pictureBox1.Height);
        }

        private void comboBoxfps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_jpegLiveSource != null)
            {
                var value = comboBoxfps.SelectedItem.ToString();
                if (value.Equals("default", StringComparison.InvariantCultureIgnoreCase))
                {
                    _jpegLiveSource.FPS = 0;
                }
                else
                {
                    int newFps;
                    if (Int32.TryParse(comboBoxfps.SelectedItem.ToString(), out newFps))
                        _jpegLiveSource.FPS = newFps;
                }
            }
        }

        private void buttonLift_Click(object sender, EventArgs e)
        {
            Configuration.Instance.ServerFQID.ServerId.UserContext.SetPrivacyMaskLifted(!Configuration.Instance.ServerFQID.ServerId.UserContext.PrivacyMaskLifted);
        }

        private void comboBoxStreamSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStreamType(pictureBox1.Width, pictureBox1.Height);
        }
    }
}
