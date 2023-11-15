using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;
using VideoOS.Platform;

namespace VideoViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Guid IntegrationId = new Guid("63AE5F55-727D-406B-BA6A-02C23F01D57F");
        private const string IntegrationName = "Video Viewer";
        private const string Version = "2.0";
        private const string ManufacturerName = "Sample Manufacturer";

        protected override void OnStartup(StartupEventArgs e)
        {
            VideoOS.Platform.SDK.Environment.Initialize();      // Initialize the standalone environment
            VideoOS.Platform.SDK.UI.Environment.Initialize();
            VideoOS.Platform.SDK.Environment.Properties.ConfigurationRefreshIntervalInMs = 5000;

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName); 
            //loginForm.AutoLogin = false;				// Can override the tick mark
            //loginForm.LoginLogoImage = someImage;		// Could add my own image here
            loginForm.ShowDialog();

            if (Connected)
            {
                new MainWindow().Show();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private static bool Connected = false;
        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }
    }
}