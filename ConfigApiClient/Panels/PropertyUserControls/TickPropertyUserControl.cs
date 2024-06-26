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
	public partial class TickPropertyUserControl : PropertyUserControl
	{
		private int _origY;
        private bool trueFalse = false;

		public TickPropertyUserControl(Property property)
			: base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;

		    string value = property.Value ?? "0";
            if (String.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
            {
                trueFalse = true;
                checkBox1.Checked = true;
            }
            else if (String.Equals(value, "false", StringComparison.OrdinalIgnoreCase))
            {
                trueFalse = true;
                checkBox1.Checked = false;
            }
            else
            {
                trueFalse = false;
                int onoff = 0;
                if (Int32.TryParse((String) property.Value, out onoff))
                    checkBox1.Checked = onoff == 1;
            }
		    checkBox1.Enabled = property.IsSettable;
		    HasChanged = false;
			_origY = checkBox1.Left;


		}

		internal override int LeftIndent
		{
			set { checkBox1.Left = _origY - value; }
		}

		private void OnCheckChanged(object sender, EventArgs e)
		{
			HasChanged = true;
			if (ValueChanged != null)
			{
				ValueChanged(this, new EventArgs());
                if (trueFalse)
    				Property.Value = checkBox1.Checked ? "True" : "False";
                else
                    Property.Value = checkBox1.Checked ? "1" : "0";	
            }
		}

	}
}
