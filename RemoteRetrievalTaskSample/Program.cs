using System;
using System.Windows.Forms;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace RemoteRetrievalTaskSample
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("0065E59A-FE12-4203-8C13-6F8ACEDFFCA1");
        private const string IntegrationName = "Remote Retrieval Task";
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
			bool c = false;
			DialogLoginForm loginForm = new DialogLoginForm((connected) => { c = connected; }, IntegrationId, IntegrationName, Version, ManufacturerName);
			loginForm.ShowDialog();

            if (c)
            {
                Application.Run(new Form1());
            }
		}
	}
}
