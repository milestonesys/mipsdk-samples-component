namespace MultiChannelMetadataProvider
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
            this.panelTimeDisplay = new System.Windows.Forms.Panel();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.textMetadata = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSessionCount = new System.Windows.Forms.TextBox();
            this.buttonSendData = new System.Windows.Forms.Button();
            this.listBoxProperties = new System.Windows.Forms.ListBox();
            this.panelTimeDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(697, 295);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // panelTimeDisplay
            // 
            this.panelTimeDisplay.BackColor = System.Drawing.Color.Azure;
            this.panelTimeDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTimeDisplay.Controls.Add(this.sourceComboBox);
            this.panelTimeDisplay.Controls.Add(this.textMetadata);
            this.panelTimeDisplay.Controls.Add(this.label4);
            this.panelTimeDisplay.Controls.Add(this.textBoxTime);
            this.panelTimeDisplay.Location = new System.Drawing.Point(167, 23);
            this.panelTimeDisplay.Name = "panelTimeDisplay";
            this.panelTimeDisplay.Size = new System.Drawing.Size(342, 254);
            this.panelTimeDisplay.TabIndex = 1;
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Items.AddRange(new object[] {
            "Bounding box",
            "GPS",
            "Other"});
            this.sourceComboBox.Location = new System.Drawing.Point(216, 30);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(121, 21);
            this.sourceComboBox.TabIndex = 16;
            this.sourceComboBox.SelectedIndexChanged += new System.EventHandler(this.DisplayIndexChanged);
            // 
            // textMetadata
            // 
            this.textMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textMetadata.CausesValidation = false;
            this.textMetadata.Location = new System.Drawing.Point(2, 60);
            this.textMetadata.Margin = new System.Windows.Forms.Padding(2);
            this.textMetadata.Multiline = true;
            this.textMetadata.Name = "textMetadata";
            this.textMetadata.ReadOnly = true;
            this.textMetadata.Size = new System.Drawing.Size(336, 190);
            this.textMetadata.TabIndex = 15;
            this.textMetadata.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Display metadata source:";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(2, 3);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(118, 20);
            this.textBoxTime.TabIndex = 0;
            this.textBoxTime.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(13, 23);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(132, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Accept Sessions";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnectClick);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(12, 52);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(133, 23);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Disconnect Sessions";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnectClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Total sessions:";
            // 
            // textBoxSessionCount
            // 
            this.textBoxSessionCount.Location = new System.Drawing.Point(101, 257);
            this.textBoxSessionCount.Name = "textBoxSessionCount";
            this.textBoxSessionCount.ReadOnly = true;
            this.textBoxSessionCount.Size = new System.Drawing.Size(44, 20);
            this.textBoxSessionCount.TabIndex = 5;
            this.textBoxSessionCount.TabStop = false;
            this.textBoxSessionCount.Text = "0";
            // 
            // buttonSendData
            // 
            this.buttonSendData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSendData.Enabled = false;
            this.buttonSendData.Location = new System.Drawing.Point(13, 295);
            this.buttonSendData.Name = "buttonSendData";
            this.buttonSendData.Size = new System.Drawing.Size(133, 23);
            this.buttonSendData.TabIndex = 2;
            this.buttonSendData.Text = "Start all metadata";
            this.buttonSendData.UseVisualStyleBackColor = true;
            this.buttonSendData.Click += new System.EventHandler(this.buttonSendData_Click);
            // 
            // listBoxProperties
            // 
            this.listBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxProperties.FormattingEnabled = true;
            this.listBoxProperties.Location = new System.Drawing.Point(527, 84);
            this.listBoxProperties.Name = "listBoxProperties";
            this.listBoxProperties.Size = new System.Drawing.Size(236, 186);
            this.listBoxProperties.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(784, 330);
            this.Controls.Add(this.listBoxProperties);
            this.Controls.Add(this.buttonSendData);
            this.Controls.Add(this.textBoxSessionCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.panelTimeDisplay);
            this.Controls.Add(this.buttonClose);
            this.Name = "MainForm";
            this.Text = "Multi Channel Metadata Provider";
            this.panelTimeDisplay.ResumeLayout(false);
            this.panelTimeDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelTimeDisplay;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonDisconnect;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSessionCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSendData;
        private System.Windows.Forms.TextBox textMetadata;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.ListBox listBoxProperties;
    }
}

