using System;
using System.Collections.ObjectModel;
using System.Windows;
using VideoOS.Platform.UI.Controls;

namespace MatrixServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VideoOSWindow
    {
        private MatrixService _matrixService;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public Collection<VideoOSTableColumnBase> TableColumns
        {
            get
            {
                return new ObservableCollection<VideoOSTableColumnBase>
                {
                    new VideoOSTableTextColumn()
                    {
                        Header = "",
                        Width = System.Windows.Controls.DataGridLength.Auto,
                        CanUserResize=false,
                        HorizontalAlignment = HorizontalAlignment.Right
                    }
                };
            }
        }

        public ObservableCollection<string> Commands { get; set; } = new ObservableCollection<string>();

        private void VideoOSWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _matrixService = new MatrixService(Guid.NewGuid(), _matrixViewItem1, 12345, "password");
            _matrixService.ShowCommandEvent += ShowCommand;
            _matrixService.Add(Guid.NewGuid(), _matrixViewItem2);
            _matrixService.Add(Guid.NewGuid(), _matrixViewItem3);
        }

        private void VideoOSWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _matrixService.ListenEnd();

            VideoOS.Platform.SDK.Environment.RemoveAllServers();
        }

        // Sender is the String !!
        private void ShowCommand(object sender, string command)
        {
            Dispatcher.Invoke(() => { Commands.Add(command); });
        }
    }
}
