using System;
using System.Drawing;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	public partial class PropertyUserControl : UserControl
	{
		internal PropertyUserControl()
		{
            InitializeComponent();
        }
        
		public PropertyUserControl(Property property)
		{
			Property = property;
			InitializeComponent();

            GotFocus += GotFocusHandling;
		}

        private void GotFocusHandling(object sender, EventArgs e)
        {
            WeGotFocus(sender, e);
        }

        internal EventHandler WeGotFocus = delegate { };

        internal virtual int LeftIndent { set {} }

		internal bool HasChanged { get; set; }
        internal EventHandler ValueChanged = delegate { };
		internal Property Property { get; set; }
        internal String ToolTip { get; set; }

        internal void OnMouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(sender as Control, ToolTip);
            toolTip1.Show(ToolTip, sender as Control, 5000);
        }

        internal void OnMouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(sender as Control);
        }

	}
}
