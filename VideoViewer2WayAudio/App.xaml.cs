using System;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace VideoViewer2WayAudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Guid IntegrationId = new Guid("8812F832-2CE3-479F-8564-C37F848D3FD9");
        private const string IntegrationName = "Video Viewer with 2-way audio";
        private const string Version = "2.0";
        private const string ManufacturerName = "Sample Manufacturer";

        public App()
        {
            VideoOS.Platform.SDK.Environment.Initialize();      // Initialize the standalone Environment
            VideoOS.Platform.SDK.UI.Environment.Initialize();       // Initialize the standalone Environment

            EnvironmentManager.Instance.TraceFunctionCalls = true;

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
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