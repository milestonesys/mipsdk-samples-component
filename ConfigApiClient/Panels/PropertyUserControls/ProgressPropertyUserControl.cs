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
	public partial class ProgressPropertyUserControl : PropertyUserControl
	{
		private int _origY;

		public ProgressPropertyUserControl(Property property)
			: base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;

			int current = 0;
			Int32.TryParse((String)property.Value, out current);
			if (current > 100)
				current = 100;
			progressBar1.Value = current;
			textBoxValue.Text = "" + progressBar1.Value;
			HasChanged = false;
			_origY = textBoxValue.Left;

		}
		internal override int LeftIndent
		{
			set { textBoxValue.Left = _origY - value; }
		}


		internal int CurrentProgress
		{
			get { return progressBar1.Value; }
		}
	}
}
