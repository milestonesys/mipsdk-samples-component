using EventAndStateViewer.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VideoOS.Platform.EventsAndState;

namespace EventAndStateViewer.EventViewer
{
    /// <summary>
    /// View model for EventViewerControl.xaml
    /// </summary>
    class EventViewerViewModel : ViewModelBase
    {
        public string TabName => "Event viewer";

        public ICommand Clear { get; }
        
        public ObservableCollection<EventViewModel> Events { get; } = new ObservableCollection<EventViewModel>();

        public EventViewerViewModel()
        {
            App.DataModel.EventReceiver.EventsReceived += OnEventsReceived;
            Clear = new DelegateCommand(OnClearEvents);
        }

        private void OnEventsReceived(object sender, IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                Events.Add(new EventViewModel(@event));
            }

            // Remove oldest events to limit the list to 100
            while (Events.Count > 100)
            {
                Events.RemoveAt(0);
            }
        }

        private void OnClearEvents()
        {
            Events.Clear();
        }
    }
}
