using ConfigAPIClient.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VideoOS.ConfigurationAPI;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.Resources;
using VideoOS.Platform.Util;

namespace ConfigAPIClient
{
    public class ConfigApiClient
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
        public ServerTypeEnum ServerType { get; set; }

        public enum ServerTypeEnum
        {
            Corporate,
            Arcus
        } 
        internal bool Connected { get; set; }

        #region Construction, Initialize and Close
        internal void InitializeClientProxy()
        {
            try
            {
                LoginSettings loginSettings = LoginSettingsCache.GetLoginSettings(ServerAddress);
                if (loginSettings.IsOAuthConnection)
                {
                    _client = CreateOAuthClientProxy(ServerAddress, Serverport, loginSettings);
                }
                else
                {
                    _client = CreateClientProxy(ServerAddress, Serverport, loginSettings);
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

        public IConfigurationService CreateClientProxy(string serverAddress, int serverport, LoginSettings loginSettings)
        {
            string address = serverport == 0 ? serverAddress : serverAddress + ":" + serverport;
            bool basic = loginSettings != null && loginSettings.IsBasicUser;

            string hostName = address;
            string uriString;
            if (ServerType == ServerTypeEnum.Corporate && loginSettings != null)
            {
                uriString = new UriBuilder(loginSettings.UriCorporate).Uri.ToString() + "ManagementServer/ConfigurationApiService.svc";
            }
            else
            {
                uriString = String.Format("http://{0}/Config", hostName);
            }
            ChannelFactory<IConfigurationService> channel = null;

            Uri uri = new UriBuilder(uriString).Uri;
            var binding = GetBinding(basic, ServerType == ServerTypeEnum.Corporate);
            var spn = SpnFactory.GetSpn(uri);
            var endpointAddress = new EndpointAddress(uri, EndpointIdentity.CreateSpnIdentity(spn));
            channel = new ChannelFactory<IConfigurationService>(binding, endpointAddress);

            ClientTokenHelper clientTokenHelper = new ClientTokenHelper(ServerAddress);
            channel.Endpoint.Behaviors.Add(new TokenServiceBehavior(clientTokenHelper));
           
            if (loginSettings != null)
            {
                if (basic)
                {
                    channel.Credentials.UserName.UserName = "[BASIC]\\" + loginSettings.NetworkCredential.UserName;
                    channel.Credentials.UserName.Password = loginSettings.NetworkCredential.Password;
                    channel.Credentials.ServiceCertificate.SslCertificateAuthentication = new X509ServiceCertificateAuthentication()
                    {
                        CertificateValidationMode = X509CertificateValidationMode.None,
                    };
                }
                else
                {
                    channel.Credentials.Windows.ClientCredential.UserName = loginSettings.NetworkCredential.UserName;
                    channel.Credentials.Windows.ClientCredential.Password = loginSettings.NetworkCredential.Password;
                }
            }
            return channel.CreateChannel();
        }

        internal void Initialize()
        {
            try
            {
                InitializeClientProxy();

                _translations = _client.GetTranslations("en-US");

                MethodInfo[] methods = _client.GetMethodInfos();
                foreach (MethodInfo mi in methods)
                {
                    _allMethodInfos.Add(mi.MethodId, mi);
                    _allMethodBitmaps.Add(mi.MethodId, MakeBitmap(mi.Bitmap));

                    //Debug.WriteLine("Method: id=" + mi.MethodId + ",DisplayName=" + mi.DisplayName + ",TranslationId=" + mi.TranslationId);
                    String translated = _translations.ContainsKey(mi.TranslationId) ? _translations[mi.TranslationId] : "not found";
                    //Debug.WriteLine("        Translation Lookup:" + translated);
                }

                Connected = true;
            }
            catch (Exception ex)
            {
                Connected = false;
                MessageBox.Show("Unable to contact server:" + ex.Message);
            }

        }

        internal String Translate(String key)
        {
            String text = "";
            _translations.TryGetValue(key, out text);
            return text;
        }

        internal String Token
        {
            get
            {
                LoginSettings ls = VideoOS.Platform.Login.LoginSettingsCache.GetLoginSettings(ServerAddress);
                return ls.Token;
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
                    if (child.ItemType.StartsWith(itemtype)
                        || ((child.ItemType == ItemTypes.StorageFolder || child.ItemType == ItemTypes.Storage) && itemtype == ItemTypes.ArchiveStorage)
                        || (child.ItemType == ItemTypes.HardwareFolder || child.ItemType == ItemTypes.Hardware) ||
                        (child.ItemType.StartsWith(ItemTypes.Camera) && itemtype == ItemTypes.PatrollingProfile))
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
                ConfigurationItem item;
                switch (itemtype)
                {
                    case ItemTypes.Role:
                        item = _client.GetItem("/RoleFolder");
                        break;
                    case ItemTypes.Layout:
                        item = _client.GetItem("/LayoutGroupFolder");
                        break;
                    case ItemTypes.VideoWall:
                    case ItemTypes.Monitor:
                    case ItemTypes.VideoWallPreset:
                        item = _client.GetItem("/VideoWallFolder");
                        break;
                    case ItemTypes.MIPItem:
                        item = _client.GetItem("/MIPKindFolder");
                        break;
                    case ItemTypes.ArchiveStorage:
                        item = _client.GetItem("/RecordingServerFolder");
                        break;
                    case ItemTypes.FailoverRecorder:
                        item = _client.GetItem("/"+ItemTypes.FailoverGroupFolder);
                        break;
                    default:
                        item = _client.GetItem("/"); 
                        break;
                }
                if (item == null)
                    return new List<ConfigurationItem>();

                if (item.ItemType.StartsWith(itemtype))
                    return new List<ConfigurationItem>() { item };

                List<ConfigurationItem> result = GetTopForItemTypeRecursive(item, itemtype);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        if (child.ItemType == itemtype || child.ItemType == itemtype + "Folder")
                        {
                            if (child.ItemType.EndsWith("Folder") && TrimTreeToolStripMenuItem)// && child.ItemType != ItemTypes.HardwareDriverFolder)
                            {
                                result.AddRange(GetTopForItemTypeRecursive(child, itemtype));
                            }
                            else
                                result.Add(child);
                            continue;
                        }

                        if (child.ItemType != itemtype)
                        {
                            if (child.ItemType == ItemTypes.RecordingServer && itemtype != ItemTypes.HardwareDriver
                                || child.ItemType == ItemTypes.MIPKind && itemtype == ItemTypes.MIPItem
                                || child.ItemType == ItemTypes.VideoWall &&(itemtype == ItemTypes.Monitor || itemtype == ItemTypes.VideoWallPreset)
                                || child.ItemType == ItemTypes.FailoverGroup && itemtype == ItemTypes.FailoverRecorder)
                            {
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
                        MessageBox.Show("GetTopForItemTypeRecursive threw exception " + ex.GetType().ToString() + ", Message: " + ex.Message);
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
                    Debug.WriteLine("InvokeMethod:" + ex.Message);
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
                    Debug.WriteLine("ValidateItem:" + ex.Message);
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

        internal ConfigurationItem[] FillChildren(string path, string[] itemTypes, ItemFilter[] itemFilters)
        {
            try
            {
                return _client.GetChildItemsHierarchy(path, itemTypes, itemFilters);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.GetChildItemsHierarchy(path, itemTypes, itemFilters);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FillChildren:" + ex.Message);
                    throw;
                }
            }
        }

        internal ConfigurationItem[] QueryItems(ItemFilter itemFilter, int maxResult)
        {
            try
            {
                return _client.QueryItems(itemFilter, maxResult);
            }
            catch (Exception)
            {
                InitializeClientProxy();
                try
                {
                    return _client.QueryItems(itemFilter, maxResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("QueryItems:" + ex.Message);
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

        internal static System.ServiceModel.Channels.Binding GetBinding(bool IsBasic, bool isCorporate)
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
                if (isCorporate)
                {
                    binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                }
                else
                {
                    binding.Security.Mode = BasicHttpSecurityMode.None;                                        
                }
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
            LoginSettings ls = LoginSettingsCache.GetLoginSettings(_serverAddress);
            if (ls!=null)
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
