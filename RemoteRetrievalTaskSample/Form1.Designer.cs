namespace RemoteRetrievalTaskSample
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRetrieveError = new System.Windows.Forms.Label();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.lblDevice = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.dataGridTasks = new System.Windows.Forms.DataGridView();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSelectCamera = new System.Windows.Forms.Button();
            this.lblLoginError = new System.Windows.Forms.Label();
            this.Device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cancel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Cleanup = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(13, 18);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(275, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect to Retrieval Service";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.70588F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.29412F));
            this.tableLayoutPanel2.Controls.Add(this.lblRetrieveError, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnRetrieve, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblDevice, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblStartTime, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblEndTime, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.dtpStartTime, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.dtpEndTime, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtDevice, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(390, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(387, 138);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // lblRetrieveError
            // 
            this.lblRetrieveError.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblRetrieveError, 2);
            this.lblRetrieveError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblRetrieveError.Location = new System.Drawing.Point(3, 107);
            this.lblRetrieveError.Name = "lblRetrieveError";
            this.lblRetrieveError.Size = new System.Drawing.Size(0, 13);
            this.lblRetrieveError.TabIndex = 3;
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRetrieve.Enabled = false;
            this.btnRetrieve.Location = new System.Drawing.Point(309, 81);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(75, 23);
            this.btnRetrieve.TabIndex = 6;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // lblDevice
            // 
            this.lblDevice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(3, 6);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(44, 13);
            this.lblDevice.TabIndex = 0;
            this.lblDevice.Text = "Device:";
            this.lblDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartTime
            // 
            this.lblStartTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(3, 32);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(54, 13);
            this.lblStartTime.TabIndex = 2;
            this.lblStartTime.Text = "Start time:";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEndTime
            // 
            this.lblEndTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(3, 58);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(51, 13);
            this.lblEndTime.TabIndex = 4;
            this.lblEndTime.Text = "End time:";
            this.lblEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartTime.CustomFormat = "dd-MM-yyyy hh:mm.ss";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(67, 29);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(317, 20);
            this.dtpStartTime.TabIndex = 7;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndTime.CustomFormat = "MM dd yyyy hh mm ss";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(67, 55);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(317, 20);
            this.dtpEndTime.TabIndex = 8;
            // 
            // txtDevice
            // 
            this.txtDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDevice.Location = new System.Drawing.Point(67, 3);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.ReadOnly = true;
            this.txtDevice.Size = new System.Drawing.Size(317, 20);
            this.txtDevice.TabIndex = 1;
            // 
            // dataGridTasks
            // 
            this.dataGridTasks.AllowUserToAddRows = false;
            this.dataGridTasks.AllowUserToDeleteRows = false;
            this.dataGridTasks.AllowUserToResizeRows = false;
            this.dataGridTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Device,
            this.UserName,
            this.CreationTime,
            this.StartTime,
            this.EndTime,
            this.State,
            this.Percent,
            this.Cancel,
            this.Cleanup});
            this.dataGridTasks.Location = new System.Drawing.Point(13, 185);
            this.dataGridTasks.MultiSelect = false;
            this.dataGridTasks.Name = "dataGridTasks";
            this.dataGridTasks.ReadOnly = true;
            this.dataGridTasks.RowHeadersVisible = false;
            this.dataGridTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridTasks.Size = new System.Drawing.Size(900, 389);
            this.dataGridTasks.TabIndex = 8;
            this.dataGridTasks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTasks_CellClick);
            // 
            // btnGet
            // 
            this.btnGet.Enabled = false;
            this.btnGet.Location = new System.Drawing.Point(12, 156);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 9;
            this.btnGet.Text = "Get tasks";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(93, 156);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSelectCamera
            // 
            this.btnSelectCamera.Enabled = false;
            this.btnSelectCamera.Location = new System.Drawing.Point(12, 93);
            this.btnSelectCamera.Name = "btnSelectCamera";
            this.btnSelectCamera.Size = new System.Drawing.Size(275, 23);
            this.btnSelectCamera.TabIndex = 11;
            this.btnSelectCamera.Text = "Select Camera ...";
            this.btnSelectCamera.UseVisualStyleBackColor = true;
            this.btnSelectCamera.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblLoginError
            // 
            this.lblLoginError.AutoSize = true;
            this.lblLoginError.Location = new System.Drawing.Point(12, 48);
            this.lblLoginError.Name = "lblLoginError";
            this.lblLoginError.Size = new System.Drawing.Size(0, 13);
            this.lblLoginError.TabIndex = 12;
            // 
            // Device
            // 
            this.Device.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Device.DataPropertyName = "DeviceName";
            this.Device.HeaderText = "Device Name";
            this.Device.Name = "Device";
            this.Device.ReadOnly = true;
            this.Device.Width = 97;
            // 
            // UserName
            // 
            this.UserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UserName.DataPropertyName = "User";
            this.UserName.HeaderText = "User Name";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            this.UserName.Width = 85;
            // 
            // CreationTime
            // 
            this.CreationTime.DataPropertyName = "CreationTime";
            this.CreationTime.HeaderText = "Creation Time (UTC)";
            this.CreationTime.Name = "CreationTime";
            this.CreationTime.ReadOnly = true;
            // 
            // StartTime
            // 
            this.StartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StartTime.DataPropertyName = "StartTime";
            this.StartTime.HeaderText = "Start Time (UTC)";
            this.StartTime.Name = "StartTime";
            this.StartTime.ReadOnly = true;
            this.StartTime.Width = 102;
            // 
            // EndTime
            // 
            this.EndTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EndTime.DataPropertyName = "EndTime";
            this.EndTime.HeaderText = "End Time (UTC)";
            this.EndTime.Name = "EndTime";
            this.EndTime.ReadOnly = true;
            this.EndTime.Width = 99;
            // 
            // State
            // 
            this.State.DataPropertyName = "State";
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // Percent
            // 
            this.Percent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Percent.DataPropertyName = "PercentCompleted";
            this.Percent.HeaderText = "Percent Completed";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            this.Percent.Width = 112;
            // 
            // Cancel
            // 
            this.Cancel.HeaderText = "Cancel";
            this.Cancel.Name = "Cancel";
            this.Cancel.ReadOnly = true;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseColumnTextForButtonValue = true;
            // 
            // Cleanup
            // 
            this.Cleanup.HeaderText = "Cleanup";
            this.Cleanup.Name = "Cleanup";
            this.Cleanup.ReadOnly = true;
            this.Cleanup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Cleanup.Text = "Cleanup";
            this.Cleanup.UseColumnTextForButtonValue = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 586);
            this.Controls.Add(this.lblLoginError);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.dataGridTasks);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.btnSelectCamera);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label lblRetrieveError;
		private System.Windows.Forms.Button btnRetrieve;
		private System.Windows.Forms.TextBox txtDevice;
		private System.Windows.Forms.Label lblDevice;
		private System.Windows.Forms.Label lblStartTime;
		private System.Windows.Forms.Label lblEndTime;
		private System.Windows.Forms.DateTimePicker dtpStartTime;
		private System.Windows.Forms.DateTimePicker dtpEndTime;
		private System.Windows.Forms.DataGridView dataGridTasks;
		private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnSelectCamera;
        private System.Windows.Forms.Label lblLoginError;
        private System.Windows.Forms.DataGridViewTextBoxColumn Device;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreationTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewButtonColumn Cancel;
        private System.Windows.Forms.DataGridViewButtonColumn Cleanup;
    }
}

