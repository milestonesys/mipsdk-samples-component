using EventAndStateViewer.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VideoOS.Platform;
using VideoOS.Platform.EventsAndState;
using VideoOS.Platform.UI;

namespace EventAndStateViewer.Subscription
{
    /// <summary>
    /// View model for a single <see cref="SubscriptionRule"/> within the <see cref="SubscriptionViewModel"/>.
    /// <br/><br/>
    /// This class contains commands for selecting <see cref="SubscriptionRule.Modifier"/>, <see cref="SubscriptionRule.ResourceTypes"/>,
    /// <see cref="SubscriptionRule.SourceIds"/>, and <see cref="SubscriptionRule.EventTypes"/> using <see cref="ItemPickerWpfWindow"/>.
    /// </summary>
    class SubscriptionRuleViewModel : ViewModelBase
    {
        private readonly SubscriptionItemProvider _itemProvider = new SubscriptionItemProvider();

        private Modifier _modifier = Modifier.Include;
        private IEnumerable<Item> _resourceTypes = Array.Empty<Item>();
        private IEnumerable<Item> _sources = Array.Empty<Item>();
        private IEnumerable<Item> _eventTypes = Array.Empty<Item>();

        public event EventHandler Removed;

        public ICommand Remove { get; }
        public ICommand EditResourceTypes { get; }
        public ICommand EditSources { get; }
        public ICommand EditEventTypes { get; }

        public IEnumerable<Modifier> Modifiers { get; } = new[] { Modifier.Include, Modifier.Exclude };

        public Modifier Modifier
        {
            get => _modifier;
            set => SetProperty(ref _modifier, value);
        }

        public string ResourceTypesText => _resourceTypes.Any() ? string.Join(", ", _resourceTypes.Select(x => x.Name)) : "Any";
        public string SourcesText => _sources.Any() ? string.Join(", ", _sources.Select(x => x.Name)) : "Any";
        public string EventTypesText => _eventTypes.Any() ? string.Join(", ", _eventTypes.Select(x => x.Name)) : "Any";

        public SubscriptionRuleViewModel()
        {
            Remove = new DelegateCommand(() => Removed?.Invoke(this, EventArgs.Empty));
            EditResourceTypes = new DelegateCommand(() => EditItems(_itemProvider.GetResourceTypes(), ref _resourceTypes, nameof(ResourceTypesText)));
            EditSources = new DelegateCommand(async () => EditItems(await _itemProvider.GetSourcesAsync(), ref _sources, nameof(SourcesText)));
            EditEventTypes = new DelegateCommand(async () => EditItems(await _itemProvider.GetEventTypesAsync(), ref _eventTypes, nameof(EventTypesText)));
        }

        /// <summary>
        /// Convert this view model to a <see cref="SubscriptionRule"/>.
        /// </summary>
        public SubscriptionRule ToRule()
        {
            return new SubscriptionRule(
                Modifier,
                _resourceTypes.Any() ? new ResourceTypes(_resourceTypes.Select(x => x.FQID.ObjectIdString)) : ResourceTypes.Any,
                _sources.Any() ? new SourceIds(_sources.Select(x => x.FQID.ObjectId)) : SourceIds.Any,
                _eventTypes.Any() ? new EventTypes(_eventTypes.Select(x => x.FQID.ObjectId)) : EventTypes.Any);
        }


        /// <summary>
        /// Show an Item Picker, allowing user to edit selected items.
        /// </summary>
        private void EditItems(IEnumerable<Item> availableItems, ref IEnumerable<Item> selectedItems, string propertyName)
        {
            var itemPicker = new ItemPickerWpfWindow
            {
                SearchEnabled = true,
                SelectionMode = SelectionModeOptions.MultiSelect,
                Items = availableItems,
                SelectedItems = selectedItems,
            };

            // Show item picker
            if (!itemPicker.ShowDialog().GetValueOrDefault())
                return;

            // Update selected items and notify the UI
            selectedItems = itemPicker.SelectedItems;
            InvokePropertyChanged(propertyName);
        }
    }
}
