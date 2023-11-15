using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace MultiSiteViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            VideoOS.Platform.SDK.Environment.Initialize();          // General initialize.  Always required
            VideoOS.Platform.SDK.UI.Environment.Initialize();       // Initialize UI references
                                                                    //VideoOS.Platform.SDK.Export.Environment.Initialize();	// Initialize export references
        }
    }
}
