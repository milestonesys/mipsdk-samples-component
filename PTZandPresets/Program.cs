using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace PTZandPresets
{
    static class Program
    {
        private static readonly Guid IntegrationId = new Guid("B03477E2-CCFA-4E44-9092-292960128807");
        private const string IntegrationName = "PTZ and Presets";
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

            VideoOS.Platform.SDK.Environment.Initialize();			// General initialize.  Always required
            VideoOS.Platform.SDK.UI.Environment.Initialize();
            VideoOS.Platform.SDK.Export.Environment.Initialize();

			EnvironmentManager.Instance.TraceFunctionCalls = true;

            bool c = false;
            DialogLoginForm loginForm = new DialogLoginForm((connected) => { c = connected; }, IntegrationId, IntegrationName, Version, ManufacturerName);
            loginForm.ShowDialog();

            if (c)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
