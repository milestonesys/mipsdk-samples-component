using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace LogRead
{
    static class Program
    {
        private static readonly Guid IntegrationId = new Guid("2D298EEF-3BA0-4EC7-944A-C8401DC95767");
        private const string IntegrationName = "Log Read";
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

            VideoOS.Platform.SDK.Environment.Initialize();      // General initialize.  Always required
            VideoOS.Platform.SDK.Log.Environment.Initialize();	// Initialize Log            
            VideoOS.Platform.SDK.UI.Environment.Initialize();	// Initialize ActiveX references


            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            //loginForm.LoginLogoImage = MyOwnImage;				// Set own header image
            Application.Run(loginForm);								// Show and complete the form and login to server
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
