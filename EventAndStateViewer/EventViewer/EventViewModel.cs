using EventAndStateViewer.Mvvm;
using System;
using VideoOS.Platform.EventsAndState;

namespace EventAndStateViewer.EventViewer
{
    /// <summary>
    /// View model for a single event within the <see cref="EventViewerViewModel"/>.
    /// </summary>
    class EventViewModel : ViewModelBase
    {
        private readonly CachedRestApiClient _restApiClient;
        private readonly Event _event;

        public DateTime Timestamp => _event.Time;
        public string Source => LoadProperty(_restApiClient.LookupResourceNameAsync(_event.Source, "displayName"));
        public string EventType => LoadProperty(_restApiClient.LookupResourceNameAsync($"eventTypes/{_event.Type}", "displayName"));

        public EventViewModel(Event @event)
        {
            _restApiClient = App.DataModel.RestApiClient;
            _event = @event;
        }
    }
}