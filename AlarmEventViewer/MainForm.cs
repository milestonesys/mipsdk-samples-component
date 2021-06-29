using System;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.Proxy.Alarm;
using VideoOS.Platform.Proxy.AlarmClient;

namespace AlarmEventViewer
{
    public partial class MainForm : Form
    {
        private object _obj1, _obj2, _obj3;
        DataGridViewRow _selectedRow = null;
        private AlarmClientManager _alarmClientManager;
        private MessageCommunication _messageCommunication;
        private enum ViewMode { Alarm, Event, Analytics, LPR, Access }
        private ViewMode _viewMode;

        public MainForm()
        {
            InitializeComponent();
            _viewMode = ViewMode.Alarm;
            MessageCommunicationManager.Start(EnvironmentManager.Instance.MasterSite.ServerId);
            _messageCommunication = MessageCommunicationManager.Get(EnvironmentManager.Instance.MasterSite.ServerId);

            _alarmClientManager = new AlarmClientManager();

            rbAlarms.Tag = ViewMode.Alarm;
            rbAnalytics.Tag = ViewMode.Analytics;
            rbEvents.Tag = ViewMode.Event;
            rbLPR.Tag = ViewMode.LPR;
            rbAccess.Tag = ViewMode.Access;

            setGrid();
        }


        private void OnLoad(object sender, EventArgs e)
        {
            LoadClient();
        }

        private void OnClose(object sender, EventArgs e)
        {
            unsubscribeAlarms();
            unsubscribeEvents();
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        #region grid handling
        private void setGrid()
        {
            dataGridViewAlarm.Rows.Clear();
            dataGridViewAlarm.Columns.Clear();
            switch (_viewMode)
            {
                case ViewMode.Alarm:
                    dataGridViewAlarm.Columns.AddRange(new DataGridViewTextBoxColumn[]
			                                   	{
			                                   		new DataGridViewTextBoxColumn() {HeaderText = "Source", Width=200},
			                                   		new DataGridViewTextBoxColumn() {HeaderText = "Time", Width=100},
			                                   		new DataGridViewTextBoxColumn() {HeaderText = "Message", Width=200},
			                                   		new DataGridViewTextBoxColumn() {HeaderText = "Priority", Width=50},
			                                   		new DataGridViewTextBoxColumn() {HeaderText = "State",Width=50},
			                                   		new DataGridViewTextBoxColumn() {HeaderText = "Alarm Definition",Width=200}
			                                   	});
                    break;
                case ViewMode.Analytics:
                    dataGridViewAlarm.Columns.AddRange(new DataGridViewTextBoxColumn[]
                                                {
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Type", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Object", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Message", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Tag", Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "time", Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Source",Width=50},
                                                });
                    break;
                case ViewMode.Event:
                    dataGridViewAlarm.Columns.AddRange(new DataGridViewTextBoxColumn[]
                                                {
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Type", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Message", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "time", Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Source",Width=50},
                                                });
                    break;
                case ViewMode.Access:
                    dataGridViewAlarm.Columns.AddRange(new DataGridViewTextBoxColumn[]
                                                {
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Type", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Message", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "time", Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Source",Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Credential Holder",Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Access System",Width=50},
                                                });
                    break;
                case ViewMode.LPR:
                    dataGridViewAlarm.Columns.AddRange(new DataGridViewTextBoxColumn[]
                                                {
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Type", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Detection", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Confidence", Width=200},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Country", Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "time", Width=50},
                                                    new DataGridViewTextBoxColumn() {HeaderText = "Source",Width=50},
                                                });
                    break;
            }
        }

        private void onModeChange(object sender, EventArgs e)
        {
            RadioButton senderRB = sender as RadioButton;

            if (senderRB != null && senderRB.Checked)
            {
                _viewMode = (ViewMode)senderRB.Tag;
                if (_viewMode != ViewMode.Alarm)
                {
                    buttonCompleted.Enabled = false;
                    buttonInprogress.Enabled = false;
                }

                setGrid();
                LoadClient();
            }
        }
        #endregion

        #region load client
        private void LoadClient()
        {
            switch (_viewMode)
            {
                case ViewMode.Alarm:
                    unsubscribeEvents();
                    LoadClientAlarm();
                    subscribeAlarms();
                    break;
                case ViewMode.Analytics:
                    unsubscribeAlarms();
                    loadClientAnalytics();
                    subscribeEvents();
                    break;
                case ViewMode.Event:
                    unsubscribeAlarms();
                    loadClientEvent();
                    subscribeEvents();
                    break;
                case ViewMode.LPR:
                    unsubscribeAlarms();
                    loadClientLPR();
                    subscribeEvents();
                    break;
                case ViewMode.Access:
                    unsubscribeAlarms();
                    loadClientAccess();
                    subscribeEvents();
                    break;
            }
        }
        private void loadClientLPR()
        {
            try
            {
                IAlarmClient alarmClient = _alarmClientManager.GetAlarmClient(EnvironmentManager.Instance.MasterSite.ServerId);

                EventLine[] events = alarmClient.GetEventLines(0, 10,
                    new VideoOS.Platform.Proxy.Alarm.EventFilter()
                    {
                        Conditions = new Condition[] { new Condition() { Operator = Operator.Equals, Target = Target.Type, Value = "LPR Event" } }
                    });


                foreach (EventLine line in events)
                {
                    BaseEvent baseevent = alarmClient.GetEvent(line.Id);
                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = baseevent;
                    AnalyticsEvent aEvent = baseevent as AnalyticsEvent;
                    if (aEvent != null)
                    {
                        string oValue = "";
                        string oConfidence = "";
                        if ( aEvent.ObjectList!=null && aEvent.ObjectList.Count > 0)
                        {
                            oValue = aEvent.ObjectList[0].Value ?? "";
                            oConfidence = aEvent.ObjectList[0].Confidence.ToString("F5");
                        }
                        row.CreateCells(dataGridViewAlarm, aEvent.EventHeader.Type, oValue, oConfidence, aEvent.EventHeader.CustomTag, aEvent.EventHeader.Timestamp.ToLocalTime(), aEvent.EventHeader.Source.Name);
                        dataGridViewAlarm.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("loading LPR Analytics", ex);
            }
        }
        private void loadClientEvent()
        {
            try
            {
                IAlarmClient alarmClient = _alarmClientManager.GetAlarmClient(EnvironmentManager.Instance.MasterSite.ServerId);

                EventLine[] events = alarmClient.GetEventLines(0, 10,
                    new VideoOS.Platform.Proxy.Alarm.EventFilter()
                    {

                    });


                foreach (EventLine line in events)
                {
                    BaseEvent baseevent = alarmClient.GetEvent(line.Id);
                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = baseevent;
                    row.CreateCells(dataGridViewAlarm, baseevent.EventHeader.Type, baseevent.EventHeader.Message, line.Timestamp.ToLocalTime(), line.SourceName);
                    dataGridViewAlarm.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("loading Event", ex);
            }
        }

        private void loadClientAccess()
        {
            try
            {
                IAlarmClient alarmClient = _alarmClientManager.GetAlarmClient(EnvironmentManager.Instance.MasterSite.ServerId);

                EventLine[] events = alarmClient.GetEventLines(0, 10,
                    new VideoOS.Platform.Proxy.Alarm.EventFilter()
                    {
                        Conditions = new Condition[] { new Condition() { Operator = Operator.Equals, Target = Target.Type, Value = "Access Control System Event" } }
                    });


                foreach (EventLine line in events)
                {
                    BaseEvent baseevent = alarmClient.GetEvent(line.Id);
                    AccessControlEvent  accessControlEvent = baseevent as AccessControlEvent;
                    if (accessControlEvent != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.Tag = baseevent;
                        string credentialHolderId = "";
                        string systemId = "";
                        if (accessControlEvent.RelatedAccessControlCredentialHolderIds != null)
                        {
                            if (accessControlEvent.RelatedAccessControlCredentialHolderIds.Length > 0)
                            {
                                credentialHolderId = accessControlEvent.RelatedAccessControlCredentialHolderIds[0];
                            }
                        }
                        if(accessControlEvent.AccessControlSystemId!=null)
                        {
                            systemId = accessControlEvent.AccessControlSystemId.ToString();
                        }
                        row.CreateCells(
                            dataGridViewAlarm,
                            baseevent.EventHeader.Type,
                            baseevent.EventHeader.Message,
                            line.Timestamp.ToLocalTime(),
                            line.SourceName,
                            credentialHolderId,
                            systemId);
                        dataGridViewAlarm.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("loading Acces Control System Event", ex);
            }
        }
        private void loadClientAnalytics()
        {
            try
            {
                IAlarmClient alarmClient = _alarmClientManager.GetAlarmClient(EnvironmentManager.Instance.MasterSite.ServerId);

                EventLine[] events = alarmClient.GetEventLines(0, 10,
                    new VideoOS.Platform.Proxy.Alarm.EventFilter()
                    {
                        Conditions = new Condition[] { new Condition() { Operator = Operator.NotEquals, Target = Target.Type, Value = "System Event" } }
                    });


                foreach (EventLine line in events)
                {
                    BaseEvent baseevent = alarmClient.GetEvent(line.Id);
                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = baseevent;

                    AnalyticsEvent aEvent = baseevent as AnalyticsEvent;
                    if (aEvent != null)
                    {
                        row.CreateCells(
                            dataGridViewAlarm,
                            baseevent.EventHeader.Type,
                            line.ObjectValue,
                            baseevent.EventHeader.Message,
                            line.CustomTag,
                            line.Timestamp.ToLocalTime(),
                            line.SourceName);
                        dataGridViewAlarm.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("loading Analytics", ex);
            }
        }


        private void LoadClientAlarm()
        {
            try
            {
                IAlarmClient alarmClient = _alarmClientManager.GetAlarmClient(EnvironmentManager.Instance.MasterSite.ServerId);
                AlarmLine[] alarms = alarmClient.GetAlarmLines(0, 10, new VideoOS.Platform.Proxy.Alarm.AlarmFilter()
                {
                    Orders = new OrderBy[] { new OrderBy() { Order = Order.Descending, Target = Target.Timestamp } }
                });

                foreach (AlarmLine line in alarms)
                {
                    Alarm alarm = alarmClient.Get(line.Id);
                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = alarm;
                    string alarmDef = alarm.RuleList != null && alarm.RuleList.Count > 0 ? alarm.RuleList[0].Name : "";
                    row.CreateCells(dataGridViewAlarm, alarm.EventHeader.Source.Name, alarm.EventHeader.Timestamp.ToLocalTime(),
                                    alarm.EventHeader.Message, alarm.EventHeader.Priority, alarm.State, alarmDef);
                    dataGridViewAlarm.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("OnLoad", ex);
            }
        }
        #endregion

        #region message handlers
        private void subscribeAlarms()
        {
            //System.Threading.Thread.Sleep(60000);
            if (_obj2 == null)
            {
                _obj2 = _messageCommunication.RegisterCommunicationFilter(ChangedAlarmMessageHandler,
                 new VideoOS.Platform.Messaging.CommunicationIdFilter(VideoOS.Platform.Messaging.MessageId.Server.ChangedAlarmIndication), null,EndPointType.Server);
            }
            //System.Threading.Thread.Sleep(60000);
            if (_obj1 == null)
            {
                _obj1 = _messageCommunication.RegisterCommunicationFilter(NewAlarmMessageHandler,
                   new VideoOS.Platform.Messaging.CommunicationIdFilter(VideoOS.Platform.Messaging.MessageId.Server.NewAlarmIndication), null, EndPointType.Server);
            }
        }
        private void unsubscribeAlarms()
        {
            if (_obj1 != null)
            {
                _messageCommunication.UnRegisterCommunicationFilter(_obj1);
                _obj1 = null;
            }
            if (_obj2 != null)
            {
                _messageCommunication.UnRegisterCommunicationFilter(_obj2);
                _obj2 = null;
            }
        }
        private void subscribeEvents()
        {
            //System.Threading.Thread.Sleep(60000);
            if (_obj3 == null)
            {
                _obj3 = _messageCommunication.RegisterCommunicationFilter(NewEventMessageHandler,
                    new VideoOS.Platform.Messaging.CommunicationIdFilter(VideoOS.Platform.Messaging.MessageId.Server.NewEventsIndication), null, EndPointType.Server);
            }
        }
        private void unsubscribeEvents()
        {
            if (_obj3 != null)
            {
                _messageCommunication.UnRegisterCommunicationFilter(_obj3);
                _obj3 = null;
            }
        }

        private object NewAlarmMessageHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            if (_viewMode != ViewMode.Alarm)
            {
                return null;
            }

            if (InvokeRequired)
            {
                Invoke(new MessageReceiver(NewAlarmMessageHandler), message, dest, source);
            }
            else
            {
                Alarm alarm = message.Data as Alarm;
                if (alarm != null)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = alarm;
                    string alarmDef = alarm.RuleList != null && alarm.RuleList.Count > 0 ? alarm.RuleList[0].Name : "";
                    row.CreateCells(dataGridViewAlarm, alarm.EventHeader.Source.Name, alarm.EventHeader.Timestamp.ToLocalTime(),
                                    alarm.EventHeader.Message, alarm.EventHeader.Priority, alarm.State, alarmDef);
                    dataGridViewAlarm.Rows.Insert(0, row);
                }
            }
            return null;
        }

        private object ChangedAlarmMessageHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            if (_viewMode != ViewMode.Alarm)
                return null;

            if (InvokeRequired)
            {
                BeginInvoke(new MessageReceiver(ChangedAlarmMessageHandler), message, dest, source);
            }
            else
            {
                try
                {
                    ChangedAlarmData changedAlarmData = message.Data as ChangedAlarmData;
                    if (changedAlarmData != null)
                    {
                        switch (changedAlarmData.ChangeHint)
                        {
                            case ChangedAlarmData.ChangeHintSingleAlarm:
                                for (int ix = 0; ix < dataGridViewAlarm.Rows.Count; ix++)
                                {
                                    Alarm alarm = dataGridViewAlarm.Rows[ix].Tag as Alarm;
                                    if (alarm != null && alarm.EventHeader.ID == changedAlarmData.AlarmId)
                                    {
                                        IAlarmClient alarmClient = LookupAlarmClient(alarm.EventHeader.Source.FQID);
                                        Alarm newAlarm = alarmClient.Get(alarm.EventHeader.ID);
                                        dataGridViewAlarm.Rows[ix].Tag = newAlarm;
                                        dataGridViewAlarm.Rows[ix].Cells[3].Value = "" + newAlarm.EventHeader.Priority;
                                        dataGridViewAlarm.Rows[ix].Cells[4].Value = "" + newAlarm.State;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    EnvironmentManager.Instance.ExceptionDialog("MessageHandler", ex);
                }
            }
            return null;
        }

        private object NewEventMessageHandler(VideoOS.Platform.Messaging.Message message, FQID destination, FQID sender)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageReceiver(NewEventMessageHandler), message, destination, sender);
            }
            else
            {
                DataGridViewRow row = new DataGridViewRow();
                var events = message.Data as System.Collections.Generic.IEnumerable<BaseEvent>;
                if (events == null) return null;
                foreach (BaseEvent ev in events)
                {
                    switch (_viewMode)
                    {
                        case ViewMode.Analytics:
                            {
                                AnalyticsEvent analyticsevent = ev as AnalyticsEvent;
                                if (analyticsevent != null)
                                {
                                    row.Tag = analyticsevent;
                                    String strObjectValue = "";
                                    if (analyticsevent.ObjectList != null && analyticsevent.ObjectList.Count > 0)
                                    {
                                        strObjectValue = (analyticsevent.ObjectList[0].Name ?? "") + "=" + (analyticsevent.ObjectList[0].Value ?? "");
                                    }
                                    row.CreateCells(dataGridViewAlarm,
                                        analyticsevent.EventHeader.Type,
                                        strObjectValue,
                                        analyticsevent.EventHeader.Message,
                                        analyticsevent.EventHeader.CustomTag,
                                        analyticsevent.EventHeader.Timestamp,
                                        analyticsevent.EventHeader.Source.Name);
                                    dataGridViewAlarm.Rows.Insert(0, row);
                                }
                            }
                            break;
                        case ViewMode.Event:
                            {   
                                row.Tag = ev;
                                row.CreateCells(dataGridViewAlarm,
                                    ev.EventHeader.Type,
                                    ev.EventHeader.Message,
                                    ev.EventHeader.Timestamp,
                                    ev.EventHeader.Source.Name);
                                dataGridViewAlarm.Rows.Insert(0, row);
                            }
                            break;
                        case ViewMode.Access:
                            {
                                AccessControlEvent accessControlEvent = ev as AccessControlEvent;
                                if (accessControlEvent != null)
                                {
                                    string credentialHolderId = "";
                                    string systemId = "";
                                    if (accessControlEvent.RelatedAccessControlCredentialHolderIds != null)
                                    {
                                        if (accessControlEvent.RelatedAccessControlCredentialHolderIds.Length > 0)
                                        {
                                            credentialHolderId = accessControlEvent.RelatedAccessControlCredentialHolderIds[0];
                                        }
                                    }
                                    if (accessControlEvent.AccessControlSystemId != null)
                                    {
                                        systemId = accessControlEvent.AccessControlSystemId.ToString();
                                    }
                                    row.Tag = ev;
                                    row.CreateCells(dataGridViewAlarm,
                                        ev.EventHeader.Type,
                                        ev.EventHeader.Message,
                                        ev.EventHeader.Timestamp,
                                        ev.EventHeader.Source.Name,
                                        credentialHolderId,
                                        systemId);
                                    dataGridViewAlarm.Rows.Insert(0, row);
                                }
                            }
                            break;
                        case ViewMode.LPR:
                            {
                                AnalyticsEvent analyticsevent = ev as AnalyticsEvent;
                                if (analyticsevent != null)
                                {
                                    row.Tag = analyticsevent;
                                    row.CreateCells(dataGridViewAlarm,
                                        analyticsevent.EventHeader.Type,
                                        analyticsevent.ObjectList[0].Value,
                                        analyticsevent.ObjectList[0].Confidence.ToString("F5"),
                                        analyticsevent.EventHeader.CustomTag,
                                        analyticsevent.EventHeader.Timestamp,
                                        analyticsevent.EventHeader.Source.Name);
                                    dataGridViewAlarm.Rows.Insert(0, row);
                                }
                            }
                            break;
                    }
                }
            }
            return null;

        }
        #endregion

        #region alarm handling
        private void OnCellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewAlarm.Rows.Count)
            {
                _selectedRow = dataGridViewAlarm.Rows[e.RowIndex];
            }
            else
            {
                _selectedRow = null;
            }
            buttonCompleted.Enabled = _selectedRow != null;
            buttonInprogress.Enabled = _selectedRow != null;
        }

        private void buttonInprogress_Click(object sender, EventArgs e)
        {
            if (_selectedRow != null)
            {
                Alarm alarm = _selectedRow.Tag as Alarm;
                if (alarm != null)
                {
                    try
                    {
                        IAlarmClient alarmClient = LookupAlarmClient(alarm.EventHeader.Source.FQID);
                        alarmClient.UpdateAlarm(alarm.EventHeader.ID, "Operator changed to InProgress", 4, 1, DateTime.UtcNow, "");
                    }
                    catch (Exception ex)
                    {
                        EnvironmentManager.Instance.ExceptionDialog("MessageHandler", ex);
                    }
                }
            }
        }

        private void buttonCompleted_Click(object sender, EventArgs e)
        {
            if (_selectedRow != null)
            {
                Alarm alarm = _selectedRow.Tag as Alarm;
                if (alarm != null)
                {
                    try
                    {
                        IAlarmClient alarmClient = LookupAlarmClient(alarm.EventHeader.Source.FQID);
                        alarmClient.UpdateAlarm(alarm.EventHeader.ID, "Operator Closed Alarm", 11, alarm.EventHeader.Priority, DateTime.UtcNow, null);
                    }
                    catch (Exception ex)
                    {
                        EnvironmentManager.Instance.ExceptionDialog("MessageHandler", ex);
                    }
                }
            }
        }
        #endregion

        private IAlarmClient LookupAlarmClient(FQID fqid)
        {
            return _alarmClientManager.GetAlarmClient(fqid.ServerId);
        }
    }
}
