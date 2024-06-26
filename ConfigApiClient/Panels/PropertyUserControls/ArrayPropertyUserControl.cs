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
	public partial class ArrayPropertyUserControl : PropertyUserControl
	{
		private int _origY;
		public ArrayPropertyUserControl(Property property) : base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName + " (comma-separated list)";

            textBoxValue.Text = property.ValueArray.Length == 0? "" : property.ValueArray.Aggregate((a, b) => a + "," + b);

            textBoxValue.ReadOnly = !property.IsSettable;
            textBoxValue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			_origY = textBoxValue.Left;

            if (!property.IsSettable)
                textBoxValue.ForeColor = Color.Gray;

			HasChanged = false;
            WeGotFocus += FocusHandling;
        }
        public void FocusHandling(object sender, EventArgs e)
        {
            textBoxValue.Focus();
        }

        internal override int LeftIndent
		{
			set { textBoxValue.Left = _origY - value; }
		}

		private void OnTextChanged(object sender, EventArgs e)
		{
			HasChanged = true;
			if (ValueChanged != null)
			{
                Property.ValueArray = textBoxValue.Text.Split(',');
                ValueChanged(this, new EventArgs());
			}
		}
	}
}
