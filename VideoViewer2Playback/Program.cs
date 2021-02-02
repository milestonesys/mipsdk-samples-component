using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace VideoViewer
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("DFAC36E1-F7BA-49CF-BAB4-2354CB3B0FFC");
        private const string IntegrationName = "Video Viewer – Individual Playback";
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
			VideoOS.Platform.SDK.UI.Environment.Initialize();	// Initialize UI

			EnvironmentManager.Instance.TraceFunctionCalls = true;

			DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
			//loginForm.AutoLogin = false;				// Can overrride the tick mark
			//loginForm.LoginLogoImage = someImage;		// Could add my own image here
			Application.Run(loginForm);
			if (Connected)
			{
				try
				{
					Application.Run(new MainForm());
				}
				catch (Exception e)
				{
				    MessageBox.Show("Program.cs:" + e.Message);
				}
			}

		}

		private static bool Connected = false;
		private static void SetLoginResult(bool connected)
		{
			Connected = connected;
		}
	}
}
