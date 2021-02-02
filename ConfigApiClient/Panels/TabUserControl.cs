using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	public partial class TabUserControl : UserControl
	{
		private ConfigurationItem _item;
	    private ConfigApiClient _configApiClient;
        private ConfigurationItem _privacyMaskItem = null;

        public TabUserControl(ConfigurationItem item, ConfigApiClient configApiClient)
		{
			InitializeComponent();

            _configApiClient = configApiClient;
			_item = item;

			OnLoad();
		}

		private void OnLoad()
		{
			if ((_item.Properties!=null && _item.Properties.Length>0) || (_item.MethodIds != null && _item.MethodIds.Length > 0))
			{
				TabPage tabPage = new TabPage(_item.ItemType);
				tabPage.Tag = _item;
				tabPage.BorderStyle = System.Windows.Forms.BorderStyle.None;
				tabControl1.Margin = new System.Windows.Forms.Padding(0);
				tabControl1.TabPages.Add(tabPage);				
			}

			if (!_item.ChildrenFilled)
			{
				_item.Children = _configApiClient.GetChildItems(_item.Path);	
			}

            _privacyMaskItem = null;
            foreach (ConfigurationItem child in _item.Children)
            {
                if (child.ItemType == ItemTypes.PrivacyMaskFolder)
                {
                    if (!child.ChildrenFilled) {
                        child.Children = _configApiClient.GetChildItems(child.Path); 
                        child.ChildrenFilled = true;
                    }
                    if (child.Children!=null && child.Children.Length > 0)
                        _privacyMaskItem = child.Children[0];
                }
            }

			foreach (ConfigurationItem child in _item.Children)
			{
                if (MainForm._navItemTypes.Contains(child.ItemType))    // Do not repeat what is on the navigation tree
                    continue;

                if (child.ItemCategory == ItemCategories.Group)
                {
                    if (!child.ChildrenFilled)
                    {
                        child.Children = _configApiClient.GetChildItems(child.Path); 
                    }

                    if (child.Children.Length == 1 && child.ItemType != ItemTypes.PtzPresetFolder)
                    {
                        GenerateTab(child.Children[0]);
                    }
                    else
                    {
                        GenerateTab(child);
                    }
                } 
                else
                {
                    GenerateTab(child);
                }
			}

			if (tabControl1.TabCount>0)
				tabControl1.SelectedTab = tabControl1.TabPages[0];
			OnTabSelect(this, null);
		}

        private void GenerateTab(ConfigurationItem child)
        {
  				TabPage tabPage = new TabPage(child.DisplayName);
				tabPage.Tag = child;
				tabPage.BorderStyle = System.Windows.Forms.BorderStyle.None;
				tabControl1.Margin = new System.Windows.Forms.Padding(0);
				tabControl1.TabPages.Add(tabPage);    
        }

		private void OnTabSelect(object sender, EventArgs e)
		{
			if (tabControl1.TabPages.Count!=0 && tabControl1.SelectedTab != null)
			{
				ConfigurationItem tabItem = tabControl1.SelectedTab.Tag as ConfigurationItem;

				if (tabItem == _item)
				{
                    tabControl1.SelectedTab.Controls.Add(new SimpleUserControl(tabItem, false, _configApiClient, _privacyMaskItem)
					                                     	{Dock = DockStyle.Fill});
				}
				else
				{
					if (!tabItem.ChildrenFilled)
					{
						tabItem.Children = _configApiClient.GetChildItems(tabItem.Path);						
					}

                    //if (tabItem.ItemType == ItemTypes.CustomPropertiesFolder)
                   // {

                    //} else
                    //if (tabItem.ItemCategory == ItemCategories.Group)
                    //{
                    //    tabControl1.SelectedTab.Controls.Add(new TabUserControl(tabItem, _configApiClient) { Dock = DockStyle.Fill });
                    //}
                    //else
                    //{
                        tabControl1.SelectedTab.Controls.Add(new SimpleUserControl(tabItem, true, _configApiClient, _privacyMaskItem) { Dock = DockStyle.Fill });
                    //}
                    
				}
			}
		}
	}
}
