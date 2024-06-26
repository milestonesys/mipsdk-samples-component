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
	public partial class IntPropertyUserControl : PropertyUserControl
	{
		private int _min = Int32.MinValue;
		private int _max = Int32.MaxValue;
		private int _origY;

		public IntPropertyUserControl(Property property)
			: base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;

			textBoxValue.Text = property.Value.ToString();
			_prevValue = textBoxValue.Text;

			textBoxValue.ReadOnly = !property.IsSettable;
            if (!property.IsSettable)
                textBoxValue.ForeColor = Color.Gray;

            HasChanged = false;

			if (property.ValueTypeInfos != null)
			{
				foreach (ValueTypeInfo vtd in property.ValueTypeInfos)
				{
					if (vtd.Name == ValueTypeInfoNames.MinValue)
						Int32.TryParse((String)vtd.Value, out _min);
					if (vtd.Name == ValueTypeInfoNames.MaxValue)
						Int32.TryParse((String)vtd.Value, out _max);
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
				int temp;
				if (Int32.TryParse(textBoxValue.Text, out temp) == false)
				{
					textBoxValue.Text = _prevValue;
				}
				else
				{
					if (!MainForm.ValidateField || (temp >= _min && temp <= _max))
					{
						_prevValue = textBoxValue.Text;
                        Property.Value = temp.ToString();
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
