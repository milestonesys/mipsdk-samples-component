using System;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.Util;
using System.Xml;
using System.Text;
using VideoOS.Common.Proxy.Server.WCF;
using System.ServiceModel.Security;
using VideoOS.Platform.UI;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceLock
{
    public class Program
    {
        private static readonly Guid IntegrationId = new Guid("F6EB0AF7-A93C-42EB-B7E3-F52FD321673D");
        private const string IntegrationName = "Evidence Lock";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Console.WriteLine("Milestone SDK evidence lock demo (XProtect Corporate only)");
            Console.WriteLine("Creates 2 new evidence locks and retrieves them using ");
            Console.WriteLine("  1) MarkedDataGet");
            Console.WriteLine("  2) MarkedDataSearch");
            Console.WriteLine("And then deletes them again");
            Console.WriteLine("");

            // Initialize the SDK - must be done in stand alone
            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();		// Initialize UI controls

            #region Connect to the server
            var loginForm = new VideoOS.Platform.SDK.UI.LoginDialog.DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            Application.Run(loginForm);								// Show and complete the form and login to server
            if (!_connected)
            {
                Console.WriteLine("Failed to connect or login");
                Console.ReadKey();
                return;
            }

            if (EnvironmentManager.Instance.MasterSite.ServerId.ServerType != ServerId.CorporateManagementServerType)
            {
                Console.WriteLine("Evidence locks are not supported on this product.");
                Console.ReadKey();
                return;				
            }
            #endregion

            #region Select a camera
            Item selectedItem = null;
            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
            itemPicker.KindsFilter = new List<Guid> { Kind.Camera };
            itemPicker.SelectionMode = SelectionModeOptions.AutoCloseOnSelect;
            itemPicker.Items = Configuration.Instance.GetItems();

            if (itemPicker.ShowDialog().Value)
            {
                selectedItem = itemPicker.SelectedItems.First();
            }

            if (selectedItem == null)
            {
                Console.WriteLine("Failed to pick a camera");
                Console.ReadKey();
                return;
            }

            if (selectedItem.FQID.ServerId.ServerType != ServerId.CorporateRecordingServerType)
            {
                Console.WriteLine("Evidence locks are not supported on this product");
                Console.ReadKey();
                return;
            }

            #endregion

            var loginSettings = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite);
            if (loginSettings.IsOAuthIdentity)
            {
                throw new NotSupportedMIPException("This sample does not support external identity providers.");
            }

            using (var client = CreateWcfClient(loginSettings))
            {
                // Get token needed for calls to manipulate evidence locks.
                var token = loginSettings.Token;
                var devicesToCreateLockOn = new[] { selectedItem.FQID.ObjectId };

                DateTime timeNow = DateTime.UtcNow;

                #region create first evidence lock

                Console.WriteLine("Creating the first evidence lock");

                var timeBegin1 = timeNow.AddMinutes(-60);
                var markedDataId1 = Guid.NewGuid();
                try {
                    var markedDataCreateResult = client.MarkedDataCreate(
                        token,
                        markedDataId1,
                        devicesToCreateLockOn,
                        timeBegin1,
                        timeBegin1.AddMinutes(5),
                        timeBegin1.AddMinutes(10),
                        string.Format("MyEvidenceLock-{0}", timeBegin1.ToLongTimeString()),
                        string.Format("AutoEvidenceLock-{0}", timeBegin1.ToLongTimeString()),
                        string.Format("AutoEvidenceLock-{0} set for a duration of 10 minutes", timeBegin1.ToLongTimeString()),
                        2, // 2 is used for evidence locks
                        true, // This must be set to true to actual lock data!
                        DateTime.MaxValue, // We never expire the the evidence lock
                        new RetentionOption { RetentionOptionType = RetentionOptionType.Indefinite, RetentionUnits = -1 }
                        );
                    Console.WriteLine("{0} when creating evidence lock 1 with ID = {1}", markedDataCreateResult.Status, markedDataCreateResult.MarkedData.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EvidenceLock 1 failed: " + ex.Message);
                    Console.WriteLine("Press any Key");
                    Console.ReadKey();
                    return;
                }

                #endregion

                #region Create a second evidence lock

                Console.WriteLine("Creating a second evidence lock - 20 minutes after the first evidence lock");
                DateTime timeBegin2 = timeBegin1.AddMinutes(20);
                var markedDataId2 = Guid.NewGuid();
                try
                {
                    var markedDataCreateResult = client.MarkedDataCreate(
                        token,
                        markedDataId2,
                        devicesToCreateLockOn,
                        timeBegin2,
                        timeBegin2.AddMinutes(5),
                        timeBegin2.AddMinutes(10),
                        string.Format("MyEvidenceLock-{0}", timeBegin2.ToLongTimeString()),
                        string.Format("AutoEvidenceLock-{0}", timeBegin2.ToLongTimeString()),
                        string.Format("AutoEvidenceLock-{0} set for a duration of 10 minutes", timeBegin2.ToLongTimeString()),
                        2, // 2 is used for evidence locks
                        true, // This must be set to true to actual lock data!
                        DateTime.MaxValue, // We never expire the the evidence lock
                        new RetentionOption { RetentionOptionType = RetentionOptionType.Indefinite, RetentionUnits = -1 }
                        );

                    Console.WriteLine("{0} when creating evidence lock 2 with ID = {1}", markedDataCreateResult.Status, markedDataCreateResult.MarkedData.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EvidenceLock 2 failed: " + ex.Message);
                    Console.WriteLine("Press any Key");
                    Console.ReadKey();
                    return;
                }


                #endregion

                #region MarkedDataSearch

                Console.WriteLine("Searching for evidence locks ...");
                var searchResult = client.MarkedDataSearch(
                    token,
                    devicesToCreateLockOn, // use empty array to search on all devices
                    null, // Use null to not search for text
                    null, // Use null to search for all users
                    DateTime.MinValue.ToUniversalTime(), 
                    DateTime.MinValue.ToUniversalTime(),
                    DateTime.UtcNow.AddHours(-1),
                    DateTime.UtcNow.AddSeconds(-10),
                    DateTime.MinValue.ToUniversalTime(),
                    DateTime.MinValue.ToUniversalTime(),
                    DateTime.MinValue.ToUniversalTime(),
                    DateTime.MinValue.ToUniversalTime(),
                    0,
                    100,
                    SortOrderOption.CreateTime,
                    true
                    );

                Console.WriteLine("-> Found {0} evidence lock{1}", searchResult.Length, searchResult.Length == 1 ? string.Empty : "s");
                if (searchResult.Length > 0)
                {
                    int counter = 1;
                    foreach (var markedData in searchResult)
                    {
                        Console.WriteLine("{0}:", counter);
                        PrintMarkedData(markedData);
                        counter++;
                    }
                }
                Console.WriteLine("");

                #endregion

                #region MarkedDataGet

                // Get first created evidence lock
                Console.WriteLine(
                    "Looking for the first evidence lock using MarkedDataGet  (finding the first of the 2 newly created)");
                var markedDataGet = client.MarkedDataGet(token, markedDataId1);
                if (markedDataGet != null)
                {
                    Console.WriteLine("-> An evidence lock was found");
                    PrintMarkedData(markedDataGet);
                }
                else
                {
                    Console.WriteLine("Sorry no evidence lock found");
                }
                Console.WriteLine("");

                #endregion

                #region Deleting evidence locks

                Console.WriteLine("Deleting the two newly created evidence locks");
                var deleteResult = client.MarkedDataDelete(token, new[] { markedDataId1, markedDataId2 });
                foreach (var markedDataResult in deleteResult)
                {
                    Console.WriteLine("Deleting marked data {0} was a {1}", markedDataResult.MarkedData.Id, markedDataResult.Status);
                }

                #endregion

                Console.WriteLine("");
                Console.WriteLine("Press any key");
                Console.Out.Flush();
                Console.ReadKey();
            }
        }

        private static void PrintMarkedData(MarkedData markedData)
        {
            Console.WriteLine("     Id  ={0} ", markedData.Id);
            Console.WriteLine("     Name={0} ", markedData.Header);
            Console.WriteLine("     Desc={0} ", markedData.Description);
            Console.WriteLine("     user={0} ", markedData.User);
            Console.WriteLine("     Start={0},  Stop={1}  ", markedData.StartTime, markedData.EndTime);
        }

        private static bool _connected = false;

        private static void SetLoginResult(bool connected)
        {
            _connected = connected;
        }

        private static ServerCommandServiceClient CreateWcfClient(LoginSettings loginSettings)
        {
            return CreateWcfClient(loginSettings, EnvironmentManager.Instance.MasterSite.ServerId.ServerHostname);
        }

        private static ServerCommandServiceClient CreateWcfClient(LoginSettings loginSettings, string hostName)
        {
            var serviceUri = new UriBuilder
            {
                Scheme = loginSettings.IsBasicUser ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
                Host = hostName,
                Path = "ManagementServer/ServerCommandService.svc"
            };

            return CreateWcfClient(loginSettings, serviceUri.Uri);
        }

        public static ServerCommandServiceClient CreateWcfClient(LoginSettings loginSettings, string hostName, int port)
        {
            var serviceUri = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttp,
                Host = hostName,
                Port = port,
                Path = "ManagementServer/ServerCommandService.svc"
            };

            return CreateWcfClient(loginSettings, serviceUri.Uri);
        }

        private static ServerCommandServiceClient CreateWcfClient(LoginSettings loginSettings, Uri uri)
        {
            var binding = GetBinding(loginSettings.IsBasicUser);
            var spn = SpnFactory.GetSpn(uri);
            var endpoint = new EndpointAddress(uri, EndpointIdentity.CreateSpnIdentity(spn));
            var client = new ServerCommandServiceClient(binding, endpoint);
            var nc = LoginSettingsCache.GetNetworkCredential(EnvironmentManager.Instance.MasterSite.ServerId);
            if (loginSettings.IsBasicUser)
            {
                client.ClientCredentials.UserName.UserName = "[BASIC]\\" + nc.UserName;
                client.ClientCredentials.UserName.Password = nc.Password;

                // If it's basic user, you need to specify the certificate validation mode yourself
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None
                };
            }
            else
            {
                client.ClientCredentials.Windows.ClientCredential = nc;
            }

            return client;
        }

        internal static System.ServiceModel.Channels.Binding GetBinding(bool IsBasic)
        {
            if (!IsBasic)
            {
                WSHttpBinding binding = new WSHttpBinding();
                var security = binding.Security;
                security.Message.ClientCredentialType = MessageCredentialType.Windows;

                binding.ReaderQuotas.MaxStringContentLength = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.MaxBufferPoolSize = 2147483647;

                binding.ReaderQuotas = XmlDictionaryReaderQuotas.Max;
                return binding;
            }
            else
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.MaxBufferSize = 2147483647;
                binding.MaxBufferPoolSize = 2147483647;
                binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
                binding.MessageEncoding = WSMessageEncoding.Text;
                binding.TextEncoding = Encoding.UTF8;
                binding.UseDefaultWebProxy = true;
                binding.AllowCookies = false;
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                return binding;
            }
        }

    }
}
