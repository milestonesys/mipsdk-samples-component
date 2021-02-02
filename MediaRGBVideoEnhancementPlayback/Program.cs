using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MediaRGBEnhancementPlayback
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
			VideoOS.Platform.SDK.Export.Environment.Initialize();		// Initialize the Export

		    VideoOS.Platform.EnvironmentManager.Instance.TraceSendDetails = true;
            VideoOS.Platform.EnvironmentManager.Instance.TracePlaybackDetails = true;

			Application.Run(new MainForm());
		}
	}
}
