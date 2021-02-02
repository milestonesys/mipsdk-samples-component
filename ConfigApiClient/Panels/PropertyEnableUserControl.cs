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
	/// <summary>
	/// Build a single line with a tickmark and one single property
	/// </summary>
	public partial class PropertyEnableUserControl : UserControl
	{
		private EventHandler _valueChangedHandler;
        private ConfigApiClient _configApiClient;
        private ConfigurationItem _item;

        public PropertyEnableUserControl(ConfigurationItem item, EventHandler valueChangedHandler, int leftOffset, ConfigApiClient configApiClient, string toHaveFocus = null)
		{
			InitializeComponent();

            _item = item;
            _valueChangedHandler = valueChangedHandler;
            _configApiClient = configApiClient;

            if (item.EnableProperty != null)
            {
                EnabledCheckBox.Checked = item.EnableProperty.Enabled;
                EnabledCheckBox.Text = item.EnableProperty.DisplayName;
            } else
            {
                EnabledCheckBox.Visible = false;
            }

		    this.Height = PanelUtils.BuildPropertiesUI(item, 0, leftOffset, panelContent, valueChangedHandler, _configApiClient, toHaveFocus);
		}

		private void OnEnableChanged(object sender, EventArgs e)
		{
            _item.EnableProperty.Enabled = EnabledCheckBox.Checked;
            panelContent.Enabled = EnabledCheckBox.Checked;
			if (_valueChangedHandler != null)
				_valueChangedHandler(this, new EventArgs());
		}
	}
}
