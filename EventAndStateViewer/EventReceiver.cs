using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoOS.Platform.EventsAndState;

namespace EventAndStateViewer
{
    /// <summary>
    /// Implementation of <see cref="IEventReceiver"/> used for processing events from an <see cref="IEventsAndStateSession"/>
    /// (provided as parameter for <see cref="EventsAndStateSession.Create(VideoOS.Platform.Login.LoginSettings, IEventReceiver)"/>).
    /// <br/><br/>
    /// Events matching subscription rules will be received after calling <see cref="IEventsAndStateSession.AddSubscriptionAsync(IEnumerable{SubscriptionRule}, System.Threading.CancellationToken)"/>.
    /// </summary>
    class EventReceiver : IEventReceiver
    {
        public event EventHandler<ConnectionState> ConnectionStateChanged;
        public event EventHandler<IEnumerable<Event>> EventsReceived;

        public async Task OnConnectionStateChangedAsync(ConnectionState newState)
        {
            // Use dispatcher to invoke event handler on UI thread
            await App.Current.Dispatcher.BeginInvoke(new Action(() => ConnectionStateChanged?.Invoke(this, newState)));
        }

        public async Task OnEventsReceivedAsync(IEnumerable<Event> events)
        {
            // Use dispatcher to invoke event handler on UI thread
            await App.Current.Dispatcher.BeginInvoke(new Action(() => EventsReceived?.Invoke(this, events)));
        }
    }
}
