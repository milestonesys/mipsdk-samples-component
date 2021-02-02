using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace ConfigAccessViaSDK
{
	static class Program
	{
        private static readonly Guid IntegrationId = new Guid("D03F8822-AE0F-4091-BC10-992246AFDB2E");
        private const string IntegrationName = "Configuration Access Via SDK";
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
            VideoOS.Platform.EnvironmentManager.Instance.EnvironmentOptions["UsePing"] = "No";

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            //loginForm.ServerConfiguration = DialogLoginForm.ServerConfigurationEnum.MasterAndSlaves;
            loginForm.ServerConfiguration = DialogLoginForm.ServerConfigurationEnum.AllServersIndividually;
            //loginForm.ServerConfiguration = DialogLoginForm.ServerConfigurationEnum.MasterOnly;
            VideoOS.Platform.SDK.Environment.Properties.ConfigurationRefreshIntervalInMs = 5000;
			Application.Run(loginForm);

			if (Connected)
			{
                SearchResult sr;
                List<Item> items = Configuration.Instance.GetItemsBySearch("BB52", 10, 5, out sr);


				Application.Run(new ConfigAccess());
			}
		}


		private static bool Connected = false;
		private static void SetLoginResult(bool connected)
		{
			Connected = connected;
		}
	}	


}
