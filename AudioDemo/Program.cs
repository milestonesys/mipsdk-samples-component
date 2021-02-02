using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AudioDemo
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

            Application.Run(new AudioForm());
        }
    }
}