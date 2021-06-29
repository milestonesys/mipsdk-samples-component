using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace MediaRGBVideoEnhancementLive
{
	///
	/// NOTE: This dll requires the application to be in x86 due to the ActiveX
	/// 
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("45BA0711-41B9-4982-A009-D2CC68C82C38");
        private const string IntegrationName = "Media RGB Enhancement Live";
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
		    VideoOS.Platform.SDK.Media.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
			//loginForm.AutoLogin = false;				// Can override the tick mark
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
