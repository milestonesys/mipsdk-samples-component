namespace MetadataLiveViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBoxMetadata = new System.Windows.Forms.GroupBox();
            this.textBoxMetadataOutput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.buttonPause = new System.Windows.Forms.Button();
            this.checkBoxClientLive = new System.Windows.Forms.CheckBox();
            this.checkBoxDiskFull = new System.Windows.Forms.CheckBox();
            this.checkBoxDBFail = new System.Windows.Forms.CheckBox();
            this.checkBoxLiveFeed = new System.Windows.Forms.CheckBox();
            this.checkBoxRec = new System.Windows.Forms.CheckBox();
            this.checkBoxNotification = new System.Windows.Forms.CheckBox();
            this.checkBoxOffline = new System.Windows.Forms.CheckBox();
            this.checkBoxMotion = new System.Windows.Forms.CheckBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deviceSelectButton = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxMetadata.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMetadata
            // 
            this.groupBoxMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMetadata.Controls.Add(this.textBoxMetadataOutput);
            this.groupBoxMetadata.Controls.Add(this.label4);
            this.groupBoxMetadata.Controls.Add(this.labelSize);
            this.groupBoxMetadata.Controls.Add(this.buttonPause);
            this.groupBoxMetadata.Controls.Add(this.checkBoxClientLive);
            this.groupBoxMetadata.Controls.Add(this.checkBoxDiskFull);
            this.groupBoxMetadata.Controls.Add(this.checkBoxDBFail);
            this.groupBoxMetadata.Controls.Add(this.checkBoxLiveFeed);
            this.groupBoxMetadata.Controls.Add(this.checkBoxRec);
            this.groupBoxMetadata.Controls.Add(this.checkBoxNotification);
            this.groupBoxMetadata.Controls.Add(this.checkBoxOffline);
            this.groupBoxMetadata.Controls.Add(this.checkBoxMotion);
            this.groupBoxMetadata.Controls.Add(this.labelCount);
            this.groupBoxMetadata.Controls.Add(this.label1);
            this.groupBoxMetadata.Controls.Add(this.deviceSelectButton);
            this.groupBoxMetadata.Location = new System.Drawing.Point(16, 5);
            this.groupBoxMetadata.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxMetadata.Name = "groupBoxMetadata";
            this.groupBoxMetadata.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxMetadata.Size = new System.Drawing.Size(897, 523);
            this.groupBoxMetadata.TabIndex = 12;
            this.groupBoxMetadata.TabStop = false;
            this.groupBoxMetadata.Text = "Metadata";
            // 
            // textBoxMetadataOutput
            // 
            this.textBoxMetadataOutput.Location = new System.Drawing.Point(22, 33);
            this.textBoxMetadataOutput.Multiline = true;
            this.textBoxMetadataOutput.Name = "textBoxMetadataOutput";
            this.textBoxMetadataOutput.ReadOnly = true;
            this.textBoxMetadataOutput.Size = new System.Drawing.Size(555, 405);
            this.textBoxMetadataOutput.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(597, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Bytes:";
            // 
            // labelSize
            // 
            this.labelSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(725, 74);
            this.labelSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(16, 17);
            this.labelSize.TabIndex = 22;
            this.labelSize.Text = "0";
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPause.Enabled = false;
            this.buttonPause.Location = new System.Drawing.Point(768, 463);
            this.buttonPause.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(100, 28);
            this.buttonPause.TabIndex = 19;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.OnClick);
            // 
            // checkBoxClientLive
            // 
            this.checkBoxClientLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxClientLive.AutoSize = true;
            this.checkBoxClientLive.Enabled = false;
            this.checkBoxClientLive.Location = new System.Drawing.Point(600, 468);
            this.checkBoxClientLive.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxClientLive.Name = "checkBoxClientLive";
            this.checkBoxClientLive.Size = new System.Drawing.Size(141, 21);
            this.checkBoxClientLive.TabIndex = 18;
            this.checkBoxClientLive.Text = "We have paused ";
            this.checkBoxClientLive.UseVisualStyleBackColor = true;
            // 
            // checkBoxDiskFull
            // 
            this.checkBoxDiskFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDiskFull.AutoSize = true;
            this.checkBoxDiskFull.Enabled = false;
            this.checkBoxDiskFull.Location = new System.Drawing.Point(600, 432);
            this.checkBoxDiskFull.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxDiskFull.Name = "checkBoxDiskFull";
            this.checkBoxDiskFull.Size = new System.Drawing.Size(79, 21);
            this.checkBoxDiskFull.TabIndex = 17;
            this.checkBoxDiskFull.Text = "Disk full";
            this.checkBoxDiskFull.UseVisualStyleBackColor = true;
            // 
            // checkBoxDBFail
            // 
            this.checkBoxDBFail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDBFail.AutoSize = true;
            this.checkBoxDBFail.Enabled = false;
            this.checkBoxDBFail.Location = new System.Drawing.Point(600, 396);
            this.checkBoxDBFail.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxDBFail.Name = "checkBoxDBFail";
            this.checkBoxDBFail.Size = new System.Drawing.Size(75, 21);
            this.checkBoxDBFail.TabIndex = 16;
            this.checkBoxDBFail.Text = "DB Fail";
            this.checkBoxDBFail.UseVisualStyleBackColor = true;
            // 
            // checkBoxLiveFeed
            // 
            this.checkBoxLiveFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLiveFeed.AutoSize = true;
            this.checkBoxLiveFeed.Enabled = false;
            this.checkBoxLiveFeed.Location = new System.Drawing.Point(600, 360);
            this.checkBoxLiveFeed.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxLiveFeed.Name = "checkBoxLiveFeed";
            this.checkBoxLiveFeed.Size = new System.Drawing.Size(92, 21);
            this.checkBoxLiveFeed.TabIndex = 15;
            this.checkBoxLiveFeed.Text = "Live Feed";
            this.checkBoxLiveFeed.UseVisualStyleBackColor = true;
            // 
            // checkBoxRec
            // 
            this.checkBoxRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRec.AutoSize = true;
            this.checkBoxRec.Enabled = false;
            this.checkBoxRec.Location = new System.Drawing.Point(600, 323);
            this.checkBoxRec.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRec.Name = "checkBoxRec";
            this.checkBoxRec.Size = new System.Drawing.Size(58, 21);
            this.checkBoxRec.TabIndex = 14;
            this.checkBoxRec.Text = "REC";
            this.checkBoxRec.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotification
            // 
            this.checkBoxNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxNotification.AutoSize = true;
            this.checkBoxNotification.Enabled = false;
            this.checkBoxNotification.Location = new System.Drawing.Point(600, 287);
            this.checkBoxNotification.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxNotification.Name = "checkBoxNotification";
            this.checkBoxNotification.Size = new System.Drawing.Size(100, 21);
            this.checkBoxNotification.TabIndex = 13;
            this.checkBoxNotification.Text = "Notification";
            this.checkBoxNotification.UseVisualStyleBackColor = true;
            // 
            // checkBoxOffline
            // 
            this.checkBoxOffline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOffline.AutoSize = true;
            this.checkBoxOffline.Enabled = false;
            this.checkBoxOffline.Location = new System.Drawing.Point(600, 251);
            this.checkBoxOffline.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxOffline.Name = "checkBoxOffline";
            this.checkBoxOffline.Size = new System.Drawing.Size(118, 21);
            this.checkBoxOffline.TabIndex = 12;
            this.checkBoxOffline.Text = "Device Offline";
            this.checkBoxOffline.UseVisualStyleBackColor = true;
            // 
            // checkBoxMotion
            // 
            this.checkBoxMotion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMotion.AutoSize = true;
            this.checkBoxMotion.Enabled = false;
            this.checkBoxMotion.Location = new System.Drawing.Point(600, 214);
            this.checkBoxMotion.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxMotion.Name = "checkBoxMotion";
            this.checkBoxMotion.Size = new System.Drawing.Size(72, 21);
            this.checkBoxMotion.TabIndex = 11;
            this.checkBoxMotion.Text = "Motion";
            this.checkBoxMotion.UseVisualStyleBackColor = true;
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(725, 36);
            this.labelCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(16, 17);
            this.labelCount.TabIndex = 7;
            this.labelCount.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(597, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Message Count:";
            // 
            // deviceSelectButton
            // 
            this.deviceSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deviceSelectButton.Location = new System.Drawing.Point(22, 463);
            this.deviceSelectButton.Margin = new System.Windows.Forms.Padding(4);
            this.deviceSelectButton.Name = "deviceSelectButton";
            this.deviceSelectButton.Size = new System.Drawing.Size(555, 28);
            this.deviceSelectButton.TabIndex = 0;
            this.deviceSelectButton.Text = "Select metadata device...";
            this.deviceSelectButton.UseVisualStyleBackColor = true;
            this.deviceSelectButton.Click += new System.EventHandler(this.OnSelect1Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(797, 551);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(116, 28);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnClose);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 594);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxMetadata);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(927, 592);
            this.Name = "MainForm";
            this.Text = "Metadata Live Viewer";
            this.groupBoxMetadata.ResumeLayout(false);
            this.groupBoxMetadata.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxMetadata;
        private System.Windows.Forms.Button deviceSelectButton;
        private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxNotification;
		private System.Windows.Forms.CheckBox checkBoxOffline;
		private System.Windows.Forms.CheckBox checkBoxMotion;
		private System.Windows.Forms.CheckBox checkBoxRec;
		private System.Windows.Forms.CheckBox checkBoxClientLive;
		private System.Windows.Forms.CheckBox checkBoxDiskFull;
		private System.Windows.Forms.CheckBox checkBoxDBFail;
		private System.Windows.Forms.CheckBox checkBoxLiveFeed;
        private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMetadataOutput;
	}
}

