namespace AlarmEventViewer
{
	partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewAlarm = new System.Windows.Forms.DataGridView();
            this.buttonInprogress = new System.Windows.Forms.Button();
            this.buttonCompleted = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLPR = new System.Windows.Forms.RadioButton();
            this.rbAnalytics = new System.Windows.Forms.RadioButton();
            this.rbEvents = new System.Windows.Forms.RadioButton();
            this.rbAlarms = new System.Windows.Forms.RadioButton();
            this.rbAccess = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(464, 442);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.OnClose);
            // 
            // dataGridViewAlarm
            // 
            this.dataGridViewAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAlarm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAlarm.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewAlarm.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlarm.Location = new System.Drawing.Point(13, 89);
            this.dataGridViewAlarm.MultiSelect = false;
            this.dataGridViewAlarm.Name = "dataGridViewAlarm";
            this.dataGridViewAlarm.ReadOnly = true;
            this.dataGridViewAlarm.RowHeadersVisible = false;
            this.dataGridViewAlarm.Size = new System.Drawing.Size(526, 347);
            this.dataGridViewAlarm.TabIndex = 4;
            this.dataGridViewAlarm.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnCellClick);
            // 
            // buttonInprogress
            // 
            this.buttonInprogress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonInprogress.Enabled = false;
            this.buttonInprogress.Location = new System.Drawing.Point(12, 442);
            this.buttonInprogress.Name = "buttonInprogress";
            this.buttonInprogress.Size = new System.Drawing.Size(98, 23);
            this.buttonInprogress.TabIndex = 5;
            this.buttonInprogress.Text = "In Progress";
            this.buttonInprogress.UseVisualStyleBackColor = true;
            this.buttonInprogress.Click += new System.EventHandler(this.buttonInprogress_Click);
            // 
            // buttonCompleted
            // 
            this.buttonCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCompleted.Enabled = false;
            this.buttonCompleted.Location = new System.Drawing.Point(126, 442);
            this.buttonCompleted.Name = "buttonCompleted";
            this.buttonCompleted.Size = new System.Drawing.Size(98, 23);
            this.buttonCompleted.TabIndex = 6;
            this.buttonCompleted.Text = "Completed";
            this.buttonCompleted.UseVisualStyleBackColor = true;
            this.buttonCompleted.Click += new System.EventHandler(this.buttonCompleted_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbAccess);
            this.groupBox1.Controls.Add(this.rbLPR);
            this.groupBox1.Controls.Add(this.rbAnalytics);
            this.groupBox1.Controls.Add(this.rbEvents);
            this.groupBox1.Controls.Add(this.rbAlarms);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 70);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Viewer mode";
            // 
            // rbLPR
            // 
            this.rbLPR.AutoSize = true;
            this.rbLPR.Location = new System.Drawing.Point(125, 44);
            this.rbLPR.Name = "rbLPR";
            this.rbLPR.Size = new System.Drawing.Size(82, 17);
            this.rbLPR.TabIndex = 3;
            this.rbLPR.Text = "LPR Events";
            this.rbLPR.UseVisualStyleBackColor = true;
            this.rbLPR.CheckedChanged += new System.EventHandler(this.onModeChange);
            // 
            // rbAnalytics
            // 
            this.rbAnalytics.AutoSize = true;
            this.rbAnalytics.Location = new System.Drawing.Point(125, 20);
            this.rbAnalytics.Name = "rbAnalytics";
            this.rbAnalytics.Size = new System.Drawing.Size(103, 17);
            this.rbAnalytics.TabIndex = 2;
            this.rbAnalytics.Text = "Analytics Events";
            this.rbAnalytics.UseVisualStyleBackColor = true;
            this.rbAnalytics.CheckedChanged += new System.EventHandler(this.onModeChange);
            // 
            // rbEvents
            // 
            this.rbEvents.AutoSize = true;
            this.rbEvents.Location = new System.Drawing.Point(7, 44);
            this.rbEvents.Name = "rbEvents";
            this.rbEvents.Size = new System.Drawing.Size(58, 17);
            this.rbEvents.TabIndex = 1;
            this.rbEvents.Text = "Events";
            this.rbEvents.UseVisualStyleBackColor = true;
            this.rbEvents.CheckedChanged += new System.EventHandler(this.onModeChange);
            // 
            // rbAlarms
            // 
            this.rbAlarms.AutoSize = true;
            this.rbAlarms.Checked = true;
            this.rbAlarms.Location = new System.Drawing.Point(7, 20);
            this.rbAlarms.Name = "rbAlarms";
            this.rbAlarms.Size = new System.Drawing.Size(56, 17);
            this.rbAlarms.TabIndex = 0;
            this.rbAlarms.TabStop = true;
            this.rbAlarms.Text = "Alarms";
            this.rbAlarms.UseVisualStyleBackColor = true;
            this.rbAlarms.CheckedChanged += new System.EventHandler(this.onModeChange);
            // 
            // rbAccess
            // 
            this.rbAccess.AutoSize = true;
            this.rbAccess.Location = new System.Drawing.Point(257, 20);
            this.rbAccess.Name = "rbAccess";
            this.rbAccess.Size = new System.Drawing.Size(132, 17);
            this.rbAccess.TabIndex = 4;
            this.rbAccess.TabStop = true;
            this.rbAccess.Text = "Access Control Events";
            this.rbAccess.UseVisualStyleBackColor = true;
            this.rbAccess.CheckedChanged += new System.EventHandler(this.onModeChange);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 477);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCompleted);
            this.Controls.Add(this.buttonInprogress);
            this.Controls.Add(this.dataGridViewAlarm);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Alarms and Events Viewer Application";
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.DataGridView dataGridViewAlarm;
		private System.Windows.Forms.Button buttonInprogress;
		private System.Windows.Forms.Button buttonCompleted;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLPR;
        private System.Windows.Forms.RadioButton rbAnalytics;
        private System.Windows.Forms.RadioButton rbEvents;
        private System.Windows.Forms.RadioButton rbAlarms;
        private System.Windows.Forms.RadioButton rbAccess;
    }
}