namespace MediaViewerBitmapSource
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonCamera = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSelectServer = new System.Windows.Forms.Button();
            this.panelPlayback = new System.Windows.Forms.Panel();
            this.buttonLoop = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioLive = new System.Windows.Forms.RadioButton();
            this.radioPlayback = new System.Windows.Forms.RadioButton();
            this.buttonLoopStop = new System.Windows.Forms.Button();
            this.panelLoop = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelLoop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Location = new System.Drawing.Point(140, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 310);
            this.panel1.TabIndex = 21;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(563, 310);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Resize += new System.EventHandler(this.OnReSizePictureBox);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "PQZ files (*.PQZ)|*.PQZ|Surpro2.ini|*.ini|All Files|*.*";
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.SupportMultiDottedExtensions = true;
            // 
            // buttonCamera
            // 
            this.buttonCamera.Enabled = false;
            this.buttonCamera.Location = new System.Drawing.Point(16, 41);
            this.buttonCamera.Name = "buttonCamera";
            this.buttonCamera.Size = new System.Drawing.Size(192, 23);
            this.buttonCamera.TabIndex = 23;
            this.buttonCamera.Text = "Select camera...";
            this.buttonCamera.UseVisualStyleBackColor = true;
            this.buttonCamera.Click += new System.EventHandler(this.OnSelectCamera);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(592, 544);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(111, 23);
            this.button8.TabIndex = 26;
            this.button8.Text = "Close";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.OnClose);
            // 
            // buttonSelectServer
            // 
            this.buttonSelectServer.Location = new System.Drawing.Point(16, 12);
            this.buttonSelectServer.Name = "buttonSelectServer";
            this.buttonSelectServer.Size = new System.Drawing.Size(192, 23);
            this.buttonSelectServer.TabIndex = 31;
            this.buttonSelectServer.Text = "Login to server...";
            this.buttonSelectServer.UseVisualStyleBackColor = true;
            this.buttonSelectServer.Click += new System.EventHandler(this.LoginClick);
            // 
            // panelPlayback
            // 
            this.panelPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPlayback.Location = new System.Drawing.Point(23, 438);
            this.panelPlayback.Name = "panelPlayback";
            this.panelPlayback.Size = new System.Drawing.Size(687, 100);
            this.panelPlayback.TabIndex = 37;
            // 
            // buttonLoop
            // 
            this.buttonLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoop.Location = new System.Drawing.Point(3, 3);
            this.buttonLoop.Name = "buttonLoop";
            this.buttonLoop.Size = new System.Drawing.Size(126, 23);
            this.buttonLoop.TabIndex = 38;
            this.buttonLoop.Text = "Loop";
            this.buttonLoop.UseVisualStyleBackColor = true;
            this.buttonLoop.Click += new System.EventHandler(this.buttonLoop_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(135, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 39;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioLive);
            this.groupBox1.Controls.Add(this.radioPlayback);
            this.groupBox1.Location = new System.Drawing.Point(16, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(92, 70);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // radioLive
            // 
            this.radioLive.AutoSize = true;
            this.radioLive.Location = new System.Drawing.Point(7, 44);
            this.radioLive.Name = "radioLive";
            this.radioLive.Size = new System.Drawing.Size(45, 17);
            this.radioLive.TabIndex = 1;
            this.radioLive.TabStop = true;
            this.radioLive.Text = "Live";
            this.radioLive.UseVisualStyleBackColor = true;
            // 
            // radioPlayback
            // 
            this.radioPlayback.AutoSize = true;
            this.radioPlayback.Checked = true;
            this.radioPlayback.Location = new System.Drawing.Point(7, 20);
            this.radioPlayback.Name = "radioPlayback";
            this.radioPlayback.Size = new System.Drawing.Size(69, 17);
            this.radioPlayback.TabIndex = 0;
            this.radioPlayback.TabStop = true;
            this.radioPlayback.Text = "Playback";
            this.radioPlayback.UseVisualStyleBackColor = true;
            this.radioPlayback.CheckedChanged += new System.EventHandler(this.OnModeChange);
            // 
            // buttonLoopStop
            // 
            this.buttonLoopStop.Location = new System.Drawing.Point(241, 3);
            this.buttonLoopStop.Name = "buttonLoopStop";
            this.buttonLoopStop.Size = new System.Drawing.Size(126, 23);
            this.buttonLoopStop.TabIndex = 41;
            this.buttonLoopStop.Text = "Stop looping";
            this.buttonLoopStop.UseVisualStyleBackColor = true;
            this.buttonLoopStop.Click += new System.EventHandler(this.buttonLoopStop_Click);
            // 
            // panelLoop
            // 
            this.panelLoop.Controls.Add(this.buttonLoop);
            this.panelLoop.Controls.Add(this.buttonLoopStop);
            this.panelLoop.Controls.Add(this.progressBar1);
            this.panelLoop.Location = new System.Drawing.Point(23, 544);
            this.panelLoop.Name = "panelLoop";
            this.panelLoop.Size = new System.Drawing.Size(516, 41);
            this.panelLoop.TabIndex = 42;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 584);
            this.Controls.Add(this.panelLoop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelPlayback);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSelectServer);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.buttonCamera);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "Media Viewer - BitmapSource";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelLoop.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button buttonCamera;
		//private TimeLineUserControl timeLineUserControl1;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button buttonSelectServer;
		private System.Windows.Forms.Panel panelPlayback;
		private System.Windows.Forms.Button buttonLoop;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioLive;
        private System.Windows.Forms.RadioButton radioPlayback;
        private System.Windows.Forms.Button buttonLoopStop;
        private System.Windows.Forms.Panel panelLoop;
    }
}

