using EventAndStateViewer.Mvvm;
using System;
using VideoOS.Platform.EventsAndState;
using VideoOS.Platform.EventsData;

namespace EventAndStateViewer.EventViewer
{
    /// <summary>
    /// View model for a single event within the <see cref="EventViewerViewModel"/>.
    /// </summary>
    class EventViewModel : ViewModelBase
    {
        private readonly CachedRestApiClient _restApiClient;
        private readonly Event _event;
        private Guid SysConId = new Guid("06c5010d-11f2-4d70-bd79-4cec3a20d589");

        public DateTime Timestamp => _event.Time;
        public string Source => LoadProperty(_restApiClient.LookupResourceNameAsync(_event.Source, "displayName"));

        public string EventType => LoadProperty(_restApiClient.LookupResourceNameAsync($"eventTypes/{_event.Type}", "displayName"));

        public string EventData
        {
            get
            {
                if (_event.Type == SysConId)
                {
                    var dataPart = _event.GetData<SystemConfigurationChangedEventData>();
                    var related = dataPart.RelatedItem;
                    var action = dataPart.Action;

                    return string.Format("Action={2}, Path={0}, Name={1}", related.RelatedItemRestResource, related.Name, action);
                }
                return string.Empty;
            }
        } 

        public EventViewModel(Event @event)
        {
            _restApiClient = App.DataModel.RestApiClient;
            _event = @event;
        }
    }
}