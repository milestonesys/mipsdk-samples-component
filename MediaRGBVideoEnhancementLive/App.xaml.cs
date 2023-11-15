using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace MediaRGBVideoEnhancementLive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Guid integrationId = new Guid("45BA0711-41B9-4982-A009-D2CC68C82C38");
            string integrationName = "Media RGB Enhancement Live";
            string manufacturerName = "Sample Manufacturer";
            string version = "2.0";

            VideoOS.Platform.SDK.Environment.Initialize();          // General initialize.  Always required
            VideoOS.Platform.SDK.Media.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();       // Initialize UI references

            bool connected = false;
            DialogLoginForm loginForm = new DialogLoginForm(new DialogLoginForm.SetLoginResultDelegate((b) => connected = b), integrationId, integrationName, version, manufacturerName);
            loginForm.ShowDialog();								// Show and complete the form and login to server

            if (!connected)
            {
                Current.Shutdown();
            }
        }
    }
}
