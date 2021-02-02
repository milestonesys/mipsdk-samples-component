using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoOS.Platform;
using VideoOS.Platform.Messaging;

namespace ConfigUpdated
{
    public class ConfigurationMonitor : IDisposable
    {
        private MessageCommunication _messageCommunication;
        private object _obj;
        private System.Threading.Timer _reloadTimer;
        private bool _firstTime = true;
        private ServerId _serverId;

        private List<FQID> _serversToLoad = new List<FQID>();

        public ConfigurationMonitor(ServerId serverId)
        {
            _reloadTimer = new System.Threading.Timer(ReloadConfigTimerHandler);
            _serverId = serverId;

            //Find out if site has an event server

            // We use the Event Server for getting the change indication
            MessageCommunicationManager.Start(_serverId);
            _messageCommunication = MessageCommunicationManager.Get(_serverId);

            _obj = _messageCommunication.RegisterCommunicationFilter(SystemConfigurationChangedIndicationHandler,
                new VideoOS.Platform.Messaging.CommunicationIdFilter(VideoOS.Platform.Messaging.MessageId.System.SystemConfigurationChangedIndication));

            _messageCommunication.ConnectionStateChangedEvent += new EventHandler(_messageCommunication_ConnectionStateChangedEvent);

            _reloadTimer.Change(0, 15000);      // Lets display now
    
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_reloadTimer != null)
                {
                    _reloadTimer.Dispose();
                    _reloadTimer = null;

                    _messageCommunication.UnRegisterCommunicationFilter(_obj);
                    MessageCommunicationManager.Stop(_serverId);
                }
            }
        }


        public delegate void ShowMessageDelegate(String message);
        public event ShowMessageDelegate ShowMessage = delegate { };

        public delegate void ConfigurationNowReloadedDelegate();
        public event ConfigurationNowReloadedDelegate ConfigurationNowReloaded = delegate { };

        public delegate void ConnectionStateChangedDelegate();
        public event ConnectionStateChangedDelegate ConnectionStateChanged = delegate { };

        public bool IsConnectedToEventServer = false;

        void _messageCommunication_ConnectionStateChangedEvent(object sender, EventArgs e)
        {
            IsConnectedToEventServer = _messageCommunication.IsConnected;
            // In case we need to understand when we are online with event server...
            ConnectionStateChanged();
        }

        private object SystemConfigurationChangedIndicationHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
        {
            lock (_serversToLoad)
            {
                List<FQID> fqidList = message.Data as List<FQID>;
                if (fqidList != null)
                {
                    foreach (FQID fqid in fqidList)
                    {
                        if (fqid.ServerId.ServerType != "XP") // Ignore unknown servers (e.g. Registered Services)
                        {
                            Item item = Configuration.Instance.GetItem(fqid.ObjectId, fqid.Kind);
                            if (item == null && fqid.Kind == Kind.Server)
                            {
                                // Could be XPE, then use ParentId (EventServer FQID has XPE-server id in this field)
                                item = Configuration.Instance.GetItem(fqid.ParentId, fqid.Kind);
                            }
                            if (item != null)
                                _serversToLoad.Add(item.FQID);
                            else
                                _serversToLoad.Add(fqid);
                        }
                    }

                    // Set timer to reload in 15 seconds (unless more changes happens, then just extent wait time)
                    _reloadTimer.Change(0, 15000);

                    ShowMessage("--- Event received to load new configuration");
                }
            }
            return null;
        }

        private void ReloadConfigTimerHandler(object state)
        {
            // Stop the timer:
            _reloadTimer.Change(Timeout.Infinite, Timeout.Infinite);

            // Reload configuration from server to this app's memory
            // This code might take some time, we perform this on the timer callback thread
            if (!_firstTime)
            {
                lock (_serversToLoad)
                {
                    Dictionary<Guid, FQID> servers = new Dictionary<Guid, FQID>();
                    foreach (FQID fqid in _serversToLoad)
                    {
                        if (fqid.Kind == Kind.Server)
                            servers[fqid.ServerId.Id] = fqid;
                        else
                        {
                            // We like to get hold of the  recorder that owns the item
                            Item serverItem = Configuration.Instance.GetItem(fqid.ServerId.Id, Kind.Server);
                            if (serverItem != null)
                                servers[serverItem.FQID.ObjectId] = serverItem.FQID;
                        }
                    }

                    foreach (FQID serverfqid in servers.Values)
                        VideoOS.Platform.SDK.Environment.ReloadConfiguration(serverfqid);
                    _serversToLoad.Clear();
                }
            }
            _firstTime = false;

            ShowMessage("--- configuration reloaded");

            // Now configuration is ready, let UI thread update the UI in this test sample
            ConfigurationNowReloaded();
        }



    }
}
