using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace MediaLiveViewer
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("14FA2FED-87D2-44E2-88B6-23A21551BD0D");
        private const string IntegrationName = "Media Live Viewer";
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
			VideoOS.Platform.SDK.Media.Environment.Initialize();        // Initialize the standalone Environment

            EnvironmentManager.Instance.EnvironmentOptions[EnvironmentOptions.HardwareDecodingMode] = "Auto";
            // EnvironmentManager.Instance.EnvironmentOptions[EnvironmentOptions.HardwareDecodingMode] = "Off";
			// EnvironmentManager.Instance.EnvironmentOptions["ToolkitFork"] = "No";

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
