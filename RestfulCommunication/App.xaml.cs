using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace RestfulCommunication
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
        [STAThread]
        static void Main()
        {
            Guid integrationId = new Guid("5af1ac90-30fe-47c0-a986-04a6cd9feea1");
            string integrationName = "RESTfulCommunication";
            string manufacturerName = "Sample Manufacturer";
            string version = "1.0";

            VideoOS.Platform.SDK.Environment.Initialize();          // General initialize.  Always required
            VideoOS.Platform.SDK.UI.Environment.Initialize();       // Initialize UI

            DialogLoginForm loginDialog = new DialogLoginForm(SetLoginResult, integrationId, integrationName, version, manufacturerName);
            loginDialog.ShowDialog();
            if (Connected)
            {
                App app = new App();
                MainWindow mainWindow = new MainWindow();
                app.Run(mainWindow);
            }
        }

        private static bool Connected = false;
        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }
    }
}


