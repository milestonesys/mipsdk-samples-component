using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;
using ConfigAPIClient.UI;

namespace ConfigAPIClient.Panels.PropertyUserControls
{
	public partial class SidPropertyUserControl : PropertyUserControl
	{
		private int _origY;

        public SidPropertyUserControl() : base()
        {
            InitializeComponent();
        }
        
        public SidPropertyUserControl(Property property, ConfigurationItem[] users)
			: base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;
            int selectedIndex = -1;
            foreach (var user in users)
            {
                String sid = FindPropertyValue(user, "Sid");
                comboBox1.Items.Add(new TagItem(user.DisplayName, sid));
                if (sid == property.Value)
                    selectedIndex = comboBox1.Items.Count -1;
            }
			HasChanged = false;
			_origY = comboBox1.Left;
            radioButtonBasic.Checked = false;
            radioButtonADUser.Checked = true;
            if (selectedIndex != -1)
            {
                comboBox1.SelectedIndex = selectedIndex;
                radioButtonBasic.Checked = true;
                radioButtonADUser.Checked = false;
            }
            //TODO - validate
            
            comboBox1.Enabled = property.IsSettable;
            radioButtonBasic.Enabled = property.IsSettable;
            radioButtonADUser.Enabled = property.IsSettable;
            
        }
        
		internal override int LeftIndent
		{
			set { comboBox1.Left = _origY - value; }
		}

        private String FindPropertyValue(ConfigurationItem configurationItem, string key)
        {
            if (configurationItem.Properties == null)
                return "";
            foreach (var p in configurationItem.Properties)
                if (p.Key == key)
                    return p.Value;
            return "";
        }

		private void OnCheckChanged(object sender, EventArgs e)
		{
			HasChanged = true;
            if (ValueChanged != null && comboBox1.SelectedItem != null)
			{
				ValueChanged(this, new EventArgs());
				Property.Value = ((TagItem) comboBox1.SelectedItem).Value.ToString();
			}
		}

        private void CheckChanged(object sender, EventArgs e)
        {
            if (radioButtonBasic.Checked)
            {
                comboBox1.Enabled = true;
                buttonAdSelect.Enabled = false;
                buttonAdSelect.Text = "";
            } else
            {
                comboBox1.Enabled = false;
                buttonAdSelect.Enabled = true;
                comboBox1.SelectedIndex = -1;
            }
        }

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void OnAdClick(object sender, EventArgs e)
        {
            DirectoryObject[] users = UserPicker.ShowUserObjectPicker(this);

            if (users != null)
            foreach (var user in users)
            {
                Property.Value = user.Sid;
                buttonAdSelect.Text = user.Name;
                break;
            }
        }
	}
}
