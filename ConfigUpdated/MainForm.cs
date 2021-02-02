using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Messaging;

namespace ConfigUpdated
{
    public partial class MainForm : Form
    {
        private Collection<ConfigurationMonitor> _configurationMonitors = new Collection<ConfigurationMonitor>();

        // List of Item Kind's to display in TreeNode table.
        private Collection<Guid> _includeInDisplay = new Collection<Guid>()
		                                             	{
		                                             		Kind.Server,
		                                             		Kind.Camera,
															Kind.Output,
															Kind.InputEvent,
															Kind.TriggerEvent,
		                                             		Kind.Folder
		                                             	};

        // Used to find the TreeNode's fast for status updates
        private Dictionary<Guid, String> _itemNameCache = new Dictionary<Guid, string>();
        private Dictionary<Guid, String> _itemNameCacheTemp = new Dictionary<Guid, string>();


        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            treeViewItems.ImageList = VideoOS.Platform.UI.Util.ImageListClone;
            treeViewItems.ShowNodeToolTips = true;

            Item siteItem = EnvironmentManager.Instance.GetSiteItem(EnvironmentManager.Instance.MasterSite);

            InitSite(siteItem);
        }

        private void InitSite(Item siteItem)
        {
            ConfigurationMonitor configurationMonitor = new ConfigurationMonitor(siteItem.FQID.ServerId);
            configurationMonitor.ShowMessage += ConfigurationMonitorOnShowMessage;
            configurationMonitor.ConfigurationNowReloaded += ConfigurationMonitorOnConfigurationNowReloaded;
            configurationMonitor.ConnectionStateChanged += configurationMonitor_ConnectionStateChanged;
            _configurationMonitors.Add(configurationMonitor);

            foreach (Item site in siteItem.GetChildren())
            {
                InitSite(site);
            }            
        }

        void configurationMonitor_ConnectionStateChanged()
        {
            int upCnt = 0;
            foreach (var cm in _configurationMonitors)
                if (cm.IsConnectedToEventServer)
                    upCnt++;
            String tx = "Servers found:" + _configurationMonitors.Count + ", Online:" + upCnt;
            BeginInvoke(new Action(() => { labelConnected.Text = tx; }));
        }

        private void ConfigurationMonitorOnConfigurationNowReloaded()
        {
            List<Item> servers = Configuration.Instance.GetItemsSorted(ItemHierarchy.SystemDefined);

            this.BeginInvoke(new Action(() => RecheckConfig(servers)));
        }

        private void ConfigurationMonitorOnShowMessage(string message)
        {
            this.BeginInvoke(new Action(() => { ShowInfo(message); }));
        }

        // This method needs to execute on the UI thread
        private void RecheckConfig(List<Item> servers)
        {

            treeViewItems.Nodes.Clear();
            _itemNameCacheTemp = new Dictionary<Guid, string>();

            foreach (Item server in servers)
            {
                // Build Top TreeNode
                TreeNode tn = new TreeNode(server.Name)
                {
                    ImageIndex = VideoOS.Platform.UI.Util.ServerIconIx,
                    SelectedImageIndex = VideoOS.Platform.UI.Util.ServerIconIx,
                    Tag = server.FQID.ServerId.Id
                };

                treeViewItems.Nodes.Add(tn);

                // Add all children
                tn.Nodes.AddRange(AddChildren(server));
            }

            foreach (Guid id in _itemNameCache.Keys)
            {
                if (_itemNameCacheTemp.ContainsKey(id) == false)
                {
                    ShowInfo("Deleted: " + _itemNameCache[id]);
                }
            }
            _itemNameCache = _itemNameCacheTemp;
            _itemNameCacheTemp = null;

            treeViewItems.ExpandAll();
            
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

                    bool isDisabled = child.Properties.ContainsKey("Enabled") && child.Properties["Enabled"] == "No";
                    if (!isDisabled)
                    {
                        TreeNode tn = new TreeNode(child.Name)
                        {
                            ImageIndex = VideoOS.Platform.UI.Util.KindToImageIndex[child.FQID.Kind],
                            SelectedImageIndex = VideoOS.Platform.UI.Util.KindToImageIndex[child.FQID.Kind],
                            Tag = id
                        };
                        if (!child.Enabled)
                            tn.ForeColor = Color.Gray;

                        if (child.FQID.Kind != Kind.Folder && child.FQID.ObjectId != child.FQID.Kind)
                        {
                            if (_itemNameCache.ContainsKey(id) == false)
                            {
                                if (_itemNameCacheTemp.ContainsKey(id) == false) // Avoid multiple add in same load
                                {
                                    _itemNameCacheTemp.Add(id, child.Name);
                                    ShowInfo("Added: " + child.Name);
                                }
                            }
                            else
                            {
                                if (_itemNameCache[id] != child.Name)
                                {
                                    if (_itemNameCache.ContainsKey(id) && _itemNameCache[id] != child.Name)
                                    {
                                        ShowInfo("Renamed from: " + _itemNameCache[id] + " to: " + child.Name);
                                    }
                                }
                                _itemNameCacheTemp[id] = child.Name; 
                            }
                        }
                        children.Add(tn);
                        tn.Nodes.AddRange(AddChildren(child));
                    }
                }
            }
            return children.ToArray();
        }

        private void ShowInfo(String tx)
        {
            String time = DateTime.Now.ToString("T");

            int ix = listBoxInfo.Items.Add(time + ": " + tx);
            listBoxInfo.SelectedIndex = ix;
        }

        private void OnClose(object sender, EventArgs e)
        {
            foreach (ConfigurationMonitor configurationMonitor in _configurationMonitors)
            {
                configurationMonitor.ShowMessage -= ConfigurationMonitorOnShowMessage;
                configurationMonitor.ConfigurationNowReloaded -= ConfigurationMonitorOnConfigurationNowReloaded;
                configurationMonitor.ConnectionStateChanged -= configurationMonitor_ConnectionStateChanged;
                configurationMonitor.Dispose();
            }
            _configurationMonitors.Clear();

            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }
    }
}
