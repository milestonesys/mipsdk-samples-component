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
            this.checkBoxZoom2 = new System.Windows.Forms.CheckBox();
            this.checkBoxZoom1 = new System.Windows.Forms.CheckBox();
            this.radioButtonStopAtEnd = new System.Windows.Forms.RadioButton();
            this.radioButtonSkip = new System.Windows.Forms.RadioButton();
            this.radioButtonNoskip = new System.Windows.Forms.RadioButton();
            this.checkBoxAudio2 = new System.Windows.Forms.CheckBox();
            this.checkBoxAudio = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSelect2 = new System.Windows.Forms.Button();
            this.buttonSelect1 = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonReverse = new System.Windows.Forms.Button();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.groupBoxPlayback = new System.Windows.Forms.GroupBox();
            this.buttonMode = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonMode2 = new System.Windows.Forms.Button();
            this.textBoxTime2 = new System.Windows.Forms.TextBox();
            this.buttonStop2 = new System.Windows.Forms.Button();
            this.buttonForward2 = new System.Windows.Forms.Button();
            this.buttonReverse2 = new System.Windows.Forms.Button();
            this.groupBoxVideo.SuspendLayout();
            this.groupBoxPlayback.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxVideo
            // 
            this.groupBoxVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVideo.Controls.Add(this.checkBoxZoom2);
            this.groupBoxVideo.Controls.Add(this.checkBoxZoom1);
            this.groupBoxVideo.Controls.Add(this.radioButtonStopAtEnd);
            this.groupBoxVideo.Controls.Add(this.radioButtonSkip);
            this.groupBoxVideo.Controls.Add(this.radioButtonNoskip);
            this.groupBoxVideo.Controls.Add(this.checkBoxAudio2);
            this.groupBoxVideo.Controls.Add(this.checkBoxAudio);
            this.groupBoxVideo.Controls.Add(this.panel2);
            this.groupBoxVideo.Controls.Add(this.panel1);
            this.groupBoxVideo.Controls.Add(this.buttonSelect2);
            this.groupBoxVideo.Controls.Add(this.buttonSelect1);
            this.groupBoxVideo.Location = new System.Drawing.Point(12, 4);
            this.groupBoxVideo.Name = "groupBoxVideo";
            this.groupBoxVideo.Size = new System.Drawing.Size(668, 294);
            this.groupBoxVideo.TabIndex = 12;
            this.groupBoxVideo.TabStop = false;
            this.groupBoxVideo.Text = "Video";
            // 
            // checkBoxZoom2
            // 
            this.checkBoxZoom2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxZoom2.AutoSize = true;
            this.checkBoxZoom2.Enabled = false;
            this.checkBoxZoom2.Location = new System.Drawing.Point(371, 234);
            this.checkBoxZoom2.Name = "checkBoxZoom2";
            this.checkBoxZoom2.Size = new System.Drawing.Size(121, 17);
            this.checkBoxZoom2.TabIndex = 11;
            this.checkBoxZoom2.Text = "Enable Digital Zoom";
            this.checkBoxZoom2.UseVisualStyleBackColor = true;
            this.checkBoxZoom2.Click += new System.EventHandler(this.OnZoom2CheckChanged);
            // 
            // checkBoxZoom1
            // 
            this.checkBoxZoom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxZoom1.AutoSize = true;
            this.checkBoxZoom1.Enabled = false;
            this.checkBoxZoom1.Location = new System.Drawing.Point(19, 235);
            this.checkBoxZoom1.Name = "checkBoxZoom1";
            this.checkBoxZoom1.Size = new System.Drawing.Size(121, 17);
            this.checkBoxZoom1.TabIndex = 10;
            this.checkBoxZoom1.Text = "Enable Digital Zoom";
            this.checkBoxZoom1.UseVisualStyleBackColor = true;
            this.checkBoxZoom1.CheckedChanged += new System.EventHandler(this.OnZoom1CheckChanged);
            // 
            // radioButtonStopAtEnd
            // 
            this.radioButtonStopAtEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonStopAtEnd.AutoSize = true;
            this.radioButtonStopAtEnd.Enabled = false;
            this.radioButtonStopAtEnd.Location = new System.Drawing.Point(514, 270);
            this.radioButtonStopAtEnd.Name = "radioButtonStopAtEnd";
            this.radioButtonStopAtEnd.Size = new System.Drawing.Size(132, 17);
            this.radioButtonStopAtEnd.TabIndex = 9;
            this.radioButtonStopAtEnd.Text = "Stop at Sequence end";
            this.radioButtonStopAtEnd.UseVisualStyleBackColor = true;
            this.radioButtonStopAtEnd.CheckedChanged += new System.EventHandler(this.OnSkipModeChanged);
            // 
            // radioButtonSkip
            // 
            this.radioButtonSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonSkip.AutoSize = true;
            this.radioButtonSkip.Checked = true;
            this.radioButtonSkip.Enabled = false;
            this.radioButtonSkip.Location = new System.Drawing.Point(514, 252);
            this.radioButtonSkip.Name = "radioButtonSkip";
            this.radioButtonSkip.Size = new System.Drawing.Size(67, 17);
            this.radioButtonSkip.TabIndex = 8;
            this.radioButtonSkip.TabStop = true;
            this.radioButtonSkip.Text = "Skip gap";
            this.radioButtonSkip.UseVisualStyleBackColor = true;
            this.radioButtonSkip.CheckedChanged += new System.EventHandler(this.OnSkipModeChanged);
            // 
            // radioButtonNoskip
            // 
            this.radioButtonNoskip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonNoskip.AutoSize = true;
            this.radioButtonNoskip.Enabled = false;
            this.radioButtonNoskip.Location = new System.Drawing.Point(514, 234);
            this.radioButtonNoskip.Name = "radioButtonNoskip";
            this.radioButtonNoskip.Size = new System.Drawing.Size(61, 17);
            this.radioButtonNoskip.TabIndex = 7;
            this.radioButtonNoskip.Text = "No skip";
            this.radioButtonNoskip.UseVisualStyleBackColor = true;
            this.radioButtonNoskip.CheckedChanged += new System.EventHandler(this.OnSkipModeChanged);
            // 
            // checkBoxAudio2
            // 
            this.checkBoxAudio2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAudio2.AutoSize = true;
            this.checkBoxAudio2.Enabled = false;
            this.checkBoxAudio2.Location = new System.Drawing.Point(371, 258);
            this.checkBoxAudio2.Name = "checkBoxAudio2";
            this.checkBoxAudio2.Size = new System.Drawing.Size(89, 17);
            this.checkBoxAudio2.TabIndex = 6;
            this.checkBoxAudio2.Text = "Enable Audio";
            this.checkBoxAudio2.UseVisualStyleBackColor = true;
            this.checkBoxAudio2.CheckedChanged += new System.EventHandler(this.OnAudio2CheckChanged);
            // 
            // checkBoxAudio
            // 
            this.checkBoxAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAudio.AutoSize = true;
            this.checkBoxAudio.Enabled = false;
            this.checkBoxAudio.Location = new System.Drawing.Point(19, 258);
            this.checkBoxAudio.Name = "checkBoxAudio";
            this.checkBoxAudio.Size = new System.Drawing.Size(89, 17);
            this.checkBoxAudio.TabIndex = 5;
            this.checkBoxAudio.Text = "Enable Audio";
            this.checkBoxAudio.UseVisualStyleBackColor = true;
            this.checkBoxAudio.CheckedChanged += new System.EventHandler(this.OnAudio1CheckChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(371, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 179);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(19, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 179);
            this.panel1.TabIndex = 1;
            // 
            // buttonSelect2
            // 
            this.buttonSelect2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect2.Location = new System.Drawing.Point(371, 205);
            this.buttonSelect2.Name = "buttonSelect2";
            this.buttonSelect2.Size = new System.Drawing.Size(271, 23);
            this.buttonSelect2.TabIndex = 4;
            this.buttonSelect2.Text = "Select camera...";
            this.buttonSelect2.UseVisualStyleBackColor = true;
            this.buttonSelect2.Click += new System.EventHandler(this.OnSelect2Click);
            // 
            // buttonSelect1
            // 
            this.buttonSelect1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect1.Location = new System.Drawing.Point(19, 205);
            this.buttonSelect1.Name = "buttonSelect1";
            this.buttonSelect1.Size = new System.Drawing.Size(271, 23);
            this.buttonSelect1.TabIndex = 0;
            this.buttonSelect1.Text = "Select camera...";
            this.buttonSelect1.UseVisualStyleBackColor = true;
            this.buttonSelect1.Click += new System.EventHandler(this.OnSelect1Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(122, 48);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(45, 23);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "| |";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.OnStop1Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Enabled = false;
            this.buttonForward.Location = new System.Drawing.Point(183, 48);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(45, 23);
            this.buttonForward.TabIndex = 6;
            this.buttonForward.Text = ">>";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.OnForward1Click);
            // 
            // buttonReverse
            // 
            this.buttonReverse.Enabled = false;
            this.buttonReverse.Location = new System.Drawing.Point(62, 48);
            this.buttonReverse.Name = "buttonReverse";
            this.buttonReverse.Size = new System.Drawing.Size(45, 23);
            this.buttonReverse.TabIndex = 4;
            this.buttonReverse.Text = "<<";
            this.buttonReverse.UseVisualStyleBackColor = true;
            this.buttonReverse.Click += new System.EventHandler(this.OnReverse1Click);
            // 
            // textBoxTime
            // 
            this.textBoxTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTime.Location = new System.Drawing.Point(62, 77);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(166, 21);
            this.textBoxTime.TabIndex = 7;
            // 
            // groupBoxPlayback
            // 
            this.groupBoxPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPlayback.Controls.Add(this.buttonMode);
            this.groupBoxPlayback.Controls.Add(this.textBoxTime);
            this.groupBoxPlayback.Controls.Add(this.buttonStop);
            this.groupBoxPlayback.Controls.Add(this.buttonForward);
            this.groupBoxPlayback.Controls.Add(this.buttonReverse);
            this.groupBoxPlayback.Location = new System.Drawing.Point(12, 304);
            this.groupBoxPlayback.Name = "groupBoxPlayback";
            this.groupBoxPlayback.Size = new System.Drawing.Size(309, 103);
            this.groupBoxPlayback.TabIndex = 19;
            this.groupBoxPlayback.TabStop = false;
            this.groupBoxPlayback.Text = "Playback Control - Left";
            // 
            // buttonMode
            // 
            this.buttonMode.Location = new System.Drawing.Point(63, 19);
            this.buttonMode.Name = "buttonMode";
            this.buttonMode.Size = new System.Drawing.Size(165, 23);
            this.buttonMode.TabIndex = 3;
            this.buttonMode.Text = "Current mode: Live";
            this.buttonMode.UseVisualStyleBackColor = true;
            this.buttonMode.Click += new System.EventHandler(this.OnMode1Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(593, 417);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 23);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnClose);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonMode2);
            this.groupBox1.Controls.Add(this.textBoxTime2);
            this.groupBox1.Controls.Add(this.buttonStop2);
            this.groupBox1.Controls.Add(this.buttonForward2);
            this.groupBox1.Controls.Add(this.buttonReverse2);
            this.groupBox1.Location = new System.Drawing.Point(366, 304);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 103);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Playback Control - Right";
            // 
            // buttonMode2
            // 
            this.buttonMode2.Location = new System.Drawing.Point(73, 19);
            this.buttonMode2.Name = "buttonMode2";
            this.buttonMode2.Size = new System.Drawing.Size(165, 23);
            this.buttonMode2.TabIndex = 3;
            this.buttonMode2.Text = "Current mode: Live";
            this.buttonMode2.UseVisualStyleBackColor = true;
            this.buttonMode2.Click += new System.EventHandler(this.OnMode2Click);
            // 
            // textBoxTime2
            // 
            this.textBoxTime2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTime2.Location = new System.Drawing.Point(72, 77);
            this.textBoxTime2.Name = "textBoxTime2";
            this.textBoxTime2.ReadOnly = true;
            this.textBoxTime2.Size = new System.Drawing.Size(166, 21);
            this.textBoxTime2.TabIndex = 7;
            // 
            // buttonStop2
            // 
            this.buttonStop2.Enabled = false;
            this.buttonStop2.Location = new System.Drawing.Point(132, 48);
            this.buttonStop2.Name = "buttonStop2";
            this.buttonStop2.Size = new System.Drawing.Size(45, 23);
            this.buttonStop2.TabIndex = 5;
            this.buttonStop2.Text = "| |";
            this.buttonStop2.UseVisualStyleBackColor = true;
            this.buttonStop2.Click += new System.EventHandler(this.OnStop2Click);
            // 
            // buttonForward2
            // 
            this.buttonForward2.Enabled = false;
            this.buttonForward2.Location = new System.Drawing.Point(193, 48);
            this.buttonForward2.Name = "buttonForward2";
            this.buttonForward2.Size = new System.Drawing.Size(45, 23);
            this.buttonForward2.TabIndex = 6;
            this.buttonForward2.Text = ">>";
            this.buttonForward2.UseVisualStyleBackColor = true;
            this.buttonForward2.Click += new System.EventHandler(this.OnForward2Click);
            // 
            // buttonReverse2
            // 
            this.buttonReverse2.Enabled = false;
            this.buttonReverse2.Location = new System.Drawing.Point(72, 48);
            this.buttonReverse2.Name = "buttonReverse2";
            this.buttonReverse2.Size = new System.Drawing.Size(45, 23);
            this.buttonReverse2.TabIndex = 4;
            this.buttonReverse2.Text = "<<";
            this.buttonReverse2.UseVisualStyleBackColor = true;
            this.buttonReverse2.Click += new System.EventHandler(this.OnReverse2Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 452);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxPlayback);
            this.Controls.Add(this.groupBoxVideo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 490);
            this.Name = "MainForm";
            this.Text = "Video Viewer";
            this.groupBoxVideo.ResumeLayout(false);
            this.groupBoxVideo.PerformLayout();
            this.groupBoxPlayback.ResumeLayout(false);
            this.groupBoxPlayback.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxVideo;
		private System.Windows.Forms.Button buttonSelect2;
		private System.Windows.Forms.Button buttonSelect1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonForward;
		private System.Windows.Forms.Button buttonReverse;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBoxPlayback;
		private System.Windows.Forms.Button buttonMode;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonMode2;
		private System.Windows.Forms.TextBox textBoxTime2;
		private System.Windows.Forms.Button buttonStop2;
		private System.Windows.Forms.Button buttonForward2;
		private System.Windows.Forms.Button buttonReverse2;
		private System.Windows.Forms.CheckBox checkBoxAudio2;
		private System.Windows.Forms.CheckBox checkBoxAudio;
		private System.Windows.Forms.RadioButton radioButtonStopAtEnd;
		private System.Windows.Forms.RadioButton radioButtonSkip;
		private System.Windows.Forms.RadioButton radioButtonNoskip;
		private System.Windows.Forms.CheckBox checkBoxZoom2;
		private System.Windows.Forms.CheckBox checkBoxZoom1;
    }
}

