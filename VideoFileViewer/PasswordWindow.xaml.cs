using System.Windows;
using VideoOS.Platform.UI.Controls;

namespace VideoFileViewer
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : VideoOSWindow
    {
        public string Password => _passwordTextBox.Password;

        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}