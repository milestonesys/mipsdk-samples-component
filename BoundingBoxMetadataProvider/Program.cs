using System;
using System.Windows.Forms;

namespace BoundingBoxMetadataProvider
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

			Application.Run(new MainForm());
		}
	}
}
