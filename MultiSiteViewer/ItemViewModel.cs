using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using VideoOS.Platform;

namespace MultiSiteViewer
{
    public class TreeViewModel : ViewModelBase
    {
        private ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> ItemViewModels
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<ItemViewModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public bool AddTop(Item item)
        {
            ItemViewModel ivm = _items.FirstOrDefault(x => x.InternalItem == item);
            if (ivm != null)
            {
                return false;
            }
            _items.Add(new ItemViewModel() { InternalItem = item });
            OnPropertyChanged(nameof(ItemViewModels));
            return true;
        }

        internal bool AddChild(Item parent, Item child)
        {
            ItemViewModel ivmparent = FindItemInTree(parent);
            ivmparent.ChildItems.Add(new ItemViewModel() { InternalItem = child });
            OnPropertyChanged(nameof(ItemViewModels));
            return true;
        }

        internal ItemViewModel FindItemInTree(Item item)
        {
            foreach (var ivmTop in _items)
            {
                var result = FindInternalItem(ivmTop, item);
                if(result != null) { return result; }
            }
            return null;
        }

        internal ItemViewModel FindInternalItem(ItemViewModel itemViewModel, Item item)
        {
            if(itemViewModel.InternalItem == item)
            {
                return itemViewModel;
            }
            foreach(var ivm in itemViewModel.ChildItems)
            {
                var result = FindInternalItem(ivm, item);   
                if (result != null) return result;
            }
            return null;
        }

        internal void RemoveOne(ItemViewModel item)
        {
            ItemViewModel itemViewModel = _items.FirstOrDefault(x => x == item);
            if (itemViewModel != null)
            {
                _items.Remove(itemViewModel);
                OnPropertyChanged(nameof(ItemViewModels));
            }
            else
            {
                foreach (var top in _items)
                {
                    SearchAndRemoveChild(top, item);
                }                
            }
        }

        private void SearchAndRemoveChild(ItemViewModel parent, ItemViewModel searchedItem)
        {
            ItemViewModel foundItem = parent.ChildItems.FirstOrDefault(x => x == searchedItem);
            if (foundItem != null)
            {
                parent.ChildItems.Remove(foundItem);
            }
            else
            {
                foreach(var child  in parent.ChildItems)
                {
                    SearchAndRemoveChild(child, searchedItem);
                }
            }
        }

        private void AddChildren(ItemViewModel parent, IEnumerable<Item> childitems)
        {
            foreach (var item in childitems)
            {
                var vm = new ItemViewModel() { InternalItem = item };
                AddChildren(vm, item.GetChildren());
                parent.ChildItems.Add(vm);
            }            
        }
    }

    public class ItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Required for items in a VideoOSTreeView.
        /// </summary>
        public bool IsEnabled { get { return true; } }

        private Item _internalItem;
        public Item InternalItem
        {
            get
            {
                return _internalItem;
            }
            set
            {
                _internalItem = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        private ObservableCollection<ItemViewModel> _childItems;
        public ObservableCollection<ItemViewModel> ChildItems
        {
            get
            {
                if (_childItems == null)
                {
                    _childItems = new ObservableCollection<ItemViewModel>();
                }
                return _childItems;
            }
            set
            {
                _childItems = new ObservableCollection<ItemViewModel>(value);
                OnPropertyChanged(nameof(ChildItems));
            }
        }

        public string Name
        {
            get
            {
                if (InternalItem == null)
                {
                    return "";
                }
                return InternalItem.Name;
            }
        }
        private bool _isSelected = false;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (InternalItem.FQID.FolderType == FolderType.No)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    abstract public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
