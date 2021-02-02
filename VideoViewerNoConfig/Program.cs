using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.SDK.Multicast;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace VideoViewer
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

            VideoOS.Platform.SDK.MultiEnvironment.InitializeUsingUserContext();
			VideoOS.Platform.SDK.UI.Environment.Initialize();

			EnvironmentManager.Instance.TraceFunctionCalls = true;
			Application.Run(new MainForm());
		}

	}
}
