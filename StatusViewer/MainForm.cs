using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;

namespace StatusViewer
{
    public partial class MainForm : Form
    {
        private List<object> _registrations = new List<object>();

        // Used to find the TreeNodes fast for status updates
        private Dictionary<Guid, TreeNode> _treeNodeCache = new Dictionary<Guid, TreeNode>();

        // List of Item Kinds to display in TreeNode table.
        private Collection<Guid> _includeInDisplay 
            = new Collection<Guid>(){ Kind.Server, Kind.Camera, Kind.Output, Kind.InputEvent, Kind.TriggerEvent, Kind.Folder };

        private MessageCommunication _messageCommunication;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            treeViewItems.ImageList = VideoOS.Platform.UI.Util.ImageListClone;
            treeViewItems.ShowNodeToolTips = true;

            MessageCommunicationManager.Start(EnvironmentManager.Instance.MasterSite.ServerId);
            _messageCommunication = MessageCommunicationManager.Get(EnvironmentManager.Instance.MasterSite.ServerId);

            // Register to retrieve all NewEventIndication's from the Event Server
            _registrations.Add(_messageCommunication.RegisterCommunicationFilter(MessageHandler,
                new VideoOS.Platform.Messaging.CommunicationIdFilter(VideoOS.Platform.Messaging.MessageId.Server.NewEventIndication)));
            _registrations.Add(_messageCommunication.RegisterCommunicationFilter(MessageHandler,
                new VideoOS.Platform.Messaging.CommunicationIdFilter(VideoOS.Platform.Messaging.MessageId.System.SystemConfigurationChangedIndication)));
            _registrations.Add(_messageCommunication.RegisterCommunicationFilter(MessageHandler,
                new VideoOS.Platform.Messaging.CommunicationIdFilter(VideoOS.Platform.Messaging.MessageId.System.SystemConfigurationChangedDetailsIndication)));

            // Register to receive the response from the ProvideCurrentStateRequest - issued later in this method.
            _registrations.Add(_messageCommunication.RegisterCommunicationFilter(ProvideCurrentStateResponseHandler,
                new VideoOS.Platform.Messaging.CommunicationIdFilter(MessageCommunication.ProvideCurrentStateResponse)));

            _messageCommunication.ConnectionStateChangedEvent += new EventHandler(_messageCommunication_ConnectionStateChangedEvent);
            // Build Top TreeNode 
            // GetItems will always return the Management Server as the single top-node - we are using System-defined hierarchy so that you can see recording servers
            Item server = Configuration.Instance.GetItems(ItemHierarchy.SystemDefined)[0];
            TreeNode tn = new TreeNode(server.Name)
            {
                ImageIndex = VideoOS.Platform.UI.Util.ServerIconIx,
                SelectedImageIndex = VideoOS.Platform.UI.Util.ServerIconIx,
                Tag = server.FQID.ServerId.Id
            };

            treeViewItems.Nodes.Add(tn);

            // Add all children
            tn.Nodes.AddRange(AddChildren(server));

            try
            {
                // Ask for current state of all Items
                _messageCommunication.TransmitMessage(
                    new VideoOS.Platform.Messaging.Message(MessageCommunication.ProvideCurrentStateRequest), null, null, null);
            }
            catch (MIPException)
            {
                MessageBox.Show(
                    "Unable to connect to EventServer's MessageCommunication service (default port 22333) - will retry every 5 seconds",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            treeViewItems.ExpandAll();
        }

        void _messageCommunication_ConnectionStateChangedEvent(object sender, EventArgs e)
        {
            if (IsHandleCreated && !IsDisposed)
            {
                this.BeginInvoke(
                    new MethodInvoker(
                        delegate ()
                        {
                            if (_messageCommunication.IsConnected)
                            {
                                labelConnected.Text = "Connected";
                                try
                                {
                                    // Ask for current state of all Items
                                    _messageCommunication.TransmitMessage(
                                    new VideoOS.Platform.Messaging.Message(MessageCommunication.ProvideCurrentStateRequest), null, null, null);
                                }
                                catch (MIPException)
                                {
                                    MessageBox.Show(
                                    "Unable to connect to EventServer's MessageCommunication service (default port 22333) - will retry every 5 seconds",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                labelConnected.Text = "Not Connected, retrying ...";
                            }
                        }));
            }
        }

        /// <summary>
        /// Recursively find and add Items to the TreeView
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private TreeNode[] AddChildren(Item item)
        {
            List<TreeNode> children = new List<TreeNode>();
            foreach (Item child in item.GetChildren())
            {
                if (_includeInDisplay.Count == 0 || _includeInDisplay.Contains(child.FQID.Kind))
                {
                    Guid id = child.FQID.ObjectId != Guid.Empty ? child.FQID.ObjectId : child.FQID.ServerId.Id;

                    TreeNode tn = new TreeNode(child.Name)
                    {
                        ImageIndex = VideoOS.Platform.UI.Util.KindToImageIndex[child.FQID.Kind],
                        SelectedImageIndex = VideoOS.Platform.UI.Util.KindToImageIndex[child.FQID.Kind],
                        Tag = id
                    };
                    if (child.FQID.Kind != Kind.Folder && child.FQID.ObjectId != child.FQID.Kind)
                    {
                        if (_treeNodeCache.ContainsKey(id) == false)
                            _treeNodeCache.Add(id, tn);
                    }
                    children.Add(tn);
                    tn.Nodes.AddRange(AddChildren(child));
                }
            }
            return children.ToArray();
        }

        /// <summary>
        /// Handle the response from the EventServer with status for all known items
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dest"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private object ProvideCurrentStateResponseHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageReceiver(ProvideCurrentStateResponseHandler), message, dest, source);
            }
            else
            {
                Collection<ItemState> result = message.Data as Collection<ItemState>;
                if (result != null)
                {
                    foreach (ItemState itemState in result)
                    {
                        UpdateState(itemState.FQID, itemState.State);
                    }
                    treeViewItems.Refresh();
                }
            }
            return null;
        }

        /// <summary>
        /// Update one specific TreeNode
        /// </summary>
        /// <param name="fqid"></param>
        /// <param name="state"></param>
        private void UpdateState(FQID fqid, String state)
        {
            Guid id = Guid.Empty;
            if (_treeNodeCache.ContainsKey(fqid.ObjectId))
            {
                id = (Guid)_treeNodeCache[fqid.ObjectId].Tag;
            }
            else
            {
                if (fqid.ObjectId == Guid.Empty && _treeNodeCache.ContainsKey(fqid.ServerId.Id))
                {
                    id = (Guid)_treeNodeCache[fqid.ServerId.Id].Tag;
                }
            }

            if (id != Guid.Empty && _treeNodeCache.ContainsKey(id))
            {
                TreeNode tn = _treeNodeCache[id];
                tn.ToolTipText = state;
                if (state == "Enabled" || state == "Responding" || state == "Server Responding")
                    tn.ForeColor = Color.Black;
                else if (state == "Disabled" || state == "Not Responding" || state == "Server Not Responding")
                    tn.ForeColor = Color.Gray;

                listBoxChanges.Items.Insert(0, DateTime.Now.ToLongTimeString() + ": " + tn.Text + " - " + state);
            }
        }

        /// <summary>
        /// A new state message has been sent by the Event Server - check for any new valued state changes
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dest"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private object MessageHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            if (message.MessageId == VideoOS.Platform.Messaging.MessageId.Server.NewEventIndication)
            {
                EventData eventData = message.Data as EventData;
                if (eventData != null)
                {
                    BeginInvoke(new MethodInvoker(delegate (){ UpdateState(eventData.EventHeader.Source.FQID, eventData.EventHeader.Message); }));
                }
                else
                {
                    BaseEvent baseEvent = message.Data as BaseEvent;
                    if (baseEvent != null)
                    {
                        BeginInvoke(new MethodInvoker(delegate () { UpdateState(baseEvent.EventHeader.Source.FQID, baseEvent.EventHeader.Message); }));
                    }
                }
            }
            if (message.MessageId == VideoOS.Platform.Messaging.MessageId.System.SystemConfigurationChangedDetailsIndication)
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    var dataInfo = "";
                    var data = message.Data as SystemConfigurationChangedDetailsIndicationData;
                    if (data != null)
                    {
						foreach (var detail in data.DetailsList)
						{
                            string name = string.Empty;

                            var configItem = VideoOS.Platform.ConfigurationItems.Factory.GetConfigurationItem(detail.FQID);
                            if (configItem != null)
                                name = configItem.Name;

                            string kindName = VideoOS.Platform.ConfigurationItems.Factory.GetItemTypeFromKind(detail.FQID.Kind);

                            dataInfo = $" - ChangeType={detail.ChangeType}, Name={name}, Kind={kindName} ({detail.FQID.Kind}), Id={detail.FQID.ObjectId}";
                        }
                    }

                    listBoxChanges.Items.Insert(0, DateTime.Now.ToLongTimeString() + ": " + message.MessageId + dataInfo);
                }));
            }
            return null;
        }

        /// <summary>
        /// Close application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClose(object sender, EventArgs e)
        {
            foreach (var registration in _registrations)
            {
                _messageCommunication.UnRegisterCommunicationFilter(registration);
            }
            
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }
    }
}