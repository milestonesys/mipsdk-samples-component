using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.UI;
using VideoOS.Platform.UI.Controls;
using VideoOS.Platform.Util;

namespace ConfigAccessViaSDK
{
    /// <summary>
    /// Interaction logic for DumpConfigurationUserControl.xaml
    /// </summary>
    public partial class DumpConfigurationUserControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        private string _itemListing;

        public string ItemListing
        {
            get
            {
                return _itemListing;
            }
            set
            {
                if (_itemListing == value)
                {
                    return;
                }

                _itemListing = value;
                OnPropertyChanged(nameof(ItemListing));
            }
        }

        private List<VideoOSTreeViewItem> _itemDetails;

        public List<VideoOSTreeViewItem> ItemDetails
        {
            get
            {
                return _itemDetails;
            }
            set
            {
                _itemDetails = value;
                OnPropertyChanged(nameof(ItemDetails));
            }
        }

        private List<VideoOSTreeViewItem> _disabledItems;

        public List<VideoOSTreeViewItem> DisabledItems
        {
            get
            {
                return _disabledItems;
            }
            set
            {
                _disabledItems = value;
                OnPropertyChanged(nameof(DisabledItems));
            }
        }

        private string _itemToken;

        public string ItemToken
        {
            get
            {
                return _itemToken;
            }
            set
            {
                if (_itemToken == value)
                {
                    return;
                }

                _itemToken = value;
                OnPropertyChanged(nameof(ItemToken));
            }
        }

        private List<Item> _filledItems = new List<Item>();

        private Item _selectedItem = null;

        internal Item SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnSelectedItemChanged();
            }
        }

        private List<FQID> _selectedDevices = null;

        internal List<FQID> SelectedDevices
        {
            get
            {
                return _selectedDevices;
            }
            set
            {
                _selectedDevices = value;
                var selectedItemsTextListing = new StringBuilder();
                if (_selectedDevices != null && _selectedDevices.Count() > 0)
                {
                    foreach (FQID fqid in _selectedDevices)
                    {
                        if (selectedItemsTextListing.Length > 0)
                        {
                            selectedItemsTextListing.Append(", ");
                        }
                        Item newDevice = Configuration.Instance.GetItem(fqid);
                        selectedItemsTextListing.Append($"{newDevice.Name}");
                    }
                }

                ItemListing = selectedItemsTextListing.ToString();
            }
        }

        public DumpConfigurationUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void Clear()
        {
            _itemPicker.Items = new List<Item>();
        }

        internal Item GetSelectedItem()
        {
            return SelectedItem;
        }

        internal void ShowInfo(string info)
        {
            ItemListing = info;
        }

        public void FillContent()
        {
            try
            {
                var list = Configuration.Instance.GetItems();
                if (list != null)
                {
                    if (_filledItems.Any())
                    {
                        _filledItems = new List<Item>();
                    }
                    _itemPicker.Items = new List<Item>();
                    FillNodes(list);

                    if (EnvironmentManager.Instance.MasterSite != null)
                    {
                        Item site = EnvironmentManager.Instance.GetSiteItem(EnvironmentManager.Instance.MasterSite);
                        if (site != null)
                        {
                            Guid parentId = Guid.Empty;
                            Guid objectId = Guid.NewGuid();
                            ServerId serverId = new ServerId(ServerId.CorporateManagementServerType, "top", 1, Guid.NewGuid());
                            Item tn = new Item(serverId, parentId, objectId, "Site-Hierarchy", FolderType.No, Kind.Folder);
                            FillNodes(new List<Item>() { site }, tn);
                        }
                    }

                    _itemPicker.Items = _filledItems;
                }
            }
            catch (Exception e)
            {
                EnvironmentManager.Instance.ExceptionDialog("FillContentAsync", e);
            }
        }

        private void FillNodes(List<Item> list, Item treeNode = null)
        {
            list.Sort((i1, i2) => Sort.NumericStringCompare(i1.Name, i2.Name));
            if (treeNode != null)
            {
                treeNode.SetChildren(list);
                list = new List<Item>() { treeNode };
            }

            foreach (Item item in list)
            {
                _filledItems.Add(item);
            }
        }

        public void FillDisabledItems()
        {
            try
            {
                List<VideoOSTreeViewItem> disabledTreeViewItems = new List<VideoOSTreeViewItem>();
                List<Item> disabledItems =
                        VideoOS.Platform.SDK.Environment.GetDisabledDevices(
                            Configuration.Instance.ServerFQID.ServerId.Id);
                Guid parentId = Guid.Empty;
                Guid objectId = Guid.NewGuid();
                ServerId serverId = new ServerId(ServerId.CorporateManagementServerType, "top", 1, Guid.NewGuid());
                Item tnDisabled = new Item(serverId, parentId, objectId, "Disabled devices...", FolderType.No, Kind.Server);

                var treeViewItem = new VideoOSTreeViewItem()
                {
                    Data = tnDisabled
                };

                foreach (Item item in disabledItems)
                {
                    var tvi = new VideoOSTreeViewItem()
                    {
                        Data = item,
                    };
                    disabledTreeViewItems.Add(tvi);
                }
                treeViewItem.Children = disabledTreeViewItems;
                DisabledItems = new List<VideoOSTreeViewItem>() { treeViewItem };
            }
            catch (Exception e)
            {
                EnvironmentManager.Instance.ExceptionDialog("FillDisabledItems", e);
            }
        }

        internal void FillContentSpecific(ItemHierarchy hierarchy)
        {
            if (_filledItems.Any())
            {
                _filledItems = new List<Item>();
            }
            _itemPicker.Items = new List<Item>();
            List<Item> top = Configuration.Instance.GetItems(hierarchy);
            FillNodes(top);
            if (EnvironmentManager.Instance.MasterSite != null)
            {
                Item site = EnvironmentManager.Instance.GetSiteItem(EnvironmentManager.Instance.MasterSite);
                if (site != null)
                {
                    Guid parentId = Guid.Empty;
                    Guid objectId = Guid.NewGuid();
                    ServerId serverId = new ServerId(ServerId.CorporateManagementServerType, "top", 1, Guid.NewGuid());
                    Item tn = new Item(serverId, parentId, objectId, "Site-Hierarchy", FolderType.No, Kind.Folder);
                    FillNodes(new List<Item>() { site }, tn);
                }
            }
            _itemPicker.Items = _filledItems;
        }

        private void OnSelect_Click(object sender, RoutedEventArgs e)
        {
            DevicePickerWindow window = new DevicePickerWindow();
            if (SelectedDevices != null && SelectedDevices.Any())
            {
                var preSelectedItems = new List<Item>();
                List<FQID> FQIDlist = new List<FQID>();
                foreach (FQID fqid in SelectedDevices)
                {
                    FQIDlist.Add(fqid);
                    var item = Configuration.Instance.GetItem(fqid);
                    preSelectedItems.Add(item);
                }
                window.SelectedFQID = FQIDlist;
                window.PreSelectedItems = preSelectedItems;
            }

            window.ShowDialog();

            List<FQID> selectedFQID = window.SelectedFQID;
            List<FQID> list = new List<FQID>();
            if (selectedFQID != null)
            {
                foreach (FQID fqid in selectedFQID)
                {
                    list.Add(fqid);
                }
                SelectedDevices = list;
            }
        }

        private void OnSelectedItemChanged()
        {
            if (_selectedItem != null)
            {
                LoginSettings loginSettings = LoginSettingsCache.GetLoginSettings(_selectedItem.FQID);
                ItemToken = (loginSettings != null) ? loginSettings.Token : "--- token not found ---";
            }
        }

        private void OnHierarchyChanged(object sender, EventArgs e)
        {
            this.FillContentSpecific((bool)_physical_CheckBox.IsChecked ? ItemHierarchy.SystemDefined : ItemHierarchy.UserDefined);
        }

        #region the detail dump methods

        private void DumpFQID(Item item, VideoOSTreeViewItem tn)
        {
            VideoOSTreeViewItem fqidNode = new VideoOSTreeViewItem()
            {
                Data = "FQID",
                IsExpanded = true,
            };

            var fqidNodeChildren = new List<VideoOSTreeViewItem>()
            {
                new VideoOSTreeViewItem() { Data = "FQID.Kind: " + item.FQID.Kind + " (" + Kind.DefaultTypeToNameTable[item.FQID.Kind] + ")" },
                new VideoOSTreeViewItem() { Data = "FQID.ServerId.ServerType: " + item.FQID.ServerId.ServerType },
                new VideoOSTreeViewItem() { Data = "FQID.ServerId.Id: " + item.FQID.ServerId.Id },
                new VideoOSTreeViewItem() { Data = "FQID.ServerId.ServerHostname: " + item.FQID.ServerId.ServerHostname },
                new VideoOSTreeViewItem() { Data = "FQID.ServerId.Serverport: " + item.FQID.ServerId.Serverport },
                new VideoOSTreeViewItem() { Data = "FQID.ServerId.ServerScheme: " + item.FQID.ServerId.ServerScheme },
                new VideoOSTreeViewItem() { Data = "FQID.ParentId: " + item.FQID.ParentId },
                new VideoOSTreeViewItem() { Data = "FQID.ObjectId: " + item.FQID.ObjectId },
                new VideoOSTreeViewItem() { Data = "FQID.ObjectIdString: " + item.FQID.ObjectIdString },
                new VideoOSTreeViewItem() { Data = "FQID.FolderType: " + item.FQID.FolderType },
            };

            fqidNode.Children = fqidNodeChildren;
            if (tn.Children != null)
            {
                tn.Children.Add(fqidNode);
            }
            else
            {
                tn.Children = new List<VideoOSTreeViewItem> { fqidNode };
            }
        }

        private void DumpProperties(Item item, VideoOSTreeViewItem tn)
        {
            VideoOSTreeViewItem propNode = new VideoOSTreeViewItem()
            {
                Data = "Properties",
                IsExpanded = true,
            };

            var propNodeChildren = new List<VideoOSTreeViewItem>()
            {
                new VideoOSTreeViewItem() { Data = "Enabled = " + item.Enabled }
            };

            if (item.Properties != null)
            {
                foreach (string key in item.Properties.Keys)
                {
                    propNodeChildren.Add(new VideoOSTreeViewItem() { Data = key + " = " + item.Properties[key] });
                }
            }

            propNode.Children = propNodeChildren;
            if (tn.Children != null)
            {
                tn.Children.Add(propNode);
            }
            else
            {
                tn.Children = new List<VideoOSTreeViewItem> { propNode };
            }
        }

        private void DumpAuthorization(Item item, VideoOSTreeViewItem tn)
        {
            VideoOSTreeViewItem authNode = new VideoOSTreeViewItem()
            {
                Data = "Authorization",
                IsExpanded = true,
            };

            var authNodeChildren = new List<VideoOSTreeViewItem>();

            if (item.Authorization != null)
            {
                foreach (string key in item.Authorization.Keys)
                {
                    if (key.ToLower().Contains("password"))
                    {
                        authNodeChildren.Add(new VideoOSTreeViewItem() { Data = key + " = " + "**********" });
                    }
                    else
                    {
                        authNodeChildren.Add(
                            new VideoOSTreeViewItem()
                            {
                                Data = key + " = " + item.Authorization[key]
                            }
                        );
                    }
                }

                authNode.Children = authNodeChildren;
                if (tn.Children != null)
                {
                    tn.Children.Add(authNode);
                }
                else
                {
                    tn.Children = new List<VideoOSTreeViewItem> { authNode };
                }
            }
        }

        private void DumpRelated(Item item, VideoOSTreeViewItem tn)
        {
            VideoOSTreeViewItem relatedNode = new VideoOSTreeViewItem()
            {
                Data = "Related",
                IsExpanded = true,
            };

            var relatedNodeChildren = new List<VideoOSTreeViewItem>();

            List<Item> related = item.GetRelated();
            if (related != null)
            {
                foreach (Item rel in related)
                {
                    var relatedTreeViewItem = new VideoOSTreeViewItem()
                    {
                        Data = rel.Name,
                    };
                    relatedNodeChildren.Add(relatedTreeViewItem);
                }
            }

            relatedNode.Children = relatedNodeChildren;
            if (tn.Children != null)
            {
                tn.Children.Add(relatedNode);
            }
            else
            {
                tn.Children = new List<VideoOSTreeViewItem> { relatedNode };
            }
        }

        private void DumpFields(Item item, VideoOSTreeViewItem tn)
        {
            VideoOSTreeViewItem fields = new VideoOSTreeViewItem()
            {
                Data = "Fields",
                IsExpanded = true,
            };

            var fieldsChildren = new List<VideoOSTreeViewItem>()
            {
                new VideoOSTreeViewItem() { Data = "HasRelated : " + item.HasRelated },
                new VideoOSTreeViewItem() { Data = "HasChildren: " + item.HasChildren },
            };

            if (item.PositioningInformation == null)
            {
                fieldsChildren.Add(new VideoOSTreeViewItem() { Data = "No PositioningInformation" });
            }
            else
            {
                fieldsChildren.Add(new VideoOSTreeViewItem()
                {
                    Data = "PositioningInformation: Latitude="
                    + item.PositioningInformation.Latitude
                    + ", longitude="
                    + item.PositioningInformation.Longitude
                });
                fieldsChildren.Add(new VideoOSTreeViewItem()
                {
                    Data = "PositioningInformation: CoverageDirection="
                    + item.PositioningInformation.CoverageDirection
                });
                fieldsChildren.Add(new VideoOSTreeViewItem()
                {
                    Data = "PositioningInformation: CoverageDepth="
                    + item.PositioningInformation.CoverageDepth
                });
                fieldsChildren.Add(new VideoOSTreeViewItem()
                {
                    Data = "PositioningInformation: CoverageFieldOfView="
                    + item.PositioningInformation.CoverageFieldOfView
                });
            }

            fields.Children = fieldsChildren;
            if (tn.Children != null)
            {
                tn.Children.Add(fields);
            }
            else
            {
                tn.Children = new List<VideoOSTreeViewItem> { fields };
            }
        }
        #endregion

        private void DisabledItemsSelectedItemsChanged(object sender, RoutedEventArgs e)
        {
            var selectedTreeViewItem = (sender as VideoOSTreeView).SelectedItem as VideoOSTreeViewItem;
            SelectedItem = (Item)selectedTreeViewItem.Data;
            SetItemDetails(SelectedItem);
        }

        private void ItemPickerOnSelectedItemChanged(object sender, EventArgs e)
        {
            var itemPickerUC = sender as ItemPickerWpfUserControl;
            if (itemPickerUC.SelectedItems.Any())
            {
                SelectedItem = itemPickerUC.SelectedItems.First();
                SetItemDetails(SelectedItem);
            }
        }

        private void SetItemDetails(Item item)
        {
            if (item != null)
            {
                item = Configuration.Instance.GetItem(_selectedItem.FQID) ?? item; // Refresh to get latest content
                var treeViewItem = new VideoOSTreeViewItem()
                {
                    Data = item.Name,
                    IsExpanded = true,
                };
                DumpFQID(item, treeViewItem);
                DumpFields(item, treeViewItem);
                DumpProperties(item, treeViewItem);
                DumpAuthorization(item, treeViewItem);
                DumpRelated(item, treeViewItem);
                ItemDetails = new List<VideoOSTreeViewItem>() { treeViewItem  };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
