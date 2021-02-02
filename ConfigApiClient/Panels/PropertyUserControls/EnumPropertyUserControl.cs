using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels.PropertyUserControls
{
	public partial class EnumPropertyUserControl : PropertyUserControl
	{
		private int _origY;

		public EnumPropertyUserControl(Property property)
			: base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;
		    if (property.ValueTypeInfos == null)
		    {
		        throw new ArgumentException("ValueTYpeInfos cannot be null for enum, key=" + property.Key);
		    }

		    foreach (ValueTypeInfo vtd in property.ValueTypeInfos)
			{
				int ix = comboBox1.Items.Add(new TagItem(vtd.Name, vtd.Value));
				if (property.Value.ToString() == vtd.Value.ToString())
					comboBox1.SelectedIndex = ix;
			}

			HasChanged = false;
			_origY = comboBox1.Left;

            comboBox1.Enabled = property.IsSettable;

            WeGotFocus += FocusHandling;

        }

        public void FocusHandling(object sender, EventArgs e)
        {
            comboBox1.Focus();
            //comboBox1.DroppedDown = true;
        }

        internal override int LeftIndent
		{
			set { comboBox1.Left = _origY - value; }
		}

		private void OnCheckChanged(object sender, EventArgs e)
		{
			HasChanged = true;
			if (ValueChanged != null)
			{
                if (comboBox1.SelectedItem != null)
                {
                    Property.Value = ((TagItem)comboBox1.SelectedItem).Value.ToString();
                    ValueChanged(this, new EventArgs());
                }
			}
		}

	}

}
