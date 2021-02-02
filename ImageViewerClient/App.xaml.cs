using System;
using System.Windows;
using System.Windows.Interop;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace ImageViewerClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        static void Main()
        {

            VideoOS.Platform.SDK.Environment.Initialize();			// General initialize.  Always required
            VideoOS.Platform.SDK.UI.Environment.Initialize();		// Initialize UI
            VideoOS.Platform.SDK.Export.Environment.Initialize();   // Initialize recordings access
            App app = new App();
            MainWindow mainWindow = new MainWindow();
            app.Run(mainWindow);
        }
    }
}


