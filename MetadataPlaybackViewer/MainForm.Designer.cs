namespace MetadataPlaybackViewer
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
            this.groupBoxPlayback = new System.Windows.Forms.GroupBox();
            this.textBoxAsked = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonReverse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonMetadataDevice = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.buttonSelectXPCO = new System.Windows.Forms.Button();
            this.buttonSelectServer = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.timeLineUserControl1 = new MetadataPlaybackViewer.TimeLineUserControl();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.groupBoxPlayback.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxPlayback
            // 
            this.groupBoxPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxPlayback.Controls.Add(this.textBoxAsked);
            this.groupBoxPlayback.Controls.Add(this.button7);
            this.groupBoxPlayback.Controls.Add(this.button4);
            this.groupBoxPlayback.Controls.Add(this.button3);
            this.groupBoxPlayback.Controls.Add(this.button2);
            this.groupBoxPlayback.Controls.Add(this.button5);
            this.groupBoxPlayback.Controls.Add(this.textBoxTime);
            this.groupBoxPlayback.Controls.Add(this.buttonStop);
            this.groupBoxPlayback.Controls.Add(this.button6);
            this.groupBoxPlayback.Controls.Add(this.buttonForward);
            this.groupBoxPlayback.Controls.Add(this.buttonReverse);
            this.groupBoxPlayback.Location = new System.Drawing.Point(15, 431);
            this.groupBoxPlayback.Name = "groupBoxPlayback";
            this.groupBoxPlayback.Size = new System.Drawing.Size(402, 106);
            this.groupBoxPlayback.TabIndex = 22;
            this.groupBoxPlayback.TabStop = false;
            this.groupBoxPlayback.Text = "Playback Control";
            // 
            // textBoxAsked
            // 
            this.textBoxAsked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAsked.Location = new System.Drawing.Point(121, 77);
            this.textBoxAsked.Name = "textBoxAsked";
            this.textBoxAsked.ReadOnly = true;
            this.textBoxAsked.Size = new System.Drawing.Size(166, 21);
            this.textBoxAsked.TabIndex = 11;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(13, 74);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 2;
            this.button7.Text = "< Frame";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.OnPreviousFrame);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(314, 77);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Frame >";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnNextFrame);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(314, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Sequence >";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnNextSequence);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "< Sequence";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnPrevSequence);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 47);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "DB Start";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxTime
            // 
            this.textBoxTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTime.Location = new System.Drawing.Point(121, 47);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(166, 21);
            this.textBoxTime.TabIndex = 7;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(181, 18);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(45, 23);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "| |";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(314, 47);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "DB End";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(242, 18);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(45, 23);
            this.buttonForward.TabIndex = 6;
            this.buttonForward.Text = ">>";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonReverse
            // 
            this.buttonReverse.Location = new System.Drawing.Point(121, 18);
            this.buttonReverse.Name = "buttonReverse";
            this.buttonReverse.Size = new System.Drawing.Size(45, 23);
            this.buttonReverse.TabIndex = 4;
            this.buttonReverse.Text = "<<";
            this.buttonReverse.UseVisualStyleBackColor = true;
            this.buttonReverse.Click += new System.EventHandler(this.buttonReverse_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "PQZ files (*.PQZ)|*.PQZ|Surpro2.ini|*.ini|All Files|*.*";
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.SupportMultiDottedExtensions = true;
            // 
            // buttonMetadataDevice
            // 
            this.buttonMetadataDevice.Enabled = false;
            this.buttonMetadataDevice.Location = new System.Drawing.Point(371, 44);
            this.buttonMetadataDevice.Name = "buttonMetadataDevice";
            this.buttonMetadataDevice.Size = new System.Drawing.Size(192, 23);
            this.buttonMetadataDevice.TabIndex = 23;
            this.buttonMetadataDevice.Text = "Select metadata device...";
            this.buttonMetadataDevice.UseVisualStyleBackColor = true;
            this.buttonMetadataDevice.Click += new System.EventHandler(this.OnSelectMetadataDevice);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(604, 515);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(111, 23);
            this.button8.TabIndex = 26;
            this.button8.Text = "Close";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.OnClose);
            // 
            // buttonSelectXPCO
            // 
            this.buttonSelectXPCO.Enabled = false;
            this.buttonSelectXPCO.Location = new System.Drawing.Point(160, 16);
            this.buttonSelectXPCO.Name = "buttonSelectXPCO";
            this.buttonSelectXPCO.Size = new System.Drawing.Size(192, 23);
            this.buttonSelectXPCO.TabIndex = 30;
            this.buttonSelectXPCO.Text = "Select database storage... (XML/SCP)"; //"Select database storage... (Folder)";
            this.buttonSelectXPCO.UseVisualStyleBackColor = true;
            this.buttonSelectXPCO.Click += new System.EventHandler(this.SelectXPCOStorageClick);
            // 
            // buttonSelectServer
            // 
            this.buttonSelectServer.Location = new System.Drawing.Point(160, 44);
            this.buttonSelectServer.Name = "buttonSelectServer";
            this.buttonSelectServer.Size = new System.Drawing.Size(192, 23);
            this.buttonSelectServer.TabIndex = 31;
            this.buttonSelectServer.Text = "Login to server...";
            this.buttonSelectServer.UseVisualStyleBackColor = true;
            this.buttonSelectServer.Click += new System.EventHandler(this.LoginClick);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(16, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(105, 17);
            this.radioButton2.TabIndex = 33;
            this.radioButton2.Text = "Open file (XPCO)";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.SourceChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(16, 50);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(104, 17);
            this.radioButton3.TabIndex = 34;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Login to a server";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.SourceChanged);
            // 
            // timeLineUserControl1
            // 
            this.timeLineUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineUserControl1.BackColor = System.Drawing.Color.Transparent;
            this.timeLineUserControl1.Item = null;
            this.timeLineUserControl1.Location = new System.Drawing.Point(606, 87);
            this.timeLineUserControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.timeLineUserControl1.Name = "timeLineUserControl1";
            this.timeLineUserControl1.Size = new System.Drawing.Size(109, 400);
            this.timeLineUserControl1.TabIndex = 24;
            // 
            // textOutput
            // 
            this.textOutput.Location = new System.Drawing.Point(15, 87);
            this.textOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textOutput.Multiline = true;
            this.textOutput.Name = "textOutput";
            this.textOutput.ReadOnly = true;
            this.textOutput.Size = new System.Drawing.Size(549, 332);
            this.textOutput.TabIndex = 35;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 569);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.buttonSelectServer);
            this.Controls.Add(this.buttonSelectXPCO);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.timeLineUserControl1);
            this.Controls.Add(this.buttonMetadataDevice);
            this.Controls.Add(this.groupBoxPlayback);
            this.Name = "MainForm";
            this.Text = "Metadata Playback Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnResize);
            this.groupBoxPlayback.ResumeLayout(false);
            this.groupBoxPlayback.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.GroupBox groupBoxPlayback;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button buttonForward;
		private System.Windows.Forms.Button buttonReverse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TextBox textBoxAsked;
		private System.Windows.Forms.Button buttonMetadataDevice;
		private TimeLineUserControl timeLineUserControl1;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button buttonSelectXPCO;
        private System.Windows.Forms.Button buttonSelectServer;
		private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TextBox textOutput;
	}
}

