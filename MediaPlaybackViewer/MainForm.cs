using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.SDK.UI.LoginDialog;
using VideoOS.Platform.UI;

namespace MediaPlaybackViewer
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

		// in UTC
		private DateTime _currentShownTime = DateTime.MinValue;

		private FQID _playbackFQID;
		private Guid _deviceId = Guid.NewGuid();
		private Item _selectedItem;
		private Item _newlySelectedItem;

		private PlaybackTimeInformationData _currentTimeInformation;

		private string _selectedStoragePath = "";
		private bool _loggedOn;

		private MyPlayCommand _nextCommand = MyPlayCommand.None;
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

        private static readonly Guid IntegrationId = new Guid("B2797C00-C602-4715-B178-956820366EE6");
        private const string IntegrationName = "Media Playback Viewer";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        #endregion

        #region construction & close

        public MainForm()
		{
			InitializeComponent();

			// In this sample we create a specific PlaybackController.
			// All commands to this controller needs to be sent via messages with the destination as _playbackFQID.
			// All message Indications coming from this controller will have sender as _playbackController.
			_playbackFQID = ClientControl.Instance.GeneratePlaybackController();

			EnvironmentManager.Instance.RegisterReceiver(PlaybackTimeChangedHandler,
											 new MessageIdFilter(MessageId.SmartClient.PlaybackCurrentTimeIndication));

			_fetchThread = new Thread(JPEGFetchThread);
			_fetchThread.Start();

			//EnvironmentManager.Instance.TraceFunctionCalls = true;
		}

		private void OnClose(object sender, EventArgs e)
		{
			if (_jpegVideoSource != null)
			{
				_jpegVideoSource.Close();
				_jpegVideoSource = null;
			}
            if (_playbackFQID != null)
            {
                ClientControl.Instance.ReleasePlaybackController(_playbackFQID);
                _playbackFQID = null;
            }

            VideoOS.Platform.SDK.Environment.Logout();
			_stop = true;
			this.Close();
		}


		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			_stop = true;
		}


        #endregion

        #region Source selection

        #region Select 'export' via file system - i.e. select SCP or XML for XPCO database

        private void SelectXPCOStorageClick(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "XML files (*.XML)|*.XML|SCP files (*.SCP)|*.scp|All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    if (filename.ToLower().EndsWith("cache.xml") ||
                        filename.ToLower().EndsWith("archives_cache.xml") ||
                        filename.ToLower().EndsWith("scp"))
                    {
                        if (filename.ToLower().EndsWith("scp"))
                        {
                            _selectedStoragePath = filename;
                        }
                        else
                        {
                            _selectedStoragePath = Path.GetDirectoryName(filename);
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

                                _loggedOn = true;
                                buttonCamera.Enabled = true;
                                break;
                            }
                            catch (NotAuthorizedMIPException)
                            {
                                PasswordForm form = new PasswordForm();
                                DialogResult result = form.ShowDialog();
                                if (result == DialogResult.Cancel)
                                {
                                    return;
                                }
                                if (result == DialogResult.OK)
                                {
                                    password = form.Password;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Only cache.xml, archives_cache.xml or .SCP files can be selected.");
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("File select", ex);
            }
        }

        #endregion

        #region login to a server

        private void LoginClick(object sender, EventArgs e)
		{
			try
			{
				if (!_loggedOn)
				{
					var loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
					loginForm.ShowDialog();
					if (Connected == false)
						return;
					_loggedOn = true;
					buttonCamera.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("File select", ex);
			}
		}

		#endregion

		#region Select camera

		private void OnSelectCamera(object sender, EventArgs e)
		{
			try
			{
				CloseCurrent();

				ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
				{
					KindsFilter = new List<Guid> { Kind.Camera },
					SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
					Items = Configuration.Instance.GetItems(ItemHierarchy.Both)
				};

                if (itemPicker.ShowDialog().Value)
				{
					_newlySelectedItem = itemPicker.SelectedItems.First();
					buttonCamera.Text = _newlySelectedItem.Name;
					timeLineUserControl1.Item = _newlySelectedItem;
					timeLineUserControl1.SetShowTime(DateTime.Now);
					timeLineUserControl1.MouseSetTimeEvent += new EventHandler(timeLineUserControl1_MouseSetTimeEvent);
					checkBoxAspect.Enabled = false;
					checkBoxFill.Enabled = false;

					var list = _newlySelectedItem.GetDataSource().GetTypes();
					foreach (DataType dt in list)
					{
						Trace.WriteLine("Datasource " + dt.Id + "  " + dt.Name);
                    }
				}
			}
			catch (Exception ex)
			{
				EnvironmentManager.Instance.ExceptionDialog("Camera select", ex);
			}
            buttonCamera.Enabled = true;
        }

		void timeLineUserControl1_MouseSetTimeEvent(object sender, EventArgs e)
		{
			//Debug.WriteLine("timeLineUserControl1_MouseSetTimeEvent: " + _nextToFetchTime.ToLongTimeString());
			DateTime newTime = ((TimeEventArgs)e).Time;
			TimeChangedHandler(newTime);
		}

		private static bool Connected = false;
		private static void SetLoginResult(bool connected)
		{
			Connected = connected;
		}
		#endregion

		#endregion

		#region Playback Click handling

		private void buttonStop_Click(object sender, EventArgs e)
		{
			EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(
														MessageId.SmartClient.PlaybackCommand,
														new PlaybackCommandData() { Command = PlaybackData.PlayStop }), _playbackFQID);
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
														new PlaybackCommandData() { Command = PlaybackData.PlayReverse, Speed = _speed }), _playbackFQID);
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
														new PlaybackCommandData() { Command = PlaybackData.PlayForward, Speed = _speed }), _playbackFQID);
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
			// Only pick up messages coming from my own PlaybackController (sender is null for the common PlaybackController)
			if (_playbackFQID.EqualGuids(sender))
			{
				DateTime time = (DateTime) message.Data;
				//Debug.WriteLine("PlaybackTimeChangedHandler: " + time.ToLongTimeString());

				TimeChangedHandler(time);

				timeLineUserControl1.SetShowTime(time);
			}
			return null;
		}

		private void TimeChangedHandler(DateTime time)
		{
			if (_currentShownTime != time)
			{
				_nextToFetchTime = time;
				//Debug.WriteLine("TimeChangedHandler: " + _nextToFetchTime.ToLongTimeString());
			}
		}

		#endregion

		#region Thread to get jpeg

		/// <summary>
		/// All calls to the Media Toolkit is done through the JPEGVideoSource within this thread.
		/// </summary>
		private bool _stop;
		private Thread _fetchThread;
		private DateTime _nextToFetchTime = DateTime.MinValue;
		private bool _requestInProgress = false;
		private bool _performCloseVideoSource = false;
		private int _newHeight = 0;
		private int _newWidth = 0;
		private bool _setNewResolution = false;

		JPEGVideoSource _jpegVideoSource = null;
		private void JPEGFetchThread()
		{
            bool errorRecovery = false;

			while (!_stop)
			{
				if (_performCloseVideoSource)
				{
					if (_jpegVideoSource != null)
					{
						_jpegVideoSource.Close();
						_jpegVideoSource = null;
					}
					_performCloseVideoSource = false;					
				}

				if (_newlySelectedItem!=null)
				{
					_selectedItem = _newlySelectedItem;
					_jpegVideoSource = new JPEGVideoSource(_selectedItem);
					if (checkBoxAspect.Checked)		
					{
						// Keeping aspect ratio can only work when the Media Toolkit knows the actual displayed area
						_jpegVideoSource.Width = pictureBox.Width;
						_jpegVideoSource.Height = pictureBox.Height;
						_jpegVideoSource.SetKeepAspectRatio(checkBoxAspect.Checked, checkBoxFill.Checked);	// Must be done before Init
					}
                    try
                    {
                        _jpegVideoSource.Init();
                        JPEGData jpegData = _currentShownTime == DateTime.MinValue ? _jpegVideoSource.GetBegin() : _jpegVideoSource.GetAtOrBefore(_currentShownTime) as JPEGData;
                        if (jpegData != null)
                        {
                            _requestInProgress = true;
                            ShowJPEG(jpegData);
                        } else
                        {
                            ShowError("");      // Clear any error messages
                        }
                        _newlySelectedItem = null;
                        errorRecovery = false;
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
                        _jpegVideoSource = null;
                        _newlySelectedItem = _selectedItem;     // Redo the Initialization
                    }
				}

                if (errorRecovery)
                {
                    Thread.Sleep(3000);
                    continue;
                }

			    if (_setNewResolution && _jpegVideoSource!=null && _requestInProgress==false)
				{
                    try
                    {
                        _jpegVideoSource.Width = _newWidth;
                        _jpegVideoSource.Height = _newHeight;
                        _jpegVideoSource.SetWidthHeight();
                        _setNewResolution = false;
                        JPEGData jpegData;
                        jpegData = _jpegVideoSource.GetAtOrBefore(_currentShownTime) as JPEGData;
                        if (jpegData != null)
                        {
                            _requestInProgress = true;
                            _currentShownTime = DateTime.MinValue;
                            ShowJPEG(jpegData);
                        }
                    } 
                    catch (Exception ex)
                    {
                        if (ex is CommunicationMIPException)
                            ShowError("Connection lost to recorder...");
                        else
                            ShowError(ex.Message);
                        errorRecovery = true;
                        _jpegVideoSource = null;
                        _newlySelectedItem = _selectedItem;     // Redo the Initialization
                    }
				}

				if (_requestInProgress==false && _jpegVideoSource!=null && _nextCommand!= MyPlayCommand.None)
				{
					JPEGData jpegData = null;
                    try
                    {
                        switch (_nextCommand)
                        {
                            case MyPlayCommand.Start:
                                jpegData = _jpegVideoSource.GetBegin();
                                break;
                            case MyPlayCommand.NextFrame:
                                jpegData = _jpegVideoSource.GetNext() as JPEGData;
                                break;
                            case MyPlayCommand.NextSequence:
                                jpegData = _jpegVideoSource.GetNextSequence();
                                break;
                            case MyPlayCommand.PrevFrame:
                                jpegData = _jpegVideoSource.GetPrevious();
                                break;
                            case MyPlayCommand.PrevSequence:
                                jpegData = _jpegVideoSource.GetPreviousSequence();
                                break;
                            case MyPlayCommand.End:
                                jpegData = _jpegVideoSource.GetEnd();
                                break;
                        }
                    } catch (Exception ex)
                    {
                        if (ex is CommunicationMIPException)
                            ShowError("Connection lost to recorder...");
                        else
                            ShowError(ex.Message);
                        errorRecovery = true;
                        _jpegVideoSource = null;
                        _newlySelectedItem = _selectedItem;     // Redo the Initialization
                    }
				    if (jpegData != null)
					{
						_requestInProgress = true;
						ShowJPEG(jpegData);
					}

					_nextCommand = MyPlayCommand.None;
				}

				if (_nextToFetchTime != DateTime.MinValue && _requestInProgress==false && _jpegVideoSource!=null)
				{
					bool willResultInSameFrame = false;
					// Lets validate if we are just asking for the same frame
					if (_currentTimeInformation!= null)
					{
						if (_currentTimeInformation.PreviousTime < _nextToFetchTime &&
							_currentTimeInformation.NextTime > _nextToFetchTime)
							willResultInSameFrame = true;
					}
					if (willResultInSameFrame)
					{
						Debug.WriteLine("Now Fetch ignored: " + _nextToFetchTime.ToLongTimeString()+" - nextToFetch="+_nextToFetchTime.ToLongTimeString());
						// Same frame -> Ignore request
						_requestInProgress = false;
						_nextToFetchTime = DateTime.MinValue;
					}
					else
					{
						Debug.WriteLine("Now Fetch: " + _nextToFetchTime.ToLongTimeString());
						DateTime time = _nextToFetchTime;
						_nextToFetchTime = DateTime.MinValue;

                        try
                        {
                            DateTime localTime = time.Kind == DateTimeKind.Local ? time : time.ToLocalTime();
                            DateTime utcTime = time.Kind == DateTimeKind.Local ? time.ToUniversalTime() : time;

                            BeginInvoke(
                                new MethodInvoker(delegate() { textBoxAsked.Text = localTime.ToString("yyyy-MM-dd HH:mm:ss.fff"); }));

                            JPEGData jpegData;                            
                            jpegData = _jpegVideoSource.GetAtOrBefore(utcTime) as JPEGData;
	                        if (jpegData == null && _mode == PlaybackPlayModeData.Stop)
	                        {
								jpegData = _jpegVideoSource.GetNearest(utcTime) as JPEGData;		                        
	                        }

                            if (_mode == PlaybackPlayModeData.Reverse)
                            {
                                while (jpegData != null && jpegData.DateTime > utcTime)
                                    jpegData = _jpegVideoSource.GetPrevious();
                            }
                            else if (_mode == PlaybackPlayModeData.Forward)
                            {
                                if (jpegData != null && jpegData.DateTime < utcTime)
                                {
                                    jpegData = _jpegVideoSource.Get(utcTime) as JPEGData;
                                }
                            }
                            if (jpegData != null)
                            {
                                _requestInProgress = true;
                                ShowJPEG(jpegData);
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
                            _jpegVideoSource = null;
                            _newlySelectedItem = _selectedItem;     // Redo the Initialization
                        }
					}
				}
				Thread.Sleep(5);
			}
			_fetchThread = null;
		}

		#endregion

		#region ShowJPEG

		private delegate void ShowJpegDelegate(JPEGData jpegData);

		private void ShowJPEG(JPEGData jpegData)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new ShowJpegDelegate(ShowJPEG), jpegData);
			}
			else
			{
				Debug.WriteLine("ShowJPEG imagetime:" + jpegData.DateTime.ToLocalTime());
                Trace.WriteLine("ShowJPEG imagetime:" + jpegData.DateTime.ToLocalTime()+", Decoding:"+jpegData.HardwareDecodingStatus);
				if (jpegData.DateTime != _currentShownTime && _selectedItem != null)
				{
					MemoryStream ms = new MemoryStream(jpegData.Bytes);
					Bitmap newBitmap = new Bitmap(ms);
					if (newBitmap.Width != pictureBox.Width || newBitmap.Height != pictureBox.Height)
					{
						pictureBox.Image = new Bitmap(newBitmap, pictureBox.Size);
					}
					else
					{
						pictureBox.Image = newBitmap;
					}

					if (jpegData.CroppingDefined)
					{
						Debug.WriteLine("Image has been cropped: " + jpegData.CropWidth + "x" + jpegData.CropHeight);
					}

					ms.Close();
					ms.Dispose();
					 
					textBoxTime.Text = jpegData.DateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");

					// Inform the PlybackController of the time information - so skipping can be done correctly
					_currentTimeInformation = new PlaybackTimeInformationData()
					                          	{
					                          		Item = _selectedItem.FQID,
					                          		CurrentTime = jpegData.DateTime,
					                          		NextTime = jpegData.NextDateTime,
					                          		PreviousTime = jpegData.PreviousDateTime
					                          	};
					EnvironmentManager.Instance.SendMessage(
						new VideoOS.Platform.Messaging.Message(MessageId.SmartClient.PlaybackTimeInformation, _currentTimeInformation), _playbackFQID);

					_currentShownTime = jpegData.DateTime;
					if (_mode == PlaybackPlayModeData.Stop)
					{
						// When playback is stopped, we move the time to where the user have scrolled, or if the user pressed 
						// one of the navigation buttons (Next..., Prev...)
						EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message( MessageId.SmartClient.PlaybackCommand,
						                                        new PlaybackCommandData()
						                                        	{
																		Command = PlaybackData.Goto, 
																		DateTime = jpegData.DateTime
																	}), 
																	_playbackFQID);
					}
					Debug.WriteLine("Image time: " + jpegData.DateTime.ToLocalTime().ToString("HH.mm.ss.fff") + ", Mode=" + _mode);
				}
				_requestInProgress = false;

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
                Bitmap bitmap = new Bitmap(640, 480);
                Graphics g = Graphics.FromImage(bitmap);
                g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                g.DrawString(errorText, new Font(FontFamily.GenericMonospace, 12), Brushes.White, new PointF(20, 100));
                g.Dispose();
                pictureBox.Image = new Bitmap(bitmap, pictureBox.Size);
                bitmap.Dispose();
            }
        }


	    #endregion

		#region Private methods

		private void CloseCurrent()
		{
			_performCloseVideoSource = true;

			buttonStop_Click(null, null);
			
			pictureBox.Image = new Bitmap(100, 60);
			buttonCamera.Text = "Select camera...";
			buttonCamera.Enabled = false;
			_selectedItem = null;
			_loggedOn = false;
			checkBoxAspect.Enabled = true;
			checkBoxFill.Enabled = true;

			if (timeLineUserControl1 != null)
			{
				timeLineUserControl1.Item = null;
			}
		}

		private void OnResize(object sender, EventArgs e)
		{
			Collection<object> result = EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.SmartClient.GetCurrentPlaybackTimeRequest), _playbackFQID);
			if (result != null && result[0] is DateTime)
			{
				timeLineUserControl1.SetShowTime((DateTime)result[0]);
			}
		}

		private void SourceChanged(object sender, EventArgs e) 
		{
			CloseCurrent();
			VideoOS.Platform.SDK.Environment.RemoveAllServers();	// We can select to remove the previous selection, or just let the list grow

			buttonSelectXPCO.Enabled = radioButton1.Checked;
			buttonSelectServer.Enabled = radioButton2.Checked;
		}
		#endregion

		private void OnReSizePictureBox(object sender, EventArgs e)
		{
			if (checkBoxAspect.Checked)
			{
				// Keeping aspect ratio can only work when the Media Toolkit knows the actual displayed area
				_newWidth = pictureBox.Width;
				_newHeight = pictureBox.Height;
				_setNewResolution = true;
			}

		}
	}
}
