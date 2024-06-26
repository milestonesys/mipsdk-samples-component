using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using VideoOS.Platform.UI.Controls;

namespace EventAndStateViewer.StateViewer
{
    /// <summary>
    /// Interaction logic for StateViewerControl.xaml
    /// </summary>
    public partial class StateViewerControl : UserControl
    {
        public StateViewerControl()
        {
            InitializeComponent();
        }

        // The VideoOSTable does not re-sort items when an item property is updated (this behavoir is the same as DataGrid).
        // Therefore the below event handlers subscribe to the PropertyChanged event on each StateViewModel and re-sorts the table,
        // if a property in the SortColumn has changed.
        // Note: This may not scale to a huge number of items.

        private INotifyCollectionChanged _subscribedCollection;
        private readonly List<INotifyPropertyChanged> _subscribedItems = new List<INotifyPropertyChanged>();

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is StateViewerViewModel viewModel)
            {
                _subscribedCollection = viewModel.States;
                _subscribedCollection.CollectionChanged += States_CollectionChanged;
                States_CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, viewModel.States));
            }
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_subscribedCollection != null)
            {
                _subscribedCollection.CollectionChanged -= States_CollectionChanged;
                _subscribedCollection = null;
                States_CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void States_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var vm in _subscribedItems)
                {
                    vm.PropertyChanged -= Item_PropertyChanged;
                }
                _subscribedItems.Clear();
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<StateViewModel>())
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                    _subscribedItems.Remove(item);
                }
            }

            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<StateViewModel>())
                {
                    item.PropertyChanged += Item_PropertyChanged;
                    _subscribedItems.Add(item);
                }
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StateTable.SortColumn.ItemSortPropertyName)
            {
                // Re-sort the SortColumn by resetting the sort direction
                var originalSortDirection = StateTable.SortDirection;
                var inverseSortDirection = StateTable.SortDirection == VideoOSTable.SortDirections.Ascending ? VideoOSTable.SortDirections.Descending : VideoOSTable.SortDirections.Ascending;
                StateTable.SortDirection = inverseSortDirection;
                StateTable.SortDirection = originalSortDirection;
            }
        }
    }
}
