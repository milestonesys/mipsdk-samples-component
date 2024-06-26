using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using VideoOS.Platform;
using VideoOS.Platform.SDK.OAuth;
using VideoOS.Platform.UI.Controls;

namespace MultiUserEnvironment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow, INotifyPropertyChanged
    {
        public ObservableCollection<string> ConfigurationMonitorItems { get; } = new ObservableCollection<string>();
        private Uri _serverUrl;
        private object _registeredReceiver;

        private ConfigurationMonitor _configurationMonitor;

#pragma warning disable CS0067 // The event 'MainWindow.PropertyChanged' is never used
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'MainWindow.PropertyChanged' is never used

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            _userContextControl1.CloseJpegLiveSource();
            _userContextControl2.CloseJpegLiveSource();

            if (_registeredReceiver != null)
            {
                EnvironmentManager.Instance.UnRegisterReceiver(_registeredReceiver);
            }
            VideoOS.Platform.SDK.MultiUserEnvironment.RemoveAllServers();
            VideoOS.Platform.SDK.MultiUserEnvironment.UnInitialize();

            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _serverUrl = new UriBuilder(_serverAddressBox.Text).Uri;

                var useOAuth = _useOAuthTokenCheckBox.IsChecked ?? false;
                var secureOnly = _secureOnlyCheckBox.IsChecked ?? false;
                var isAdUser = _negotiateAuthTypeRadioButton.IsChecked ?? false;

                if (useOAuth)
                {
                    MipTokenCache tokenCache = IdpHelper.GetTokenCache(_serverUrl, _userNameBox.Text, _passwordBox.Password, isAdUser);
                    // General initialize for multi user Environment.  Always required
                    VideoOS.Platform.SDK.MultiUserEnvironment.InitializeUsingUserContext(secureOnly, _serverUrl, tokenCache, "");
                }
                else
                {
                    // General initialize for multi user Environment.  Always required
                    VideoOS.Platform.SDK.MultiUserEnvironment.InitializeUsingUserContext(secureOnly, _serverUrl, _userNameBox.Text, _passwordBox.Password, isAdUser, masterOnly: false);
                }

                if (VideoOS.Platform.SDK.MultiUserEnvironment.InitializeLoggedIn == false)
                {
                    MessageBox.Show(@"Incorrect parameters... try again");
                    VideoOS.Platform.SDK.MultiUserEnvironment.UnInitialize();
                    return;
                }

                

                _registeredReceiver = EnvironmentManager.Instance.RegisterReceiver(SystemConfigChangedEvent,
                            new VideoOS.Platform.Messaging.MessageIdFilter(VideoOS.Platform.Messaging.MessageId.System.SystemConfigurationChangedIndication));

                _serverGroupBox.IsEnabled = false;
                _userContext1GroupBox.IsEnabled = true;
                _userContext2GroupBox.IsEnabled = true;

                _configurationMonitor = new ConfigurationMonitor(Configuration.Instance.ServerFQID.ServerId);

                // Pass data to user context controls
                _userContextControl1.ConfigurationMonitor = _configurationMonitor;
                _userContextControl2.ConfigurationMonitor = _configurationMonitor;
                _userContextControl1.ServerUri = _serverUrl;
                _userContextControl2.ServerUri = _serverUrl;
                _userContextControl1.AuthTypeGroupName = "AuthTypeUserContext1";
                _userContextControl2.AuthTypeGroupName = "AuthTypeUserContext2";

                _configurationMonitor.ShowMessage += _configurationMonitor_ShowMessage;
                _configurationMonitor.ConfigurationNowReloaded += _configurationMonitor_ConfigurationNowReloaded;

            }
            catch (Exception ex)
            {
                EnvironmentManager.Instance.ExceptionDialog("Uri Save", ex);
            }
        }

        private void _configurationMonitor_ShowMessage(string message)
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(() => { _configurationMonitor_ShowMessage(message); }));
            }
            else
            {
                string time = DateTime.Now.ToString("T");
                ConfigurationMonitorItems.Add(time + ": " + message);
            }
        }

        //Handler for ConfigurationMonitor configuration reload
        private void _configurationMonitor_ConfigurationNowReloaded()
        {
            FillCameraLists();
        }

        //Handler for the system configuration changed event
        private object SystemConfigChangedEvent(VideoOS.Platform.Messaging.Message message, FQID dest, FQID sender)
        {
            FillCameraLists();
            return null;
        }

        //Shared utility method for configuration updates
        private void FillCameraLists()
        {
            _userContextControl1.FillCameraList();
            _userContextControl2.FillCameraList();
        }
    }
}
