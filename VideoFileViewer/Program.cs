using System;
using System.Windows.Forms;

namespace VideoFileViewer
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
            VideoOS.Platform.SDK.UI.Environment.Initialize();		// Initialize UI
            VideoOS.Platform.SDK.Export.Environment.Initialize();   // Initialize playback
            Application.Run(new MainForm());
        }

    }
}
