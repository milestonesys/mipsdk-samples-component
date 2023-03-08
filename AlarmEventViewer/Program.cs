using System;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace AlarmEventViewer
{
    static class Program
	{
        private static readonly Guid IntegrationId = new Guid("2D4932B1-B2DB-4A28-94BC-8FC30495446F");
        private const string IntegrationName = "Alarm Event Viewer";
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
            VideoOS.Platform.SDK.Environment.Initialize();			// General initialize.  Always required
			VideoOS.Platform.SDK.UI.Environment.Initialize();		// Initialize UI controls
            //VideoOS.Platform.EnvironmentManager.Instance.TraceMessageCommunication = true;
			
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
