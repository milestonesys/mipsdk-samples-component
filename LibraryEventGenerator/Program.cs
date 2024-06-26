using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace LibraryEventGenerator
{
    static class Program
    {
        private static readonly Guid IntegrationId = new Guid("CC3F5173-1366-4594-A024-A3F6E0668950");
        private const string IntegrationName = "Library Event Generator";
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

            VideoOS.Platform.SDK.Environment.Initialize();
            //VideoOS.Platform.SDK.UI.Environment.Initialize();				// Initialize the UI

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            //loginForm.AutoLogin = false;				// Can override the tick mark
            //loginForm.LoginLogoImage = someImage;		// Could add my own image here
            Application.Run(loginForm);
            if (Connected)
            {
                Application.Run(new EventForm());
            }
        }

        private static bool Connected = false;
        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }

    }
}
