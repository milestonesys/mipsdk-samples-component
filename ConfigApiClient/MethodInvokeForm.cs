using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigAPIClient.Panels;
using ConfigAPIClient;
using VideoOS.ConfigurationAPI;
using ConfigAPIClient.Panels.PropertyUserControls;

namespace ConfigAPIClient
{
	public partial class MethodInvokeForm : Form
	{
		private ConfigurationItem _item;
		private String _refreshId;
        private ConfigApiClient _configApiClient;

        public MethodInvokeForm(ConfigurationItem item, ConfigApiClient configApiClient)
		{
			InitializeComponent();

            _configApiClient = configApiClient;
            _item = item;
            SessionDataId = "0";

			FillForm();
		}

        public ConfigurationItem ResultItem { get { return _item; } }
        public string SessionDataId { get; set; }


        private void FillForm()
		{
            this.scrollPanel1.Clear();

			this.Text = _item.DisplayName;

			int y = 20; 
			foreach (Property property in _item.Properties)
			{
                if (property.UIImportance != UIImportance.Hidden || MainForm.ShowHiddenProperties)
				{
					PropertyUserControl uc;
                    if (property.TranslationId == "PropertyBasicUserSid")
                    {
                        ConfigurationItem[] users = _configApiClient.GetChildItems("/" + ItemTypes.BasicUserFolder);
                        uc = new SidPropertyUserControl(property, users);
                    }
                    else switch (property.ValueType)
					{
						case ValueTypes.IntType:
							uc = new IntPropertyUserControl(property);
							break;
						case ValueTypes.DoubleType:
							uc = new DoublePropertyUserControl(property);
							break;
						case ValueTypes.TickType:
							uc = new TickPropertyUserControl(property);
							break;
						case ValueTypes.EnumType:
							uc = new EnumPropertyUserControl(property);
							break;
						case ValueTypes.SliderType:
							uc = new SliderPropertyUserControl(property);
							break;
						case ValueTypes.ProgressType:
							ProgressPropertyUserControl progressuc = new ProgressPropertyUserControl(property);
					        uc = progressuc;
							break;
                        case ValueTypes.Path:
					        uc = new PathPropertyUserControl(property, _configApiClient);
                            break;
                        case ValueTypes.PathList:
                            uc = new PathListPropertyUserControl(property, _configApiClient);
                            break;
                        case ValueTypes.DateTimeType:
                        case "Time":
                        case "Date":
                            if (property.IsSettable)
                                uc = new DateTimePickerPropertyUserControl(property);
                            else
                                uc = new DateTimeDisplayPropertyUserControl(property);
                            break;

                        case ValueTypes.SeparatorType:
                            uc = new SeperatorPropertyUserControl(property);
                            break;
                        case ValueTypes.Array:
                            uc = new ArrayPropertyUserControl(property);
                            break;
                        case ValueTypes.StringType:
						default:
							uc = new StringPropertyUserControl(property);
							break;
					}
					uc.Location = new Point(0, y);
					uc.Tag = property;
					uc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
					uc.ValueChanged += new EventHandler(ValueChangedHandler);
                    if (property.ToolTipTranslationId != null)
                    {
                        uc.ToolTip = _configApiClient.Translate(property.ToolTipTranslationId);
                    }

                    if (property.UIImportance == 3)
                        uc.Enabled = false;

                    if (property.Key == "SessionDataId")
                        SessionDataId = property.Value;

                    scrollPanel1.Add(uc);
                    y += uc.Height;
				}
			}

			if (_item.Children != null && _item.Children.Any())
			{
                if (_item.Children[0].ItemType == ItemTypes.Task)
                {
                    UserControl folderUC = new TaskUserControl(_item, _item.Children, _configApiClient)
                    { Location = new Point(0, y), Size = new Size(this.Width, this.Height - y) };
                    scrollPanel1.Add(folderUC);
                }
                else
                {
                    UserControl folderUC = new FolderUserControl(_item, _item.Children)
                    { Location = new Point(0, y), Size = new Size(this.Width, this.Height - y) };
                    scrollPanel1.Add(folderUC);
                }
            }

            panelMain.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            int x = this.Width - 24;
			if (_item.MethodIds != null)
			{
				for (int i = _item.MethodIds.Length - 1; i > -1; i--)
				{
                    MethodInfo mi = _configApiClient.AllMethodInfos[_item.MethodIds[i]];
                    if (mi==null)
                    {
                        MessageBox.Show("Method is missing:" + _item.MethodIds[i]);
                        timer1.Stop();
                        this.Close();
                    }
					Button b = new Button() {Text = mi.DisplayName, Tag = mi, UseVisualStyleBackColor=true};
					b.Size = new System.Drawing.Size(150, 24);
					x -= 150;
					b.Location = new Point(x, 6);
					x -= 24;
					b.Click += PerformAction;
					panelActions.Controls.Add(b);
				}
			}
            string text = _item.MethodIds == null || _item.MethodIds.Length == 0 ? "Close" : "Cancel";
            Button cancel = new Button() { Text = text, UseVisualStyleBackColor = true };
			cancel.Size = new System.Drawing.Size(100, 24);
			x -= 100;
			cancel.Location = new Point(x, 6);
			cancel.Click += PerformCancel;
			panelActions.Controls.Add(cancel);

            Property pathProperty = _item.Properties.FirstOrDefault<Property>(p => p.Key == InvokeInfoProperty.Path);
            if (pathProperty != null)
            {
                if (pathProperty.Value.StartsWith("Task"))
                {
                    taskPath = pathProperty.Value;
                    timer1.Start();
                }
            }
        }

        private string taskPath = null;
		private void PerformAction(object sender, EventArgs e)
		{
			MethodInfo mi = ((Control)sender).Tag as MethodInfo;
			if (mi!=null)
			{
				// changed properties are stored directy in the _item class, no need to read here...
				_refreshId = mi.MethodId;
                try
                {
                    ConfigurationItem result = _configApiClient.InvokeMethod(_item, mi.MethodId);

                    if (result != null && result.ItemType == ItemTypes.InvokeInfo)
                    {
                        _item = result;
                        scrollPanel1.Clear();
                        panelActions.Controls.Clear();
                        FillForm();

                        timer1.Stop();
                        timer1.Dispose();
                    }
                    else //if (result != null && result.ItemType == ItemTypes.InvokeResult)
                    {
                        _item = result;
                        scrollPanel1.Clear();
                        panelActions.Controls.Clear();
                        FillForm();

                        Property stateProperty = result.Properties.FirstOrDefault<Property>(p => p.Key == InvokeInfoProperty.State);
                        Property pathProperty = result.Properties.FirstOrDefault<Property>(p => p.Key == InvokeInfoProperty.Path);
                        if (stateProperty != null && stateProperty.Value == InvokeInfoStates.Success && !MainForm.ShowInvokeResult)
                        {
                            // All done and no error, just close dialog
                            this.Close();
                        }
                        if (pathProperty != null)
                        {
                            if (pathProperty.Value.StartsWith("Task"))
                                taskPath = pathProperty.Value;
                        }
                        Property sessionDataProperty = result.Properties.FirstOrDefault<Property>(p => p.Key == "SessionDataId");
                        if (sessionDataProperty != null)
                        {
                            SessionDataId = sessionDataProperty.Value;
                        }

                    }
                    /*
                    else
                    {
                        timer1.Stop();
                        timer1.Dispose();
                        this.Close();
                    }*/
                } catch (Exception ex)
                {
                    timer1.Stop();
                    timer1.Dispose();
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
			}
		}

		private void PerformCancel(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		void ValueChangedHandler(object sender, EventArgs e)
		{
            PropertyUserControl c = sender as PropertyUserControl;
            if (c != null)
            {
                if (c.Property.ServerValidation)
                {
                    PerformItemValidation(sender);
                }
            }
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
                        MessageBox.Show(result.ErrorResults[0].ErrorText, "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        nextToFocusOn = result.ErrorResults[0].ErrorProperty;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Validate on server:" + ex.Message);
            }

            FillForm();// nextToFocusOn);
        }


        private void OnTimerTick(object sender, EventArgs e)
		{
            try
            {
                if (_item != null && _item.ItemType == ItemTypes.InvokeResult || _item.ItemType == ItemTypes.Task)
                {
                    if (taskPath != null)
                    {
                        _item = _configApiClient.GetItem(taskPath);
                        if (_item != null)
                        {
                            this.SuspendLayout();
                            scrollPanel1.Clear();
                            panelActions.Controls.Clear();
                            FillForm();
                            this.ResumeLayout();

                            Property stateProperty = _item.Properties.FirstOrDefault<Property>(p => p.Key == InvokeInfoProperty.State);
                            Property pathProperty = _item.Properties.FirstOrDefault<Property>(p => p.Key == InvokeInfoProperty.Path);
                            if (pathProperty != null)
                            {
                                if (pathProperty.Value.StartsWith("Task"))
                                    taskPath = pathProperty.Value;
                            }
                            if (stateProperty != null && (stateProperty.Value == "Success" || stateProperty.Value == "Error" || stateProperty.Value == "Completed"))
                                timer1.Stop();
                        }
                        else
                        {
                            timer1.Stop();
                        }
                    }
                }
                else
                {
                    ConfigurationItem result = _configApiClient.InvokeMethod(_item, _refreshId);

                    if (result != null && result.ItemType == ItemTypes.InvokeResult)
                    {
                        _item = result;
                        this.SuspendLayout();
                        scrollPanel1.Clear();
                        panelActions.Controls.Clear();
                        FillForm();
                        this.ResumeLayout();
                    }
                    else
                    {
                        timer1.Stop();
                        timer1.Dispose();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _configApiClient.InitializeClientProxy();
                timer1.Stop();
                timer1.Dispose();
                MessageBox.Show(ex.Message);
                this.Close();
            }
		}

	}
}
