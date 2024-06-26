using ConfigAPIBatch.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VideoOS.ConfigurationAPI;
using VideoOS.Platform.Login;
using VideoOS.Platform.Util;

namespace ConfigAPIBatch
{
    public class ConfigAPIClient
    {
        private IConfigurationService _client = null;

        private Dictionary<String, MethodInfo> _allMethodInfos = new Dictionary<string, MethodInfo>();
        private Dictionary<String, String> _translations = new Dictionary<String, String>();

        private Dictionary<String, Bitmap> _allMethodBitmaps = new Dictionary<string, Bitmap>();

        internal Dictionary<String, MethodInfo> AllMethodInfos { get { return _allMethodInfos; } }
        internal Dictionary<String, Bitmap> AllMethodBitmaps { get { return _allMethodBitmaps; } }

        internal bool TrimTreeToolStripMenuItem { get; set; }
        public string ServerAddress { get; set; }
        public int Serverport { get; set; }
        public bool BasicUser { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public bool SecureOnly { get; set; }

        public bool Connected { get; set; }

        #region Construction, Initialize and Close
        internal void InitializeClientProxy()
        {
            try
            {
                
                bool isOAuth = Task.Run(() => IdpClientProxy.IsOAuthServer(ServerAddress, Serverport)).GetAwaiter().GetResult();
                if (isOAuth)
                {
                    _client = CreateOAuthClientProxy(ServerAddress, Serverport, Username, Password, BasicUser);
                }
                else
                {
                    _client = CreateClientProxy(ServerAddress, Serverport, Username, Password, BasicUser);
                }
            }
            catch (EndpointNotFoundException)
            {
            }
        }

        private IConfigurationService CreateOAuthClientProxy(string address, int serverPort, string username, string password, bool isBasic)
        {
            // If the OAuth server is available it uses OAuth version of ServerCommandService.
            var uri = ConfigApiServiceOAuthHelper.CalculateServiceUrl(address, serverPort);
            var oauthBinding = ConfigApiServiceOAuthHelper.GetOAuthBinding(serverPort == 443);
            string spn = SpnFactory.GetSpn(uri);
            EndpointAddress endpointAddress = new EndpointAddress(uri, EndpointIdentity.CreateSpnIdentity(spn));

            ChannelFactory<IConfigurationService> channel = new ChannelFactory<IConfigurationService>(oauthBinding, endpointAddress);
            AccessTokenResponse accessToken = null;
            if(isBasic)
            {
                accessToken = Task.Run(() => IdpClientProxy.GetAccessTokenForBasicUserAsync(address, serverPort, username, password)).GetAwaiter().GetResult();
            }
            else
            {
                string httpScheme = Serverport == 80 ? Uri.UriSchemeHttp : Uri.UriSchemeHttps;
                Uri serverUri = new UriBuilder(httpScheme, ServerAddress, Serverport).Uri;
                string authType = BasicUser ? "Basic" : "Negotiate";
                var credential = Util.BuildNetworkCredential(uri, username, password, authType);
                accessToken = Task.Run(() => IdpClientProxy.GetAccessTokenForWindowsUserAsync(address, serverPort, credential)).GetAwaiter().GetResult();
            }
            channel.Endpoint.EndpointBehaviors.Add(new AddTokenBehavior(accessToken.Access_Token));
            ConfigApiServiceOAuthHelper.ConfigureEndpoint(channel.Endpoint);
            return channel.CreateChannel();
        }

        public IConfigurationService CreateClientProxy(string serverAddress, int serverport, string username, string password, bool isBasic)
        {
            string httpScheme = Serverport == 80 ? Uri.UriSchemeHttp : Uri.UriSchemeHttps;

            if (isBasic)
            {
                httpScheme = Uri.UriSchemeHttps;
                Serverport = 443;
            }

            Uri serverUri = new UriBuilder(httpScheme, ServerAddress, Serverport).Uri;

            string uriString = serverUri.ToString() + "ManagementServer/ConfigurationApiService.svc";
            ChannelFactory<IConfigurationService> channel = null;

            Uri uri = new UriBuilder(uriString).Uri;
            var binding = GetBinding(isBasic);
            var spn = SpnFactory.GetSpn(uri);
            var endpointAddress = new EndpointAddress(uri, EndpointIdentity.CreateSpnIdentity(spn));
            channel = new ChannelFactory<IConfigurationService>(binding, endpointAddress);

            ClientTokenHelper clientTokenHelper = new ClientTokenHelper(ServerAddress);
            channel.Endpoint.Behaviors.Add(new TokenServiceBehavior(clientTokenHelper));
            
            if (isBasic)
            {
                channel.Credentials.UserName.UserName = "[BASIC]\\" + Username;
                channel.Credentials.UserName.Password = Password;
                channel.Credentials.ServiceCertificate.SslCertificateAuthentication = new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                };
            }
            else
            {
                channel.Credentials.Windows.ClientCredential.UserName = Username;
                channel.Credentials.Windows.ClientCredential.Password = Password;
            }
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
                    _allMethodInfos.Add(mi.MethodId, mi);
                    _allMethodBitmaps.Add(mi.MethodId, MakeBitmap(mi.Bitmap));
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
            _allMethodInfos.Clear();
            _allMethodBitmaps.Clear();

        }

        #endregion

        #region utilities
        internal List<ConfigurationItem> GetNextForItemType(ConfigurationItem item, String itemtype)
        {
            List<ConfigurationItem> result = new List<ConfigurationItem>();
            ConfigurationItem[] children = _client.GetChildItems(item.Path);
            if (children != null)
                foreach (ConfigurationItem child in children)
                {
                    if (child.ItemType.StartsWith(itemtype))
                    {
                        result.Add(child);
                        continue;
                    }
                    if (child.ItemType == ItemTypes.HardwareFolder || child.ItemType == ItemTypes.Hardware)
                    {
                        result.Add(child);
                        continue;
                    }
                }
            return result;
        }

        private static List<String> _topItemTypes = new List<string>()
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

        private List<ConfigurationItem> GetTopForItemTypeRecursive(ConfigurationItem item, String itemtype)
        {
            List<ConfigurationItem> result = new List<ConfigurationItem>();
            ConfigurationItem[] children = _client.GetChildItems(item.Path);
            if (children != null)
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

                            if (_topItemTypes.Contains(child.ItemType))
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

        internal Dictionary<String, String> GetTranslations(string language)
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

        internal ConfigurationItem InvokeMethod(ConfigurationItem item, String methodId)
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

        #region private methods
        private Bitmap MakeBitmap(byte[] data)
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
            } else
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
                binding.Namespace = "VideoOS.ConfigurationAPI";
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                return binding;
            }
        }
        #endregion
    }

    /// <summary>
    /// This class assist in adding the login token to the SOAP header
    /// </summary>
    public class ClientTokenHelper : TokenHelper
    {
        private String _serverAddress;
        public ClientTokenHelper(string serverAddress)
        {
            _serverAddress = serverAddress;
        }
        public override string GetToken()
        {
            LoginSettings ls = VideoOS.Platform.Login.LoginSettingsCache.GetLoginSettings(_serverAddress);
            if (ls != null)
                return ls.Token;
            return "";
        }
        public override bool ValidateToken(string token)
        {
            // Not used on client side
            return true;
        }
    }

}
