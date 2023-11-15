using System;
using System.Windows;
using VideoOS.Platform.Login;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace EventAndStateViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Guid _integrationId = new Guid("b71d9aff-493e-4597-aec7-5e5f4334bd33");
        private const string _integrationName = "Events and state viewer sample";
        private const string _manufacturerName = "Sample Manufacturer";
        private const string _version = "1.0";

        internal static DataModel DataModel { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();

            var connected = false;
            var loginDialog = new DialogLoginForm(x => connected = x, _integrationId, _integrationName, _version, _manufacturerName);
            loginDialog.ShowDialog();

            if (!connected)
            {
                // Login was cancelled
                Shutdown();
                return;
            }

            var loginSettings = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite);
            DataModel = new DataModel(loginSettings);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            DataModel?.Dispose();
        }
    }
}
