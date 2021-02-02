using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigAPIClient;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	public partial class PathListPropertyUserControl : PropertyUserControl
	{
		private int _origY;
        private List<string> itemTypes = new List<string>();
        private ConfigApiClient _configApiClient;

        public PathListPropertyUserControl(Property property, ConfigApiClient configApiClient)
			: base(property)
		{
			InitializeComponent();

            _configApiClient = configApiClient;

			HasChanged = false;
            _origY = button1.Left;

            labelOfProperty.Text = property.DisplayName;
            string[] parts = !string.IsNullOrEmpty(property.Value)? property.Value.Split(';'): new string[0];
            button1.Text = string.Format("{0} selected. Modify ...", parts.Length);
		}

		internal override int LeftIndent
		{
            set { button1.Left = _origY - value; }
		}

        private void OnClick(object sender, EventArgs e)
        {
            PathListForm form = new PathListForm();
            form.Init(base.Property, _configApiClient);
            form.ValueChanged += this.ValueChanged;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = this.PointToScreen(button1.Location);
            this.Hide();

            if (form.ShowDialog() == DialogResult.OK)
            {
                string[] parts = !string.IsNullOrEmpty(Property.Value) ? Property.Value.Split(';') : new string[0];
                button1.Text = string.Format("{0} selected. Modify ...", parts.Length);
                form.Dispose();
            }
            this.Show();
        }

	}
}
