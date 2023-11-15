using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MultiSiteViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow, INotifyPropertyChanged
    {
        Guid integrationId = new Guid("1ae44977-3468-472e-9486-e66d82e681e0");
        string integrationName = "MultiSiteViewer";
        string manufacturerName = "Your company name";
        string version = "1.0";

        private Item _selectItem1;
        private Item _selectItem2;

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public TreeViewModel TreeViewModel { get; set; } = new TreeViewModel();
        public string CameraName1 => _selectItem1 == null ? "Select camera.." : _selectItem1.Name;
        public string CameraName2 => _selectItem2 == null ? "Select camera.." : _selectItem2.Name;        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void DoClose(object sender, RoutedEventArgs e)
        {
            if (_imageViewerControl1 != null)
            {
                _imageViewerControl1.Disconnect();
            }
            if (_imageViewerControl2 != null)
            {
                _imageViewerControl2.Disconnect();
            }
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        private void DoAdd(object sender, RoutedEventArgs e)
        {
            var dialog = new SiteAddWindow();
            if (dialog.ShowDialog() == true)
            {
                VideoOS.Platform.SDK.Environment.AddServer(
                    dialog.IsSecure,
                    dialog.SelectedSiteItem,
                    dialog.CredentialCache,
                    !dialog.SDKLoadedChildSites
                    );
                VideoOS.Platform.SDK.Environment.Login(
                    dialog.SelectedSiteItem.FQID.ServerId.Uri,
                    integrationId,
                    integrationName,
                    version,
                    manufacturerName,
                    !dialog.SDKLoadedChildSites
                    );
                TreeViewModel.AddTop(dialog.SelectedSiteItem);
                if (dialog.SampleLoadedChildSites)
                {
                    foreach (Item site in dialog.SelectedSiteItem.GetChildren())
                    {                        
                        AddSite(dialog.IsSecure, site, dialog.CredentialCache);
                    }
                }
                if (dialog.SDKLoadedChildSites)
                {
                    foreach (Item site in dialog.SelectedSiteItem.GetChildren())
                    {
                        AddSiteToTreeOnly(dialog.SelectedSiteItem, site);
                    }
                }
            }
        }

        private void AddSiteToTreeOnly(Item grandparent, Item parent)
        {
            TreeViewModel.AddChild(grandparent, parent);
            foreach (Item site in parent.GetChildren())
            {
                AddSiteToTreeOnly(parent, site);
            }
        }

        /// <summary>
        /// Recursive add all child sites. This can be used as an alternative to doing delayed load
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="credentialCache"></param>
        /// <param name="parentTn"></param>
        private void AddSite(bool secureOnly, Item parent, CredentialCache credentialCache)
        {
            VideoOS.Platform.SDK.Environment.AddServer(secureOnly, parent, credentialCache, true);
            VideoOS.Platform.SDK.Environment.Login(parent.FQID.ServerId.Uri, integrationId, integrationName, version, manufacturerName, true);
            TreeViewModel.AddTop(parent);
            foreach (Item site in parent.GetChildren())
            {
                AddSite(secureOnly, site, credentialCache);
            }
        }

        /// <summary>
        /// Remove one Server from the MIP SDK configuration.
        /// Any still running ImageViewerControl will continue until next token need to be refreshed.  
        /// For real applications, the ImageViewerControl should be closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoRemove(object sender, RoutedEventArgs e)
        {
            if (_configTreeView.SelectedItem != null)
            {
                var viewModel = _configTreeView.SelectedItem as ItemViewModel;
                if (viewModel != null)
                {
                    TreeViewModel.RemoveOne(viewModel);
                    RemoveServers(viewModel);
                }
            }
        }

        private void RemoveServers(ItemViewModel itemViewModel)
        {
            var list = new List<Item>();
            BuildListChildItems(itemViewModel, list);
            foreach (var item in list)
            {
                VideoOS.Platform.SDK.Environment.RemoveServer(item.FQID.ServerId.Id);
            }
        }

        private void BuildListChildItems(ItemViewModel itemViewModel, List<Item> list)
        {
            list.Add(itemViewModel.InternalItem);
            foreach (var item in itemViewModel.ChildItems)
            {
                BuildListChildItems(item, list);
            }
        }

        private void DoSelectionCamera1(object sender, RoutedEventArgs e)
        {
            if (_imageViewerControl1 != null)
            {
                _imageViewerControl1.Disconnect();
            }

            var dialog = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid>() { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItemsByKind(Kind.Camera, ItemHierarchy.SystemDefined),

            };
            if (dialog.ShowDialog() == true)
            {
                _selectItem1 = dialog.SelectedItems.FirstOrDefault();
                OnPropertyChanged(nameof(CameraName1));
                _imageViewerControl1.CameraFQID = _selectItem1.FQID;
                _imageViewerControl1.EnableVisibleHeader = true;
                _imageViewerControl1.Initialize();
                _imageViewerControl1.Connect();
                _imageViewerControl1.Selected = true;
            }
        }

        private void DoSelectionCamera2(object sender, RoutedEventArgs e)
        {
            if (_imageViewerControl2 != null)
            {
                _imageViewerControl2.Disconnect();
            }

            var dialog = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid>() { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItemsByKind(Kind.Camera),
            };
            if (dialog.ShowDialog() == true)
            {
                _selectItem2 = dialog.SelectedItems.FirstOrDefault();
                OnPropertyChanged(nameof(CameraName2));
                _imageViewerControl2.CameraFQID = _selectItem2.FQID;
                _imageViewerControl2.EnableVisibleHeader = true;
                _imageViewerControl2.Initialize();
                _imageViewerControl2.Connect();
                _imageViewerControl2.Selected = true;
            }
        }
    }
}