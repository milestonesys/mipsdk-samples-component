using EventAndStateViewer.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoOS.Platform.EventsAndState;

namespace EventAndStateViewer.StateViewer
{
    /// <summary>
    /// View model for StateViewerControl.xaml 
    /// </summary>
    class StateViewerViewModel : ViewModelBase
    {
        private readonly IEventsAndStateSession _session;

        public string TabName => "State viewer";

        public ICommand Refresh { get; }

        public ObservableCollection<StateViewModel> States { get; } = new ObservableCollection<StateViewModel>();

        public StateViewerViewModel()
        {
            _session = App.DataModel.Session;
            App.DataModel.EventReceiver.EventsReceived += OnEventsReceived;

            Refresh = new DelegateCommand(OnRefreshAsync);
        }

        private void OnEventsReceived(object sender, IEnumerable<Event> events)
        {
            // Update state based on stateful events
            foreach (var @event in events.Where(e => e.StateGroupId.HasValue))
            {
                UpdateState(@event.Source, @event.StateGroupId.Value, @event.Type);
            }
        }

        private async Task OnRefreshAsync()
        {
            // Clear states and have the session resend states to the event receiver
            States.Clear();
            var states = await _session.GetCurrentStateAsync(default);
            foreach (var state in states)
            {
                UpdateState(state.Source, state.StateGroupId.Value, state.Type);
            }
        }

        public void UpdateState(string sourcePath, Guid stateGroupId, Guid eventType)
        {
            // Add or update state with matching source or state group
            var state = States.FirstOrDefault(s => s.SourcePath == sourcePath && s.StateGroupId == stateGroupId);
            if (state == null)
            {
                state = new StateViewModel(sourcePath, stateGroupId, eventType);
                States.Add(state);
            }
            else
            {
                state.Update(eventType);
            }
        }
    }
}
