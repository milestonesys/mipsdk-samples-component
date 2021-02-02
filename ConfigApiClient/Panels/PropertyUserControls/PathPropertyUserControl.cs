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
	public partial class PathPropertyUserControl : PropertyUserControl
	{
		private int _origY;
        private string itemtype = "";
        private ConfigApiClient _configApiClient;

        public PathPropertyUserControl(Property property, ConfigApiClient configApiClient)
			: base(property)
		{
			InitializeComponent();

            _configApiClient = configApiClient;

			labelOfProperty.Text = property.DisplayName;

            if (property.IsSettable)
            {
                if (property.ValueTypeInfos == null)
                    throw new Exception("ValueTypeInfo is null on Path");

                foreach (ValueTypeInfo valueTypeInfo in property.ValueTypeInfos)
                {
                    if (valueTypeInfo.Name == ValueTypeInfoNames.PathItemType)
                    {
                        itemtype = valueTypeInfo.Value;
                        break;
                    }
                }
                if (itemtype.EndsWith("Folder"))
                    itemtype = itemtype.Substring(0, itemtype.Length - 6);
                if (!String.IsNullOrEmpty(property.Value))
                    button1.Text = property.Value;
                else
                    button1.Text = "Select a " + itemtype;
            } else
            {
                button1.Text = property.Value;
                button1.Enabled = false;
            }
			HasChanged = false;
            _origY = button1.Left;

            this.GotFocus += EnumPropertyUserControl_GotFocus;

        }

        private void EnumPropertyUserControl_GotFocus(object sender, EventArgs e)
        {
            button1.Focus();
        }

        internal override int LeftIndent
		{
            set { button1.Left = _origY - value; }
		}

        private void OnClick(object sender, EventArgs e)
        {
            List<ConfigurationItem> top = _configApiClient.GetTopForItemType(itemtype);
            MemberPicker picker = new MemberPicker(top, new List<string>() { itemtype }, false, _configApiClient);
            if (picker.ShowDialog() == DialogResult.OK)
            {
                ConfigurationItem selectedItem = picker.SelectedConfigurationItem;
                if (selectedItem!=null)
                {
                    this.Property.Value = selectedItem.Path;
                    button1.Text = selectedItem.DisplayName;
                } else
                {
                    this.Property.Value = null;
                    button1.Text = "Select a " + itemtype;
                }
                ValueChanged(this, e);
            }            
        }

	}
}
