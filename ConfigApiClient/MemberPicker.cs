using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ConfigAPIClient.UI;
using VideoOS.ConfigurationAPI;
using VideoOS.Platform.Proxy.ConfigApi;
using VideoOS.Platform.Util;

namespace ConfigAPIClient
{
    public partial class MemberPicker : Form
    {
        private readonly List<string> _itemTypes;
        private ConfigApiClient _configApiClient;
        private bool _allowAll = false;

        public ConfigurationItem SelectedConfigurationItem;
        public String SelectedAllItem = null;
        private IEnumerable<ConfigurationItem> _topItems;

        public MemberPicker(IEnumerable<ConfigurationItem> topItems, List<string> itemTypes, bool allowAll, ConfigApiClient configApiClient)
        {
            InitializeComponent();

            _configApiClient = configApiClient;
            _itemTypes = itemTypes;
            _allowAll = allowAll;
            _topItems = topItems;

            foreach (string itemType in itemTypes)
            {
                comboBoxItemType.Items.Add(itemType);
            }

            if (itemTypes.Count == 0)
            {
                comboBoxItemType.Items.Add("ItemType not available, use GUIDs");
            }
            else
            {
                comboBoxItemType.SelectedIndex = 0;

                treeView1.ImageList = Icons.IconList;

                FillTreeView();
            }
        }

        private void FillTreeView()
        {
            treeView1.Nodes.Clear();
            if (_allowAll)
            {
                foreach (string itemType in _itemTypes)
                {
                    TreeNode tn = new TreeNode(String.Format("All {0}", itemType));
                    tn.Tag = String.Format("/{0}Folder", itemType);
                    tn.ImageIndex = tn.SelectedImageIndex = Icons.GetImageIndex(itemType);
                    treeView1.Nodes.Add(tn);
                }
            }

            foreach (ConfigurationItem item in _topItems)
            {
                TreeNode tn = new TreeNode(item.DisplayName);
                tn.Tag = item;
                tn.ImageIndex = tn.SelectedImageIndex = Icons.GetImageIndex(item.ItemType);
                tn.Nodes.Add("...");

                treeView1.Nodes.Add(tn);
            }        
        }

        private void OnBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode tn = e.Node;
            ConfigurationItem item = tn.Tag as ConfigurationItem;

            if (tn.Nodes.Count == 1 && item != null)
            {
                tn.Nodes.Clear();

                string itemType = comboBoxItemType.SelectedItem as string;

                List<ConfigurationItem> children = _configApiClient.GetNextForItemType(item, itemType);
                if (!children.Any())
                {
                    var path = new ConfigurationItemPath(item.Path);
                    if (path.IsFolder) // Might be that the type is deeper in the tree
                    {
                        children = _configApiClient.GetChildItems(item.Path).ToList();
                    }
                }
				children.Sort((i1, i2) => Sort.NumericStringCompare(i1.DisplayName, i2.DisplayName));
                foreach (ConfigurationItem child in children.Where(c => c.ItemType == itemType || _configApiClient.GetChildItems(c.Path).Any()))
                {
                    TreeNode tnNew = new TreeNode(child.DisplayName);
                    tnNew.Tag = child;
                    tnNew.ImageIndex = tnNew.SelectedImageIndex = Icons.GetImageIndex(child.ItemType);
                    tnNew.Nodes.Add("...");

                    tn.Nodes.Add(tnNew);
                }
            }
        }

        private void OnOK(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode!=null)
            {
                SelectedConfigurationItem = treeView1.SelectedNode.Tag as ConfigurationItem;
                SelectedAllItem = treeView1.SelectedNode.Tag as string;
            }
            this.Close();
        }


        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag is string)
                {
                    buttonOK.Enabled = true;
                    return;
                }
                ConfigurationItem check = treeView1.SelectedNode.Tag as ConfigurationItem;
                string itemType = comboBoxItemType.SelectedItem as string;

                buttonOK.Enabled = check != null && (check.ItemType == itemType);
            }
        }

        private void OnItemTypeChanged(object sender, EventArgs e)
        {
            FillTreeView();
        }
    }
}
