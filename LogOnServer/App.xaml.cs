using System;
using System.Windows;
using VideoOS.Platform.SDK.UI.LoginDialog;

namespace LogOnServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Guid integrationId = new Guid("CD52BF80-A58B-4A35-BF30-83BC40680AFC");
            string integrationName = "Log Message To Server";
            string manufacturerName = "Sample Manufacturer";
            string version = "2.0";

            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.Log.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();

            LogResourceHandler.RegisterMyMessages();

            bool connected = false;
            DialogLoginForm loginForm = new DialogLoginForm(new DialogLoginForm.SetLoginResultDelegate((b) => connected = b), integrationId, integrationName, version, manufacturerName);
            loginForm.ShowDialog();

            if (!connected)
            {
                Current.Shutdown();
            }
        }
    }
}
