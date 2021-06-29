using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Live;
using VideoOS.Platform.SDK.Platform;
using VideoOS.Platform.ConfigurationItems;

namespace ConfigAddCameras
{
    class Program
    {
        enum Authorizationmodes
        {
            DefaultWindows,
            Windows,
            Basic
        };
        static string _url = "localhost";
        static Authorizationmodes _auth;
        static string _user = "username";
        static string _pass = "password";
        static bool _secureOnly = true; // change to false to connect to servers older than 2021 R1 or servers not running HTTPS on the Identity/Management Server communication
        static string _cvsFile = @"c:\test\test.txt";

        private static readonly Guid IntegrationId = new Guid("67D9C3E8-11DD-4444-A5A2-6056FC5DD5B7");
        private const string IntegrationName = "Config Add Cameras";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        static void Main(string[] args)
        {
            VideoOS.Platform.SDK.Environment.Initialize();

            if (args.Length == 5)
            {
                _url = args[0];
                if (!_url.StartsWith("http://", true, null) && !_url.StartsWith("https://", true, null)) _url = "http://" + _url;
                string auth = args[1];
                if (auth.StartsWith("B", true, null)) _auth = Authorizationmodes.Basic;
                if (auth.StartsWith("W", true, null)) _auth = Authorizationmodes.Windows;
                if (auth.StartsWith("D", true, null)) _auth = Authorizationmodes.DefaultWindows;
                _user = args[2];
                _pass = args[3];
                _cvsFile = args[4];
            }
            else
            {
                if (args.Length == 3)
                {
                    _url = args[0];
                    if (!_url.StartsWith("http://", true, null) && !_url.StartsWith("https://", true, null)) _url = "http://" + _url;
                    string auth = args[1];
                    if (auth.StartsWith("D", true, null)) _auth = Authorizationmodes.DefaultWindows;
                    _cvsFile = args[2];
                }
                else
                {
                    GetLoginParams();
                }
            }



            
            if (LoginUsingCurrentCredentials())
            {
                int counter = 0;
                string line;
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(_cvsFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        System.Console.WriteLine("Adding- "+line);
                        if (AddCamera(line))
                        {
                            counter++;
                            Console.WriteLine("Added - " + line);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception" + e.Message);
                }
                Console.WriteLine(counter + " cameras added in total.");
                
                Console.WriteLine(Environment.NewLine+"Press any key to exit.");
            }

            Console.ReadKey();
            Environment.Exit(0);
        }
        
        /// <summary>
        /// If not called with command line parameters, this will ask for the necessary parameters
        /// </summary>
        static void GetLoginParams()
        {
            Console.Write("XProtect server (url): ");
            _url = Console.ReadLine();
            
            if (!_url.StartsWith("http://", true, null) && !_url.StartsWith("https://", true, null)) _url = "http://" + _url;
            Console.Write("Authentication: Windows Default, Windows or Basic (D/W/B) ");
            string str = Console.ReadLine().ToUpper();
            if (str != null)
            {
                _auth = Authorizationmodes.DefaultWindows;
                if (str.StartsWith("W",true,null)) _auth=Authorizationmodes.Windows;
                if (str.StartsWith("B", true, null)) _auth = Authorizationmodes.Basic;
            }
            if (_auth != Authorizationmodes.DefaultWindows)
            {
                Console.Write("Username: ");
                _user = Console.ReadLine();
                Console.Write("Password: ");
                _pass = Console.ReadLine();
            }
            Console.Write("Full path and name of csv file containing the cameras to add ");
            _cvsFile = Console.ReadLine();
           
        }

        /// <summary>
        /// Login routine 
        /// </summary>
        /// <returns></returns> True if successfully logged in
        static private bool LoginUsingCurrentCredentials()
        {
            Uri uri = new UriBuilder(_url).Uri;
            NetworkCredential nc;
            switch (_auth)
            {
                case Authorizationmodes.DefaultWindows:
                    nc = CredentialCache.DefaultNetworkCredentials;
                    VideoOS.Platform.SDK.Environment.AddServer(_secureOnly, uri, nc);
                    break;
                case Authorizationmodes.Windows:
                    nc = new NetworkCredential(_user, _pass);
                    VideoOS.Platform.SDK.Environment.AddServer(_secureOnly, uri, nc);
                    break;
                case Authorizationmodes.Basic:
                    CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, _user, _pass, "Basic");
                    VideoOS.Platform.SDK.Environment.AddServer(_secureOnly, uri, cc);
                    break;
            }

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

        static private bool AddCamera(string parms)
        {
            string[] parameters = parms.Split(',');
            string ip = parameters[0];
            string user = parameters[1];
            string pass = parameters[2];
            string drivernr = parameters[3];
            string hwname = parameters[4];
            string cameraName = parameters[5];
            string rsName = parameters[6];
            string groupName = parameters[7];
            int drivernum = int.Parse(drivernr);

            ManagementServer managementServer = new ManagementServer(EnvironmentManager.Instance.MasterSite);
            RecordingServer recordingServer = managementServer.RecordingServerFolder.RecordingServers.FirstOrDefault(x => x.Name == rsName);
            if (recordingServer == null)
            {
                Console.WriteLine("Error. Did not find recording server: " + rsName);
                return false;
            }
            string hardwareDriverPath = recordingServer.HardwareDriverFolder.HardwareDrivers.Where(x=>x.Number == drivernum).FirstOrDefault()?.Path;
            if (hardwareDriverPath == null)
            {
                Console.WriteLine("Error. Did not find hardware driver: " + drivernum);
                return false;
            }
            Console.WriteLine("Will now attempt to add: " + cameraName);
            ServerTask addHardwareServerTask = recordingServer.AddHardware(ip, hardwareDriverPath, user, pass);
            while (addHardwareServerTask.State != StateEnum.Error && addHardwareServerTask.State != StateEnum.Success)
            {
                System.Threading.Thread.Sleep(1000);
                addHardwareServerTask.UpdateState();
            }
            Console.WriteLine("Hardware add task: " + addHardwareServerTask.State);
            if (addHardwareServerTask.State == StateEnum.Error)
            {
                Console.WriteLine("Hardware add error: " + addHardwareServerTask.ErrorText);
                return false;
            }
            else if (addHardwareServerTask.State == StateEnum.Success)
            {
                string path = addHardwareServerTask.Path;       // For the added hardware
                Hardware hardware = new Hardware(EnvironmentManager.Instance.MasterSite.ServerId, path);
                hardware.Name = hwname;
                hardware.Enabled = true;
                hardware.Save();
                Camera camera = hardware.CameraFolder.Cameras.First();
                camera.Name = cameraName;
                camera.Enabled = true;
                camera.Save();
                // alter other camera properties(?)
                CameraGroup cameraGroup = FindOrAddCameraGroup(managementServer, groupName);
                if(cameraGroup!=null) cameraGroup.CameraFolder.AddDeviceGroupMember(camera.Path); // make sure the camera is member of one group
            }
            
            return true;
        }
        static private CameraGroup FindOrAddCameraGroup(ManagementServer ms, string groupName)
        {
            CameraGroup groupExist= ms.CameraGroupFolder.CameraGroups.Where(x => x.Name == groupName).FirstOrDefault();
            if (groupExist != null) return groupExist;

            CameraGroupFolder folder = ms.CameraGroupFolder;
            ServerTask task = folder.AddDeviceGroup(groupName, "group added by tool");
            Console.WriteLine("Camera group add (" + groupName + ") task: " + task.State);
            if(task.State == StateEnum.Success)
            {
                string path = task.Path;
                return new CameraGroup(EnvironmentManager.Instance.MasterSite.ServerId, path);
            }
            return null;
        }
    }
}
