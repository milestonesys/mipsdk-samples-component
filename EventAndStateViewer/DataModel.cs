using System;
using VideoOS.Platform.Login;
using VideoOS.Platform.EventsAndState;

namespace EventAndStateViewer
{
    /// <summary>
    /// A data model handling the lifetime of <see cref="IEventsAndStateSession"/>, <see cref="EventAndStateViewer.EventReceiver"/>, and <see cref="CachedRestApiClient"/>.
    /// </summary>
    class DataModel : IDisposable
    {
        public EventReceiver EventReceiver { get; } = new EventReceiver();
        public IEventsAndStateSession Session { get; }
        public CachedRestApiClient RestApiClient { get; }

        public DataModel(LoginSettings loginSettings)
        {
            // Create events and state session
            Session = EventsAndStateSession.Create(loginSettings, EventReceiver);
            RestApiClient = new CachedRestApiClient(loginSettings);
        }

        public void Dispose()
        {            
            // Dispose session to close the connection
            Session?.Dispose();
            RestApiClient?.Dispose();
        }
    }
}
