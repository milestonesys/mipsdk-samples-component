using System;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace AudioRecorder
{
    /// <summary>
    /// This application is provided on an as-is basis, and may not work.
    /// </summary>
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("FA57599E-D58A-437B-8604-2AD17D864104");
        private const string IntegrationName = "Audio Live Recorder";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        /// <summary>
        /// 
        /// </summary>
        [STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            VideoOS.Platform.SDK.Environment.Initialize();		// Initialize the standalone Environment
            VideoOS.Platform.SDK.Media.Environment.Initialize();		// Initialize the standalone Environment

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
