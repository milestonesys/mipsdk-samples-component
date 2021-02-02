namespace VideoFileViewer
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._panelImageViewer = new System.Windows.Forms.Panel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonDB = new System.Windows.Forms.Button();
            this.buttonSelectCamera = new System.Windows.Forms.Button();
            this._panelPlayback = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(462, 498);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.OnClose);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PQZ files|*.pqz";
            // 
            // _panelImageViewer
            // 
            this._panelImageViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panelImageViewer.Location = new System.Drawing.Point(13, 42);
            this._panelImageViewer.Name = "_panelImageViewer";
            this._panelImageViewer.Size = new System.Drawing.Size(524, 360);
            this._panelImageViewer.TabIndex = 1;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(12, 498);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "<<";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonForward.Location = new System.Drawing.Point(175, 498);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(75, 23);
            this.buttonForward.TabIndex = 3;
            this.buttonForward.Text = ">";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStop.Location = new System.Drawing.Point(94, 498);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "||";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // buttonDB
            // 
            this.buttonDB.Location = new System.Drawing.Point(13, 12);
            this.buttonDB.Name = "buttonDB";
            this.buttonDB.Size = new System.Drawing.Size(74, 23);
            this.buttonDB.TabIndex = 6;
            this.buttonDB.Text = "Media";
            this.buttonDB.UseVisualStyleBackColor = true;
            this.buttonDB.Click += new System.EventHandler(this.buttonDB_Click);
            // 
            // buttonSelectCamera
            // 
            this.buttonSelectCamera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectCamera.Enabled = false;
            this.buttonSelectCamera.Location = new System.Drawing.Point(94, 13);
            this.buttonSelectCamera.Name = "buttonSelectCamera";
            this.buttonSelectCamera.Size = new System.Drawing.Size(443, 23);
            this.buttonSelectCamera.TabIndex = 7;
            this.buttonSelectCamera.Text = "Camera ..";
            this.buttonSelectCamera.UseVisualStyleBackColor = true;
            this.buttonSelectCamera.Click += new System.EventHandler(this.buttonSelectCamera_Click);
            // 
            // _panelPlayback
            // 
            this._panelPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panelPlayback.Location = new System.Drawing.Point(13, 409);
            this._panelPlayback.Name = "_panelPlayback";
            this._panelPlayback.Size = new System.Drawing.Size(524, 83);
            this._panelPlayback.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 533);
            this.Controls.Add(this._panelPlayback);
            this.Controls.Add(this.buttonSelectCamera);
            this.Controls.Add(this.buttonDB);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this._panelImageViewer);
            this.Controls.Add(this.buttonClose);
            this.Name = "MainForm";
            this.Text = "VideoFileViewer Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel _panelImageViewer;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonDB;
        private System.Windows.Forms.Button buttonSelectCamera;
        private System.Windows.Forms.Panel _panelPlayback;
    }
}