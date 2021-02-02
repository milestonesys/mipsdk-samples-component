using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.Util;

namespace ConfigAccessViaSDK
{
    public class DumpConfigurationUserControl : UserControl
	{
		private TextBox textBoxSelected;
		private Button buttonSelect;
		private TreeView treeViewItems;
		private TreeView treeViewDetail;
		private Label label1;
		private Label label2;

        private ArrayList selectedDevices = null;
        private TextBox textBoxToken;
        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private Label label3;
        private TreeView treeViewDisabledItems;
        private Item _selectedItem = null;

        public DumpConfigurationUserControl()
        {
            InitializeComponent();

            treeViewItems.ImageList = VideoOS.Platform.UI.Util.ImageList;
            treeViewDisabledItems.ImageList = VideoOS.Platform.UI.Util.ImageList;
        }

        public void Clear()
        {
            treeViewItems.Nodes.Clear();            
        }

        public void FillContentAsync()
        {
            try
            {
                treeViewItems.Nodes.Clear();
                Configuration.Instance.GetItemsAsync(AsyncItemsHandler, this, null);
            }
            catch (Exception e)
            {
                EnvironmentManager.Instance.ExceptionDialog("FillContentAsync", e);
            }
        }
		public void FillDisabledItems()
		{
			try
			{
			    List<Item> disabledItems =
			        VideoOS.Platform.SDK.Environment.GetDisabledDevices(Configuration.Instance.ServerFQID.ServerId.Id);

                TreeNode treeViewDisabled = new TreeNode("Disabled devices...");
			    treeViewDisabledItems.Nodes.Add(treeViewDisabled);
			    foreach (Item item in disabledItems)
			    {
			        TreeNode tn = new TreeNode(item.Name);
			        int ix = VideoOS.Platform.UI.Util.BuiltInItemToImageIndex(item);
			        tn.ImageIndex = tn.SelectedImageIndex = ix;
			        tn.Tag = item;
			        treeViewDisabled.Nodes.Add(tn);
			    }
			} catch (Exception e)
			{
				EnvironmentManager.Instance.ExceptionDialog("FillDisabledItems", e);
			}
		}

		private void AsyncItemsHandler(List<Item> list, object callerref)
		{
			if (list != null)
			{
				FillNodes(list, treeViewItems.Nodes);

				if (EnvironmentManager.Instance.MasterSite!=null)
				{
					Item site = EnvironmentManager.Instance.GetSiteItem(EnvironmentManager.Instance.MasterSite);
					if (site != null)
					{
						TreeNode tn = new TreeNode("Site-Hierarchy");
						tn.ImageIndex = tn.SelectedImageIndex = VideoOS.Platform.UI.Util.FolderIconIx;
						treeViewItems.Nodes.Add(tn);
						FillNodes(new List<Item>() {site}, tn.Nodes);
					}
				}
				buttonSelect.Enabled = true;
			}
		}

		internal void FillContentSpecific(ItemHierarchy hierarchy)
		{
			treeViewItems.Nodes.Clear();
            _selectedItem = null;
			List<Item> top = Configuration.Instance.GetItems(hierarchy);
			FillNodes(top, treeViewItems.Nodes);
            if (EnvironmentManager.Instance.MasterSite != null)
            {
                Item site = EnvironmentManager.Instance.GetSiteItem(EnvironmentManager.Instance.MasterSite);
                if (site != null)
                {
                    TreeNode tn = new TreeNode("Site-Hierarchy");
                    tn.ImageIndex = tn.SelectedImageIndex = VideoOS.Platform.UI.Util.FolderIconIx;
                    treeViewItems.Nodes.Add(tn);
                    FillNodes(new List<Item>() { site }, tn.Nodes);
                }
            }
        }

        private void FillNodes(List<Item> list, TreeNodeCollection treeNodeCollection)
		{
			list.Sort((i1, i2) => Sort.NumericStringCompare(i1.Name, i2.Name));
			foreach (Item item in list)
			{
				TreeNode tn = new TreeNode(item.Name);
				int ix = VideoOS.Platform.UI.Util.BuiltInItemToImageIndex(item);
                tn.ImageIndex = tn.SelectedImageIndex = ix;

				tn.Tag = item;
				if (item.HasChildren != VideoOS.Platform.HasChildren.No)
				{
					tn.Nodes.Add("...");
				}
				treeNodeCollection.Add(tn);
			}
		}

		internal ArrayList SelectedDevices
		{
			set
			{
				selectedDevices = value;
					textBoxSelected.Text = "";
				if (selectedDevices != null && selectedDevices.Count > 0)
				{
                    foreach (FQID fqid in selectedDevices)
                    {
                        Item newDevice = Configuration.Instance.GetItem(fqid);
                        textBoxSelected.Text += newDevice.Name + ", ";

                    }
				}
			}
            get { return selectedDevices;}
		}

        internal void ShowInfo(string info)
        {
            textBoxSelected.Text = info;
        }

        internal Item GetSelectedItem()
        {
            return _selectedItem;
        }

		#region Designer Generated code

		private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxSelected = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.treeViewItems = new System.Windows.Forms.TreeView();
            this.treeViewDetail = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.treeViewDisabledItems = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // textBoxSelected
            // 
            this.textBoxSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelected.Location = new System.Drawing.Point(137, 443);
            this.textBoxSelected.Name = "textBoxSelected";
            this.textBoxSelected.ReadOnly = true;
            this.textBoxSelected.Size = new System.Drawing.Size(550, 20);
            this.textBoxSelected.TabIndex = 2;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelect.Enabled = false;
            this.buttonSelect.Location = new System.Drawing.Point(7, 440);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(99, 23);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Select Instances";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.OnSelect);
            // 
            // treeViewItems
            // 
            this.treeViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewItems.Location = new System.Drawing.Point(8, 42);
            this.treeViewItems.Name = "treeViewItems";
            this.treeViewItems.Size = new System.Drawing.Size(295, 172);
            this.treeViewItems.TabIndex = 3;
            this.treeViewItems.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnBeforeExpand);
            this.treeViewItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
            // 
            // treeViewDetail
            // 
            this.treeViewDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDetail.Location = new System.Drawing.Point(309, 42);
            this.treeViewDetail.Name = "treeViewDetail";
            this.treeViewDetail.Size = new System.Drawing.Size(378, 352);
            this.treeViewDetail.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "List of all defined items:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Details for the selected Item:";
            // 
            // textBoxToken
            // 
            this.textBoxToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxToken.Location = new System.Drawing.Point(137, 417);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.ReadOnly = true;
            this.textBoxToken.Size = new System.Drawing.Size(550, 20);
            this.textBoxToken.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "List of all disabled items:";
            // 
            // treeViewDisabledItems
            // 
            this.treeViewDisabledItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDisabledItems.Location = new System.Drawing.Point(7, 238);
            this.treeViewDisabledItems.Name = "treeViewDisabledItems";
            this.treeViewDisabledItems.Size = new System.Drawing.Size(296, 156);
            this.treeViewDisabledItems.TabIndex = 9;
            this.treeViewDisabledItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
            // 
            // DumpConfigurationUserControl
            // 
            this.Controls.Add(this.treeViewDisabledItems);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxToken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewDetail);
            this.Controls.Add(this.treeViewItems);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.textBoxSelected);
            this.Name = "DumpConfigurationUserControl";
            this.Size = new System.Drawing.Size(695, 482);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

		#region user click event handling

		private void OnSelect(object sender, EventArgs e)
		{
			DevicePickerForm form = new DevicePickerForm();
			if (selectedDevices!=null && selectedDevices.Count>0)
			{
				FQID[] FQIDlist = new FQID[selectedDevices.Count];
				int ix = 0;
				foreach (FQID fqid in selectedDevices)
					FQIDlist[ix++] = fqid;
				form.SelectedFQID = FQIDlist;
			}

			form.ShowDialog();

			FQID[] selectedFQID = form.SelectedFQID;
			ArrayList list = new ArrayList();
			if (selectedFQID!=null)
			foreach (FQID fqid in selectedFQID)
				list.Add(fqid);
			SelectedDevices = list;
		}

		private void OnAfterSelect(object sender, TreeViewEventArgs e)
		{
			_selectedItem = e.Node.Tag as Item;
            if (_selectedItem != null)
			{
                _selectedItem = Configuration.Instance.GetItem(_selectedItem.FQID) ?? _selectedItem; // Refresh to get latest content

				treeViewDetail.Nodes.Clear();
                TreeNode tn = new TreeNode(_selectedItem.Name);
                DumpFQID(_selectedItem, tn);
                DumpFields(_selectedItem, tn);
                DumpProperties(_selectedItem, tn);
                DumpAuthorization(_selectedItem, tn);
                DumpRelated(_selectedItem, tn);
				treeViewDetail.Nodes.Add(tn);
				tn.ExpandAll();
			}
		}

		private void OnBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			// If this is the first time we expand, get the children
			if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null)
			{
				e.Node.Nodes.Clear();					// Skip the dummy entry
				Item item = e.Node.Tag as Item;
				if (item!=null)
				{
					FillNodes(item.GetChildren(), e.Node.Nodes);
				}
			}
		}

		#endregion

		#region the detail dump methods

		private void DumpFQID(Item item, TreeNode tn)
		{
			TreeNode fqidNode = new TreeNode("FQID",1,1);
			tn.Nodes.Add(fqidNode);
			fqidNode.Nodes.Add(new TreeNode("FQID.Kind: " + item.FQID.Kind + " (" + Kind.DefaultTypeToNameTable[item.FQID.Kind] + ")",1,1));
			fqidNode.Nodes.Add(new TreeNode("FQID.ServerId.ServerType: " + item.FQID.ServerId.ServerType,1,1));
			fqidNode.Nodes.Add(new TreeNode("FQID.ServerId.Id: " + item.FQID.ServerId.Id,1,1));
			fqidNode.Nodes.Add(new TreeNode("FQID.ServerId.ServerHostname: " + item.FQID.ServerId.ServerHostname,1,1));
			fqidNode.Nodes.Add(new TreeNode("FQID.ServerId.Serverport: " + item.FQID.ServerId.Serverport,1,1));
            fqidNode.Nodes.Add(new TreeNode("FQID.ServerId.ServerScheme: " + item.FQID.ServerId.ServerScheme, 1, 1));
            fqidNode.Nodes.Add(new TreeNode("FQID.ParentId: " + item.FQID.ParentId,1,1));
			fqidNode.Nodes.Add(new TreeNode("FQID.ObjectId: " + item.FQID.ObjectId,1,1));
			fqidNode.Nodes.Add(new TreeNode("FQID.ObjectIdString: " + item.FQID.ObjectIdString,1,1));
			fqidNode.Nodes.Add(new TreeNode("FQID.FolderType: " + item.FQID.FolderType, 1, 1));
		}

		private void DumpProperties(Item item, TreeNode tn)
		{
			TreeNode propNode = new TreeNode("Properties", 1, 1);
			tn.Nodes.Add(propNode);
            propNode.Nodes.Add("Enabled = " + item.Enabled);

            if (item.Properties!=null)
			{
				foreach (string key in item.Properties.Keys)
				{
					propNode.Nodes.Add(key + " = " + item.Properties[key]);
				}
			}
		}

		private void DumpAuthorization(Item item, TreeNode tn)
		{
			TreeNode propNode = new TreeNode("Authorization");
			tn.Nodes.Add(propNode);
			if (item.Authorization != null)
			{
				foreach (string key in item.Authorization.Keys)
				{
					if (key.ToLower().Contains("password"))
						propNode.Nodes.Add(key + " = " + "**********");
					else
						propNode.Nodes.Add(new TreeNode(key + " = " + item.Authorization[key]));
				}
			}
		}

		private void DumpRelated(Item item, TreeNode tn)
		{
			TreeNode relatedNode = new TreeNode("Related");
			tn.Nodes.Add(relatedNode);
			List<Item> related = item.GetRelated();
			if (related!=null)
			{
				foreach (Item rel in related)
				{
					TreeNode relNode = new TreeNode(rel.Name);
					relNode.ImageIndex = relNode.SelectedImageIndex = VideoOS.Platform.UI.Util.KindToImageIndex[rel.FQID.Kind];
					relatedNode.Nodes.Add(relNode);
				}
			}
		}

		private void DumpFields(Item item, TreeNode tn)
		{
			TreeNode fields = new TreeNode("Fields");
			tn.Nodes.Add(fields);
			fields.Nodes.Add("HasRelated : " + item.HasRelated);
			fields.Nodes.Add("HasChildren: " + item.HasChildren);
            if (item.PositioningInformation == null)
            {
                fields.Nodes.Add("No PositioningInformation");
            }
            else
            {
                fields.Nodes.Add("PositioningInformation: Latitude=" + item.PositioningInformation.Latitude + ", longitude=" + item.PositioningInformation.Longitude);
                fields.Nodes.Add("PositioningInformation: CoverageDirection=" + item.PositioningInformation.CoverageDirection);
                fields.Nodes.Add("PositioningInformation: CoverageDepth=" + item.PositioningInformation.CoverageDepth);
                fields.Nodes.Add("PositioningInformation: CoverageFieldOfView=" + item.PositioningInformation.CoverageFieldOfView);
            }

        }
        #endregion

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_selectedItem != null)
            {
                LoginSettings loginSettings = LoginSettingsCache.GetLoginSettings(_selectedItem.FQID);
                if (loginSettings != null)
                    textBoxToken.Text = loginSettings.Token;
                else
                    textBoxToken.Text = "--- token not found ---";
            }
        }
    }
}
