using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	public partial class TaskUserControl : UserControl
	{
        private ConfigApiClient _configApiClient;

        public TaskUserControl(ConfigurationItem item, ConfigurationItem[] childrens, ConfigApiClient configApiClient)
		{
			InitializeComponent();

            _configApiClient = configApiClient;

			if (childrens!=null && childrens.Any())
			{
			    tableLayoutPanel1.ColumnCount = 6;
				tableLayoutPanel1.RowCount = childrens.Length +1;

                tableLayoutPanel1.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
			    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                for (int ix = 0; ix < childrens.Length; ix++)
                {
					ConfigurationItem child = childrens[ix];

                    int iy = 0;

					if (ix < tableLayoutPanel1.RowStyles.Count)
					{
						tableLayoutPanel1.RowStyles[ix] = new RowStyle(SizeType.AutoSize);							
					} else
					{
						tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
					}

                    tableLayoutPanel1.Controls.Add(MakeControl(child.Path), iy + 0, ix);

                    foreach (Property pi in child.Properties) // We assume here that the children have same settings!
                    {
                        if (pi.Key == "State")
                            tableLayoutPanel1.Controls.Add(MakeControl(pi), iy + 1, ix);
                        if (pi.Key == "Progress")
                            tableLayoutPanel1.Controls.Add(MakeControl(pi), iy + 2, ix);
                    }

                    if (child.MethodIds.Contains("TaskStop"))
                        tableLayoutPanel1.Controls.Add(MakeButton("Stop", child), iy + 3, ix);
                    if (child.MethodIds.Contains("TaskCleanup"))
                        tableLayoutPanel1.Controls.Add(MakeButton("Cleanup", child), iy + 4, ix);

                    tableLayoutPanel1.Controls.Add(MakeButton("Details", child), iy + 5, ix);

                    tableLayoutPanel1.Height = 25 * (childrens.Length + 1) + 1;
				}
			}
		}

        private Control MakeButton(string name, ConfigurationItem task)
        {
            Button tb = new Button();
            tb.Dock = DockStyle.Fill;
            tb.Text = name;
            tb.Click += Tb_Click;
            tb.Tag = task;
            return tb;
        }

        private void Tb_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ConfigurationItem task = button.Tag as ConfigurationItem;
            if (button.Text == "Stop")
            {
                ConfigurationItem result = _configApiClient.InvokeMethod(task, "TaskStop");
            }
            if (button.Text == "Cleanup")
            {
                ConfigurationItem result = _configApiClient.InvokeMethod(task, "TaskCleanup");
            }
            if (button.Text == "Details")
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var p in task.Properties)
                {
                    stringBuilder.Append(p.Key + " = " + p.Value);
                    stringBuilder.Append(Environment.NewLine);
                }
                textBox1.Text = stringBuilder.ToString();
            }
        }

        private Control MakeControl(string name)
        {
            TextBox tb = new TextBox();
            tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb.Dock = DockStyle.Fill;
            tb.Text = name;
            tb.ReadOnly = true;
            return tb;
        }

        private Control MakeControl(Property pi)
		{
			TextBox tb = new TextBox();
			//tb.BackColor = MainForm.MyBackColor;
			//tb.ForeColor = MainForm.MyForeColor;
			tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			tb.Dock = DockStyle.Fill;
			tb.Text = pi.Value.ToString();
			tb.ReadOnly = !pi.IsSettable;
			return tb;
		}
		private Control MakeControlName(Property pi)
		{
			TextBox tb = new TextBox();
			//tb.BackColor = MainForm.MyBackColor;
			//tb.ForeColor = MainForm.MyForeColor;
			tb.BorderStyle = System.Windows.Forms.BorderStyle.None;
			tb.Dock = DockStyle.Fill;
			tb.Text = pi.DisplayName;
			tb.ReadOnly = true;
			return tb;
		}
	}
}
