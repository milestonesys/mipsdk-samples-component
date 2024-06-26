using System;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace TransactClient
{
    static class Program
    {
        private static readonly Guid IntegrationId = new Guid("12108438-4095-4933-BBE3-E86B8CF186C4");
        private const string IntegrationName = "Transact Client";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            VideoOS.Platform.SDK.Environment.Initialize();          // General initialize.  Always required
            VideoOS.Platform.SDK.UI.Environment.Initialize();       // Initialize UI references

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            //loginForm.LoginLogoImage = MyOwnImage;				// Set own header image
            Application.Run(loginForm);                             // Show and complete the form and login to server
            if (Connected)
            {
                Application.Run(new MainForm());
            }
        }

        private static bool Connected = false;
        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }
    }
}
