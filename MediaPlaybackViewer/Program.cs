using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;

namespace MediaPlaybackViewer
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

			VideoOS.Platform.SDK.Environment.Initialize();				// Initialize the standalone Environment
			VideoOS.Platform.SDK.UI.Environment.Initialize();
			VideoOS.Platform.SDK.Media.Environment.Initialize();		// Initialize the Media
			VideoOS.Platform.SDK.Export.Environment.Initialize();		// Initialize the Export

		    VideoOS.Platform.EnvironmentManager.Instance.EnvironmentOptions[EnvironmentOptions.HardwareDecodingMode] = "Auto";

			Application.Run(new MainForm());
		}
	}
}
