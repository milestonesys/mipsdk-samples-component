using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.SDK.Platform;

namespace MultiSiteStatusViewer
{
	public partial class MainForm : Form
	{
	    private CredentialCache _credentialCache;
	    private Dictionary<Guid, ServerObject> _dicServerObjects = new Dictionary<Guid, ServerObject>();
	    private Dictionary<Guid, TreeNode> _treeNodeItemsCache = new Dictionary<Guid, TreeNode>();
		private Collection<Guid> _includeInDisplay = new Collection<Guid>()
		                                             	{
		                                             		Kind.Server,
		                                             		Kind.Camera,
															Kind.Output,
															Kind.InputEvent,
															Kind.TriggerEvent,
		                                             		Kind.Folder
		                                             	};

        private static readonly Guid IntegrationId = new Guid("DE16D566-34ED-4EC4-A76E-A1971B7DD4A4");
        private const string IntegrationName = "Multi Site Status Viewer";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        /// <summary>
        /// The purpose of this sample is to login on multiple servers and continuesly 
        /// receive "NewEventIndication" and present the events.
        /// </summary>

        public MainForm()
		{
			InitializeComponent();
		    _credentialCache = new CredentialCache();
            timer1.Tick += new EventHandler(timer1_Tick);
		}

        #region Eventhandlers
        /// <summary>
        /// event handler for the connection changed event
        /// The Sites Treeview is updated, if a site is disconnected it is shown in grey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageConnectionStateChangedEvent(object sender, EventArgs e)
	    {
	        MessageCommunication messageCommunication = (MessageCommunication) sender;
	        ServerObject smo; 
	        if (_dicServerObjects.TryGetValue(messageCommunication.ServerId.Id, out smo))
	        {
	            if (IsHandleCreated && !IsDisposed)
	                this.BeginInvoke(new MethodInvoker(delegate()
	                    {
                            TreeNode tn = smo.SitesTreeNode;
                            RefreshButtons(treeViewSites.SelectedNode);
                            if (smo.MessCommunication != null)
	                        {
	                            if (smo.MessCommunication.IsConnected)
	                            {
	                                tn.ToolTipText = "connected";
	                                tn.ForeColor = Color.Black;
                                    toolStripStatusLabel1.Text = "This is a developer's code sample only. It is neither a test program nor an attempt to make an application.";
	                            }
	                            else
	                            {
	                                tn.ToolTipText = "not connected";
	                                tn.ForeColor = Color.Gray;
	                            }
	                        }
	                    }));
	        }
            
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
			    string server = message.ExternalMessageSourceEndPoint.ServerId.ServerHostname;
                //listBoxChanges.Items.Insert(0, server + " " + DateTime.Now.ToLongTimeString() + ": Current state - ");
                listBoxChanges.Items.Insert(0, DateTime.Now.ToLongTimeString() + ": " + server.PadRight(26, ' ') + " -- Current state --");
				if (result != null)
				{
					foreach (ItemState itemState in result)
					{
						UpdateState(itemState.FQID, itemState.State, server);
					}
					treeViewItems.Refresh();
				}
			}
			return null;
		}
        /// <summary>
        /// A new state message has been send by the EventServer - check for any new valued state changes
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
                string server = message.ExternalMessageSourceEndPoint.ServerId.ServerHostname;
                if (eventData != null)
                {
                    BeginInvoke(new MethodInvoker(delegate()
                    {
                        UpdateState(eventData.EventHeader.Source.FQID, eventData.EventHeader.Message, server);
                    }));
                }
                else
                {
                    BaseEvent baseEvent = message.Data as BaseEvent;
                    if (baseEvent != null)
                    {
                        BeginInvoke(new MethodInvoker(delegate()
                        {
                            UpdateState(baseEvent.EventHeader.Source.FQID,
                                        baseEvent.EventHeader.Message, server);
                        }));
                    }
                }
            }
            return null;
        }

        /// <summary>
		/// Update one specific TreeNode
		/// </summary>
		/// <param name="fqid"></param>
		/// <param name="state"></param>
		private void UpdateState(FQID fqid, String state, string server)
		{
			Guid id = Guid.Empty;
			if (_treeNodeItemsCache.ContainsKey(fqid.ObjectId))
			{
				id = (Guid)_treeNodeItemsCache[fqid.ObjectId].Tag;
			}
			else
			{
				if (fqid.ObjectId== Guid.Empty && _treeNodeItemsCache.ContainsKey(fqid.ServerId.Id))
				{
					id = (Guid) _treeNodeItemsCache[fqid.ServerId.Id].Tag;
				}
			}

			if (id != Guid.Empty && _treeNodeItemsCache.ContainsKey(id))
			{
				TreeNode tn = _treeNodeItemsCache[id];
				tn.ToolTipText = state;
				if (state == "Enabled" || state == "Responding" || state == "Server Responding")
					tn.ForeColor = Color.Black;
				else
					tn.ForeColor = Color.Gray;

			    //if (state != "Motion Detected")
			    {
			        string str = server.PadRight(36 - server.Length);
			        listBoxChanges.Items.Insert(0, DateTime.Now.ToLongTimeString() + ": " + str + " " + tn.Text + " - " + state);
			    }
			}
		}

        
        /// <summary>
        /// Timer function used to re-try Login, if Login has failed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            bool none = true;
            foreach (ServerObject smo in _dicServerObjects.Values)
            {
                if (!VideoOS.Platform.SDK.Environment.IsLoggedIn(smo.SiteItem.FQID.ServerId.Uri))
                {
                    none = false;
                    if (AddServer(smo))
                    {
                        StartMessaging(smo);
                        toolStripStatusLabel1.Text = "This is a developer's code sample only. It is neither a test program nor an attempt to make an application.";
                    }
                }
            }
            if (none)
            {
                timer1.Interval = 100000;
                toolStripStatusLabel1.Text = "This is a developer's code sample only. It is neither a test program nor an attempt to make an application.";
            }
            else
            {
                timer1.Start();
            }
        }

        #endregion
        
        #region starter helper functions
        /// <summary>
        /// Enviroment.Addserver is followed by a Login, if Login fails the AddServer is reversed with a RemoveServer
        /// Note; that while the SiteAddForms checks in a way that you know login to the Top site will succeed,
        /// Login to child site might not succeed
        /// </summary>
        /// <param name="so"></param>
        /// <returns></returns>
        private bool AddServer(ServerObject so)
        {
            // try login
            VideoOS.Platform.SDK.Environment.AddServer(so.SecureOnly, so.SiteItem,_credentialCache);
            try
            {
                VideoOS.Platform.SDK.Environment.Login(so.SiteItem.FQID.ServerId.Uri, IntegrationId, IntegrationName, Version, ManufacturerName);
            }
            catch (ServerNotFoundMIPException snfe)
            {
                toolStripStatusLabel1.Text = "Server not found: " + snfe.Message;
                VideoOS.Platform.SDK.Environment.RemoveServer(so.SiteItem.FQID.ServerId.Id);
                return false;
            }
            catch (InvalidCredentialsMIPException ice)
            {
                toolStripStatusLabel1.Text = "Invalid credentials for: " + ice.Message;
                VideoOS.Platform.SDK.Environment.RemoveServer(so.SiteItem.FQID.ServerId.Id);
                return false;
            }
            catch (Exception)
            {
                toolStripStatusLabel1.Text = "Internal error connecting to: " + so.SiteItem.FQID.ServerId.Uri.DnsSafeHost;
                VideoOS.Platform.SDK.Environment.RemoveServer(so.SiteItem.FQID.ServerId.Id);
                return false;
            }
            //return true;
            return VideoOS.Platform.SDK.Environment.IsLoggedIn(so.SiteItem.FQID.ServerId.Uri);
        }
        private void StartMessaging(ServerObject smo)
        {
            if (smo.MessCommunication == null)
            {
            	//VideoOS.Platform.SDK.Environment.AddServer(smo.SiteItem, _credentialCache);
            	Item site = smo.SiteItem;

                MessageCommunicationManager.Start(site.FQID.ServerId);
            	MessageCommunication messageCommunication = MessageCommunicationManager.Get(site.FQID.ServerId);
            	object newEventIndicationHandler = messageCommunication.RegisterCommunicationFilter(
            		MessageHandler,
            		new VideoOS.Platform.Messaging.CommunicationIdFilter(
            		    VideoOS.Platform.Messaging.MessageId.Server.NewEventIndication));
            	object provideCurrentStateResponseHandler =
            		messageCommunication.RegisterCommunicationFilter(ProvideCurrentStateResponseHandler,
            		                                                    new VideoOS.Platform.Messaging.
            		                                                        CommunicationIdFilter(
            		                                                        MessageCommunication
            		                                                            .ProvideCurrentStateResponse));
            	TreeNode treeNode = BuildTree(site.FQID);
            	if (treeNode != null)
            	{
            		smo.MessCommunication = messageCommunication;
            		smo.NewEventIndicationHandler = newEventIndicationHandler;
            		smo.ProvideCurrentStateResponseHandler = provideCurrentStateResponseHandler;
            	}

            	try
            	{
            		messageCommunication.TransmitMessage(
            		    new VideoOS.Platform.Messaging.Message(MessageCommunication.ProvideCurrentStateRequest),
            		    null, null, null);
            	}
            	catch (Exception ex)
            	{
                    toolStripStatusLabel1.Text = "TransmitMessage Exception: " + ex.Message +
                        " -Happens on server: " + smo.SiteItem.Name;
                    // todo
                    
                    {
                        if (IsHandleCreated && !IsDisposed)
                            this.BeginInvoke(new MethodInvoker(delegate()
                            {
                                TreeNode tn = smo.SitesTreeNode;
                                if (smo.MessCommunication != null)
                                {
                                    if (smo.MessCommunication.IsConnected)
                                    {
                                        tn.ToolTipText = "connected";
                                        tn.ForeColor = Color.Black;

                                    }
                                    else
                                    {
                                        tn.ToolTipText = "not connected";
                                        tn.ForeColor = Color.Gray;
                                    }
                                }
                            }));
                    }
                
            	}

            	messageCommunication.ConnectionStateChangedEvent +=
            		new EventHandler(MessageConnectionStateChangedEvent);
            }
        }
        private void AddSiteInfo(Item parent, CredentialCache credentialCache, TreeNode parentTn, bool secureOnly)
        {
           if (true)
           {
               if (_dicServerObjects.ContainsKey(parent.FQID.ServerId.Id))
               {
                   MessageBox.Show(
                       parent.Name + " is allready in the system and will not be added again.",
                       "Warning",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning
                       );
               }
               else
               {
                   TreeNode tn = parentTn.Nodes.Add(parent.FQID.ServerId.ServerHostname + " - " + parent.Name);
                   tn.Tag = parent;
                   foreach (Item site in parent.GetChildren())
                   {
                       AddSiteInfo(site, credentialCache, tn, secureOnly);
                   }
                   _dicServerObjects.Add(parent.FQID.ServerId.Id, new ServerObject()
                       {
                           Name = parent.Name,
                           SiteItem = parent,
                           SitesTreeNode=tn,
                           SecureOnly = secureOnly
                       });
               }
           }
        }

        /// <summary>
        /// build the itemTreeview
        /// </summary>
        /// <param name="siteFQID"></param>
        private TreeNode  BuildTree(FQID siteFQID)
        {
            treeViewItems.ImageList = VideoOS.Platform.UI.Util.ImageListClone;
            treeViewItems.ShowNodeToolTips = true;
            // Build Top TreeNode
            try
            {
                Item server = Configuration.Instance.GetItem(siteFQID); //hangs
            
                TreeNode tn = new TreeNode(server.Name)
                {
                    ImageIndex = VideoOS.Platform.UI.Util.ServerIconIx,
                    SelectedImageIndex = VideoOS.Platform.UI.Util.ServerIconIx,
                    Tag = server.FQID.ServerId.Id
                };
                treeViewItems.Nodes.Add(tn);
                _treeNodeItemsCache.Add(server.FQID.ServerId.Id,tn);
                // Add all children
                tn.Nodes.AddRange(AddChildren(server));
                return tn;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text  = "Exception on GetItem "+ ex.Message +
                    " Happens on server " + siteFQID.ServerId.ServerHostname ;
                return null;
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
				if (_includeInDisplay.Count==0 || _includeInDisplay.Contains(child.FQID.Kind))
				{
					Guid id = child.FQID.ObjectId != Guid.Empty ? child.FQID.ObjectId : child.FQID.ServerId.Id;

					TreeNode tn = new TreeNode(child.Name)
					              	{
					              		ImageIndex = VideoOS.Platform.UI.Util.KindToImageIndex[child.FQID.Kind],
					              		SelectedImageIndex = VideoOS.Platform.UI.Util.KindToImageIndex[child.FQID.Kind],
					              		Tag = id
					              	};
					if (child.FQID.Kind != Kind.Folder && child.FQID.ObjectId!=child.FQID.Kind)
					{
						if (_treeNodeItemsCache.ContainsKey(id) == false)
							_treeNodeItemsCache.Add(id, tn);
					}
					children.Add(tn);
					tn.Nodes.AddRange(AddChildren(child));
				}
			}
			return children.ToArray();
		}
        #endregion

        #region UI interaction
        /// <summary>
        /// start the SiteAddForm
        /// make a dictionary of the added site
        /// subscribe to events from the sites in the dictionary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SiteAddForm form = new SiteAddForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Item ii = form.SelectedSiteItem;
                if (_dicServerObjects.ContainsKey(form.SelectedSiteItem.FQID.ServerId.Id))
                {
                    MessageBox.Show(
                        form.SelectedSiteItem.Name + " is allready in the system and will not be added again.",
                       "Multi-site StatusViewer adding servers",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning
                       );
                }
                else
                {
                    _credentialCache = form.CredentialCache;
                    string myName = form.SelectedSiteItem.FQID.ServerId.ServerHostname + " - " +
                                    form.SelectedSiteItem.Name;

                    TreeNode tn = treeViewSites.Nodes.Add(myName);
                    tn.Tag = form.SelectedSiteItem;
                    treeViewSites.ShowNodeToolTips = true;

                    _dicServerObjects.Add(form.SelectedSiteItem.FQID.ServerId.Id, new ServerObject()
                        {
                            Name = form.SelectedSiteItem.Name,
                            SiteItem = form.SelectedSiteItem,
                            SitesTreeNode = tn,
                            SecureOnly = form.SecureOnly
                        });
                    if (form.IncludeChildSites)
                    {
                        foreach (Item site in form.SelectedSiteItem.GetChildren())
                        {
                            AddSiteInfo(site, form.CredentialCache, tn, form.SecureOnly);
                        }
                    }
                    treeViewSites.ShowNodeToolTips = true;
                    treeViewSites.Refresh();

                    bool startTimer = false;
                    foreach (ServerObject smo in _dicServerObjects.Values)
                    {
                        if (AddServer(smo))
                        {
                            StartMessaging(smo);
                        }
                        else
                        {
                            startTimer = true; //todo
                            TreeNode stn = smo.SitesTreeNode;
                                if (IsHandleCreated && !IsDisposed)
                                    this.BeginInvoke(new MethodInvoker(delegate()
                                    {
                                        stn.ToolTipText = "not logged in";
                                        stn.ForeColor = Color.Red;
                                    }
                                    ));
                        }
                    }
                    if (startTimer)
                    {
                        timer1.Interval = 30000;
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                }
            }
        }

        /// <summary>
        /// when something is eslected in the SitesTreeview you can use the Remove button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshButtons(treeViewSites.SelectedNode);
 
        }
        private void RefreshButtons(TreeNode tn)
        {
            if (tn != null)
            {
                buttonRemoveServer.Enabled = true;
                Item g = (Item)treeViewSites.SelectedNode.Tag;
                if (VideoOS.Platform.SDK.Environment.IsLoggedIn(g.FQID.ServerId.Uri))
                {
                    ServerObject smo;
                    if (_dicServerObjects.TryGetValue(g.FQID.ServerId.Id, out smo))
                    {


                        if (smo.MessCommunication != null)
                        {
                            if (smo.MessCommunication.IsConnected)
                            {
                                buttonCurrent.Enabled = true;

                            }
                            else
                            {
                                buttonCurrent.Enabled = false;
                            }
                        }

                    }

                }

            }
            else
            {
                buttonRemoveServer.Enabled = false;
                buttonCurrent.Enabled = false;
            }
        }
        /// <summary>
        /// request a new Current State
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCurrent_Click(object sender, EventArgs e)
        {
            Item g = (Item)treeViewSites.SelectedNode.Tag;
            ServerObject smo;
            if (_dicServerObjects.TryGetValue(g.FQID.ServerId.Id, out smo))
            {
                try
                {
                    smo.MessCommunication.TransmitMessage(
                        new VideoOS.Platform.Messaging.Message(MessageCommunication.ProvideCurrentStateRequest),
                        null, null, null);
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = "TransmitMessage Exception: " + ex.Message +
                        "Happens on server: " + smo.SiteItem.Name;
                    {
                        TreeNode tn = smo.SitesTreeNode;
                        if (smo.MessCommunication != null)
                        {
                            if (smo.MessCommunication.IsConnected)
                            {
                                tn.ToolTipText = "connected";
                                tn.ForeColor = Color.Black;

                            }
                            else
                            {
                                tn.ToolTipText = "not connected";
                                tn.ForeColor = Color.Gray;
                            }
                        }
                    }
                }

            }
        }        
        
        /// <summary>
        /// Remove a server, unregister message events, update the treeviews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveClick(object sender, EventArgs e)
        {
            Item g = (Item)treeViewSites.SelectedNode.Tag;
            ServerObject smo;
            if (_dicServerObjects.TryGetValue(g.FQID.ServerId.Id, out smo))
            {
                if (smo.MessCommunication != null)
                {
                    if (smo.NewEventIndicationHandler != null)
                        smo.MessCommunication.UnRegisterCommunicationFilter(smo.NewEventIndicationHandler);
                    if (smo.ProvideCurrentStateResponseHandler != null)
                        smo.MessCommunication.UnRegisterCommunicationFilter(smo.ProvideCurrentStateResponseHandler);
                    smo.MessCommunication.ConnectionStateChangedEvent -= (MessageConnectionStateChangedEvent);
                }
                VideoOS.Platform.SDK.Environment.RemoveServer(smo.SiteItem.FQID.ObjectId);

                treeViewSites.BeginUpdate();
                List<TreeNode> childNodes = new List<TreeNode>();
                foreach (TreeNode n in treeViewSites.SelectedNode.Nodes)
                {
                    childNodes.Add(n);
                }
                treeViewSites.SelectedNode.Remove();
                foreach (TreeNode tr in childNodes)
                {
                    treeViewSites.Nodes.Add(tr);
                }
                treeViewSites.EndUpdate();

                TreeNode tn;
                if (_treeNodeItemsCache.TryGetValue(smo.SiteItem.FQID.ServerId.Id, out tn)) //if not logged in you get false
                {
                    treeViewItems.Nodes.Remove(tn);
                    _treeNodeItemsCache.Remove(smo.SiteItem.FQID.ServerId.Id);
                }

                _dicServerObjects.Remove(g.FQID.ServerId.Id);

                if (treeViewSites.SelectedNode != null)
                {
                    buttonRemoveServer.Enabled = true;
                    if (VideoOS.Platform.SDK.Environment.IsLoggedIn(g.FQID.ServerId.Uri))
                    {
                        buttonCurrent.Enabled = true;
                    }
                    else
                    {
                        buttonCurrent.Enabled = false;
                    }
                }
                else
                {
                    buttonRemoveServer.Enabled = false;
                    buttonCurrent.Enabled = false;
                }
            }
        }
		
        /// <summary>
		/// Close application, unregister events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
            Close();
		}
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregClose();
        }
        private void UnregClose()
        {
            foreach (ServerObject obj in _dicServerObjects.Values)
            {
                if(obj.MessCommunication!=null)
                {
                if (obj.NewEventIndicationHandler != null) obj.MessCommunication.UnRegisterCommunicationFilter(obj.NewEventIndicationHandler);
                if (obj.ProvideCurrentStateResponseHandler != null) obj.MessCommunication.UnRegisterCommunicationFilter(obj.ProvideCurrentStateResponseHandler);
                obj.MessCommunication.ConnectionStateChangedEvent -= (MessageConnectionStateChangedEvent);
                }
            }
			VideoOS.Platform.SDK.Environment.RemoveAllServers();
        }
        #endregion

        private class ServerObject
        {
            public string Name
            {
                get;
                set;
            }

            public Item SiteItem
            {
                get;
                set;
            }

            public bool SecureOnly { get; set; }

            public TreeNode SitesTreeNode { get; set; }
            
            public MessageCommunication MessCommunication
            {
                get;
                set;
            }

            public object NewEventIndicationHandler
            {
                get;
                set;
            }

            //public object NewAlarmIndicationHandler
            //{
            //    get;
            //    set;
            //}

            public object ProvideCurrentStateResponseHandler
            {
                get;
                set;
            }
            
        }

        


	}
}
