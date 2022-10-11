using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Live;
using VideoOS.Platform.UI;
using VideoOS.Platform.Util;

namespace MultiUserEnvironment
{
	public partial class MainForm : Form
	{
		private UserContext userContext1;
		private VideoOS.Platform.Item _selectItem1;
		private JPEGLiveSource _jpegLiveSource;

		private UserContext userContext2;
		private VideoOS.Platform.Item _selectItem2;
		private JPEGLiveSource _jpegLiveSource2;
	    private object _regObj1;

        private ConfigurationMonitor _configurationMonitor;

		public MainForm()
		{
			InitializeComponent();


		}

		private void OnClose(object sender, EventArgs e)
		{
			if (_jpegLiveSource != null)
				_jpegLiveSource.Close();
			if (_jpegLiveSource2 != null)
				_jpegLiveSource2.Close();

			VideoOS.Platform.SDK.MultiUserEnvironment.RemoveAllServers();
		    VideoOS.Platform.SDK.MultiUserEnvironment.UnInitialize();
			Close();
		}

		private void OnSaveClick(object sender, EventArgs e)
		{
            try
            {
                Uri uri = new UriBuilder(textBoxUri.Text).Uri;

                VideoOS.Platform.SDK.MultiUserEnvironment.InitializeUsingUserContext(checkBoxSecureOnly.Checked, uri, textBoxUserMain.Text, textBoxPasswordMain.Text, usingAd: true, masterOnly: false);          // General initialize for multi user Environment.  Always required

                if (VideoOS.Platform.SDK.MultiUserEnvironment.InitializeLoggedIn == false)
                {
                    MessageBox.Show(@"Incorrect parameters... try again");
                    VideoOS.Platform.SDK.MultiUserEnvironment.UnInitialize();
                    return;
                }

                //Note: this sample was changed for MIPSDK 2016, as sub-sites are now added by SDK

                VideoOS.Platform.SDK.Media.Environment.Initialize();

                _regObj1 = EnvironmentManager.Instance.RegisterReceiver(SystemConfigChangedEvent,
                            new VideoOS.Platform.Messaging.MessageIdFilter(VideoOS.Platform.Messaging.MessageId.System.SystemConfigurationChangedIndication));

                groupBoxServer.Enabled = false;
                groupBoxUserContext1.Enabled = true;
                groupBoxUserContext2.Enabled = true;

                _configurationMonitor = new ConfigurationMonitor(Configuration.Instance.ServerFQID.ServerId);
                _configurationMonitor.ShowMessage += _configurationMonitor_ShowMessage1;
                _configurationMonitor.ConfigurationNowReloaded += _configurationMonitor_ConfigurationNowReloaded;

            }
			catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("Uri Save", ex);
			}
		}

        private void _configurationMonitor_ConfigurationNowReloaded()
        {
            BeginInvoke(new Action(() => { 
            if (userContext1 != null)
                FillCameraList(userContext1, listBox1);
            if (userContext2 != null)
                FillCameraList(userContext2, listBox2);
            }));
        }

        private void _configurationMonitor_ShowMessage1(string message)
        {
            BeginInvoke( new Action(() => 
            {
                String time = DateTime.Now.ToString("T");
                int ix = listBox3.Items.Add(time + ": " + message);
                listBox3.SelectedIndex = ix;
            }));            
        }


        private void FillCameraList(UserContext userContext, ListBox listBox)
		{
            listBox.Items.Clear();

            SearchResult searchResult;
            IEnumerable<Item> allCameras = userContext.Configuration.GetItemsBySearch(Kind.Camera.ToString(), 100, 5, out searchResult).Where(i => i.FQID.FolderType == FolderType.No);
            foreach (Item item in allCameras)
            {
                if (item.Enabled)
                    listBox.Items.Add(item);
            }
        }

		#region User Context 1

		private void OnLogonUC1(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			try
			{
				userContext1 = VideoOS.Platform.SDK.MultiUserEnvironment.CreateUserContext(textBoxUserName.Text, textBoxPassword.Text,
				                                                            radioButtonAD.Checked);
				VideoOS.Platform.SDK.MultiUserEnvironment.LoginUserContext(userContext1);

				panelVideo.Enabled = true;
                listBox1.Items.Clear();

				FillCameraList(userContext1, listBox1);

                _configurationMonitor.AddUserContext(userContext1);

                button2.Enabled = true;
                button1.Enabled = false;
			} catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("Logon User 1", ex);				
			}
			Cursor = Cursors.Default;
        }

        private void OnLogoutUC1(object sender, EventArgs e)
        {
            Close1();
            VideoOS.Platform.SDK.MultiUserEnvironment.Logout(userContext1);
            VideoOS.Platform.SDK.MultiUserEnvironment.RemoveUserContext(userContext1);
            userContext1 = null;
            panelVideo.Enabled = false;
            listBox1.Items.Clear();
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void OnSelectCameraUC1(object sender, EventArgs e)
		{
			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;

            List<Item> system = userContext1.Configuration.GetItemsByKind(Kind.Camera, ItemHierarchy.SystemDefined);
            List<Item> user = userContext1.Configuration.GetItemsByKind(Kind.Camera, ItemHierarchy.UserDefined);
            var systemItems = system.All(i => i.FQID.FolderType == FolderType.No);
            var userItems = user.All(i => i.FQID.FolderType == FolderType.No);

            form.Init(userContext1.Configuration.GetItems());

			// Ask user to select a camera
			if (form.ShowDialog() == DialogResult.OK)
			{
				Close1();

				_selectItem1 = form.SelectedItem;
				buttonSelect.Text = _selectItem1.Name;

				_jpegLiveSource = new JPEGLiveSource(_selectItem1);
				try
				{
					_jpegLiveSource.Width = pictureBoxUC1.Width;
					_jpegLiveSource.Height = pictureBoxUC1.Height;
					_jpegLiveSource.LiveModeStart = true;
					_jpegLiveSource.Init();
					_jpegLiveSource.LiveContentEvent += new EventHandler(JpegLiveSource1LiveNotificationEvent);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Could not Init:" + ex.Message);
					_jpegLiveSource = null;
				}
			}

		}

		/// <summary>
		/// This event is called when JPEG is available or some exception has occurred
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void JpegLiveSource1LiveNotificationEvent(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
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

						int width = args.LiveContent.Width;
						int height = args.LiveContent.Height;

						if (pictureBoxUC1.Width != 0 && pictureBoxUC1.Height != 0)
						{
							MemoryStream ms = new MemoryStream(args.LiveContent.Content);
							Bitmap newBitmap = new Bitmap(ms);
							//labelResolution.Text = "" + width + "x" + height;
							if (newBitmap.Width != pictureBoxUC1.Width || newBitmap.Height != pictureBoxUC1.Height)
							{
								pictureBoxUC1.Image = new Bitmap(newBitmap, pictureBoxUC1.Size);
							}
							else
							{
								pictureBoxUC1.Image = newBitmap;
							}
							ms.Close();
							ms.Dispose();
						}

						args.LiveContent.Dispose();
					}
					else if (args.Exception != null)
					{
						// Handle any exceptions occurred inside toolkit or on the communication to the VMS
						if (pictureBoxUC1.Width != 0 && pictureBoxUC1.Height != 0)
						{
							Bitmap bitmap = new Bitmap(320, 240);
							Graphics g = Graphics.FromImage(bitmap);
							g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
							if (args.Exception is CommunicationMIPException)
							{
								g.DrawString("Connection lost to server ...", new Font(FontFamily.GenericMonospace, 12),
											 Brushes.White, new PointF(20, pictureBoxUC1.Height / 2 - 20));
							}
							else
							{
								g.DrawString(args.Exception.Message, new Font(FontFamily.GenericMonospace, 12),
											 Brushes.White, new PointF(20, pictureBoxUC1.Height / 2 - 20));
							}
							g.Dispose();
							pictureBoxUC1.Image = new Bitmap(bitmap, pictureBoxUC1.Size);
							bitmap.Dispose();
						}
					}
				}
			}
		}
		#endregion

		#region User Context 2
		private void OnLogonUC2(object sender, EventArgs e)
		{
            Cursor = Cursors.WaitCursor;
            try
			{
				userContext2 = VideoOS.Platform.SDK.MultiUserEnvironment.CreateUserContext(textBoxUSerName2.Text, textBoxPassword2.Text,
																			radioButtonAD2.Checked);
				VideoOS.Platform.SDK.MultiUserEnvironment.LoginUserContext(userContext2);

                listBox2.Items.Clear();

				FillCameraList(userContext2, listBox2);
				panelVideo2.Enabled = true;
                _configurationMonitor.AddUserContext(userContext2);
                button4.Enabled = true;
                button3.Enabled = false;
            }
            catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("Logon User 2", ex);
			}
            Cursor = Cursors.Default;
        }

        private void OnLogoutUC2(object sender, EventArgs e)
        {
            Close2();
            VideoOS.Platform.SDK.MultiUserEnvironment.Logout(userContext2);
            VideoOS.Platform.SDK.MultiUserEnvironment.RemoveUserContext(userContext2);
            userContext2 = null;
            panelVideo2.Enabled = false;
            listBox2.Items.Clear();
            button4.Enabled = false;
            button3.Enabled = true;
        }

        private void OnSelectCameraUC2(object sender, EventArgs e)
		{
			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(userContext2.Configuration.GetItems());

			// Ask user to select a camera
			if (form.ShowDialog() == DialogResult.OK)
			{
				Close2();

				_selectItem2 = form.SelectedItem;
				buttonSelectCamera2.Text = _selectItem2.Name;

				_jpegLiveSource2 = new JPEGLiveSource(_selectItem2);
				try
				{
					_jpegLiveSource2.Width = pictureBoxUC2.Width;
					_jpegLiveSource2.Height = pictureBoxUC2.Height;
					_jpegLiveSource2.LiveModeStart = true;
					_jpegLiveSource2.Init();
					_jpegLiveSource2.LiveContentEvent += new EventHandler(JpegLiveSource2LiveNotificationEvent);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Could not Init:" + ex.Message);
					_jpegLiveSource2 = null;
				}
			}

		}

		/// <summary>
		/// This event is called when JPEG is available or some exception has occurred
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void JpegLiveSource2LiveNotificationEvent(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				// Make sure we execute on the UI thread before updating UI Controls
				BeginInvoke(new EventHandler(JpegLiveSource2LiveNotificationEvent), new[] { sender, e });
			}
			else
			{
				LiveContentEventArgs args = e as LiveContentEventArgs;
				if (args != null)
				{
					if (args.LiveContent != null)
					{
						// Display the received JPEG

						int width = args.LiveContent.Width;
						int height = args.LiveContent.Height;
						if (pictureBoxUC2.Width != 0 && pictureBoxUC2.Height != 0)
						{
							MemoryStream ms = new MemoryStream(args.LiveContent.Content);
							Bitmap newBitmap = new Bitmap(ms);
							if (newBitmap.Width != pictureBoxUC2.Width || newBitmap.Height != pictureBoxUC2.Height)
							{
								pictureBoxUC2.Image = new Bitmap(newBitmap, pictureBoxUC2.Size);
							}
							else
							{
								pictureBoxUC2.Image = newBitmap;
							}
							ms.Close();
							ms.Dispose();
						}

						args.LiveContent.Dispose();
					}
					else if (args.Exception != null)
					{
						// Handle any exceptions occurred inside toolkit or on the communication to the VMS
						if (pictureBoxUC2.Width != 0 && pictureBoxUC2.Height != 0)
						{
							Bitmap bitmap = new Bitmap(320, 240);
							Graphics g = Graphics.FromImage(bitmap);
							g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
							if (args.Exception is CommunicationMIPException)
							{
								g.DrawString("Connection lost to server ...", new Font(FontFamily.GenericMonospace, 12),
											 Brushes.White, new PointF(20, pictureBoxUC2.Height / 2 - 20));
							}
							else
							{
								g.DrawString(args.Exception.Message, new Font(FontFamily.GenericMonospace, 12),
											 Brushes.White, new PointF(20, pictureBoxUC2.Height / 2 - 20));
							}
							g.Dispose();
							pictureBoxUC2.Image = new Bitmap(bitmap, pictureBoxUC2.Size);
							bitmap.Dispose();
						}
					}

				}
			}
		}

		#endregion

		private void OnCameraListSelect1(object sender, EventArgs e)
		{
			Item item = listBox1.SelectedItem as Item;
			if (item != null)
			{
				ItemSelected1(item);
			}
		}

		private void OnCameraListSelect2(object sender, EventArgs e)
		{
			Item item = listBox2.SelectedItem as Item;
			if (item != null)
			{
				ItemSelected2(item);
			}
		}

		private void ItemSelected1(Item item)
		{
			_selectItem1 = item;
			buttonSelect.Text = _selectItem1.Name;

			Close1();

			Cursor = Cursors.WaitCursor;
			new System.Threading.Thread(() => InitLiveSource1(pictureBoxUC1.Width, pictureBoxUC1.Height)).Start();
		}

		private void InitLiveSource1(int width, int height)
		{
			string error = null;
            _jpegLiveSource = new JPEGLiveSource(_selectItem1);
            try
            {
                _jpegLiveSource.Width = width;
                _jpegLiveSource.Height = height;
                _jpegLiveSource.LiveModeStart = true;
                _jpegLiveSource.Init();
                _jpegLiveSource.LiveContentEvent += new EventHandler(JpegLiveSource1LiveNotificationEvent);
            }
            catch (Exception ex)
            {
                error = "Could not Init:" + ex.Message;
                _jpegLiveSource = null;
            }
			Invoke(new Action(() => InitCompleted(error)));
        }

		private void InitCompleted(string errorMessage)
		{
			Cursor = Cursors.Default;
			if (!string.IsNullOrEmpty(errorMessage))
			{
				MessageBox.Show(errorMessage);
			}
		}

		private void ItemSelected2(Item item)
		{
			_selectItem2 = item;
			buttonSelectCamera2.Text = _selectItem2.Name;

			Close2();

            Cursor = Cursors.WaitCursor;
            new System.Threading.Thread(() => InitLiveSource2(pictureBoxUC2.Width, pictureBoxUC2.Height)).Start();
        }

        private void InitLiveSource2(int width, int height)
        {
            string error = null;
            _jpegLiveSource2 = new JPEGLiveSource(_selectItem2);
            try
            {
                _jpegLiveSource2.Width = width;
                _jpegLiveSource2.Height = height;
                _jpegLiveSource2.LiveModeStart = true;
                _jpegLiveSource2.Init();
                _jpegLiveSource2.LiveContentEvent += new EventHandler(JpegLiveSource2LiveNotificationEvent);
            }
            catch (Exception ex)
            {
                error = "Could not Init:" + ex.Message;
                _jpegLiveSource2 = null;
            }
            Invoke(new Action(() => InitCompleted(error)));
        }

        private void Close1()
		{
			if (_jpegLiveSource!=null)
			{
				_jpegLiveSource.LiveContentEvent += new EventHandler(JpegLiveSource1LiveNotificationEvent);
				_jpegLiveSource.Close();
				_jpegLiveSource = null;
			}

		}

		private void Close2()
		{
			if (_jpegLiveSource2 != null)
			{
				_jpegLiveSource2.LiveContentEvent += new EventHandler(JpegLiveSource2LiveNotificationEvent);
				_jpegLiveSource2.Close();
				_jpegLiveSource2 = null;
			}

		}


        private object SystemConfigChangedEvent(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
        {
            if (userContext1 != null)
            {
                listBox1.Items.Clear();
                FillCameraList(userContext1, listBox1);                
            }
            if (userContext2 != null)
            {
                listBox2.Items.Clear();
                FillCameraList(userContext2, listBox2);
            }
            return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            VideoOS.Platform.SDK.MultiUserEnvironment.ReloadConfiguration(userContext1, userContext1.Configuration.ServerFQID);
            FillCameraList(userContext1, listBox1);
        }

    }
}
