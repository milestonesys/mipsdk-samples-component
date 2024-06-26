using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace ConfigAPIUpdateFirmwareWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Guid IntegrationId = new Guid("e6c77e84-0612-4f32-a34f-ee20d3d1889c");
        private const string IntegrationName = "Configuration API Firmware Update";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";
        private static bool _connected;

        [STAThread]
        private static void Main()
        {
            VideoOS.Platform.SDK.Environment.Initialize();			// General initialize. Always required
            VideoOS.Platform.SDK.UI.Environment.Initialize();		// Initialize UI
            App app = new App();
            MainWindow mainWindow = new MainWindow();

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            loginForm.ShowDialog();
            if (_connected)
            {
                loginForm.Close();
                app.Run(mainWindow);
                VideoOS.Platform.SDK.Environment.RemoveAllServers();
            }
        }

        private static void SetLoginResult(bool connected)
        {
            _connected = connected;
        }
    }
}