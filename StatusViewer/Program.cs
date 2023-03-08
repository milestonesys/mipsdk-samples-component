using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace StatusViewer
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("7CFF6657-18E9-43E0-BEAF-3DECCFF89A12");
        private const string IntegrationName = "Status Viewer";
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

			DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
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
