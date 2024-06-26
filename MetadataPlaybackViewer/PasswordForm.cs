using System.Windows.Forms;

namespace MetadataPlaybackViewer
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        public string Password
        {
            get { return textBoxPassword.Text; }
        }
    }
}
