using System.Windows;

namespace EventAndStateViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Only create view model if login was successful
            if (App.DataModel != null)
                DataContext = new MainViewModel();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                // Select subscription tab when loaded
                TabControl.SelectedItem = mainViewModel.SubscriptionViewModel;

                // Select state viewer tab when successfully subscribed
                mainViewModel.SubscriptionViewModel.Subscribed += (s, _) => TabControl.SelectedItem = mainViewModel.StateViewerViewModel;
            }
        }
    }
}
