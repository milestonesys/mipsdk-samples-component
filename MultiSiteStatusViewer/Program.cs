using System;
using System.Windows.Forms;

namespace MultiSiteStatusViewer
{
    static class Program
	{
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
            VideoOS.Platform.EnvironmentManager.Instance.EnvironmentOptions["UsePing"] = "No";
            Application.Run(new MainForm());
        }

		private static bool Connected = false;
		private static void SetLoginResult(bool connected)
		{
			Connected = connected;
		}

	}
}
