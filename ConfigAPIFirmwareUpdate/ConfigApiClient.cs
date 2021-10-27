using ConfigAPIClient.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VideoOS.ConfigurationAPI;
using VideoOS.Platform.Login;
using VideoOS.Platform.Util;

namespace ConfigAPIUpdateFirmwareWPF
{
    public class ConfigApiClient
    {
        private readonly string NOT_LICENSED_RECORDER_SERVER_MESSAGE = "Value cannot be null.\r\nParameter name: Hardware Driver Path empty";

        private IConfigurationService _client;
        private Dictionary<string, string> _translations = new Dictionary<string, string>();

        internal Dictionary<string, MethodInfo> AllMethodInfos { get; } = new Dictionary<string, MethodInfo>();
        internal Dictionary<string, Bitmap> AllMethodBitmaps { get; } = new Dictionary<string, Bitmap>();
        internal bool TrimTreeToolStripMenuItem { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public bool BasicUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public bool Connected { get; set; }
        public string DomainUser { get; internal set; }

        public string LastError { get; private set; }

        #region Construction, Initialize and Close
        internal void InitializeClientProxy()
        {
            try
            {
                LoginSettings loginSettings = LoginSettingsCache.GetLoginSettings(ServerAddress);
                if (loginSettings.IsOAuthConnection)
                {
                    _client = CreateOAuthClientProxy(ServerAddress, ServerPort, loginSettings);
                }
                else
                {

                    _client = CreateClientProxy(ServerAddress, ServerPort, loginSettings);
                }

                Connected = false;
            }
            catch (EndpointNotFoundException)
            {
            }

        }

        private IConfigurationService CreateOAuthClientProxy(string address, int serverPort, LoginSettings loginSettings)
        {
            // If the OAuth server is available it uses OAuth version of ServerCommandService.
            var uri = ConfigApiServiceOAuthHelper.CalculateServiceUrl(address, serverPort);
            var oauthBinding = ConfigApiServiceOAuthHelper.GetOAuthBinding(serverPort == 443);
            string spn = SpnFactory.GetSpn(uri);
            EndpointAddress endpointAddress = new EndpointAddress(uri, EndpointIdentity.CreateSpnIdentity(spn));

            ChannelFactory<IConfigurationService> channel = new ChannelFactory<IConfigurationService>(oauthBinding, endpointAddress);
            channel.Endpoint.EndpointBehaviors.Add(new AddTokenBehavior(loginSettings.IdentityTokenCache.Token));
            ConfigApiServiceOAuthHelper.ConfigureEndpoint(channel.Endpoint);

            return channel.CreateChannel();
        }

        private IConfigurationService CreateClientProxy(string serverAddress, int serverPort, LoginSettings loginSettings)
        {
            LastError = string.Empty;
            string uriString;
            string address = ServerPort == 0 ? ServerAddress : ServerAddress + ":" + ServerPort;

            if (BasicUser)
            {
                if (ServerPort == 80)
                    address = ServerAddress;
                uriString = $"https://{address}/ManagementServer/ConfigurationApiService.svc";
            }
            else
                uriString = $"http://{address}/ManagementServer/ConfigurationApiService.svc";

            Uri uri = new UriBuilder(uriString).Uri;
            var binding = GetBinding(BasicUser);
            var spn = SpnFactory.GetSpn(uri);
            var endpointAddress = new EndpointAddress(uri, EndpointIdentity.CreateSpnIdentity(spn));
            var channel = new ChannelFactory<IConfigurationService>(binding, endpointAddress);

            if (BasicUser)
            {
                // Note the domain == [BASIC] 
                if (channel.Credentials != null)
                {
                    DomainUser = "[BASIC]\\" + Username;
                    channel.Credentials.UserName.UserName = DomainUser;
                    channel.Credentials.UserName.Password = Password;
                }
            }
            else
            {
                if (channel.Credentials != null)
                {
                    channel.Credentials.Windows.ClientCredential.UserName = Username;
                    channel.Credentials.Windows.ClientCredential.Password = Password;
                    channel.Credentials.Windows.ClientCredential.Domain = Domain;

                    // VmoClient needs the domain along with the user name
                    DomainUser = string.IsNullOrWhiteSpace(Domain) ? Username : Domain + "\\" + Username;
                }
            }
            

            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            Connected = false;

            return channel.CreateChannel();
        }

        public void Initialize()
        {
            try
            {
                InitializeClientProxy();

                MethodInfo[] methods = _client.GetMethodInfos();
                foreach (MethodInfo mi in methods)
                {
                    if (!AllMethodInfos.ContainsKey(mi.MethodId))
                    {
                        AllMethodInfos.Add(mi.MethodId, mi);
                    }
                    if (!AllMethodBitmaps.ContainsKey(mi.MethodId))
                    {
                        AllMethodBitmaps.Add(mi.MethodId, MakeBitmap(mi.Bitmap));
                    }
                }

                _translations = _client.GetTranslations("en-US");
                Connected = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Connected = false;
                throw;
            }

        }

        internal void Close()
        {
            AllMethodInfos.Clear();
            AllMethodBitmaps.Clear();

        }

        #endregion

        #region Utilities

     
        private static readonly List<string> TopItemTypes = new List<string>
        {
            ItemTypes.System,
            ItemTypes.RecordingServer,
            ItemTypes.RecordingServerFolder,
            ItemTypes.HardwareDriverFolder,
            ItemTypes.HardwareDriver,
            ItemTypes.HardwareFolder,
            ItemTypes.Hardware
        };

        public List<ConfigurationItem> GetTopForItemType(string itemtype)
        {
            try
            {
                ConfigurationItem item = _client.GetItem("/");		// Start from top
                if (item == null)
                    return new List<ConfigurationItem>();

                if (item.ItemType == itemtype)
                    return new List<ConfigurationItem>() { item };

                List<ConfigurationItem> result = GetTopForItemTypeRecursive(item, itemtype);	// Loop recursive in hierarchy until itemtype is found
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetTopForItemType exception:" + ex.Message);
                return new List<ConfigurationItem>();
            }
        }

        private List<ConfigurationItem> GetTopForItemTypeRecursive(ConfigurationItem item, string itemtype)
        {
            List<ConfigurationItem> result = new List<ConfigurationItem>();
            ConfigurationItem[] children = _client.GetChildItems(item.Path);
            if (children == null) return result;
            foreach (ConfigurationItem child in children)
            {
                try
                {
                    if (child.ItemType != itemtype)
                    {
                        if (child.ItemType == ItemTypes.RecordingServer && itemtype != ItemTypes.HardwareDriver)
                        {
                            result.Add(child);
                            continue;
                        }
                        if (child.ItemType == itemtype || child.ItemType == itemtype + "Folder")
                        {
                            if (child.ItemType.EndsWith("Folder") && TrimTreeToolStripMenuItem && child.ItemType != ItemTypes.HardwareDriverFolder)
                            {
                                result.AddRange(GetTopForItemTypeRecursive(child, itemtype));
                            }
                            else
                                result.Add(child);
                            continue;
                        }

                        if (TopItemTypes.Contains(child.ItemType))
                        {
                            result.AddRange(GetTopForItemTypeRecursive(child, itemtype));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("GetTopForItemTypeRecursive exception:" + ex.Message);
                    throw;
                }
            }
            return result;
        }

        #endregion

        #region Configuration API calls
        internal ConfigurationItem[] GetChildItems(string path)
        {
            try
            {
                return _client.GetChildItems(path);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.GetChildItems(path);
                }
                catch (Exception)
                {
                    return new ConfigurationItem[0];
                }
            }
        }

        internal MethodInfo[] GetMethodInfos()
        {
            try
            {
                return _client.GetMethodInfos();
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.GetMethodInfos();
                }
                catch (Exception)
                {
                    return new MethodInfo[0];
                }
            }
        }

        internal Dictionary<string, string> GetTranslations(string language)
        {
            try
            {
                return _client.GetTranslations(language);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.GetTranslations(language);
                }
                catch (Exception)
                {
                    return new Dictionary<string, string>();
                }
            }
        }

        internal ConfigurationItem InvokeMethod(ConfigurationItem item, string methodId)
        {
            try
            {
                return _client.InvokeMethod(item, methodId);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.InvokeMethod(item, methodId);
                }
                catch (FaultException fe)
                {
                    if (fe.Message.Equals(NOT_LICENSED_RECORDER_SERVER_MESSAGE))
                    {
                        LastError = string.Format("Invoking method {0} failed. Please make sure the recording server is licensed to be able to add hardware.", methodId);
                        throw new InvalidOperationException(LastError);
                    }
                    else
                    {
                        LastError = string.Format("Invoking method {0} failed with error {1}. ", methodId, fe.Reason.ToString());
                        throw new InvalidOperationException(LastError);
                    }

                    throw;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        internal ValidateResult ValidateItem(ConfigurationItem item)
        {
            try
            {
                return _client.ValidateItem(item);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.ValidateItem(item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }


        internal ConfigurationItem GetItem(string path)
        {
            try
            {
                return _client.GetItem(path);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.GetItem(path);
                }
                catch (Exception)
                {
                    // Let caller handle it
                    throw;
                }
            }
        }

        internal ValidateResult SetItem(ConfigurationItem item)
        {
            try
            {
                return _client.SetItem(item);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.SetItem(item);
                }
                catch (Exception)
                {
                    // Let caller handle it
                    throw;
                }
            }
        }
        #endregion

        #region Private Methods
        private static Bitmap MakeBitmap(byte[] data)
        {
            try
            {
                if (data == null)
                    return null;
                MemoryStream ms = new MemoryStream(data);
                Bitmap b = new Bitmap(ms);
                ms.Close();
                return b;
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static System.ServiceModel.Channels.Binding GetBinding(bool isBasic)
        {
            if (!isBasic)
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
                BasicHttpBinding binding = new BasicHttpBinding
                {
                    ReaderQuotas = { MaxStringContentLength = 2147483647 },
                    MaxReceivedMessageSize = 2147483647,
                    MaxBufferSize = 2147483647,
                    MaxBufferPoolSize = 2147483647,
                    HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                    MessageEncoding = WSMessageEncoding.Text,
                    TextEncoding = Encoding.UTF8,
                    UseDefaultWebProxy = true,
                    AllowCookies = false,
                    Namespace = "VideoOS.ConfigurationAPI",
                    Security =
                    {
                        Mode = BasicHttpSecurityMode.Transport,
                        Transport = {ClientCredentialType = HttpClientCredentialType.Basic}
                    }
                };
                return binding;
            }
        }
        #endregion
    }
}
