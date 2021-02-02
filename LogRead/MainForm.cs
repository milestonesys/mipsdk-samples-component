using System;
using System.Windows.Forms;

namespace LogRead
{
    public partial class MainForm : Form
    {
        private System.Collections.ArrayList _result;
        private System.Collections.ArrayList _names;
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnClose(object sender, EventArgs e)
        {
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            bool isInitialized = VideoOS.Platform.Log.LogClient.Instance.Initialized;
            System.Collections.ArrayList groups = VideoOS.Platform.Log.LogClient.Instance.ReadGroups(VideoOS.Platform.EnvironmentManager.Instance.MasterSite.ServerId);
            foreach (string group in groups)
            {
                listBox1.Items.Add(group.Trim());
            }
            
            listBox1.SetSelected(listBox1.Items.IndexOf("System"), true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGroup = listBox1.SelectedItem.ToString();
            dGridViewLog.Rows.Clear();
            dGridViewLog.Columns.Clear();
            fillGrid(selectedGroup);
        }

        private void fillGrid(string group)
        {
            VideoOS.Platform.Log.LogClient.Instance.ReadLog(VideoOS.Platform.EnvironmentManager.Instance.MasterSite.ServerId, 1, out _result, out _names, group);
            var colNum = new DataGridViewTextBoxColumn { HeaderText = "Number" };
            dGridViewLog.Columns.Add(colNum);

            foreach (string name in _names)
            {
                var col = new DataGridViewTextBoxColumn { HeaderText = name, Width = 150 };
                dGridViewLog.Columns.Add(col);
            }

            foreach (System.Collections.ArrayList arrayList in _result)
            {
                DataGridViewRow row = (DataGridViewRow)dGridViewLog.RowTemplate.Clone();
                row.CreateCells(dGridViewLog, arrayList.ToArray());
                dGridViewLog.Rows.Add(row);
            }
        }
    }
}
