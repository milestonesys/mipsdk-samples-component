using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MultiSiteViewer
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

			VideoOS.Platform.SDK.Environment.Initialize();
			VideoOS.Platform.SDK.UI.Environment.Initialize();
            VideoOS.Platform.EnvironmentManager.Instance.EnvironmentOptions["UsePing"] = "No";

			Application.Run(new MultiSiteForm());
		}
	}
}
