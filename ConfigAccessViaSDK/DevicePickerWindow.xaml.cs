using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VideoOS.Platform;
using VideoOS.Platform.UI.Controls;

namespace ConfigAccessViaSDK
{
    /// <summary>
    /// Interaction logic for DevicePickerForm.xaml
    /// </summary>
    public partial class DevicePickerWindow : VideoOSWindow
    {
        private List<Item> _items = new List<Item>();

        public List<Item> Items
        {
            get
            {
                return _items;
            }
        }

        private List<Item> _preSelectedItems = new List<Item>();

        public List<Item> PreSelectedItems
        {
            get
            {
                return _preSelectedItems;
            }
            set
            {
                _preSelectedItems = value;
            }
        }

        private List<FQID> _selectedFQID = new List<FQID>();

        public List<FQID> SelectedFQID
        {
            get
            {
                return _selectedFQID;
            }
            set
            {
                _selectedFQID = value;
            }
        }

        private List<VideoOSDropDownItem> _kinds = new List<VideoOSDropDownItem>();

        public DevicePickerWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            SelectedFQID.Clear();
            Item groupItem = ItemBuilder("Groups", FolderType.SystemDefined, new Guid());
            groupItem.SetChildren(Configuration.Instance.GetItems(ItemHierarchy.SystemDefined));
            Item serverItem = ItemBuilder("Servers", FolderType.UserDefined, new Guid());
            serverItem.SetChildren(Configuration.Instance.GetItems(ItemHierarchy.UserDefined));
            List<Item> items = new List<Item>() { groupItem, serverItem };

            _items = items;
            _itemPicker.Items = _items;
            _itemPicker.SelectedItems = _preSelectedItems;

            FillFilterDropDown();
        }

        private void FillFilterDropDown()
        {
            foreach (DictionaryEntry entry in Kind.DefaultTypeToNameTable)
            {
                _kinds.Add(new VideoOSDropDownItem()
                {
                    Tag = entry,
                    Data = entry.Value
                });
            }

            _filter.ItemsSource = _kinds;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var totalItemsSelected = _itemPicker.SelectedItems.Count();
            for (var i = 0; i < totalItemsSelected; i++)
            {
                var item = _itemPicker.SelectedItems.ElementAt(i);
                var fqid = item.FQID;
                _selectedFQID.Add(fqid);
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Filter_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            VideoOSDropDownItem kind = _filter.SelectedItem as VideoOSDropDownItem;
            DictionaryEntry entry = (DictionaryEntry)kind.Tag;
            Filter((Guid)entry.Key);
        }

        private void Filter(Guid kind)
        {
            _itemPicker.KindsFilter.Clear();
            _itemPicker.KindsFilter.Add(kind);
            _itemPicker.Items = _items;
        }

        public Item ItemBuilder(string name, FolderType folderType, Guid kind)
        {
            Guid parentId = Guid.Empty;
            Guid objectId = Guid.NewGuid();
            ServerId serverId = new ServerId(ServerId.CorporateManagementServerType, "top", 1, Guid.NewGuid());
            return new Item(serverId, parentId, objectId, name, folderType, kind);
        }
    }
}
