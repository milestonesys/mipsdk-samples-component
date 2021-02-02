using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigAPIClient;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	/// <summary>
	/// Build a propertylist with a heading of item Displayname and an Enable tickmark, if it is defined.
	/// </summary>
	public partial class PropertyListUserControl : UserControl
	{
		private EventHandler _valueChangedHandler;
        private ConfigApiClient _configApiClient;

        public PropertyListUserControl(ConfigurationItem item, EventHandler valueChangedHandler, int leftOffset, ConfigApiClient configApiClient, string toHaveFocus)
		{
			InitializeComponent();

            _configApiClient = configApiClient;
            _valueChangedHandler = valueChangedHandler;

			if (item.EnableProperty!=null)
			{
				EnabledCheckBox.Visible = true;
				EnabledCheckBox.Checked = item.EnableProperty.Enabled;
				EnabledCheckBox.Text = item.EnableProperty.DisplayName;
			} else
			{
				EnabledCheckBox.Visible = false;
				textBoxName.Location = EnabledCheckBox.Location;
			}

			textBoxName.Text = item.DisplayName;

			int totalContentHeight = this.Height;// panelContent.Location.Y;
			totalContentHeight += PanelUtils.BuildPropertiesUI(item, 0, leftOffset, panelContent, valueChangedHandler, _configApiClient, toHaveFocus);

			if (!item.ChildrenFilled)	//TODO exception stuff, or load elsewhere
			{
                try
                {
                    item.Children = _configApiClient.GetChildItems(item.Path);
                    item.ChildrenFilled = true;
                } catch (Exception ex)
                {
                    MessageBox.Show(String.Format("GetChildItems({0}) : {1}", item.Path, ex.Message));
                }
			}

			if (item.Children != null)
			{
				leftOffset += Constants.LeftIndentChildControl;
				foreach (ConfigurationItem child in item.Children)
				{
					if ((child.Children == null || child.Children.Length == 0) && child.EnableProperty != null)
					{
						UserControl uc = new PropertyEnableUserControl(child, valueChangedHandler, leftOffset, _configApiClient, toHaveFocus);
                        uc.Width = panelContent.Width;
						uc.Location = new Point(Constants.LeftIndentChildControl, panelContent.Height);
						uc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
						this.Height += uc.Height;
						totalContentHeight += uc.Height;
						panelContent.Controls.Add(uc);
					}
					else
					{
						UserControl uc = new PropertyListUserControl(child, valueChangedHandler, leftOffset, _configApiClient, toHaveFocus);
                        uc.Width = panelContent.Width;
						uc.Location = new Point(Constants.LeftIndentChildControl, panelContent.Height);
						uc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
						this.Height += uc.Height;
						totalContentHeight += uc.Height;
						panelContent.Controls.Add(uc);
					}
				} 
				//panelContent.Height = totalContentHeight;
			}
            this.Height = totalContentHeight;
        }

		private void OnEnableChanged(object sender, EventArgs e)
		{
			panelContent.Enabled = EnabledCheckBox.Checked;
			if (_valueChangedHandler != null)
				_valueChangedHandler(this, new EventArgs());
		}
	}
}
