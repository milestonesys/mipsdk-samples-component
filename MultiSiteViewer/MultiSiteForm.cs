using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Client;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;

namespace MultiSiteViewer
{
	/// <summary>
	/// This sample show how to add multiple server configurations to the MIP SDK at the same time,
	/// regardless of different domains and site structure.
	/// After the sites/servers has been added, the MIP SDK provides access to all configuration 
	/// available on these sites/servers.
	/// 
	/// For this sample it just shows that you can start two ImageViewerControls with camera
	/// defined on different sites or domains, or with different credentials.
	/// </summary>
	public partial class MultiSiteForm : Form
	{
		private Item _selectItem1;
		private ImageViewerControl _imageViewerControl1;

		private Item _selectItem2;
		private ImageViewerControl _imageViewerControl2;

        private Dictionary<Guid, TreeNode> _serverNodes = new Dictionary<Guid, TreeNode>();
	    private bool _treeViewCompleted = false;
	    private bool _noChildSites = false;

		public MultiSiteForm()
		{
			InitializeComponent();

		    EnvironmentManager.Instance.RegisterReceiver(LocalConfigurationHandler,
		        new MessageIdFilter(VideoOS.Platform.Messaging.MessageId.System.LocalConfigurationChangedIndication));
		}

        private object LocalConfigurationHandler(VideoOS.Platform.Messaging.Message message, FQID f1, FQID f2)
        {
            Item siteItem = EnvironmentManager.Instance.GetSiteItem(message.RelatedFQID);
            if (siteItem != null)
            {
                if (_serverNodes.ContainsKey(siteItem.FQID.ServerId.Id))
                {
                    RedrawTreeChildren(siteItem, _serverNodes[siteItem.FQID.ServerId.Id]);
                }
            }
            return null;
	    }

	    private void RedrawTreeChildren(Item serverItem, TreeNode node)
	    {
	        if (_treeViewCompleted)
	            return;
	        if (_noChildSites)
	            return;

            node.Nodes.Clear();
	        List<Item> children = serverItem.GetChildren();
	        foreach (Item child in children)
	        {
                TreeNode tn = node.Nodes.Add(child.Name);
	            tn.Tag = child;
	        }
	    }

		/// <summary>
		/// Open the AddSite Dialog and let the user select what to add.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonAddClick(object sender, EventArgs e)
		{
			SiteAddForm form = new SiteAddForm();
			if (form.ShowDialog() == DialogResult.OK)
			{
				_noChildSites = form.NoChildSites;

				VideoOS.Platform.SDK.Environment.AddServer(form.SecureOnly, form.SelectedSiteItem, form.CredentialCache, !form.SDKLoadedChildSites);
				TreeNode tn = treeView1.Nodes.Add(form.SelectedSiteItem.Name);
				tn.Tag = form.SelectedSiteItem;

			    _serverNodes[form.SelectedSiteItem.FQID.ServerId.Id] = tn;

			    CredentialCache cc = form.CredentialCache;
                if (form.SampleLoadedChildSites)
                { 
					foreach (Item site in form.SelectedSiteItem.GetChildren())
					{
                        form.AddUriToCache(cc, site);
						AddSite(form.SecureOnly, site, cc);
					}
                    _treeViewCompleted = true;
                }

				if(form.SDKLoadedChildSites)
                {
					RedrawTreeChildren(form.SelectedSiteItem, tn);
                }
			}
		}

		/// <summary>
		/// Recursive add all child sites. This can be used as an alternative to doing delayed load
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="credentialCache"></param>
		/// <param name="parentTn"></param>
		private void AddSite(bool secureOnly, Item parent, CredentialCache credentialCache)
		{

			VideoOS.Platform.SDK.Environment.AddServer(secureOnly, parent, credentialCache);
			TreeNode tn = treeView1.Nodes.Add(parent.Name);
			tn.Tag = parent;
			foreach (Item site in parent.GetChildren())
			{
				AddSite(secureOnly, site, credentialCache);
			}			
		}

		/// <summary>
		/// Cancel - do not add anything
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonCloseClick(object sender, EventArgs e)
		{
            if (_imageViewerControl1 != null)
            {
                _imageViewerControl1.Disconnect();
            }
            if (_imageViewerControl2 != null)
            {
                _imageViewerControl2.Disconnect();
            }

			Close();
		}

		/// <summary>
		/// Remove one Server from the MIP SDK configuration.
		/// Any still running ImageViewerControl will continue until next token need to be refreshed.  
		/// For real applications, the ImageViewerControl should be closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonRemoveClick(object sender, EventArgs e)
		{
			if (treeView1.SelectedNode!=null)
			{
				Item item = treeView1.SelectedNode.Tag as Item;
				if (item != null)
				{
					VideoOS.Platform.SDK.Environment.RemoveServer(item.FQID.ServerId.Id);
					treeView1.Nodes.Remove(treeView1.SelectedNode);
				    if (_serverNodes.ContainsKey(item.FQID.ServerId.Id))
				        _serverNodes.Remove(item.FQID.ServerId.Id);

				}
			}
		}

		/// <summary>
		/// Select and show a camera in top view.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonSelect1Click(object sender, EventArgs e)
		{
			if (_imageViewerControl1 != null)
			{
				_imageViewerControl1.Disconnect();
			}

			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems(ItemHierarchy.SystemDefined));
			if (form.ShowDialog() == DialogResult.OK)
			{
				_selectItem1 = form.SelectedItem;
				buttonSelect1.Text = _selectItem1.Name;

				_imageViewerControl1 = ClientControl.Instance.GenerateImageViewerControl();
				_imageViewerControl1.Dock = DockStyle.Fill;
				panel1.Controls.Clear();
				panel1.Controls.Add(_imageViewerControl1);
				_imageViewerControl1.CameraFQID = _selectItem1.FQID;
				_imageViewerControl1.EnableVisibleHeader = true;
				_imageViewerControl1.Initialize();
				_imageViewerControl1.Connect();
				_imageViewerControl1.Selected = true;
			}

		}


		/// <summary>
		/// Select and show a camera in bottom view.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonSelect2Click(object sender, EventArgs e)
		{
			if (_imageViewerControl2 != null)
			{
				_imageViewerControl2.Disconnect();
			}

			ItemPickerForm form = new ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
            form.Init(Configuration.Instance.GetItems(ItemHierarchy.SystemDefined));
			if (form.ShowDialog() == DialogResult.OK)
			{
				_selectItem2 = form.SelectedItem;
				buttonSelect2.Text = _selectItem2.Name;

				_imageViewerControl2 = ClientControl.Instance.GenerateImageViewerControl();
				_imageViewerControl2.Dock = DockStyle.Fill;
				panel2.Controls.Clear();
				panel2.Controls.Add(_imageViewerControl2);
				_imageViewerControl2.CameraFQID = _selectItem2.FQID;
				_imageViewerControl2.EnableVisibleHeader = true;
				_imageViewerControl2.Initialize();
				_imageViewerControl2.Connect();
				_imageViewerControl2.Selected = true;
			}

		}

	}
}
