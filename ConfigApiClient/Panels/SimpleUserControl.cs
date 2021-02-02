using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	public partial class SimpleUserControl : UserControl
	{
		private ConfigurationItem _item;
        private ConfigurationItem _privacyMaskItem;
        private string _originalName = "";
        private ConfigApiClient _configApiClient;
        private bool _showChildren;
        private UI.PrivacyMaskUserControl _privacyMaskUserControl;
        private UI.MotionDetectUserControl _motionDetectMaskUserControl;

        public SimpleUserControl(ConfigurationItem item, bool showChildren, ConfigApiClient configApiClient)
        {
            InitializeComponent();

            _configApiClient = configApiClient;
            _showChildren = showChildren;
            _item = item;
            _privacyMaskItem = null;
            InitalizeUI();
        }

        public SimpleUserControl(ConfigurationItem item, bool showChildren, ConfigApiClient configApiClient, ConfigurationItem privacyMaskItem)
		{
			InitializeComponent();

            _configApiClient = configApiClient;
            _showChildren = showChildren;
			_item = item;
            _privacyMaskItem = privacyMaskItem;
            InitalizeUI();
		}

        private void InitalizeUI(string propertyToFocus = null)
        {
            buttonSave.Enabled = (_item.ItemCategory != ItemCategories.Group);

            scrollPanel1.Clear();
            _originalName = _item.DisplayName;
            textBoxName.Text = _item.DisplayName;
            textBoxName.ReadOnly = true;

            if (_item.EnableProperty != null)
            {
                EnabledCheckBox.Text = _item.EnableProperty.DisplayName;
                EnabledCheckBox.Checked = _item.EnableProperty.Enabled;
                scrollPanel1.EnableContent = _item.EnableProperty.Enabled || !_item.EnableProperty.UIToFollowEnabled;
            }
            else
            {
                EnabledCheckBox.Visible = false;
            }

            pictureBox1.Image = UI.Icons.IconListBlack.Images[UI.Icons.GetImageIndex(_item.ItemType)];

            if (!_item.ChildrenFilled)
            {
                try
                {
                    _item.Children = _configApiClient.GetChildItems(_item.Path);
                }
                catch (Exception)
                {
                    _item.Children = new ConfigurationItem[0];
                }
                _item.ChildrenFilled = true;
            }

            int top = 10; // panelId.Height;
            if (_item.Properties != null)
            {
                top = ConfigAPIClient.Panels.PanelUtils.BuildPropertiesUI(_item, top, 0, scrollPanel1, ValueChangedHandler, _configApiClient, propertyToFocus);
                if (_item.ItemType == ItemTypes.PrivacyMask)
                {
                    _privacyMaskUserControl = new UI.PrivacyMaskUserControl(_item, _configApiClient);
                    _privacyMaskUserControl.Location = new Point(80, top);
                    top += _privacyMaskUserControl.Height + 10;
                    scrollPanel1.Add(_privacyMaskUserControl);
                }
                else if (_item.ItemType == ItemTypes.MotionDetection)
                {
                    _motionDetectMaskUserControl = new UI.MotionDetectUserControl(_item, _privacyMaskItem, _configApiClient);
                    _motionDetectMaskUserControl.Location = new Point(80, top);
                    top += _motionDetectMaskUserControl.Height + 10;
                    scrollPanel1.Add(_motionDetectMaskUserControl);
                }
                else
                {
                    _privacyMaskUserControl = null;
                    _motionDetectMaskUserControl = null;
                }
                
            }

            // Show command buttons
            if (_item.MethodIds != null && _item.MethodIds.Length > 0)
            {
                foreach (String id in _item.MethodIds)
                {
                    if (_configApiClient.AllMethodInfos.ContainsKey(id))
                    {
                        MethodInfo mi = _configApiClient.AllMethodInfos[id];
                        Button b = new Button() { Text = mi.DisplayName, Tag = mi, UseVisualStyleBackColor = true };
                        ToolTip toolTip = new ToolTip();
                        toolTip.SetToolTip(b, mi.MethodId);
                        int width = mi.DisplayName.Length < 20 ? 150 : 300;
                        b.Size = new System.Drawing.Size(width, 24);
                        b.Location = new Point(10, top);
                        top += b.Height + 10;
                        b.Click += PerformAction;
                        scrollPanel1.Add(b);
                    }
                }
            }

            if (_showChildren && _item.Children != null)
            {
                int leftOffset = Constants.LeftIndentChildControl;
                foreach (ConfigurationItem child in _item.Children)
                {
                    if (MainForm._navItemTypes.Contains(child.ItemType))    // Do not repeat what is on the navigation tree
                        continue;

                    if ((child.Children == null || child.Children.Length == 0) && child.EnableProperty != null)
                    {
                        UserControl uc = new PropertyEnableUserControl(child, ValueChangedHandler, leftOffset, _configApiClient, null);
                        uc.Width = scrollPanel1.Width;
                        uc.Location = new Point(leftOffset, top);
                        uc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        top += uc.Height;
                        scrollPanel1.Add(uc);
                    }
                    else
                    {
                        UserControl uc = new PropertyListUserControl(child, ValueChangedHandler, leftOffset, _configApiClient, null);
                        uc.Width = scrollPanel1.Width;
                        uc.Location = new Point(leftOffset, top);
                        uc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        top += uc.Height;
                        scrollPanel1.Add(uc);
                    }

                    if (_item.ItemCategory == ItemCategories.Group)
                    {
                        // Add save button on individual items as group can not be saved
                        Button b = new Button() { Text = "Save", Tag = child, UseVisualStyleBackColor = true };
                        b.Size = new System.Drawing.Size(75, 24);
                        b.Location = new Point(10, top);
                        top += b.Height + 10;
                        b.Click += PerformItemSave;
                        scrollPanel1.Add(b);
                    }
                }
            }

            if (propertyToFocus == null)
                scrollPanel1.ScrollToTop();
        }

		void ValueChangedHandler(object sender, EventArgs e)
		{
			if (_item.ItemCategory != ItemCategories.Group)
                buttonSave.Enabled = true;
            if (_privacyMaskUserControl != null)
                _privacyMaskUserControl.Refresh();
            if (_motionDetectMaskUserControl != null)
                _motionDetectMaskUserControl.Refresh();

            PropertyUserControl c = sender as PropertyUserControl;
            if (c != null)
            {
                if (c.Property.ServerValidation)
                {
                    PerformItemValidation(sender);
                }
            }
            // TODO: Enable/disable save button on child items?
		}
        private string _sessionDataId = "0";

        private void PerformAction(object sender, EventArgs e)
        {
            MethodInfo mi = ((Control)sender).Tag as MethodInfo;
            if (mi != null)
            {
                try
                {
                    ConfigurationItem result = _configApiClient.InvokeMethod(_item, mi.MethodId);
                    if (result != null)
                    {
                        Property sessionDataProperty = result.Properties.FirstOrDefault<Property>(p => p.Key == "SessionDataId");
                        if (sessionDataProperty != null)
                        {
                            sessionDataProperty.Value = _sessionDataId;
                            _sessionDataId = "0";
                        }

                        MethodInvokeForm form = new MethodInvokeForm(result, _configApiClient);
                        form.ShowDialog();

                        _sessionDataId = form.SessionDataId;
                        //if (_item.ItemCategory != ItemCategories.ChildItem)
                        //    _item = _configApiClient.GetItem(_item.Path);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to perform action:" + ex.Message);
                }
            }

            InitalizeUI();
            MainForm.UpdateTree();
        }

        private void PerformItemSave(object sender, EventArgs e)
        {
            ConfigurationItem childItem = ((Control)sender).Tag as ConfigurationItem;
            if (childItem == null)
                return;

            try
            {
                ValidateResult result = _configApiClient.ValidateItem(childItem);
                if (result.ValidatedOk)
                {
                    _configApiClient.SetItem(childItem);
                    // Reload parent item
                    _item = _configApiClient.GetItem(_item.Path);
                }
                else
                {
                    if (result.ErrorResults.Any())
                    {
                        MessageBox.Show(result.ErrorResults[0].ErrorText, "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Save:" + ex.Message);
            }

            InitalizeUI();
        }

        private void PerformItemValidation(object sender)
        {
            string nextToFocusOn = null;
            try
            {
                ValidateResult result = _configApiClient.ValidateItem(_item);
                _item = result.ResultItem;
                if (!result.ValidatedOk)
                {
                    if (result.ErrorResults.Any())
                    {
                        //MessageBox.Show(result.ErrorResults[0].ErrorText, "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        nextToFocusOn = result.ErrorResults[0].ErrorProperty;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Validate on server:" + ex.Message);
            }

            InitalizeUI(nextToFocusOn);  
        }


        private void OnSave(object sender, EventArgs e)
		{
			try
			{
                ValidateResult result = _configApiClient.ValidateItem(_item);
				if (result.ValidatedOk)
				{
                    string path = _item.Path;
                    _item = _configApiClient.SetItem(_item).ResultItem;
                    if (_item == null)
                        _item = _configApiClient.GetItem(path);

                    buttonSave.Enabled = false;
				} 
                else
				{
					if (result.ErrorResults.Any())
					{
						MessageBox.Show(result.ErrorResults[0].ErrorText, "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
                    if (result.ResultItem != null)
                        _item = result.ResultItem;      // new From 2018, March
				}
			} catch (Exception ex)
			{
				MessageBox.Show("Unable to Save:" + ex.Message);
			}
			buttonSave.Enabled = false;
            if (_originalName != _item.DisplayName)
            {
                MainForm.UpdateTree();
                if (this.Parent is TabPage)
                {
                    this.Parent.Text = _item.DisplayName;
                }
            } 
            InitalizeUI();
		}

		private void OnEnabledChanged(object sender, EventArgs e)
		{
			if (EnabledCheckBox.Visible && _item!=null && _item.EnableProperty!=null)
			{
				_item.EnableProperty.Enabled = EnabledCheckBox.Checked;
				if (_item.EnableProperty.UIToFollowEnabled)
				{
					scrollPanel1.EnableContent = _item.EnableProperty.Enabled || !_item.EnableProperty.UIToFollowEnabled;					
				}
			}
		}

	}
}
