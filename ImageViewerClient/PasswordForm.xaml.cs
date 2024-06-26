using System.Windows;

namespace ImageViewerClient
{
    /// <summary>
    /// Interaction logic for PasswordForm.xaml
    /// </summary>
    public partial class PasswordForm : Window
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        public string Password
        {
            get { return textBoxPassword.Password; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
