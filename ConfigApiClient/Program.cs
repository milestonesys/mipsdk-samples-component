using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace ConfigAPIClient
{
    static class Program
    {
        private static readonly Guid IntegrationId = new Guid("9DBF146E-9A28-4D18-A9E8-77B16C6DC6CB");
        private const string IntegrationName = "Configuration API Client";
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
            VideoOS.Platform.SDK.Media.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();

            DialogLoginForm loginForm = new DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            Application.Run(loginForm);
            if (Connected)
            {
                string serverAddress = EnvironmentManager.Instance.MasterSite.ServerId.ServerHostname;
                int serverPort = EnvironmentManager.Instance.MasterSite.ServerId.Serverport;
                bool corporate = EnvironmentManager.Instance.MasterSite.ServerId.ServerType == ServerId.CorporateManagementServerType;
                Application.Run(new MainForm(serverAddress, serverPort, corporate));
            }
        }

        private static bool Connected = false;
        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }
    }
}
