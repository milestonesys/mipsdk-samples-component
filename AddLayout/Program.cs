using System;
using System.Linq;
using System.Net;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;
using VideoOS.Platform.SDK.Platform;

namespace AddLayout
{
    class Program
    {
        const string Uri = "http://localhost";
        const bool SecureOnly = true; // change to false to connect to servers older than 2021 R1 or servers not running HTTPS on the Identity/Management Server communication
        const string User = "";
        const string Password = "";
        const string Auth = "Negotiate";
        const string DefinitionXmlName = "LayoutNEW.xml";
        const string LayoutName = "Layout";
        const string LayoutDescription = "The best";

        private static readonly Guid IntegrationId = new Guid("7F1B4B62-A6F1-49E4-9C61-D541CC54437B");
        private const string IntegrationName = "Add Layout";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        static void Main(string[] args)
        {
            VideoOS.Platform.SDK.Environment.Initialize();
            if (Login())
            {
                string definitionXml = System.IO.File.ReadAllText(DefinitionXmlName);
                ManagementServer mgtServer = new ManagementServer(EnvironmentManager.Instance.MasterSite.ServerId);
                LayoutFolder layoutFolder = mgtServer.LayoutGroupFolder.LayoutGroups.FirstOrDefault().LayoutFolder;
                try
                {
                    layoutFolder.AddLayout(LayoutName, LayoutDescription, definitionXml);
                    Console.WriteLine("Added new layout. " + LayoutName);
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
            Uri uri = new UriBuilder(Uri).Uri;
            CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, User, Password, Auth);
            VideoOS.Platform.SDK.Environment.AddServer(SecureOnly, uri, cc);

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
