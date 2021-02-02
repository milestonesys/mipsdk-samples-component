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
	public partial class SliderPropertyUserControl : PropertyUserControl
	{
		private int _min = 0;
		private int _max = 100;
        private int _step = 1;
		private int _origY1;
		private int _origY2;


		public SliderPropertyUserControl(Property property) : base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;

			if (property.ValueTypeInfos != null)
			{
				foreach (ValueTypeInfo vtd in property.ValueTypeInfos)
				{
					if (vtd.Name == ValueTypeInfoNames.MinValue)
						Int32.TryParse((String) vtd.Value, out _min);
					if (vtd.Name == ValueTypeInfoNames.MaxValue)
						Int32.TryParse((String) vtd.Value, out _max);
                    if (vtd.Name == ValueTypeInfoNames.StepValue)
                    {
                        Int32.TryParse((String) vtd.Value, out _step);
                        hScrollBar1.SmallChange = _step;
                    }
				}
			}
			hScrollBar1.Minimum = _min;
			hScrollBar1.Maximum = _max + hScrollBar1.LargeChange-1;

			int current = 0;
			Int32.TryParse((String)property.Value, out current);
			hScrollBar1.Value = current;

			textBoxValue.Text = "" + hScrollBar1.Value;
			HasChanged = false;

			_origY1 = textBoxValue.Left;
			_origY2 = hScrollBar1.Left;
		}

		internal override int LeftIndent
		{
			set
			{
				textBoxValue.Left = _origY1 - value;
				hScrollBar1.Left = _origY2 - value;
			}
		}

		private void OnScroll(object sender, ScrollEventArgs e)
		{
			HasChanged = true;
			if (ValueChanged != null)
			{
                int test = hScrollBar1.Value;
                if (test < _min) test = _min;
                if (test > _max) test = _max;
                if (test % _step != 0)
                {
                    test = test - test % _step;
                }
                Property.Value = test.ToString();
                ValueChanged(this, new EventArgs());
            }
            textBoxValue.Text = "" + Property.Value;
		}

	}
}
