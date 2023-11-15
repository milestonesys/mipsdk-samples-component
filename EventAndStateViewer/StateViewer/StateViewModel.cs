using EventAndStateViewer.Mvvm;
using System;

namespace EventAndStateViewer.StateViewer
{
    /// <summary>
    /// View model for a single state within the <see cref="StateViewerViewModel"/>.
    /// </summary>
    class StateViewModel : ViewModelBase
    {
        private readonly CachedRestApiClient _restApiClient;
        private Guid _eventType;

        public string SourcePath { get; }
        public Guid StateGroupId { get; }

        public string Source => LoadProperty(_restApiClient.LookupResourceNameAsync(SourcePath, "displayName"));
        public string StateGroup => LoadProperty(_restApiClient.LookupResourceNameAsync($"stateGroups/{StateGroupId}", "displayName"));
        public string State => LoadProperty(_restApiClient.LookupResourceNameAsync($"eventTypes/{_eventType}", "state"));

        public StateViewModel(string sourcePath, Guid stateGroupId, Guid eventType)
        {
            _restApiClient = App.DataModel.RestApiClient;

            SourcePath = sourcePath;
            StateGroupId = stateGroupId;
            _eventType = eventType;
        }

        public void Update(Guid eventType)
        {
            _eventType = eventType;
            InvokePropertyChanged(nameof(State));
        }

    }
}