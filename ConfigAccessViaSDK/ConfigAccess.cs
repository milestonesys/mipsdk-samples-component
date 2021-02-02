using System;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.Messaging;

namespace ConfigAccessViaSDK
{
	public partial class ConfigAccess : Form
	{
	    private ConfigManager _configManager;
        private object _localConfigurationChangedIndicationReference;

        private static readonly Guid IntegrationId = new Guid("6F31CE44-3E9D-44A5-95CF-2E17FABC494D");
        private const string IntegrationName = "Config Access via SDK";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        public ConfigAccess()
		{
			InitializeComponent();

		}

		private void OnLoad(object sender, EventArgs e)
		{
			dumpConfigurationUserControl1.FillContentAsync();
            dumpConfigurationUserControl1.FillDisabledItems();

            _configManager = new ConfigManager();
		    _configManager.Init();

            EnvironmentManager.Instance.EnvironmentOptions[EnvironmentOptions.ConfigurationChangeCheckInterval] = "10";

            // Setup to receive a message when the local memory copy of the configuration is updated:
            _localConfigurationChangedIndicationReference = EnvironmentManager.Instance.RegisterReceiver(LocalConfigUpdatedHandler,
                new MessageIdFilter(MessageId.System.LocalConfigurationChangedIndication));
            _localConfigurationChangedIndicationReference = EnvironmentManager.Instance.RegisterReceiver(ConfigUpdatedHandler,
                new MessageIdFilter(MessageId.Server.ConfigurationChangedIndication));

        }


        private void OnClose(object sender, EventArgs e)
	    {
            EnvironmentManager.Instance.UnRegisterReceiver(_localConfigurationChangedIndicationReference);
            _configManager.Close();
			Close();
		}


        private object LocalConfigUpdatedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            // Update UI - if name changed
            dumpConfigurationUserControl1.SelectedDevices = dumpConfigurationUserControl1.SelectedDevices;
            return null;
        }

        private object ConfigUpdatedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            dumpConfigurationUserControl1.ShowInfo("MIP config changed: " + message.RelatedFQID.Kind);
            return null;
        }


        private void ShowLicense(object sender, EventArgs e)
		{
			string lic = "SLC=" + EnvironmentManager.Instance.SystemLicense.SLC + Environment.NewLine +
			             "Expire=" + EnvironmentManager.Instance.SystemLicense.Expire.ToLongDateString() + Environment.NewLine;
            foreach (String feature in EnvironmentManager.Instance.SystemLicense.FeatureFlags)
            {
                lic += "Feature: " + feature + Environment.NewLine;
            }
            lic += "ProductCode: " + EnvironmentManager.Instance.SystemLicense.ProductCode + Environment.NewLine;
		    foreach (String key in EnvironmentManager.Instance.SystemLicense.ExpirationDateTimes.Keys)
		    {
                lic += "Expiration of:" + key + " is " + EnvironmentManager.Instance.SystemLicense.ExpirationDateTimes[key].ToLongDateString() + Environment.NewLine;		        
		    }
			MessageBox.Show(lic, "License");
		}

		private void OnHierarchyChanged(object sender, EventArgs e)
		{
			dumpConfigurationUserControl1.FillContentSpecific(checkBox1.Checked ? ItemHierarchy.SystemDefined : ItemHierarchy.UserDefined);
		}

        private void button2_Click(object sender, EventArgs e)
        {
            VideoOS.Platform.SDK.Environment.Logout();
            dumpConfigurationUserControl1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginSettings ls = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite);
            VideoOS.Platform.SDK.Environment.Login(ls.Uri, IntegrationId, IntegrationName, Version, ManufacturerName);
            dumpConfigurationUserControl1.FillContentAsync();
        }

        private void buttonTrigger_Click(object sender, EventArgs e)
        {
            Item item = dumpConfigurationUserControl1.GetSelectedItem();
            if (item == null)
            {
                MessageBox.Show("Select an item");
                return;
            }

            // Kind.TriggerEvent

            // If the serverType is CorporateManagementServerType and it is user-defined event
            // triggering an event with a camera FQID (as a releatedFQID) is supported.
            // This is useful because you can then create and use rules "Use devices from metadata"
            // to determine on which device to do the action.
            // If you do not need a parameter with the trigger you can leave it out, the trigger 
            // command will work without a releatedFQID.

            if (item.FQID.Kind == Kind.TriggerEvent && item.FQID.ServerId.ServerType == ServerId.CorporateManagementServerType)
            {
                var form = new VideoOS.Platform.UI.ItemPickerForm();
                form.KindFilter = Kind.Camera;
                form.Init();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    EnvironmentManager.Instance.SendMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand,
                            form.SelectedItem.FQID), item.FQID);
                    return;
                }
                else   // if cancel trigger without relatedFQID
                {
                    EnvironmentManager.Instance.SendMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand), item.FQID);
                    return;
                }
            }

            // For More information see "Introduction to Controlling Output, PTZ and Matrix" in the MIP SDK Documentation

            // Kind.Matrix

            // Trigger of Matrix only makes sense with a relatedFQID, 
            // the relatedFQID will identify the camera being pushed to the matrix

            if (item.FQID.Kind == Kind.Matrix)
            {
                var form = new VideoOS.Platform.UI.ItemPickerForm();
                form.KindFilter = Kind.Camera;
                form.Init();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    EnvironmentManager.Instance.SendMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand,
                            form.SelectedItem.FQID), item.FQID);
                    return;
                }
                else
                {
                    MessageBox.Show("To trigger matrix without a camera is not useful.");
                    return;
                }
            }

            // Kind.Output and Kind.Preset (and Kind.TriggerEvent in E-code)

            // If not user defined event (triggerEvent) or matrix a relatedFQID is not relevant and not supported.
            // Trigger of outputs and PTZ Presets are the items that can be triggered which are not already mentioned.

            EnvironmentManager.Instance.SendMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand), item.FQID);
        }
	}
}
