using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace MediaViewerBitmapSource
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Guid integrationId = new Guid("FE72F0F2-1EDE-4823-B9EF-5E3C28EB6775");
            string integrationName = "Media Viewer BitmapSource";
            string manufacturerName = "Sample Manufacturer";
            string version = "2.0";

            VideoOS.Platform.SDK.Environment.Initialize();              // Initialize the standalone Environment
            VideoOS.Platform.SDK.UI.Environment.Initialize();           // Initialize the UI
            VideoOS.Platform.SDK.Media.Environment.Initialize();        // Initialize the Media
            VideoOS.Platform.SDK.Export.Environment.Initialize();		// Initialize the Export

            bool connected = false;
            DialogLoginForm loginForm = new DialogLoginForm(new DialogLoginForm.SetLoginResultDelegate((b) => connected = b), integrationId, integrationName, version, manufacturerName);
            
            // Show the login dialog
            loginForm.ShowDialog();

            if (!connected)
            {
                Current.Shutdown();
            }
        }
    }
}
