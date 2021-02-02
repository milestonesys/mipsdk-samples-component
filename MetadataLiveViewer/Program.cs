using System;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace MetadataLiveViewer
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("67366C4C-29FE-4481-8B3C-05F0F4A38FB8");
        private const string IntegrationName = "Metadata Live Viewer";
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

			VideoOS.Platform.SDK.Environment.Initialize();		// Initialize the standalone Environment
			VideoOS.Platform.SDK.Media.Environment.Initialize();		// Initialize the standalone Environment

																// NOTE: This dll requires the application to be in x86 due to the ActiveX

			EnvironmentManager.Instance.TraceFunctionCalls = true;

			var loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
			//loginForm.AutoLogin = false;				// Can overrride the tick mark
			//loginForm.LoginLogoImage = someImage;		// Could add my own image here
			Application.Run(loginForm);
			if (_connected)
			{
				Application.Run(new MainForm());
			}
		}

		private static bool _connected = false;
		private static void SetLoginResult(bool connected)
		{
			_connected = connected;
		}
	}
}
