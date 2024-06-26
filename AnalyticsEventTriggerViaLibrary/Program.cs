using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using VideoOS.Platform.SDK.UI.LoginDialog;

namespace TriggerAnalyticsEventSDK
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("5FABA131-D653-4E69-B537-34F4A3998A56");
        private const string IntegrationName = "Analytics Event Trigger via MIP .Net library";
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

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            Application.Run(loginForm);
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
