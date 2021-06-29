using System;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.Live;
using VideoOS.Platform.SDK.Platform;
using VideoOS.Platform.Login;
using System.Security;

namespace CameraStreamResolution
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
        static string _user = "";
        static SecureString _securePwd = new SecureString();
        static bool _secureOnly = true; // change to false to connect to servers older than 2021 R1 or servers not running HTTPS on the Identity/Management Server communication
        static string _camerasearch = "test";
        static Item _camera;
        static AutoResetEvent _resetEvent;
        private static Timer _timeoutEvent;

        private static readonly Guid IntegrationId = new Guid("AA2730FE-50BF-4166-8352-920D590B4C07");
        private const string IntegrationName = "Camera Stream Resolution";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        static void Main(string[] args)
        {
            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.Media.Environment.Initialize();
            VideoOS.Platform.SDK.Export.Environment.Initialize();

            _resetEvent = new AutoResetEvent(false);

            if (args.Length == 5)
            {
                _url = args[0];
                if (!_url.StartsWith("http://", true, null) && !_url.StartsWith("https://", true, null)) _url = "http://" + _url;
                string auth = args[1];
                if (auth.StartsWith("B", true, null)) _auth = Authorizationmodes.Basic;
                if (auth.StartsWith("W", true, null)) _auth = Authorizationmodes.Windows;
                if (auth.StartsWith("D", true, null)) _auth = Authorizationmodes.DefaultWindows;
                _user = args[2];
                _securePwd = ConvertToSecureString(args[3]);
                _securePwd.MakeReadOnly();
                _camerasearch = args[4];
            }
            else
            {
                if (args.Length == 3)
                {
                    _url = args[0];
                    if (!_url.StartsWith("http://", true, null) && !_url.StartsWith("https://", true, null)) _url = "http://" + _url;
                    string auth = args[1];
                    if (auth.StartsWith("D", true, null)) _auth = Authorizationmodes.DefaultWindows;
                    _camerasearch = args[2];
                }
                else
                {
                    GetLoginParams();
                }
            }

            if (LoginUsingCredentials())
            {
                if (FindCamera())
                {
                    GetRes();
                    GetDefaultStream();
                    GetStreams();
                }
                Console.WriteLine(Environment.NewLine + "Press any key to exit.");
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
            string str = Console.ReadLine();
            if (str != null)
            {
                _auth = Authorizationmodes.DefaultWindows;
                if (str.StartsWith("W", true, null)) _auth = Authorizationmodes.Windows;
                if (str.StartsWith("B", true, null)) _auth = Authorizationmodes.Basic;
            }
            if (_auth != Authorizationmodes.DefaultWindows)
            {
                Console.Write("Username: ");
                _user = Console.ReadLine();
                ConsoleKeyInfo key;
                Console.Write("Password: ");
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Enter)
                    {
                        // Append the character to the password.
                        _securePwd.AppendChar(key.KeyChar);
                        Console.Write("*");
                    }
                    // Exit if Enter key is pressed.
                } while (key.Key != ConsoleKey.Enter);
                _securePwd.MakeReadOnly();
                Console.WriteLine();
            }
            Console.Write("Search for a camera name that contains: ");
            _camerasearch = Console.ReadLine();
        }

        /// <summary>
        /// Login routine 
        /// </summary>
        /// <returns></returns> True if successfully logged in
        static private bool LoginUsingCredentials()
        {
            Uri uri = new UriBuilder(_url).Uri;
            CredentialCache cc = new CredentialCache();

            switch (_auth)
            {
                case Authorizationmodes.DefaultWindows:
                    cc = Util.BuildCredentialCache(uri, "", "", "Negotiate");
                    break;
                case Authorizationmodes.Windows:
                    cc = Util.BuildCredentialCache(uri, _user, _securePwd, "Negotiate");
                    break;
                case Authorizationmodes.Basic:
                    cc = Util.BuildCredentialCache(uri, _user, _securePwd, "Basic");
                    break;
            }
            VideoOS.Platform.SDK.Environment.AddServer(_secureOnly, uri, cc);

            //  This will reuse the Windows credentials you are logged in with
            //    CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, "", "", "Negotiate");
            //  This will use specific Windows credentials
            //    CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, "mydomain\\test", "M1le!st0n3123", "Negotiate");
            //  You need this to apply Basic user credentials. 
            //    CredentialCache cc = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, "test", "test", "Basic");
            
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

            LoginSettings loginSettings = LoginSettingsCache.GetLoginSettings(uri.DnsSafeHost);
            if (loginSettings == null)
            {
                Console.WriteLine($"Login not succeeded for user: {_user} on server: {uri.DnsSafeHost}.");
                VideoOS.Platform.SDK.Environment.RemoveServer(uri);
                return false;
            }

            Console.WriteLine($"Login succeeded for user: {_user} on server: {loginSettings.Uri}.");
            return true;
        }

        #region Find Camera
        /// <summary>
        /// Recursive routine finds the camera based on the name of the camera
        /// </summary>
        /// <returns></returns>
        static private bool FindCamera()
        {
            Console.WriteLine("Searching for a camera containing the string \"{0}\" ", _camerasearch);
            _camera = null;

            // Get the root level Items, which are the servers added. Configuration is not loaded implicitly yet.
            List<Item> list = Configuration.Instance.GetItemsByKind(Kind.Camera);

            // For each root level Item, check the children. We are certain, none of the root level Items is a camera
            foreach (Item item in list)
            {
                CheckChildren(item);
            }

            if (_camera == null)
                Console.WriteLine("No camera with a matching name");
            else
                Console.WriteLine("Camera: {0}.", _camera.Name);

            Console.WriteLine(" ");
            return _camera != null;
        }

        static private void CheckChildren(Item parent)
        {
            List<Item> itemsOnNextLevel = parent.GetChildren(); // This causes the configuration Items to be loaded.
            if (itemsOnNextLevel != null)
            {
                foreach (Item item in itemsOnNextLevel)
                {
                    // If we find the camera we want, remember it and return with no further checks
                    // It must have Kind == Camera and it must not be a folder (It seems that camera folders have Kind == Camera)
                    if (item.FQID.Kind == Kind.Camera && item.FQID.FolderType == FolderType.No)
                    {
                        // Does the name match the camera name we are looking for? Here we accept a non-perfect match
                        if (item.Name.Contains(_camerasearch))
                        {
                            // Remember this camera and stop checking.
                            _camera = item;
                            break;
                        }
                    }
                    else
                    {
                        // We have not found our camera, so check the next level of Items in case this Item has children.
                        if (item.HasChildren != HasChildren.No)
                            CheckChildren(item);
                    }
                }
            }
        }
        #endregion

        #region JPEG Sources use
        /// <summary>
        /// Get the resolution of the latest recorded image
        /// </summary>
        static void GetRes()
        {
            var jpegSource = new JPEGVideoSource(_camera);
            jpegSource.Init();
            jpegSource.Height = 0;
            jpegSource.Width = 0;
            Console.WriteLine("Asking for the last recorded image from camera {0} ",
              _camera.Name);

            var jpegData = jpegSource.GetEnd();
            if (jpegData == null)
            {
                Console.WriteLine("No recorded image from " + _camera.Name);
            }
            else
            {
                Console.WriteLine("The resolution of the last recorded image " +
                                  "from camera {0} is ({1},{2})" + Environment.NewLine,
                                  _camera.Name,
                                  jpegData.Width,
                                  jpegData.Height);
            }
        }

        /// <summary>
        /// Getting the resolution of the default stream by stream ID null
        /// </summary>
        static void GetDefaultStream()
        {
            Console.WriteLine("Asking for a live image from camera {0} default stream ",
              _camera.Name);
            GetResLive(Guid.Empty);
        }

        /// <summary>
        /// Loop to get all live streams, write out properties, ask for image and display resolution.
        /// </summary>
        /// <returns></returns>
        static void GetStreams()
        {
            var streamDataSource = new StreamDataSource(_camera);
            List<DataType> dataTypes = streamDataSource.GetTypes();
            if (dataTypes == null || dataTypes.Count == 0)
                return;

            foreach (DataType dataType in dataTypes)
            {

                Console.WriteLine(Environment.NewLine +
                                  "Stream name: {0} Streamid: {1}" + Environment.NewLine + Environment.NewLine +
                                  "Properties (property-value pairs) ",
                    dataType.Name, dataType.Id);
                foreach (KeyValuePair<string, string> prop in dataType.Properties)
                {
                    Console.WriteLine("Property: {0} Value: {1}",
                        prop.Key, prop.Value);
                }
                Console.WriteLine("Getting an image from that stream..");
                GetResLive(dataType.Id);
            }

            Console.WriteLine(Environment.NewLine + "Number of streams {0}", dataTypes.Count);
        }

        /// <summary>
        /// General routine for asking for a stream
        /// </summary>
        /// <param name="streamid"></param>
        static void GetResLive(Guid streamid)
        {
            var jpegLive = new JPEGLiveSource(_camera);
            if (streamid != Guid.Empty) jpegLive.StreamId = streamid;
            _timeoutEvent = new Timer(TimeOutCallBack, null, 10000, 10000);

            jpegLive.LiveModeStart = true;
            jpegLive.Height = 0;
            jpegLive.Width = 0;
            jpegLive.Init();
            jpegLive.LiveContentEvent += jpegLiveContentEventHandler;

            _resetEvent.WaitOne();
            _timeoutEvent.Dispose();
            jpegLive.LiveContentEvent -= jpegLiveContentEventHandler;
        }

        /// <summary>
        /// Event Handler that runs when a live image is received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void jpegLiveContentEventHandler(object sender, EventArgs e)
        {
            var args = e as LiveContentEventArgs;
            if (args != null)
            {
                if (args.LiveContent != null)
                {
                    Console.WriteLine("The resolution of the live image " +
                                   "from camera {0} is ({1},{2})",
                                   _camera.Name,
                                   args.LiveContent.Width,
                                   args.LiveContent.Height);
                }
                else if (args.Exception != null)
                {
                    Console.WriteLine("Error occurred: " + args.Exception.Message);
                }
            }
            _resetEvent.Set();
        }

        /// <summary>
        /// Event handler to implement a time out
        /// </summary>
        /// <param name="obj"></param>
        static void TimeOutCallBack(Object obj)
        {
            Console.WriteLine("Waited for 10 seconds for an image. Likely the camera is not streaming right now.");
            Console.WriteLine(Environment.NewLine + "Press any key to exit.");

            _timeoutEvent.Dispose();
            Console.ReadKey();
            Environment.Exit(0);
        }
        #endregion

        static private SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }
    }
}
