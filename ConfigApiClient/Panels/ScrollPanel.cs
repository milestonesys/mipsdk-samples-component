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
	public partial class ScrollPanel : UserControl
	{
		public ScrollPanel()
		{
			InitializeComponent();

		}

		public void Add(Control control)
		{
			panelContent.Controls.Add(control);

			if (control.Top+control.Height > panelContent.Height)
			{
				panelContent.Height = control.Top + control.Height;
                vScrollBar1.Maximum = panelContent.Height - 1;// vScrollBar1.Height;
			}
		}

        public void Clear()
        {
            panelContent.Controls.Clear();
            panelContent.Height = 0;
        }

		internal bool EnableContent
		{
			set { panelContent.Enabled = value; }
			get { return panelContent.Enabled; }
		}

		internal void ScrollToTop()
		{
			vScrollBar1.Value = 0;
			panelContent.Location = new Point(0, -vScrollBar1.Value);
		}

		private void OnScroll(object sender, ScrollEventArgs e)
		{
			panelContent.Location = new Point(0, -vScrollBar1.Value);
		}

		private void OnResize(object sender, EventArgs e)
		{
            int overrun = panelContent.Height - vScrollBar1.Height;
            if (overrun <= 0)
            {
                vScrollBar1.Maximum = vScrollBar1.Height - 1;
                vScrollBar1.LargeChange = vScrollBar1.Height;
                vScrollBar1.Value = 0;
                panelContent.Top = 0;
            }
            else
            {
                vScrollBar1.LargeChange = vScrollBar1.Height / 5;
                vScrollBar1.SmallChange = vScrollBar1.Height / 10;
                vScrollBar1.Maximum = overrun + vScrollBar1.LargeChange + 30;// panelContent.Height - 1;
                if (vScrollBar1.Value > vScrollBar1.Maximum)
                {
                    vScrollBar1.Value = Math.Max(0, vScrollBar1.Maximum - vScrollBar1.Height);
                    panelContent.Top = -vScrollBar1.Value;
                }
            }
		}
	}
}
