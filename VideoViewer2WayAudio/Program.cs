using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace VideoViewer2WayAudio
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("8812F832-2CE3-479F-8564-C37F848D3FD9");
        private const string IntegrationName = "Video Viewer with 2-way audio";
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
			VideoOS.Platform.SDK.UI.Environment.Initialize();		// Initialize the standalone Environment

			EnvironmentManager.Instance.TraceFunctionCalls = true;

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
