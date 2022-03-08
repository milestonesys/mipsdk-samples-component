using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.SDK.Platform;

namespace RawVideoSourceRetriever
{
    class Program
    {
        const string Uri = "http://localhost";
        const bool SecureOnly = false; // change to false to connect to servers older than 2021 R1 or servers not running HTTPS on the Identity/Management Server communication
        const string User = "";
        const string Password = "";
        const string Auth = "Negotiate";

        private static readonly Guid IntegrationId = new Guid("32BF2DAA-7041-4DB0-AF68-9718B8C3BD9D");
        private const string IntegrationName = "RawVideoSourceRetriever";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        private static readonly Guid CameraId = new Guid("420763FA-BA6C-4513-82F8-13AB0372DD39"); // Use relevant Camera Id here....

        static void Main(string[] args)
        {
            Console.WriteLine("Sample logs in and retrieve raw video data 30 sec back in time from camera: " + CameraId.ToString());
            Console.WriteLine("The camera must be recording!");
            Console.WriteLine();

            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.Export.Environment.Initialize();
            if (Login())
            {
                var item = Configuration.Instance.GetItem(CameraId, Kind.Camera);

                RawVideoSource rawVideoSource = new RawVideoSource(item);
                rawVideoSource.Init();

                var dataArr = rawVideoSource.GetAtOrBefore(DateTime.UtcNow - TimeSpan.FromSeconds(30)) as RawVideoSourceDataList;
                int i = 0;
                while (i++ < 5 && dataArr != null)
                {
                    Console.WriteLine("Frames: " + dataArr.List.Count());
                    Console.WriteLine("KeyFrameSize: " + dataArr.List.First().Content.Length);
                    dataArr = rawVideoSource.GetNext() as RawVideoSourceDataList;
                }

                rawVideoSource.Close();

                Console.WriteLine();
                Console.WriteLine("Note that the data has a 32-bytes GenericByteHeader on each frame, that can just be removed");
                Console.WriteLine(Environment.NewLine + "Press any key to exit.");
            }
            Console.ReadKey();
        }


        static private bool Login()
        {
            Uri uri = new UriBuilder(Uri).Uri;
            CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, User, Password, Auth);
            VideoOS.Platform.SDK.Environment.AddServer(SecureOnly, uri, cc);

            //  You need this to apply "basic" credentials. 
            //CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, "test", "test", "Basic");
            //  The BuildCredentialCache can also build credential for Windows login ..
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
