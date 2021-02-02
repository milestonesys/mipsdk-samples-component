using System;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.Multicast;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace VideoViewerNoConfigAdmin
{
	///
	/// NOTE: This dll requires the application to be in x86 due to the ActiveX
	/// 
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("193E1D23-0743-42F8-AA9A-77219AC31A10");
        private const string IntegrationName = "ImageViewer No Admin";
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
			VideoOS.Platform.SDK.UI.Environment.Initialize();

			EnvironmentManager.Instance.TraceFunctionCalls = true;
		    EnvironmentManager.Instance.EnvironmentOptions[EnvironmentOptions.MulticastLog] = EnvironmentOptions.OptionYes;
		    EnvironmentManager.Instance.EnvironmentOptions[EnvironmentOptions.MulticastErrorRate] = "0.01";

			DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
			//loginForm.AutoLogin = false;				// Can overrride the tick mark
			//loginForm.LoginLogoImage = someImage;		// Could add my own image here
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
