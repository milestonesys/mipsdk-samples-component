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
	public partial class DateTimeDisplayPropertyUserControl : PropertyUserControl
	{
		private int _origY;
        public DateTimeDisplayPropertyUserControl(Property property)
            : base(property)
		{
			InitializeComponent();

            if (property.ValueType == ValueTypes.DateTimeType)
            {
                DateTime dateTime;
                if (DateTime.TryParse(property.Value, out dateTime))
                {
                    if (dateTime.Kind == DateTimeKind.Utc)
                        dateTime = dateTime.ToLocalTime();
                    textBoxValue.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                    textBoxValue.Text = "";
            } else
            {
                textBoxValue.Text = property.Value;
            }

            labelOfProperty.Text = property.DisplayName;

            textBoxValue.Enabled = false;

			_origY = textBoxValue.Left;

			HasChanged = false;
		}

		internal override int LeftIndent
		{
			set { textBoxValue.Left = _origY - value; }
		}

		private void OnTextChanged(object sender, EventArgs e)
		{
		}
	}
}
