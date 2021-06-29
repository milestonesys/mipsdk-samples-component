using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace LogOnServer
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("CD52BF80-A58B-4A35-BF30-83BC40680AFC");
        private const string IntegrationName = "Log Message To Server";
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
			VideoOS.Platform.SDK.Log.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();

			LogResourceHandler.RegisterMyMessages();

			DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
			Application.Run(loginForm);
			if (Connected)
			{
				Application.Run(new LogForm());
			}
		}

		private static bool Connected = false;
		private static void SetLoginResult(bool connected)
		{
			Connected = connected;
		}
	}
}
