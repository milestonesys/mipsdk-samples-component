using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml;
using ConfigAPIClient.Panels;
using VideoOS.ConfigurationAPI;
using VideoOS.ConfigurationAPI.ConfigurationFaultException;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigAPIClient
{
	public partial class MainForm : Form
	{
	    private ConfigApiClient _configApiClient;

		private static bool _advancedUI = true;
        private static bool _showInvokeResultsToolStripMenuItem = true;

        internal static bool ValidateField = false;   // For testing purpose

        internal static Collection<String> _navItemTypes = new Collection<string>()
	                                                   {
	                                                       ItemTypes.System,
	                                                       ItemTypes.RecordingServer,
	                                                       ItemTypes.RecordingServerFolder,
	                                                       ItemTypes.HardwareDriverFolder,
	                                                       ItemTypes.HardwareDriver,
	                                                       ItemTypes.Hardware,
	                                                       ItemTypes.HardwareFolder,
	                                                       ItemTypes.CameraFolder,
	                                                       ItemTypes.InputEventFolder,
	                                                       ItemTypes.OutputFolder,
	                                                       ItemTypes.MicrophoneFolder,
	                                                       ItemTypes.SpeakerFolder,
	                                                       ItemTypes.MetadataFolder,
	                                                       ItemTypes.Camera,
	                                                       ItemTypes.InputEvent,
	                                                       ItemTypes.Output,
	                                                       ItemTypes.Microphone,
	                                                       ItemTypes.Speaker,
	                                                       ItemTypes.Metadata,
                                                           ItemTypes.CameraGroup,
                                                           ItemTypes.CameraGroupFolder,
                                                           ItemTypes.MetadataGroup,
                                                           ItemTypes.MetadataGroupFolder,
                                                           ItemTypes.MicrophoneGroup,
                                                           ItemTypes.MicrophoneGroupFolder,
                                                           ItemTypes.SpeakerGroup,
                                                           ItemTypes.SpeakerGroupFolder,
                                                           ItemTypes.InputEventGroup,
                                                           ItemTypes.InputEventGroupFolder,
                                                           ItemTypes.OutputGroup,
                                                           ItemTypes.OutputGroupFolder,
                                                           ItemTypes.BasicUserFolder,
														   ItemTypes.BasicUser,
                                                           ItemTypes.Role,
                                                           ItemTypes.RoleFolder,
                                                           ItemTypes.StorageFolder,
                                                           ItemTypes.Storage,
                                                           ItemTypes.LayoutFolder,
                                                           ItemTypes.LayoutGroup,
                                                           ItemTypes.LayoutGroupFolder,
                                                           ItemTypes.VideoWall,
                                                           ItemTypes.VideoWallFolder,
                                                           ItemTypes.Monitor,
                                                           ItemTypes.MonitorFolder,
                                                           ItemTypes.VideoWallPreset,
                                                           ItemTypes.VideoWallPresetFolder,
                                                           ItemTypes.MonitorPresetFolder,
                                                           ItemTypes.UserDefinedEventFolder,
                                                           ItemTypes.UserDefinedEvent,
                                                           ItemTypes.AnalyticsEventFolder,
                                                           ItemTypes.AnalyticsEvent,
                                                           ItemTypes.GenericEventFolder,
                                                           ItemTypes.GenericEventDataSourceFolder,
                                                           ItemTypes.GisMapLocation,
                                                           ItemTypes.GisMapLocationFolder,
                                                           ItemTypes.TimeProfile,
                                                           ItemTypes.TimeProfileFolder,
                                                           ItemTypes.MIPKind,
                                                           ItemTypes.MIPKindFolder,
                                                           ItemTypes.MIPItem,
                                                           ItemTypes.MIPItemFolder,
                                                           ItemTypes.AlarmDefinition,
                                                           ItemTypes.AlarmDefinitionFolder,
                                                           ItemTypes.LprMatchList,
                                                           ItemTypes.LprMatchListFolder,
                                                           ItemTypes.SaveSearches,
                                                           ItemTypes.FindSaveSearches,
                                                           ItemTypes.SaveSearchesFolder,
                                                           ItemTypes.Rule,
                                                           ItemTypes.RuleFolder,
                                                           ItemTypes.AccessControlSystem,
                                                           ItemTypes.AccessControlSystemFolder,
                                                           ItemTypes.AccessControlUnit,
                                                           ItemTypes.AccessControlUnitFolder,
                                                           ItemTypes.Site,
                                                           ItemTypes.SiteFolder,
                                                           ItemTypes.LicenseInformationFolder,
                                                           ItemTypes.LicenseInstalledProductFolder,
                                                           ItemTypes.LicenseOverviewAllFolder,
                                                           ItemTypes.LicenseDetailFolder,
                                                           ItemTypes.LicenseInformation,
                                                           ItemTypes.LicenseInstalledProduct,
                                                           ItemTypes.LicenseOverviewAll,
                                                           ItemTypes.LicenseDetail,
                                                           ItemTypes.BasicOwnerInformationFolder,
                                                           ItemTypes.BasicOwnerInformation,
                                                           ItemTypes.ToolOptionFolder,
                                                           ItemTypes.ToolOption,
                                                           ItemTypes.ViewGroupFolder,
                                                           ItemTypes.ViewGroup,
                                                           ItemTypes.ViewFolder,
                                                           ItemTypes.View,
                                                           ItemTypes.RecordingServerMulticastFolder,
                                                           ItemTypes.RecordingServerMulticast,
                                                           ItemTypes.FailoverGroupFolder,
                                                           ItemTypes.FailoverGroup,
                                                           ItemTypes.FailoverRecorderFolder,
                                                           ItemTypes.FailoverRecorder,
                                                           ItemTypes.ClientProfileFolder,
                                                           ItemTypes.RecordingServerFailoverFolder,
                                                           ItemTypes.EventTypeGroupFolder,
                                                           ItemTypes.EventTypeGroup,
                                                           ItemTypes.EventTypeFolder,
                                                           ItemTypes.EventType,
                                                           ItemTypes.StateGroupFolder,
                                                           ItemTypes.StateGroup,
                                                       };


	    private static MainForm Instance;
	    private string _serverAddress;
	    private int _serverPort;
	    private bool _corporate;

		public MainForm(string serverAddress, int serverPort, bool corporate)
		{
			InitializeComponent();

		    _serverAddress = serverAddress;
            _serverPort = serverPort;
		    _corporate = corporate;

            Instance = this;

            UI.Icons.Init();

            _advancedUI = true;
            ShowHiddenProperties = true;
            trimTreeToolStripMenuItem.Checked = false;
		}

		private void Onload(object sender, EventArgs e)
		{
			BeginInvoke(new MethodInvoker(OnLogon));
		}

		internal static bool Advanced { get { return _advancedUI; } }
        internal static bool ShowHiddenProperties { get; set; }
        internal static bool ShowInvokeResult { get { return _showInvokeResultsToolStripMenuItem; } }

		private void OnLogon(object sender, EventArgs e)
		{
			OnLogon();
		}

		private void OnLogon()
		{
            treeView.ImageList = UI.Icons.IconListBlack;

		    _configApiClient = new ConfigApiClient();
            _configApiClient.ServerAddress = _serverAddress;
            _configApiClient.Serverport = _serverPort;
		    _configApiClient.ServerType = _corporate
		                                      ? ConfigApiClient.ServerTypeEnum.Corporate
		                                      : ConfigApiClient.ServerTypeEnum.Arcus;
		    _configApiClient.Initialize();
            if (_configApiClient.Connected)
                toolStripStatusLabel1.Text = "Logged on";
            else
                toolStripStatusLabel1.Text = "Error logging on";

            UpdateTreeView();
		}


		private void OnLogoff(object sender, EventArgs e)
		{
			_configApiClient.Close();
			_configApiClient = null;
			treeView.Nodes.Clear();
			panelMain.Controls.Clear();
            toolStripStatusLabel1.Text = "Logged off";
        }

		private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DoAfterSelect(e.Node);
            Cursor.Current = Cursors.Default;
        }

        private void DoAfterSelect(TreeNode node)
		{
            ConfigurationItem item = node.Tag as ConfigurationItem;
            if (item != null && !item.ChildrenFilled && item.ItemType != ItemTypes.System)
            {
                item.Children = _configApiClient.GetChildItems(item.Path);
                item.ChildrenFilled = true;
                node.Nodes.Clear();
                if (item.ItemType == ItemTypes.HardwareDriverFolder)
                    AddHardwareDriversSorted(node, item.Children);
                else
                {
                    AddChildren(node, item);
                }
            }
            if (item != null)
            {
                if (item.ItemType == "Role")
                    item = _configApiClient.GetItem(item.Path);     //Re-read to get profiles filled.

                toolStripStatusLabel1.Text = item.Path;
                UpdateSidePanel(item);
            }
		}

        private void AddHardwareDriversSorted(TreeNode tn, ConfigurationItem[] items)
        {
            SortedDictionary<String, List<ConfigurationItem>> driverGroupped = new SortedDictionary<string, List<ConfigurationItem>>();
            foreach (ConfigurationItem childItem in items)
            {
                Property groupName = childItem.Properties.FirstOrDefault<Property>(p => p.Key == "GroupName");
                String name = "Other";
                if (groupName != null) name = groupName.Value;
                if (driverGroupped.ContainsKey(name) == false)
                    driverGroupped.Add(name, new List<ConfigurationItem>());
                driverGroupped[name].Add(childItem);
            }

            foreach (String name in driverGroupped.Keys)
            {
                TreeNode groupNode = new TreeNode(name)
                {
                    Tag = null,
                    Checked = false,
                    ImageIndex = UI.Icons.GetImageIndex(ItemTypes.HardwareDriverFolder),
                    SelectedImageIndex = UI.Icons.GetImageIndex(ItemTypes.HardwareDriverFolder),
                };
                tn.Nodes.Add(groupNode);
                SortedList<String, ConfigurationItem> namesSorted = new SortedList<string, ConfigurationItem>();
                foreach (ConfigurationItem childItem in driverGroupped[name])
                {
                    namesSorted.Add(childItem.DisplayName, childItem);
                }
                foreach (ConfigurationItem childItem in namesSorted.Values)
                {
                    
                    TreeNode driverNode = new TreeNode(childItem.DisplayName)
                    {
                        Tag = childItem,
                        Checked = false,
                        ImageIndex = UI.Icons.GetImageIndex(childItem.ItemType),
                        SelectedImageIndex = UI.Icons.GetImageIndex(childItem.ItemType)
                    };
                    groupNode.Nodes.Add(driverNode);
                }
            }
        }

        private void UpdateSidePanel(ConfigurationItem item)
        {
            panelMain.SuspendLayout();
            panelMain.Controls.Clear();
            if (item != null)
            {
                switch (item.ItemType)
                {
                    case VideoOS.ConfigurationAPI.ItemTypes.System:
                    case VideoOS.ConfigurationAPI.ItemTypes.RecordingServer:
                        panelMain.Controls.Add(new SimpleUserControl(item, true, _configApiClient) { Dock = DockStyle.Fill });
                        break;
                    case VideoOS.ConfigurationAPI.ItemTypes.Hardware:
                    case VideoOS.ConfigurationAPI.ItemTypes.Camera:
                    case VideoOS.ConfigurationAPI.ItemTypes.Microphone:
                    case VideoOS.ConfigurationAPI.ItemTypes.Speaker:
                    case VideoOS.ConfigurationAPI.ItemTypes.InputEvent:
                    case VideoOS.ConfigurationAPI.ItemTypes.Output:
                    case VideoOS.ConfigurationAPI.ItemTypes.Metadata:
                    case ItemTypes.ClientProfile:
                        panelMain.Controls.Add(new TabUserControl(item, _configApiClient) { Dock = DockStyle.Fill });
                        break;
                    default:
                        panelMain.Controls.Add(new SimpleUserControl(item, true, _configApiClient) { Dock = DockStyle.Fill });
                        break;
                }
            }
            else
            {
                panelMain.Controls.Add(new HelpUserControl() { Dock = DockStyle.Fill });
            }
            panelMain.ResumeLayout();
        }

		internal static void UpdateTree()
		{
			Instance.BeginInvoke(new MethodInvoker(Instance.UpdateTreeView));
		}

        private void ShowTopNodeOnly()
        {
            Cursor.Current = Cursors.WaitCursor;
            treeView.SuspendLayout();
            treeView.Nodes.Clear();
            try
            {
                var server = _configApiClient.GetItem("/");
                TreeNode serverTn = new TreeNode(server.DisplayName)
                {
                    Tag = server,
                    Checked = false,
                    ImageIndex = UI.Icons.GetImageIndex(server.ItemType),
                    SelectedImageIndex = UI.Icons.GetImageIndex(server.ItemType)
                };
                treeView.Nodes.Add(serverTn);
                DoAfterSelect(serverTn);
                serverTn.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get top node: " + ex.Message);
            }

            treeView.ResumeLayout();
            Cursor.Current = Cursors.Default;
        }

        private void UpdateTreeView()
		{
            //panelMain.Controls.Clear();
			treeView.SuspendLayout();
            treeView.Nodes.Clear();
            try
            {
				//InitializeClientProxy();
				var server = _configApiClient.GetItem("/");
                TreeNode serverTn = new TreeNode(server.DisplayName)
                {
                    Tag = server,
                    Checked = false,
                    ImageIndex = UI.Icons.GetImageIndex(server.ItemType),
                    SelectedImageIndex = UI.Icons.GetImageIndex(server.ItemType)
                };
                treeView.Nodes.Add(serverTn);
                var childItems = _configApiClient.GetChildItems("/");

				foreach (var childItem in childItems)
				{
					TreeNode tn = new TreeNode(childItem.DisplayName)
					{
						Tag = childItem,
						Checked = false,
						ImageIndex = UI.Icons.GetImageIndex(childItem.ItemType),
						SelectedImageIndex = UI.Icons.GetImageIndex(childItem.ItemType)
					};
                    serverTn.Nodes.Add(tn);
				}
                if (!sortTreeToolStripMenuItem.Checked)
                    treeView.TreeViewNodeSorter = null;
                treeView.Sort();
                treeView.ExpandAll();                
			}

			catch (Exception ex)
			{
				MessageBox.Show("Unable to build treeView: "+ ex.Message);
			}

            if (treeView.Nodes.Count > 0)
                treeView.Nodes[0].EnsureVisible();

			treeView.ResumeLayout();
		}

		private void AddChildren(TreeNode tn, ConfigurationItem item)
		{
		    bool expandTree = _navItemTypes.Contains(item.ItemType);
			if (expandTree)
			{
                try
                {
                    var childs = item.ChildrenFilled? item.Children : _configApiClient.GetChildItems(item.Path);
                    item.ChildrenFilled = true;
                    item.Children = childs;
                    if (childs != null)
                    {
                        foreach (var childItem in childs)
                        {
                            if (childItem.ItemType == ItemTypes.HardwareDriverSettingsFolder ||
                                childItem.ItemType == ItemTypes.DeviceDriverSettingsFolder ||
                                childItem.ItemType == ItemTypes.PtzPresetFolder ||
                                childItem.ItemType == ItemTypes.StreamFolder ||
                                childItem.ItemType == ItemTypes.MotionDetectionFolder ||
                                childItem.ItemType == ItemTypes.PrivacyMaskFolder ||
                                childItem.ItemType == ItemTypes.CustomPropertiesFolder ||
                                childItem.ItemType == ItemTypes.ClientSettingsFolder ||
                                childItem.ItemType == ItemTypes.PrivacyProtectionFolder ||
                                childItem.ItemType == ItemTypes.HardwareDeviceEventFolder ||
                                childItem.ItemType == ItemTypes.PatrollingProfileFolder 
                                )
                                continue;

                            if (childItem.ItemType.EndsWith("Folder") && trimTreeToolStripMenuItem.Checked)
                            {
                                if (childItem.ItemType != ItemTypes.HardwareDriverFolder)
                                    AddChildren(tn, childItem);
                            }
                            else
                            {
                                TreeNode tnChild = new TreeNode(childItem.DisplayName)
                                                       {
                                                           Tag = childItem,
                                                           Checked = false,
                                                           ImageIndex = UI.Icons.GetImageIndex(childItem.ItemType),
                                                           SelectedImageIndex =
                                                               UI.Icons.GetImageIndex(childItem.ItemType)
                                                       };
                                tn.Nodes.Add(tnChild);
                                if (childItem.ItemType != ItemTypes.HardwareDriverFolder)
                                    AddChildren(tnChild, childItem);
                            }
                        }                       
                    }
                } catch (Exception ex)
                {
                    Trace.WriteLine("--- GetChildren Exception:" + ex.Message);
                }
			}			
		}

		private void OnAdvancedChanged(object sender, EventArgs e)
		{
			_advancedUI = advancedToolStripMenuItem.Checked;
		}

		private void OnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
            if (e.Button == MouseButtons.Right)
            {
                treeView.SelectedNode = e.Node;
            }
		}

		private void OnContextMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
            ContextMenuTag mi = e.ClickedItem.Tag as ContextMenuTag;
			if (mi!=null)
			{
                try
                {
                    ConfigurationItem result = _configApiClient.InvokeMethod(mi.Item, mi.MethodInfo.MethodId);
                    if (result != null)
                    {
                        Form form = new MethodInvokeForm(result, _configApiClient);
                        form.ShowDialog();
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Unable to perform action:" + ex.Message);
                }
			}

			UpdateTreeView();
            UpdateSidePanel(null);
		}

        private void OnTrimChanged(object sender, EventArgs e)
        {
            _configApiClient.TrimTreeToolStripMenuItem = trimTreeToolStripMenuItem.Checked;
            if (trimTreeToolStripMenuItem.Checked)
            {
                ShowTopNodeOnly();
            }
            else
            {
                UpdateTreeView();
            }
        }

        private void OnShowHiddenChanged(object sender, EventArgs e)
        {
            ShowHiddenProperties = showHiddenPropertiesToolStripMenuItem.Checked;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            contextMenuStrip1.Items.Clear();
            ConfigurationItem selectedItemForContextMenu = treeView.SelectedNode.Tag as ConfigurationItem;
            if (selectedItemForContextMenu != null)
            {
                toolStripStatusLabel1.Text = selectedItemForContextMenu.Path;
                if (selectedItemForContextMenu.MethodIds != null)
                {
                    foreach (String id in selectedItemForContextMenu.MethodIds)
                    {
                        if (_configApiClient.AllMethodInfos.ContainsKey(id))
                        {
                            MethodInfo mi = _configApiClient.AllMethodInfos[id];
                            contextMenuStrip1.Items.Add(new ToolStripMenuItem(mi.DisplayName, _configApiClient.AllMethodBitmaps[id]) { Tag = new ContextMenuTag(selectedItemForContextMenu, mi) });
                        }
                    }
                }

                if (selectedItemForContextMenu.Children != null)
                {
                    foreach (ConfigurationItem hiddenChild in selectedItemForContextMenu.Children)
                    {
                        if (hiddenChild.ItemType.EndsWith("Folder") && trimTreeToolStripMenuItem.Checked)
                        {
                            if (hiddenChild.MethodIds != null)
                            {
                                foreach (String id in hiddenChild.MethodIds)
                                {
                                    if (_configApiClient.AllMethodInfos.ContainsKey(id))
                                    {
                                        MethodInfo mi = _configApiClient.AllMethodInfos[id];
                                        string displayName = mi.DisplayName;
                                        if (trimTreeToolStripMenuItem.Checked)
                                            displayName = hiddenChild.DisplayName + ": " + displayName;
                                        contextMenuStrip1.Items.Add(new ToolStripMenuItem(displayName, _configApiClient.AllMethodBitmaps[id]) { Tag = new ContextMenuTag(hiddenChild, mi) });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (contextMenuStrip1.Items.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void OnShowInvokeResultChange(object sender, EventArgs e)
        {
            _showInvokeResultsToolStripMenuItem = showInvokeResultsToolStripMenuItem.Checked;
        }

        private void toolStripStatusLabel1_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(toolStripStatusLabel1.Text);
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            foreach (TreeNode serverNode in treeView.Nodes)
            {
                foreach (TreeNode node in serverNode.Nodes)
                {
                    ConfigurationItem item = node.Tag as ConfigurationItem;
                    if (item != null)
                    {
                        item.Children = null;
                        item.ChildrenFilled = false;
                        node.Nodes.Clear();
                    }
                }
            }

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                OnRefresh(this, new EventArgs());
            }
        }

        private void OnCopyPath(object sender, EventArgs e)
        {
            Clipboard.SetText(toolStripStatusLabel1.Text);
        }
    }


    public class ContextMenuTag
    {
        public ConfigurationItem Item;
        public MethodInfo MethodInfo;
        public ContextMenuTag(ConfigurationItem item, MethodInfo methodInfo)
        {
            Item = item;
            MethodInfo = methodInfo;
        }
    }
}
