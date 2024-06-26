using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace PTZandPresets
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Guid integrationId = new Guid("B03477E2-CCFA-4E44-9092-292960128807");
            string integrationName = "PTZ and Presets";
            string manufacturerName = "Sample Manufacturer";
            string version = "2.0";

            VideoOS.Platform.SDK.Environment.Initialize();          // General initialize.  Always required
            VideoOS.Platform.SDK.UI.Environment.Initialize();       // Initialize UI references
            VideoOS.Platform.SDK.Export.Environment.Initialize();	// Initialize export references

            bool connected = false;
            DialogLoginForm loginForm = new DialogLoginForm(new DialogLoginForm.SetLoginResultDelegate((b) => connected = b), integrationId, integrationName, version, manufacturerName);
            //loginForm.LoginLogoImage = MyOwnImage;				// Set own header image
            loginForm.ShowDialog();								// Show and complete the form and login to server

            if (!connected)
            {
                Current.Shutdown();
            }
        }
    }
}
