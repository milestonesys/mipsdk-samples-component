using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigAPIClient.Panels
{
	/*
	public partial class GroupUserControl : UserControl
	{
		private int _initialHeight = 0;	// This field is here in case we want to make a graphical GroupBox border at some point
		private int _leftAlign = 25;
		private int _midAlign = 95;
		private int _rightAlign = 230;
		private int _vertSpacing = 5;

		public GroupUserControl()
		{
			InitializeComponent();
			this.Height = _initialHeight;
		}

		public int LeftAlignPos
		{
			get { return _leftAlign; }
			set { _leftAlign = value; }
		}

		public int RightAlignPos
		{
			get { return _rightAlign; }
			set { _rightAlign = value; }
		}

		public void AddLeft(Control control)
		{
			control.Location = new Point(_leftAlign, this.Height + _vertSpacing);
			panelContent.Controls.Add(control);

			if (control.Top + control.Height+_initialHeight > this.Height)
			{
				this.Height = control.Top + this.Height +_initialHeight;
			}
		}

		public void AddRight(Control control)
		{
			control.Location = new Point(_rightAlign, this.Height + _vertSpacing);
			panelContent.Controls.Add(control);

			if (control.Top + control.Height + _initialHeight > this.Height)
			{
				this.Height = control.Top + this.Height + _initialHeight;
			}
		}
	
	}
	 * */
}
