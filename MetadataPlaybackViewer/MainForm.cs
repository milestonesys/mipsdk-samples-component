using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.SDK.UI.LoginDialog;
using VideoOS.Platform.UI;

namespace MetadataPlaybackViewer
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

        private static readonly Guid IntegrationId = new Guid("7D0C0530-FC10-41AA-AA90-BD03CAFE6493");
        private const string IntegrationName = "Metadata Playback Viewer";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";
        private double _speed = 1.0;
        private string _mode = PlaybackPlayModeData.Stop;


        // in UTC
        private DateTime _currentShownTime = DateTime.MinValue;

        private readonly FQID _playbackFQID;
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

            _fetchThread = new Thread(MetadataFetchThread);
            _fetchThread.Start();

            EnvironmentManager.Instance.TraceFunctionCalls = true;
        }

        private void OnClose(object sender, EventArgs e)
        {
            if (_playbackFQID != null)
            {
                ClientControl.Instance.ReleasePlaybackController(_playbackFQID);
            }
            VideoOS.Platform.SDK.Environment.Logout();
            _stop = true;
            Close();
        }


        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _stop = true;
        }


        #endregion

        #region Source selection

        #region Select 'metadata device' via file system - i.e. select SCP or XML for XPCO database

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
                            VideoOS.Platform.SDK.Environment.AddServer(uri, new System.Net.NetworkCredential("", password));
                            try
                            {
                                VideoOS.Platform.SDK.Environment.Login(uri, IntegrationId, IntegrationName, Version, ManufacturerName);
                                VideoOS.Platform.SDK.Environment.LoadConfiguration(uri);

                                _loggedOn = true;
                                buttonMetadataDevice.Enabled = true;
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
                    {
                        return;
                    }
                    _loggedOn = true;
                    buttonMetadataDevice.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("File select", ex);
            }
        }

        #endregion


        #region Select metadata device

        private void OnSelectMetadataDevice(object sender, EventArgs e)
        {
            try
            {
                CloseCurrent();

                var form = new ItemPickerForm { KindFilter = Kind.Metadata, AutoAccept = true };
                form.Init(Configuration.Instance.GetItems(ItemHierarchy.Both));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _newlySelectedItem = form.SelectedItem;
                    buttonMetadataDevice.Text = _newlySelectedItem.Name;
                    timeLineUserControl1.Item = _newlySelectedItem;
                    timeLineUserControl1.SetShowTime(DateTime.Now);
                    timeLineUserControl1.MouseSetTimeEvent += timeLineUserControl1_MouseSetTimeEvent;                    
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Metadata device select", ex);
            }
        }

        void timeLineUserControl1_MouseSetTimeEvent(object sender, EventArgs e)
        {
            Debug.WriteLine("timeLineUserControl1_MouseSetTimeEvent: " + _nextToFetchTime.ToLongTimeString());
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
                                                        new PlaybackCommandData { Command = PlaybackData.PlayStop }), _playbackFQID);
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
                                                        new PlaybackCommandData { Command = PlaybackData.PlayReverse, Speed = _speed }), _playbackFQID);
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
                                                        new PlaybackCommandData { Command = PlaybackData.PlayForward, Speed = _speed }), _playbackFQID);
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
                var time = (DateTime) message.Data;
                Debug.WriteLine("PlaybackTimeChangedHandler: " + time.ToLongTimeString());

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
                Debug.WriteLine("TimeChangedHandler: " + _nextToFetchTime.ToLongTimeString());
            }
        }

        #endregion

        #region Thread to get Metadata

        /// <summary>
        /// All calls to the Media Toolkit is done through the MetadataPlaybackSource within this thread.
        /// </summary>
        private bool _stop;
        private Thread _fetchThread;
        private DateTime _nextToFetchTime = DateTime.MinValue;
        private bool _requestInProgress = false;
        private bool _performCloseMetadataSource = false;

        private void MetadataFetchThread()
        {
            MetadataPlaybackSource metadataSource = null;
            bool errorRecovery = false;

            while (!_stop)
            {
                if (_performCloseMetadataSource)
                {
                    if (metadataSource != null)
                    {
                        metadataSource.Close();
                        metadataSource = null;
                    }
                    _performCloseMetadataSource = false;					
                }

                if (_newlySelectedItem!=null)
                {
                    _selectedItem = _newlySelectedItem;
                    metadataSource = new MetadataPlaybackSource(_selectedItem);
                    try
                    {
                        metadataSource.Init();
                        var metadata = _currentShownTime == DateTime.MinValue ? metadataSource.GetBegin() : metadataSource.GetAtOrBefore(_currentShownTime);
                        if (metadata != null)
                        {
                            _requestInProgress = true;
                            ShowMetadata(metadata);
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
                        metadataSource = null;
                        _newlySelectedItem = _selectedItem;     // Redo the Initialization
                    }
                }

                if (errorRecovery)
                {
                    Thread.Sleep(3000);
                    continue;
                }

                if (_requestInProgress==false && metadataSource!=null && _nextCommand!= MyPlayCommand.None)
                {
                    MetadataPlaybackData metadataPlaybackData = null;
                    try
                    {
                        switch (_nextCommand)
                        {
                            case MyPlayCommand.Start:
                                metadataPlaybackData = metadataSource.GetBegin();
                                break;
                            case MyPlayCommand.NextFrame:
                                metadataPlaybackData = metadataSource.GetNext();
                                break;
                            case MyPlayCommand.NextSequence:
                                metadataPlaybackData = metadataSource.GetNextSequence();
                                break;
                            case MyPlayCommand.PrevFrame:
                                metadataPlaybackData = metadataSource.GetPrevious();
                                break;
                            case MyPlayCommand.PrevSequence:
                                metadataPlaybackData = metadataSource.GetPreviousSequence();
                                break;
                            case MyPlayCommand.End:
                                metadataPlaybackData = metadataSource.GetEnd();
                                break;
                        }
                    } catch (Exception ex)
                    {
                        if (ex is CommunicationMIPException)
                            ShowError("Connection lost to recorder...");
                        else
                            ShowError(ex.Message);
                        errorRecovery = true;
                        metadataSource = null;
                        _newlySelectedItem = _selectedItem;     // Redo the Initialization
                    }
                    if (metadataPlaybackData != null)
                    {
                        _requestInProgress = true;
                        ShowMetadata(metadataPlaybackData);
                    }

                    _nextCommand = MyPlayCommand.None;
                }

                if (_nextToFetchTime != DateTime.MinValue && _requestInProgress==false && metadataSource!=null)
                {
                    bool willResultInSameFrame = false;
                    // Lets validate if we are just asking for the same frame
                    if (_currentTimeInformation != null)
                    {
                        if (_currentTimeInformation.PreviousTime < _nextToFetchTime &&
                            _currentTimeInformation.NextTime > _nextToFetchTime)
                            willResultInSameFrame = true;
                    }
                    if (willResultInSameFrame)
                    {
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
                                new MethodInvoker(delegate { textBoxAsked.Text = localTime.ToString("yyyy-MM-dd HH:mm:ss.fff"); }));

                            var metadata = metadataSource.GetAtOrBefore(utcTime);
                            if (_mode == PlaybackPlayModeData.Reverse)
                            {
                                while (metadata != null && metadata.DateTime > utcTime)
                                    metadata = metadataSource.GetPrevious();
                            }
                            else if (_mode == PlaybackPlayModeData.Forward)
                            {
                                if (metadata != null && metadata.DateTime < utcTime)
                                {
                                    metadata = metadataSource.Get(utcTime);
                                }
                            }
                            if (metadata != null)
                            {
                                _requestInProgress = true;
                                ShowMetadata(metadata);
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
                            metadataSource = null;
                            _newlySelectedItem = _selectedItem;     // Redo the Initialization
                        }
                    }
                }
                Thread.Sleep(5);
            }
            _fetchThread = null;
        }

        #endregion

        #region ShowMetadata

        private delegate void ShowMetadataDelegate(MetadataPlaybackData metadata);

        private void ShowMetadata(MetadataPlaybackData metadata)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ShowMetadataDelegate(ShowMetadata), metadata);
            }
            else
            {
                Debug.WriteLine("ShowMetadata imagetime:" + metadata.DateTime.ToLocalTime());
                if (metadata.DateTime != _currentShownTime && _selectedItem != null)
                {
                    textOutput.Text = metadata.Content.GetMetadataString();
                    textBoxTime.Text = metadata.DateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");

                    // Inform the PlybackController of the time information - so skipping can be done correctly
                    _currentTimeInformation = new PlaybackTimeInformationData
                    {
                                                    Item = _selectedItem.FQID,
                                                    CurrentTime = metadata.DateTime,
                                                    NextTime = metadata.NextDateTime ?? metadata.DateTime.AddMilliseconds(1),
                                                    PreviousTime = metadata.PreviousDateTime ?? metadata.DateTime.AddMilliseconds(-1)
                    };
                    EnvironmentManager.Instance.SendMessage(
                        new VideoOS.Platform.Messaging.Message(MessageId.SmartClient.PlaybackTimeInformation, _currentTimeInformation), _playbackFQID);

                    _currentShownTime = metadata.DateTime;
                    if (_mode == PlaybackPlayModeData.Stop)
                    {
                        // When playback is stopped, we move the time to where the user have scrolled, or if the user pressed 
                        // one of the navigation buttons (Next..., Prev...)
                        EnvironmentManager.Instance.SendMessage(new VideoOS.Platform.Messaging.Message(MessageId.SmartClient.PlaybackCommand,
                                                                new PlaybackCommandData
                                                                {
                                                                        Command = PlaybackData.Goto,
                                                                        DateTime = metadata.DateTime
                                                                    }),
                                                                    _playbackFQID);
                    }
                    Debug.WriteLine("Image time: " + metadata.DateTime.ToLocalTime().ToString("HH.mm.ss.fff") + ", Mode=" + _mode);
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
                textOutput.Text = errorText;
            }
        }


        #endregion

        private void CloseCurrent()
        {
            _performCloseMetadataSource = true;

            buttonStop_Click(null, null);
            
            _selectedItem = null;
            _loggedOn = false;

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
            buttonMetadataDevice.Text = "Select metadata device...";
            buttonMetadataDevice.Enabled = false;

            CloseCurrent();
            VideoOS.Platform.SDK.Environment.RemoveAllServers();	// We can select to remove the previous selection, or just let the list grow

            buttonSelectXPCO.Enabled = radioButton2.Checked;
            buttonSelectServer.Enabled = radioButton3.Checked;
        }
    }
}
