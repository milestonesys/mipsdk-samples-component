namespace MultiSiteStatusViewer
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.treeViewItems = new System.Windows.Forms.TreeView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listBoxChanges = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCurrent = new System.Windows.Forms.Button();
            this.treeViewSites = new System.Windows.Forms.TreeView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemoveServer = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(338, 218);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // treeViewItems
            // 
            this.treeViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewItems.Location = new System.Drawing.Point(7, 19);
            this.treeViewItems.Name = "treeViewItems";
            this.treeViewItems.Size = new System.Drawing.Size(406, 333);
            this.treeViewItems.TabIndex = 1;
            // 
            // listBoxChanges
            // 
            this.listBoxChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxChanges.FormattingEnabled = true;
            this.listBoxChanges.Location = new System.Drawing.Point(450, 30);
            this.listBoxChanges.Name = "listBoxChanges";
            this.listBoxChanges.Size = new System.Drawing.Size(655, 589);
            this.listBoxChanges.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 633);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1126, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(561, 17);
            this.toolStripStatusLabel1.Text = "This is a developer\'s code sample only. It is neither a test program nor an attem" +
    "pt to make an application.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCurrent);
            this.groupBox1.Controls.Add(this.treeViewSites);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.buttonRemoveServer);
            this.groupBox1.Controls.Add(this.buttonClose);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 247);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servers monitored";
            // 
            // buttonCurrent
            // 
            this.buttonCurrent.Enabled = false;
            this.buttonCurrent.Location = new System.Drawing.Point(127, 218);
            this.buttonCurrent.Name = "buttonCurrent";
            this.buttonCurrent.Size = new System.Drawing.Size(124, 23);
            this.buttonCurrent.TabIndex = 5;
            this.buttonCurrent.Text = "Get current state";
            this.buttonCurrent.UseVisualStyleBackColor = true;
            this.buttonCurrent.Click += new System.EventHandler(this.buttonCurrent_Click);
            // 
            // treeViewSites
            // 
            this.treeViewSites.Location = new System.Drawing.Point(7, 20);
            this.treeViewSites.Name = "treeViewSites";
            this.treeViewSites.Size = new System.Drawing.Size(406, 192);
            this.treeViewSites.TabIndex = 4;
            this.treeViewSites.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AfterSelect);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(6, 218);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemoveServer
            // 
            this.buttonRemoveServer.Enabled = false;
            this.buttonRemoveServer.Location = new System.Drawing.Point(257, 218);
            this.buttonRemoveServer.Name = "buttonRemoveServer";
            this.buttonRemoveServer.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveServer.TabIndex = 1;
            this.buttonRemoveServer.Text = "Remove Server";
            this.buttonRemoveServer.UseVisualStyleBackColor = true;
            this.buttonRemoveServer.Click += new System.EventHandler(this.RemoveClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.treeViewItems);
            this.groupBox2.Location = new System.Drawing.Point(13, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 363);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuration View";
            // 
            // timer1
            // 
            this.timer1.Interval = 100000;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 655);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listBoxChanges);
            this.Name = "MainForm";
            this.Text = "StatusViewer Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.TreeView treeViewItems;
		private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox listBoxChanges;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeViewSites;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemoveServer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCurrent;
        private System.Windows.Forms.Timer timer1;
	}
}