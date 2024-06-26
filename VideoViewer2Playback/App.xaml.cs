using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;
using VideoOS.Platform;

namespace VideoViewer2Playback
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Guid IntegrationId = new Guid("DFAC36E1-F7BA-49CF-BAB4-2354CB3B0FFC");
        private const string IntegrationName = "Video Viewer – Individual Playback";
        private const string Version = "2.0";
        private const string ManufacturerName = "Sample Manufacturer";
        
        public App()
        {
            VideoOS.Platform.SDK.Environment.Initialize();      // Initialize the standalone Environment
            VideoOS.Platform.SDK.UI.Environment.Initialize();   // Initialize UI

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            //loginForm.AutoLogin = false;				// Can override the tick mark
            //loginForm.LoginLogoImage = someImage;		// Could add my own image here
            loginForm.ShowDialog();

            if (Connected)
            {
                try
                {
                    new MainWindow().Show();
                }
                catch (Exception e)
                {
                    MessageBox.Show("App.xaml.cs: " + e.Message);
                    Environment.Exit(0);
                }
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