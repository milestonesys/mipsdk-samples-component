namespace VideoViewer
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
            this.groupBoxVideo = new System.Windows.Forms.GroupBox();
            this.checkBoxHeader = new System.Windows.Forms.CheckBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxDigitalZoom = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSelect2 = new System.Windows.Forms.Button();
            this.buttonSelect1 = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonReverse = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.groupBoxPlayback = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonMode = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button11 = new System.Windows.Forms.Button();
            this.buttonLiftMask = new System.Windows.Forms.Button();
            this.checkBoxAdaptiveStreaming = new System.Windows.Forms.CheckBox();
            this.groupBoxVideo.SuspendLayout();
            this.groupBoxPlayback.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxVideo
            // 
            this.groupBoxVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVideo.Controls.Add(this.checkBoxAdaptiveStreaming);
            this.groupBoxVideo.Controls.Add(this.checkBoxHeader);
            this.groupBoxVideo.Controls.Add(this.button10);
            this.groupBoxVideo.Controls.Add(this.button9);
            this.groupBoxVideo.Controls.Add(this.button8);
            this.groupBoxVideo.Controls.Add(this.button1);
            this.groupBoxVideo.Controls.Add(this.panel2);
            this.groupBoxVideo.Controls.Add(this.checkBoxDigitalZoom);
            this.groupBoxVideo.Controls.Add(this.panel1);
            this.groupBoxVideo.Controls.Add(this.buttonSelect2);
            this.groupBoxVideo.Controls.Add(this.buttonSelect1);
            this.groupBoxVideo.Location = new System.Drawing.Point(12, 4);
            this.groupBoxVideo.Name = "groupBoxVideo";
            this.groupBoxVideo.Size = new System.Drawing.Size(668, 284);
            this.groupBoxVideo.TabIndex = 12;
            this.groupBoxVideo.TabStop = false;
            this.groupBoxVideo.Text = "Video";
            // 
            // checkBoxHeader
            // 
            this.checkBoxHeader.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxHeader.AutoSize = true;
            this.checkBoxHeader.Checked = true;
            this.checkBoxHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeader.Location = new System.Drawing.Point(292, 229);
            this.checkBoxHeader.Name = "checkBoxHeader";
            this.checkBoxHeader.Size = new System.Drawing.Size(91, 17);
            this.checkBoxHeader.TabIndex = 7;
            this.checkBoxHeader.Text = "Show Header";
            this.checkBoxHeader.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.Green;
            this.button10.Location = new System.Drawing.Point(509, 245);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(33, 23);
            this.button10.TabIndex = 6;
            this.button10.Text = "| |";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.OnStopRecording2);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.Green;
            this.button9.Location = new System.Drawing.Point(152, 247);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(33, 23);
            this.button9.TabIndex = 2;
            this.button9.Text = "| |";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.OnStopRecording1);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.Red;
            this.button8.Location = new System.Drawing.Point(470, 245);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(33, 23);
            this.button8.TabIndex = 5;
            this.button8.Text = "|>";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.OnStartRecording2);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(113, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "|>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnStartRecording1);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(371, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 180);
            this.panel2.TabIndex = 3;
            // 
            // checkBoxDigitalZoom
            // 
            this.checkBoxDigitalZoom.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxDigitalZoom.AutoSize = true;
            this.checkBoxDigitalZoom.Location = new System.Drawing.Point(292, 246);
            this.checkBoxDigitalZoom.Name = "checkBoxDigitalZoom";
            this.checkBoxDigitalZoom.Size = new System.Drawing.Size(83, 17);
            this.checkBoxDigitalZoom.TabIndex = 3;
            this.checkBoxDigitalZoom.Text = "Digital zoom";
            this.checkBoxDigitalZoom.UseVisualStyleBackColor = true;
            this.checkBoxDigitalZoom.CheckedChanged += new System.EventHandler(this.checkBoxDigitalZoom_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(19, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 180);
            this.panel1.TabIndex = 1;
            // 
            // buttonSelect2
            // 
            this.buttonSelect2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect2.Location = new System.Drawing.Point(371, 206);
            this.buttonSelect2.Name = "buttonSelect2";
            this.buttonSelect2.Size = new System.Drawing.Size(271, 23);
            this.buttonSelect2.TabIndex = 4;
            this.buttonSelect2.Text = "Select camera...";
            this.buttonSelect2.UseVisualStyleBackColor = true;
            this.buttonSelect2.Click += new System.EventHandler(this.buttonSelect2_Click);
            // 
            // buttonSelect1
            // 
            this.buttonSelect1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect1.Location = new System.Drawing.Point(19, 206);
            this.buttonSelect1.Name = "buttonSelect1";
            this.buttonSelect1.Size = new System.Drawing.Size(271, 23);
            this.buttonSelect1.TabIndex = 0;
            this.buttonSelect1.Text = "Select camera...";
            this.buttonSelect1.UseVisualStyleBackColor = true;
            this.buttonSelect1.Click += new System.EventHandler(this.buttonSelect1_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(310, 41);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(45, 23);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "| |";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(371, 41);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(45, 23);
            this.buttonForward.TabIndex = 6;
            this.buttonForward.Text = ">>";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonReverse
            // 
            this.buttonReverse.Location = new System.Drawing.Point(250, 41);
            this.buttonReverse.Name = "buttonReverse";
            this.buttonReverse.Size = new System.Drawing.Size(45, 23);
            this.buttonReverse.TabIndex = 4;
            this.buttonReverse.Text = "<<";
            this.buttonReverse.UseVisualStyleBackColor = true;
            this.buttonReverse.Click += new System.EventHandler(this.buttonReverse_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(94, 41);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "DB Start";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(499, 41);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "DB End";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // textBoxTime
            // 
            this.textBoxTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTime.Location = new System.Drawing.Point(250, 70);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(166, 21);
            this.textBoxTime.TabIndex = 7;
            // 
            // groupBoxPlayback
            // 
            this.groupBoxPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPlayback.Controls.Add(this.button7);
            this.groupBoxPlayback.Controls.Add(this.button4);
            this.groupBoxPlayback.Controls.Add(this.button3);
            this.groupBoxPlayback.Controls.Add(this.button2);
            this.groupBoxPlayback.Controls.Add(this.buttonMode);
            this.groupBoxPlayback.Controls.Add(this.button5);
            this.groupBoxPlayback.Controls.Add(this.textBoxTime);
            this.groupBoxPlayback.Controls.Add(this.buttonStop);
            this.groupBoxPlayback.Controls.Add(this.button6);
            this.groupBoxPlayback.Controls.Add(this.buttonForward);
            this.groupBoxPlayback.Controls.Add(this.buttonReverse);
            this.groupBoxPlayback.Location = new System.Drawing.Point(12, 294);
            this.groupBoxPlayback.Name = "groupBoxPlayback";
            this.groupBoxPlayback.Size = new System.Drawing.Size(668, 100);
            this.groupBoxPlayback.TabIndex = 19;
            this.groupBoxPlayback.TabStop = false;
            this.groupBoxPlayback.Text = "Playback Control";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(94, 68);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 2;
            this.button7.Text = "< Frame";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.OnPreviousFrame);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(499, 71);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Frame >";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnNextFrame);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(499, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Sequence >";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnNextSequence);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "< Sequence";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnPrevSequence);
            // 
            // buttonMode
            // 
            this.buttonMode.Location = new System.Drawing.Point(251, 12);
            this.buttonMode.Name = "buttonMode";
            this.buttonMode.Size = new System.Drawing.Size(165, 23);
            this.buttonMode.TabIndex = 3;
            this.buttonMode.Text = "Current mode: Live";
            this.buttonMode.UseVisualStyleBackColor = true;
            this.buttonMode.Click += new System.EventHandler(this.OnModeClick);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(593, 407);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 23);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnClose);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button11.Location = new System.Drawing.Point(12, 407);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(202, 23);
            this.button11.TabIndex = 21;
            this.button11.Text = "Open Exported DB...";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // buttonLiftMask
            // 
            this.buttonLiftMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLiftMask.Location = new System.Drawing.Point(235, 407);
            this.buttonLiftMask.Name = "buttonLiftMask";
            this.buttonLiftMask.Size = new System.Drawing.Size(95, 23);
            this.buttonLiftMask.TabIndex = 22;
            this.buttonLiftMask.Text = "Lift privacy mask";
            this.buttonLiftMask.UseVisualStyleBackColor = true;
            this.buttonLiftMask.Click += new System.EventHandler(this.buttonLiftMask_Click);
            // 
            // checkBoxAdaptiveStreaming
            // 
            this.checkBoxAdaptiveStreaming.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxAdaptiveStreaming.AutoSize = true;
            this.checkBoxAdaptiveStreaming.Location = new System.Drawing.Point(292, 261);
            this.checkBoxAdaptiveStreaming.Name = "checkBoxAdaptiveStreaming";
            this.checkBoxAdaptiveStreaming.Size = new System.Drawing.Size(118, 17);
            this.checkBoxAdaptiveStreaming.TabIndex = 8;
            this.checkBoxAdaptiveStreaming.Text = "Adaptive Streaming";
            this.checkBoxAdaptiveStreaming.UseVisualStyleBackColor = true;
            this.checkBoxAdaptiveStreaming.CheckedChanged += new System.EventHandler(this.checkBoxAdaptiveStreaming_CheckedChanged);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 442);
            this.Controls.Add(this.buttonLiftMask);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxPlayback);
            this.Controls.Add(this.groupBoxVideo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(708, 481);
            this.Name = "MainForm";
            this.Text = "Video Viewer";
            this.groupBoxVideo.ResumeLayout(false);
            this.groupBoxVideo.PerformLayout();
            this.groupBoxPlayback.ResumeLayout(false);
            this.groupBoxPlayback.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxVideo;
		private System.Windows.Forms.Button buttonSelect2;
		private System.Windows.Forms.Button buttonSelect1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox checkBoxDigitalZoom;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonForward;
		private System.Windows.Forms.Button buttonReverse;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBoxPlayback;
		private System.Windows.Forms.Button buttonMode;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkBoxHeader;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button buttonLiftMask;
        private System.Windows.Forms.CheckBox checkBoxAdaptiveStreaming;
    }
}

