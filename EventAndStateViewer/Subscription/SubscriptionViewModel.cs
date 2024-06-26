using EventAndStateViewer.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoOS.Platform.EventsAndState;
using VideoOS.Platform.UI.Controls;

namespace EventAndStateViewer.Subscription
{
    /// <summary>
    /// View model for SubscriptionControl.xaml
    /// </summary>
    class SubscriptionViewModel : ViewModelBase
    {
        private readonly IEventsAndStateSession _session;
        private Guid _subscriptionId;
        private bool _isDirty;

        public string TabName => "Subscription";

        public event EventHandler Subscribed;

        public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
        }

        public ICommand Subscribe { get; }
        public ICommand AddRule { get; }

        public ObservableCollection<SubscriptionRuleViewModel> Rules { get; } = new ObservableCollection<SubscriptionRuleViewModel>();

        public SubscriptionViewModel()
        {
            _session = App.DataModel.Session;

            Subscribe = new DelegateCommand(OnSubscribeAsync);
            AddRule = new DelegateCommand(OnAddRule);

            // Add a rule
            AddRule.Execute(null);

            // Discourage subscribing to everything by forcing user to make some change
            IsDirty = false;
        }

        private async Task OnSubscribeAsync()
        {
            // Unsubscribe, if needed
            if (_subscriptionId != Guid.Empty)
            {
                await _session.RemoveSubscriptionAsync(_subscriptionId, default);
            }

            // Subscribe
            var rules = Rules.Select(r => r.ToRule());
            try
            {
                _subscriptionId = await _session.AddSubscriptionAsync(rules, default);
                IsDirty = false;

                Subscribed?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                // using null as parent window is not a good practice, but it is used here for simplicity - will cause the message box to be centered on the screen
                VideoOSMessageBox.Show(null, "Failed to subscribe", "Failed to subscribe", e.Message, VideoOSMessageBox.Buttons.OK, VideoOSMessageBox.ResultButtons.OK, new VideoOSIconBuiltInSource() { Icon = VideoOSIconBuiltInSource.Icons.Error_Combined });
            }
        }

        private void OnAddRule()
        {
            var rule = new SubscriptionRuleViewModel();
            rule.Removed += OnRuleRemoved;
            rule.PropertyChanged += OnRuleChanged;
            Rules.Add(rule);
            IsDirty = true;
        }

        private void OnRuleRemoved(object sender, EventArgs e)
        {
            if (sender is SubscriptionRuleViewModel rule)
            {
                rule.Removed -= OnRuleRemoved;
                rule.PropertyChanged -= OnRuleChanged;
                Rules.Remove(rule);
            }
            if (Rules.Count == 0)
            {
                // Add a new rule to prevent 0 rules
                AddRule.Execute(null);
            }
            IsDirty = true;
        }

        private void OnRuleChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
        }
    }
}
