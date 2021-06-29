using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.SDK;
using VideoOS.Platform.SDK.UI.LoginDialog;
using VideoOS.Platform.UI;
using BitmapData = VideoOS.Platform.Data.BitmapData;

namespace MediaRGBEnhancementPlayback
{
    /// <summary>
    /// This sample has been changed between MIPSDK 3.6 and 4.0:
    /// - All Initialization and Close is now done in one thread.
    /// 
    /// This assist in re-Initializing when a session was lost and a new is tried every 3 seconds.
    /// 
    /// This sample will now support if a recorder is unavailable for a few seconds / minutes, or
    /// a failover of a recorder has happened.
    /// </summary>
	public partial class MainForm : Form
	{
		#region private fields

		private double _speed = 1.0;
		private string _mode = PlaybackPlayModeData.Stop;
		private DateTime _currentShownTime = DateTime.MinValue;

        //Update 2016
        //private SDKPlaybackController _sdkPlaybackController = new SDKPlaybackController();

		private Guid _deviceId = Guid.NewGuid();
		private Item _selectedItem;
	    private Item _newlySelectedItem;
		private bool _redisplay;
        private string _selectedStoragePath;

        private ToolkitRGBEnhancement.RGBHandling.Transform transform;

		private MyPlayCommand _nextCommand = MyPlayCommand.None;
		private PlaybackTimeInformationData _currentTimeInformation = null;

        private static readonly Guid IntegrationId = new Guid("E2C4102C-E1D2-404A-912A-9855BF8F42C5");
        private const string IntegrationName = "Media RGB Enhancement Playback";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        enum MyPlayCommand
		{
			None,
			Start,
			End,
			NextSequence,
			PrevSequence,
			NextFrame,
			PrevFrame
		}
		#endregion

		#region constructor

		public MainForm()
		{
			InitializeComponent();

			EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler,
											 new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));

			transform = new ToolkitRGBEnhancement.RGBHandling.Transform();

			_fetchThread = new Thread(BitmapFetchThread);
			_fetchThread.Start();

			OnScrollChange(null, null);

			groupBoxPlayback.Enabled = false;
		}

		private void OnClose(object sender, EventArgs e)
		{
			_stop = true;
			this.Close();
		}


		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			_stop = true;
		}


		#endregion

		#region Source selection

		#region Select 'camera' via file system - e.g. select a PQZ file

		private void OnOpenMediaFiles(object sender, EventArgs e)
		{
            try
            {
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                {

                    return;
                }

                _selectedStoragePath = folderBrowserDialog1.SelectedPath;
                if (!File.Exists(Path.Combine(_selectedStoragePath, "cache.xml")) &&
                    !File.Exists(Path.Combine(_selectedStoragePath, "archives_cache.xml")))
                {
                    MessageBox.Show("No cache.xml or archives_cache.xml file were found in selected folder.");
                    return;
                }

                Uri uri = new Uri("file:\\" + _selectedStoragePath);
                string password = "";
                while (true)
                {
                    VideoOS.Platform.SDK.Environment.RemoveAllServers();
                    VideoOS.Platform.SDK.Environment.AddServer(false, uri, new System.Net.NetworkCredential("", password));
                    try
                    {
                        VideoOS.Platform.SDK.Environment.Login(uri, IntegrationId, IntegrationName, Version, ManufacturerName);
                        VideoOS.Platform.SDK.Environment.LoadConfiguration(uri);
                        buttonCameraPick.Enabled = true;
                        break;
                    }
                    catch (NotAuthorizedMIPException)
                    {
                        PasswordForm form = new PasswordForm();
                        DialogResult result = form.ShowDialog();
                        if (result == DialogResult.Cancel)
                            return;
                        if (result == DialogResult.OK)
                            password = form.Password;
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("OnOpenMediaFiles exception", ex);
            }

        }
        #endregion

        #region Select camera from a VMS system

        private void OnSelectCamera(object sender, EventArgs e)
		{
			try
			{
                _performCloseVideoSource = true;

				ItemPickerForm form = new ItemPickerForm();
				form.KindFilter = Kind.Camera;
				form.AutoAccept = true;
				form.Init(Configuration.Instance.GetItems());
				if (form.ShowDialog() == DialogResult.OK)
				{
                    _newlySelectedItem = form.SelectedItem;
                    buttonCameraPick.Text = _newlySelectedItem.Name;
				}
			}
			catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("Camera select", ex);
			}
		}

		private static bool Connected = false;
		private static void SetLoginResult(bool connected)
		{
			Connected = connected;
		}
        #endregion

        private void OnLogin(object sender, EventArgs e)
        {
            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            loginForm.ShowDialog();
            if (Connected == false)
                return;
            //_loggedOn = true;
            buttonCameraPick.Enabled = true;
        }
		#endregion

		#region Playback Click handling

		private void buttonStop_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PlayStop }));
			EnvironmentManager.Instance.Mode = Mode.ClientPlayback;
			_speed = 0.0;
			_mode = PlaybackPlayModeData.Stop;
		}

		private void buttonReverse_Click(object sender, EventArgs e)
		{
			if (_speed == 0.0)
				_speed = 1.0;
			else
				_speed *= 2;
			_mode = PlaybackPlayModeData.Reverse;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed }));
		}

		private void buttonForward_Click(object sender, EventArgs e)
		{
			if (_speed == 0.0)
				_speed = 1.0;
			else
				_speed *= 2;
			_mode = PlaybackPlayModeData.Forward;
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed }));
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			_nextCommand = MyPlayCommand.Start;
		}

		private void buttonEnd_Click(object sender, EventArgs e)
		{
			_nextCommand = MyPlayCommand.End;
		}

		private void OnPrevSequence(object sender, EventArgs e)
		{
			_nextCommand = MyPlayCommand.PrevSequence;
		}

		private void OnNextSequence(object sender, EventArgs e)
		{
			_nextCommand = MyPlayCommand.NextSequence;
		}

		private void OnPreviousFrame(object sender, EventArgs e)
		{
			_nextCommand = MyPlayCommand.PrevFrame;
		}

		private void OnNextFrame(object sender, EventArgs e)
		{
			_nextCommand = MyPlayCommand.NextFrame;
		}

		#endregion

		#region Time changed event handler

		private object PlaybackTimeChangedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
		{
			DateTime time = (DateTime) message.Data;

			TimeChangedHandler(time);

			return null;
		}

		private void TimeChangedHandler(DateTime time)
		{
			_nextToFetchTime = time;
		}

		#endregion

		#region Thread to get images

		private bool _stop;
		private Thread _fetchThread;
		private DateTime _nextToFetchTime;
        private bool _performCloseVideoSource = false;
        private bool _performReSize = false;
	    private int _reSizeWidth = 0;
	    private int _reSizeHeight = 0;

		// Next line new property for MIPSDK 2014
		private bool _requestInProgress = false;

		private void BitmapFetchThread()
		{
            bool errorRecovery = false;
		    BitmapVideoSource _bitmapVideoSource = null;

			while (!_stop)
			{
                if (_performCloseVideoSource)
                {
                    if (_bitmapVideoSource != null)
                    {
                        _bitmapVideoSource.Close();
                        _bitmapVideoSource = null;
                    }
                    _performCloseVideoSource = false;
                }

                if (_newlySelectedItem != null)
                {
                    _selectedItem = _newlySelectedItem;
                    _bitmapVideoSource = new BitmapVideoSource(_selectedItem);
                    _bitmapVideoSource.Width = pictureBox.Width;
                    _bitmapVideoSource.Height = pictureBox.Height;
                    _bitmapVideoSource.SetKeepAspectRatio(true, true);

                    try
                    {

                        _bitmapVideoSource.Init();

                        BitmapData bitmapData = _bitmapVideoSource.GetAtOrBefore(DateTime.Now) as BitmapData;
                        if (bitmapData != null)
                        {
							ShowBitmap(bitmapData);
                        }
                        else
                        {
                            ShowError("");      // Clear any error messages
                        }
                        _newlySelectedItem = null;
                        errorRecovery = false;

						BeginInvoke( new MethodInvoker(delegate() {  groupBoxPlayback.Enabled = true;}));
                        
                    } catch (Exception ex)
                    {
                        if (ex is CommunicationMIPException)
                        {
                            ShowError("Connection lost to server ...");
                        }
                        else
                        {
                            ShowError(ex.Message);
                        }
                        errorRecovery = true;
                        _bitmapVideoSource = null;
                        _newlySelectedItem = _selectedItem;     // Redo the Initialization
                    }
                }

                if (errorRecovery || _bitmapVideoSource==null)
                {
                    if (this.IsHandleCreated)
                        BeginInvoke(new MethodInvoker(delegate() { groupBoxPlayback.Enabled = false; }));
                    if (_mode != PlaybackData.PlayStop)
                    {
                        buttonStop_Click(null, null);
                    }
                    Thread.Sleep(3000);
                    continue;
                }

                if (_performReSize)
                {
                    _bitmapVideoSource.Width = _reSizeWidth;
                    _bitmapVideoSource.Height = _reSizeHeight;
    				_bitmapVideoSource.SetWidthHeight();
	    			if (_mode == PlaybackPlayModeData.Stop)
		    		{
			    		_nextToFetchTime = _currentShownTime;
				    	_redisplay = true;
				    }
                }

				if (_nextCommand != MyPlayCommand.None && _requestInProgress == false)
				{
                    try
                    {
                        BitmapData bitmapData = null;
                        switch (_nextCommand)
                        {
                            case MyPlayCommand.Start:
                                bitmapData = _bitmapVideoSource.GetBegin();
                                break;
                            case MyPlayCommand.End:
                                bitmapData = _bitmapVideoSource.GetEnd();
                                break;
                            case MyPlayCommand.PrevSequence:
                                bitmapData = _bitmapVideoSource.GetPreviousSequence();
                                break;
                            case MyPlayCommand.NextSequence:
                                bitmapData = _bitmapVideoSource.GetNextSequence();
                                break;
                            case MyPlayCommand.PrevFrame:
                                bitmapData = _bitmapVideoSource.GetPrevious();
                                break;
                            case MyPlayCommand.NextFrame:
                                bitmapData = _bitmapVideoSource.GetNext() as BitmapData;
                                break;
                        }
                        if (bitmapData != null)
                        {
                            ShowBitmap(bitmapData);
                            bitmapData.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is CommunicationMIPException)
                        {
                            ShowError("Connection lost to server ...");
                        }
                        else
                        {
                            ShowError(ex.Message);
                        }
                        errorRecovery = true;
                        _bitmapVideoSource = null;
                        _newlySelectedItem = _selectedItem;     // Redo the Initialization
						_nextCommand = MyPlayCommand.None;
						Thread.Sleep(3000);
						continue;
                    }
				    _nextCommand = MyPlayCommand.None;
				}

				if (_nextToFetchTime != DateTime.MinValue && _requestInProgress == false)
				{
                    Debug.WriteLine("NextToFetch = "+_nextToFetchTime.ToString());

					DateTime time = _nextToFetchTime;
					DateTime localTime = time.Kind == DateTimeKind.Local ? time : time.ToLocalTime();
					DateTime utcTime = time.Kind == DateTimeKind.Local ? time.ToUniversalTime() : time;

					// Next 10 lines new for MIPSDK 2014
					bool willResultInSameFrame = false;
					// Lets validate if we are just asking for the same frame
					if (_currentTimeInformation != null)
					{
						if (_mode == PlaybackPlayModeData.Forward &&
							_currentTimeInformation.NextTime > utcTime)
							willResultInSameFrame = true;
						if (_mode == PlaybackPlayModeData.Reverse &&
							_currentTimeInformation.PreviousTime < utcTime)
							willResultInSameFrame = true;
					}
					_nextToFetchTime = DateTime.MinValue;

					if (willResultInSameFrame)
					{
						// Same frame -> Ignore request
					}
					else
					{
						try
						{
							BeginInvoke(
								new MethodInvoker(delegate() { textBoxAsked.Text = localTime.ToString("yyyy-MM-dd HH:mm:ss.fff"); }));

							BitmapData bitmapData = null;
							switch (_mode)
							{
								case PlaybackPlayModeData.Stop:
									bitmapData = _bitmapVideoSource.GetAtOrBefore(utcTime) as BitmapData;

									// Next 2 lines new for MIPSDK 2014
									if (bitmapData != null && bitmapData.IsPreviousAvailable == false)
										bitmapData.PreviousDateTime = bitmapData.DateTime - TimeSpan.FromMilliseconds(10);
									break;
								case PlaybackPlayModeData.Forward:
									_bitmapVideoSource.GoTo(utcTime, _mode);
									bitmapData = _bitmapVideoSource.GetNext() as BitmapData;

									// Next 2 lines new for MIPSDK 2014
									if (bitmapData != null && bitmapData.IsPreviousAvailable == false)
										if (utcTime - TimeSpan.FromMilliseconds(10) < bitmapData.DateTime)
											bitmapData.PreviousDateTime = utcTime - TimeSpan.FromMilliseconds(10);
										else
											bitmapData.PreviousDateTime = bitmapData.DateTime;
									break;
								case PlaybackPlayModeData.Reverse:
									_bitmapVideoSource.GoTo(utcTime, _mode);
									bitmapData = _bitmapVideoSource.GetPrevious();
									// Next 2 lines new for MIPSDK 2014
									if (bitmapData != null && bitmapData.IsPreviousAvailable == false)
										bitmapData.PreviousDateTime = bitmapData.DateTime - TimeSpan.FromMilliseconds(10);
									break;
							}

							if (bitmapData != null)
							{
								ShowBitmap(bitmapData);
								bitmapData.Dispose();
							}
						}
						catch (Exception ex)
						{
							if (ex is CommunicationMIPException)
							{
								ShowError("Connection lost to server ...");
							}
							else
							{
								ShowError(ex.Message);
							}
							errorRecovery = true;
							_bitmapVideoSource = null;
							_nextCommand = MyPlayCommand.None;
							_newlySelectedItem = _selectedItem; // Redo the Initialization
						}
					}
				}
			}
			_fetchThread = null;
		}

		#endregion

		#region ShowBitmap

		private delegate void ShowBitmapDelegate(BitmapData bitmapData);

		// The ShowBitmap now devided into 2 methods, for MIPSDK 2014
		private void ShowBitmap(BitmapData bitmapData)
	    {
			// Next 15 lines new for MIPSDK 2014
			if (_currentTimeInformation != null &&
		        _currentTimeInformation.PreviousTime < bitmapData.DateTime &&
		        _currentTimeInformation.NextTime > bitmapData.DateTime)
		    {
			    Debug.WriteLine("----> Duplicate bitmap at " + bitmapData.DateTime);	// this should only happen a few times during startup
			    return;
		    }
		    _currentTimeInformation = new PlaybackTimeInformationData()
		    {
			    Item = _selectedItem.FQID,
			    CurrentTime = bitmapData.DateTime,
			    NextTime = bitmapData.NextDateTime,
			    PreviousTime = bitmapData.PreviousDateTime
		    };

			_requestInProgress = true;
			if (InvokeRequired)
		    {
				Invoke(new ShowBitmapDelegate(ShowBitmap2), bitmapData);			    
		    }
		    else
		    {
				ShowBitmap2(bitmapData);
		    }
	    }

	    private void ShowBitmap2(BitmapData bitmapData)
		{
			{
				if (bitmapData.DateTime != _currentShownTime || _redisplay)
				{
					_redisplay = false;

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

					int width = bitmapData.GetPlaneWidth(0);
					int height = bitmapData.GetPlaneHeight(0);
					int stride = bitmapData.GetPlaneStride(0);

					// When using RGB / BGR bitmaps, they have all bytes continues in memory.  The PlanePointer(0) is used for all planes:
					IntPtr plane0 = bitmapData.GetPlanePointer(0);

					IntPtr newPlane0 = transform.Perform(plane0, stride, width,  height);		// Make the sample transformation / color change

					Image myImage = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, newPlane0);

					// We need to resize to the displayed area
					pictureBox.Image = new Bitmap(myImage, pictureBox.Width, pictureBox.Height);

					myImage.Dispose();
					// ---- bitmapData.Dispose();
					transform.Release(newPlane0);

					textBoxTime.Text = bitmapData.DateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");

					// Inform the PlybackController of the time information - so skipping can be done correctly

                    //Update 2016
                    EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.SmartClient.PlaybackTimeInformation,
                            new PlaybackTimeInformationData() { Item = _selectedItem.FQID, CurrentTime = bitmapData.DateTime, PreviousTime = bitmapData.PreviousDateTime, NextTime = bitmapData.NextDateTime}));
                    
					if (_mode == PlaybackPlayModeData.Stop)
					{
                        //Update 2016
					    EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.SmartClient.PlaybackCommand,
					        new PlaybackCommandData() {Command = PlaybackData.Goto, DateTime = bitmapData.DateTime}));
					}
					_currentShownTime = bitmapData.DateTime;
					Debug.WriteLine("Image time: " + bitmapData.DateTime.ToLocalTime().ToString("HH.mm.ss.fff") + ", Mode=" + _mode);
				}
				_requestInProgress = false;
			}
		}

		#endregion

		private void OnResize(object sender, EventArgs e)
		{
            if (pictureBox.Width != 0)
            {
                _reSizeWidth = pictureBox.Width;
                _reSizeHeight = pictureBox.Height;
                _performReSize = true;
            }
		}

		private void OnScrollChange(object sender, ScrollEventArgs e)
		{
			transform.SetVectors(vScrollBarR.Value * hScrollBarExpose.Value, vScrollBarG.Value * hScrollBarExpose.Value, vScrollBarB.Value * hScrollBarExpose.Value, hScrollBarOffset.Value);
			if (_mode == PlaybackPlayModeData.Stop)
			{
				_nextToFetchTime = _currentShownTime;
                _currentTimeInformation = null;
                _redisplay = true;
			}
		}

        /// <summary>
        /// New code as from MIPSDK 4.0 - to handle connection issues
        /// </summary>
        /// <param name="errorText"></param>
        private delegate void ShowErrorDelegate(String errorText);
        private void ShowError(String errorText)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ShowErrorDelegate(ShowError), errorText);
            }
            else
            {
                Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                g.DrawString(errorText, new Font(FontFamily.GenericMonospace, 12), Brushes.White, new PointF(20, 100));
                g.Dispose();
                pictureBox.Image = new Bitmap(bitmap, pictureBox.Size);
                bitmap.Dispose();
            }
        }
    }
}
