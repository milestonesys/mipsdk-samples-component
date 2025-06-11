using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;
using VideoOS.Platform.UI;
using System.Linq;
using Camera = VideoOS.Platform.ConfigurationItems.Camera;

namespace RemoteRetrievalTaskSample
{
    public partial class Form1 : Form
    {
        List<TaskDetailsWrapper> _tasks;
        Camera _selectedCamera;        

        public Form1()
        {
            InitializeComponent();
            dtpStartTime.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern;
            dtpEndTime.CustomFormat = CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern;
        }

        private void StartRetrieval(object sender, EventArgs e)
        {
            DateTime utcStartTime = dtpStartTime.Value.ToUniversalTime();
            DateTime utcEndTime = dtpEndTime.Value.ToUniversalTime();

            var task = _selectedCamera.RetrieveEdgeStorage(utcStartTime, utcEndTime);
            if (_tasks == null)
            {
                _tasks = new List<TaskDetailsWrapper>();
            }
            _tasks.Add(new TaskDetailsWrapper(task, _selectedCamera.ServerId));
            dataGridTasks.DataSource = null;
            dataGridTasks.AutoGenerateColumns = false;
            dataGridTasks.DataSource = _tasks;
            dataGridTasks.Rows[dataGridTasks.Rows.Count - 1].Selected = true;
        }

        private void GetTasks_Click(object sender, EventArgs e)
        {
            GetTasks();
        }

        private void GetTasks()
        {   
            var serverTasks = new ManagementServer(_selectedCamera.ServerId).LoadTasks(true, "StorageRetrieval");
            _tasks = serverTasks.ClassCollection.Select(t => new TaskDetailsWrapper(t, _selectedCamera.ServerId)).ToList();
            dataGridTasks.AutoGenerateColumns = false;
            dataGridTasks.DataSource = _tasks;
        }

        private void Refresh(object sender, EventArgs e)
        {
            foreach (var task in _tasks)
            {
                task.UpdateState();
            }
            dataGridTasks.Refresh();
        }

        private void GridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == dataGridTasks.Columns["Stop"].Index)
            {
                _tasks[e.RowIndex].Stop();
            }
            else if (e.ColumnIndex == dataGridTasks.Columns["Cleanup"].Index)
            {
                _tasks[e.RowIndex].Cleanup();
                GetTasks();
            }
        }

        private void SelectCamera(object sender, EventArgs e)
        {
            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.Camera },
                SelectionMode = SelectionModeOptions.AutoCloseOnSelect,
                Items = Configuration.Instance.GetItems()
            };

            if (itemPicker.ShowDialog().Value)
            {
                var item = itemPicker.SelectedItems.First();

                txtDevice.Text = item.Name;
                _selectedCamera = new Camera(item.FQID);
                
                btnGet.Enabled = true;
                btnRefresh.Enabled = true;
                btnRetrieve.Enabled = true;
            }
        }
    }
}
