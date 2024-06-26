using EventAndStateViewer.EventViewer;
using EventAndStateViewer.StateViewer;
using EventAndStateViewer.Subscription;
using System.Collections;

namespace EventAndStateViewer
{
    /// <summary>
    /// View model for MainWindow.xaml
    /// </summary>
    class MainViewModel
    {
        public ConnectionStateViewModel ConnectionStateViewModel { get; } = new ConnectionStateViewModel();
        public SubscriptionViewModel SubscriptionViewModel { get; } = new SubscriptionViewModel();
        public StateViewerViewModel StateViewerViewModel { get; } = new StateViewerViewModel();
        public EventViewerViewModel EventViewerViewModel { get; } = new EventViewerViewModel();

        public ICollection Tabs => new object[] { SubscriptionViewModel, StateViewerViewModel, EventViewerViewModel };

        public MainViewModel()
        {
            // Refresh state and clear events, when a new subscription is made
            SubscriptionViewModel.Subscribed += (s, e) => StateViewerViewModel.Refresh.Execute(null);
            SubscriptionViewModel.Subscribed += (s, e) => EventViewerViewModel.Clear.Execute(null);
        }
    }
}
