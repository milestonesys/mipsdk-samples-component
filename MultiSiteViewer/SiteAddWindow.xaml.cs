using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;

namespace MultiSiteViewer
{
    /// <summary>
    /// Interaction logic for SiteAddWindow.xaml
    /// </summary>
    public partial class SiteAddWindow : VideoOS.Platform.UI.Controls.VideoOSWindow, INotifyPropertyChanged
    {
        private CredentialCache _credentialCache = null;
        public SiteAddWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region Properties controlling the UI
        public string ServerUrl { get; set; }
        public string UserName { get; set; } 
        public bool IsAD { get; set; } = true;
        public bool IsBasic { get; set; }
        public bool IsCurrent { get; set; }        
        public bool IsEnabledUserPass { get; set; } = true;
        public bool IsEnabledExitButton { get; set; }
        #endregion
        #region Properties to be used when adding server(s)
        public bool IsSecure { get; set; }
        
        //Child sites being loaded by SDK, by MasterOnly set to false
        public bool SDKLoadedChildSites { get; set; } = false;

        //Child sites loaded one, by one by the sample
        public bool SampleLoadedChildSites { get; set; } = false;

        //Child sites not loaded
        public bool NoChildSites { get; set; } = true;

        internal Item SelectedSiteItem
        {
            get
            {
                if (_sitesIP.SelectedItems != null)
                {
                    return _sitesIP.SelectedItems.First();
                }
                return null;
            }
        }

        internal CredentialCache CredentialCache
        {
            get
            {
                // Very important: Include ALL sites in the Credential Cache, in order to be allowed to access the sites.
                AddUriToCache(_credentialCache, SelectedSiteItem);
                return _credentialCache;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion


        // Add a given URI to the CredentialCache
        internal void AddUriToCache(CredentialCache cc, Item item)
        {
            String authorization = IsBasic ? "Basic" : "Negotiate";
            // only add to the credential cache if not already present
            if (cc.GetCredential(item.FQID.ServerId.Uri, authorization) == null)
            {
                try
                {
                    if (IsCurrent)
                        cc.Add(item.FQID.ServerId.Uri, authorization, CredentialCache.DefaultNetworkCredentials);
                    else
                    {
                        string[] parts = UserName.Split('\\');
                        if (parts.Length == 1)
                            cc.Add(item.FQID.ServerId.Uri, authorization, new NetworkCredential(UserName, _password.SecurePassword));
                        else
                            cc.Add(item.FQID.ServerId.Uri, authorization, new NetworkCredential(parts[1], _password.SecurePassword, parts[0]));
                    }
                }
                catch (Exception ex)
                {
                    EnvironmentManager.Instance.Log(true, "AddUriToCache", ex.Message);
                    VideoOSMessageBox.Show(this, "Exception in Site Add", "Exception in AddUriToCache", ex.Message, VideoOSMessageBox.Buttons.OK, VideoOSMessageBox.ResultButtons.OK, new VideoOSIconBuiltInSource() { Icon = VideoOSIconBuiltInSource.Icons.Error_Combined });
                }
            }

            if (SDKLoadedChildSites || SampleLoadedChildSites)
            {
                foreach (Item child in item.GetChildren())
                {
                    AddUriToCache(cc, child);
                }
            }
        }

        private void AfterSelect(object sender, EventArgs e)
        {
            var control = sender as ItemPickerWpfUserControl;
            if(control == null || control.SelectedItems == null ) 
            { 
                return; 
            }
            var select = control.SelectedItems.FirstOrDefault();
            if ( select == null ) 
            { 
                return; 
            }

            if (select.Name != "Unable to contact server with given credentials")
            {
                IsEnabledExitButton = true;
                OnPropertyChanged(nameof(IsEnabledExitButton));
            }
        }
        

        private void DoClose(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void DoCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void CurrentUncheck(object sender, RoutedEventArgs e)
        {
            IsEnabledUserPass = true;
            OnPropertyChanged(nameof(IsEnabledUserPass));
        }

        private void CurrentChecked(object sender, RoutedEventArgs e)
        {
            UserName = Environment.UserDomainName + "\\" + Environment.UserName;
           

            IsEnabledUserPass = false;
            OnPropertyChanged(nameof(IsEnabledUserPass));
            OnPropertyChanged(nameof(UserName));
        }

        private void Validate(object sender, RoutedEventArgs e)
        {
            if (ServerUrl.StartsWith("http://") == false && ServerUrl.StartsWith("https://") == false)
            {
                ServerUrl = "https://" + ServerUrl;
            }
                
            Uri uri = new Uri(ServerUrl);
            String authorization = IsBasic == true ? "Basic" : "Negotiate";
            String username = IsCurrent == true ? "" : UserName;
            _credentialCache = VideoOS.Platform.Login.Util.BuildCredentialCache(uri, username, _password.SecurePassword, authorization);
            Item siteItem = VideoOS.Platform.SDK.Environment.LoadSiteItem(IsSecure, uri, _credentialCache);
            if (siteItem == null)
            {
                _sitesIP.Items = new List<Item>() { ItemBuilder("Unable to contact server with given credentials", FolderType.SystemDefined, Kind.Server) };                
                IsEnabledExitButton = false;
                OnPropertyChanged(nameof(IsEnabledExitButton));
            }
            else
            {
                _sitesIP.Items = new List<Item>() { siteItem };
            }
        }

        public Item ItemBuilder(string name, FolderType folderType, Guid kind)
        {
            ServerId serverId = new ServerId(ServerId.CorporateManagementServerType, "top", 1, Guid.NewGuid());
            return new Item(serverId, Guid.Empty, Guid.NewGuid(), name, folderType, kind);
        }
    }
}
