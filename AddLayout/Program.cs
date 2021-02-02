using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;
using VideoOS.Platform.SDK.Platform;

namespace AddLayout
{
    class Program
    {
        const string URI = "http://localhost";
        const string USER = "";
        const string PASSWORD = "";
        const string AUTH = "Negotiate";
        const string DEFINITIONXMLNAME = "LayoutNEW.xml";
        const string LAYOUTNAME = "LayoutC";
        const string LAYOUTDESCRIPTION = "The best";

        private static readonly Guid IntegrationId = new Guid("7F1B4B62-A6F1-49E4-9C61-D541CC54437B");
        private const string IntegrationName = "Add Layout";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        static void Main(string[] args)
        {
            VideoOS.Platform.SDK.Environment.Initialize();
            if (Login())
            {
                string definitionXml = System.IO.File.ReadAllText(DEFINITIONXMLNAME);
                ManagementServer mgtServer = new ManagementServer(EnvironmentManager.Instance.MasterSite.ServerId);
                LayoutFolder layoutFolder = mgtServer.LayoutGroupFolder.LayoutGroups.FirstOrDefault().LayoutFolder;
                try
                {
                    layoutFolder.AddLayout(LAYOUTNAME, LAYOUTDESCRIPTION, definitionXml);
                    Console.WriteLine("Added new layout. " + LAYOUTNAME);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception. " + e.Message);
                }
                
                Console.WriteLine(Environment.NewLine + "Press any key to exit.");
            }
            Console.ReadKey();
            Environment.Exit(0);
        }

        static private bool Login()
        {
            Uri uri = new UriBuilder(URI).Uri;
            CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, USER, PASSWORD, AUTH);
            VideoOS.Platform.SDK.Environment.AddServer(uri, cc);

            //  You need this to apply Enterprise "basic" credentials. 
            //CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, "test", "test", "Basic");
            //  The BuildCredentialCache can aslo build credential for Windows login ..
            //CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, "myserver\\testuser", "Str0ngPa$", "Negotiate");
            // For reuse of Windows credentials ..
            //CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, "", "", "Negotiate");

            try
            {
                VideoOS.Platform.SDK.Environment.Login(uri, IntegrationId, IntegrationName, Version, ManufacturerName);
            }
            catch (ServerNotFoundMIPException snfe)
            {
                Console.WriteLine("Server not found: " + snfe.Message);
                VideoOS.Platform.SDK.Environment.RemoveServer(uri);
                return false;
            }
            catch (InvalidCredentialsMIPException ice)
            {
                Console.WriteLine("Invalid credentials for: " + ice.Message);
                VideoOS.Platform.SDK.Environment.RemoveServer(uri);
                return false;
            }
            catch (Exception)
            {
                Console.WriteLine("Internal error connecting to: " + uri.DnsSafeHost);
                VideoOS.Platform.SDK.Environment.RemoveServer(uri);
                return false;
            }

            Console.WriteLine("Login succeeded.");
            return true;
        }
    }
}
