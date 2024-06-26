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
	public partial class StringPropertyUserControl : PropertyUserControl
	{
		private int _origY;
		public StringPropertyUserControl(Property property) : base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;
            if (property.UIImportance == UIImportance.Password)
                textBoxValue.PasswordChar = '*';
			textBoxValue.Text = property.Value??"";

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
                Property.Value = textBoxValue.Text;
                ValueChanged(this, new EventArgs());
			}
		}
	}
}
