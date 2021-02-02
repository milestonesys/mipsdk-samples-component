using System;
using System.Windows.Forms;

namespace MatrixServer
{
	public partial class MainForm : Form
	{
		private MatrixService _matrixService;


		public MainForm()
		{
			InitializeComponent();
		}

		private void OnClose(object sender, EventArgs e)
		{
			_matrixService.ListenEnd();

			VideoOS.Platform.SDK.Environment.RemoveAllServers();
			Close();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			_matrixService = new MatrixService(Guid.NewGuid(), matrixViewItem1, 12345, "password");
			_matrixService.ShowCommandEvent += ShowCommand;
			_matrixService.Add(Guid.NewGuid(), matrixViewItem2);
			_matrixService.Add(Guid.NewGuid(), matrixViewItem3);
		}

		// Sender is the String !!
		private void ShowCommand(object sender, EventArgs e)
		{
			Invoke(new MethodInvoker(delegate() { listBox1.Items.Add((string) sender); }));
		}
	}
}
