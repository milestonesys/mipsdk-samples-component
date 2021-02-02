using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	public partial class DoublePropertyUserControl : PropertyUserControl
	{
		private double _min = double.MinValue;
		private double _max = double.MaxValue;
		private int _origY;

		public DoublePropertyUserControl(Property property)
			: base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;
			textBoxValue.Text = property.Value.ToString();

			_prevValue = textBoxValue.Text;

			textBoxValue.Enabled = property.IsSettable;

			HasChanged = false;

			if (property.ValueTypeInfos != null)
			{
				foreach (ValueTypeInfo vtd in property.ValueTypeInfos)
				{
					if (vtd.Name == ValueTypeInfoNames.MinValue)
						_min = double.Parse(vtd.Value,CultureInfo.InvariantCulture);
					if (vtd.Name == ValueTypeInfoNames.MaxValue)
						_max = double.Parse(vtd.Value, CultureInfo.InvariantCulture);
				}
			}
			_origY = textBoxValue.Left;

		}

		internal override int LeftIndent
		{
			set { textBoxValue.Left = _origY - value; }
		}

		private string _prevValue = "";
		private void OnTextChanged(object sender, EventArgs e)
		{
			HasChanged = true;
			if (ValueChanged != null)
			{
				double temp;
				if (double.TryParse(textBoxValue.Text,NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InstalledUICulture, out temp) == false)
				{
					textBoxValue.Text = _prevValue;
				}
				else
				{
                    
					if (!MainForm.ValidateField || (temp >= _min && temp <= _max))
					{
						_prevValue = textBoxValue.Text;
                        Property.Value = temp.ToString(CultureInfo.InvariantCulture);
                        ValueChanged(this, new EventArgs());
					} else
					{
						MessageBox.Show("Keep within values " + _min + " and " + _max);
					}                    
				}
			}
		}

	}
}
