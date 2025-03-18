using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace ConfigAccessViaSDK
{
    /// <summary>
    /// Interaction logic for ConfigAccess.xaml
    /// </summary>
    public partial class ConfigAccess : VideoOSWindow, INotifyPropertyChanged
    {
        private ConfigManager _configManager = new ConfigManager();
        private object _localConfigurationChangedIndicationReference, _configurationChangedIndicationReference;
        private bool _loggingIn = false;

        public ConfigAccess()
        {
            InitializeComponent();
            DataContext = this;
        }

        private bool _loggedIn = true;

        public bool IsLoggedIn
        {
            get
            {
                return _loggedIn;
            }
            set
            {
                _loggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(IsLoggedOut));
            }
        }
        public bool IsLoggedOut
        {
            get
            {
                return !_loggedIn;
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _dumpConfigurationUC.FillContent();
            _dumpConfigurationUC.FillDisabledItems();

            _configManager = new ConfigManager();
            _configManager.Init();

            EnvironmentManager.Instance.EnvironmentOptions[EnvironmentOptions.ConfigurationChangeCheckInterval] = "10";

            // Setup to receive a message when the local memory copy of the configuration is updated:
            _localConfigurationChangedIndicationReference = EnvironmentManager.Instance.RegisterReceiver(LocalConfigUpdatedHandler,
                new MessageIdFilter(MessageId.System.LocalConfigurationChangedIndication));
            _configurationChangedIndicationReference = EnvironmentManager.Instance.RegisterReceiver(ConfigUpdatedHandler,
                new MessageIdFilter(MessageId.Server.ConfigurationChangedIndication));
        }

        private object LocalConfigUpdatedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            if (!_loggingIn) // While logging in configuration will be updated and requesting a UI refresh at this point can cause a deadlock. Login method will refresh UI itself anyways.
            {
                // Update tree view
                Dispatcher.Invoke(() => _dumpConfigurationUC.FillContentSpecific());
            }
            return null;
        }

        private object ConfigUpdatedHandler(VideoOS.Platform.Messaging.Message message, FQID dest, FQID source)
        {
            _dumpConfigurationUC.ShowInfo("MIP config changed: " + message.RelatedFQID.Kind);
            return null;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            VideoOS.Platform.SDK.Environment.Logout();
            IsLoggedIn = false;
            _dumpConfigurationUC.Clear();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _loggingIn = true;
            LoginSettings ls = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite);
            VideoOS.Platform.SDK.Environment.Login(
                ls.Uri, App.integrationId,
                App.integrationName,
                App.version,
                App.manufacturerName,
                true);
            IsLoggedIn = VideoOS.Platform.SDK.Environment.IsLoggedIn(ls.Uri);
            _dumpConfigurationUC.FillContent();
            _loggingIn = false;
        }

        private void ShowLicense(object sender, RoutedEventArgs e)
        {
            string lic = "SLC: " + EnvironmentManager.Instance.SystemLicense.SLC + Environment.NewLine +
             "Expire: " + EnvironmentManager.Instance.SystemLicense.Expire.ToLongDateString() + Environment.NewLine;
            foreach (String feature in EnvironmentManager.Instance.SystemLicense.FeatureFlags.Where(ff => !string.IsNullOrEmpty(ff)))
            {
                lic += "Feature: " + feature + Environment.NewLine;
            }
            lic += "ProductCode: " + EnvironmentManager.Instance.SystemLicense.ProductCode + Environment.NewLine;
            foreach (String key in EnvironmentManager.Instance.SystemLicense.ExpirationDateTimes.Keys)
            {
                lic += "Expiration of:" + key + " is " + EnvironmentManager.Instance.SystemLicense.ExpirationDateTimes[key].ToLongDateString() + Environment.NewLine;
            }
            VideoOSMessageBox.Show(
                this,
                "License",
                "License",
                $"{lic}",
                VideoOSMessageBox.Buttons.OK);
        }

        private void OnClose(object sender, EventArgs e)
        {
            EnvironmentManager.Instance.UnRegisterReceiver(_localConfigurationChangedIndicationReference);
            EnvironmentManager.Instance.UnRegisterReceiver(_configurationChangedIndicationReference);
            _configManager.Close();
            Close();
        }

        private void triggerButton_Click(object sender, RoutedEventArgs e)
        {
            var item = _dumpConfigurationUC.GetSelectedItem();
            if (item == null)
            {
                VideoOSMessageBox.Show(
                    this,
                    "Item missing",
                    "Item Missing",
                    "Select an item",
                    VideoOSMessageBox.Buttons.OK);
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
                ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
                itemPicker.KindsFilter = new List<Guid> { Kind.Camera };
                itemPicker.Items = Configuration.Instance.GetItems();

                if (itemPicker.ShowDialog().Value)
                {
                    EnvironmentManager.Instance.PostMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand,
                            itemPicker.SelectedItems.First().FQID
                        ),
                        item.FQID
                    );

                    return;
                }
                else
                {
                    EnvironmentManager.Instance.PostMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand
                        ),
                        item.FQID
                    );

                    return;
                }
            }

            // For More information see "Introduction to Controlling Output, PTZ and Matrix" in the MIP SDK Documentation

            // Kind.Matrix

            // Trigger of Matrix only makes sense with a relatedFQID, 
            // the relatedFQID will identify the camera being pushed to the matrix

            if (item.FQID.Kind == Kind.Matrix)
            {
                ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
                itemPicker.KindsFilter = new List<Guid> { Kind.Matrix };
                itemPicker.Items = Configuration.Instance.GetItems();

                if (itemPicker.ShowDialog().Value)
                {
                    EnvironmentManager.Instance.PostMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand,
                            itemPicker.SelectedItems.First().FQID
                        ),
                        item.FQID
                    );
                    return;
                }
                else
                {
                    VideoOSMessageBox.Show(
                        this,
                        "Missing camera trigger",
                        "Missing camera trigger",
                        "To trigger matrix without a camera is not useful.",
                        VideoOSMessageBox.Buttons.OK);
                    return;
                }
            }

            // Kind.Output and Kind.Preset (and Kind.TriggerEvent in E-code)

            // If not user defined event (triggerEvent) or matrix a relatedFQID is not relevant and not supported.
            // Trigger of outputs and PTZ Presets are the items that can be triggered which are not already mentioned.

            EnvironmentManager.Instance.PostMessage(
                        new VideoOS.Platform.Messaging.Message(
                            VideoOS.Platform.Messaging.MessageId.Control.TriggerCommand), item.FQID);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
