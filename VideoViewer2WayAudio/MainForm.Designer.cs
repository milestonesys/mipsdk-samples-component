namespace VideoViewer2WayAudio
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
            this.groupBoxVideo = new System.Windows.Forms.GroupBox();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.comboBoxSampleRate = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Select_WAV_File = new System.Windows.Forms.Button();
            this.rb_from_file = new System.Windows.Forms.RadioButton();
            this.rb_pc_mic = new System.Windows.Forms.RadioButton();
            this.buttonSelect1 = new System.Windows.Forms.Button();
            this.progressBarMeter = new System.Windows.Forms.ProgressBar();
            this.buttonTalk = new System.Windows.Forms.Button();
            this.checkBoxSpeaker = new System.Windows.Forms.CheckBox();
            this.checkBoxAudio = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonReverse = new System.Windows.Forms.Button();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.groupBoxPlayback = new System.Windows.Forms.GroupBox();
            this.buttonMode = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.errorProviderSpeaker = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxVideo.SuspendLayout();
            this.groupBoxPlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSpeaker)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxVideo
            // 
            this.groupBoxVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVideo.Controls.Add(this.labelSampleRate);
            this.groupBoxVideo.Controls.Add(this.comboBoxSampleRate);
            this.groupBoxVideo.Controls.Add(this.textBox1);
            this.groupBoxVideo.Controls.Add(this.btn_Select_WAV_File);
            this.groupBoxVideo.Controls.Add(this.rb_from_file);
            this.groupBoxVideo.Controls.Add(this.rb_pc_mic);
            this.groupBoxVideo.Controls.Add(this.buttonSelect1);
            this.groupBoxVideo.Controls.Add(this.progressBarMeter);
            this.groupBoxVideo.Controls.Add(this.buttonTalk);
            this.groupBoxVideo.Controls.Add(this.checkBoxSpeaker);
            this.groupBoxVideo.Controls.Add(this.checkBoxAudio);
            this.groupBoxVideo.Controls.Add(this.panel1);
            this.groupBoxVideo.Location = new System.Drawing.Point(12, 4);
            this.groupBoxVideo.Name = "groupBoxVideo";
            this.groupBoxVideo.Size = new System.Drawing.Size(517, 471);
            this.groupBoxVideo.TabIndex = 12;
            this.groupBoxVideo.TabStop = false;
            this.groupBoxVideo.Text = "Video";
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.AutoSize = true;
            this.labelSampleRate.Enabled = false;
            this.labelSampleRate.Location = new System.Drawing.Point(280, 385);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(88, 13);
            this.labelSampleRate.TabIndex = 24;
            this.labelSampleRate.Text = "Sample rate (Hz):";
            // 
            // comboBoxSampleRate
            // 
            this.comboBoxSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampleRate.Enabled = false;
            this.comboBoxSampleRate.FormattingEnabled = true;
            this.comboBoxSampleRate.Items.AddRange(new object[] {
            "8000",
            "16000"});
            this.comboBoxSampleRate.Location = new System.Drawing.Point(374, 381);
            this.comboBoxSampleRate.Name = "comboBoxSampleRate";
            this.comboBoxSampleRate.Size = new System.Drawing.Size(64, 21);
            this.comboBoxSampleRate.TabIndex = 23;
            this.comboBoxSampleRate.SelectedValueChanged += new System.EventHandler(this.comboBoxSampleRate_SelectedValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(393, 347);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(45, 20);
            this.textBox1.TabIndex = 22;
            // 
            // btn_Select_WAV_File
            // 
            this.btn_Select_WAV_File.Location = new System.Drawing.Point(182, 416);
            this.btn_Select_WAV_File.Name = "btn_Select_WAV_File";
            this.btn_Select_WAV_File.Size = new System.Drawing.Size(93, 26);
            this.btn_Select_WAV_File.TabIndex = 21;
            this.btn_Select_WAV_File.Text = "Select WAV file";
            this.btn_Select_WAV_File.UseVisualStyleBackColor = true;
            this.btn_Select_WAV_File.Click += new System.EventHandler(this.onBtnClick_Select_WAV_File);
            // 
            // rb_from_file
            // 
            this.rb_from_file.AutoSize = true;
            this.rb_from_file.Location = new System.Drawing.Point(53, 416);
            this.rb_from_file.Name = "rb_from_file";
            this.rb_from_file.Size = new System.Drawing.Size(89, 17);
            this.rb_from_file.TabIndex = 20;
            this.rb_from_file.TabStop = true;
            this.rb_from_file.Text = "from WAV file";
            this.rb_from_file.UseVisualStyleBackColor = true;
            this.rb_from_file.CheckedChanged += new System.EventHandler(this.onCheckedChanged_Radio_button);
            // 
            // rb_pc_mic
            // 
            this.rb_pc_mic.AutoSize = true;
            this.rb_pc_mic.Checked = true;
            this.rb_pc_mic.Location = new System.Drawing.Point(53, 383);
            this.rb_pc_mic.Name = "rb_pc_mic";
            this.rb_pc_mic.Size = new System.Drawing.Size(121, 17);
            this.rb_pc_mic.TabIndex = 19;
            this.rb_pc_mic.TabStop = true;
            this.rb_pc_mic.Text = "from PC Microphone";
            this.rb_pc_mic.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.rb_pc_mic.UseVisualStyleBackColor = true;
            this.rb_pc_mic.CheckedChanged += new System.EventHandler(this.onCheckedChanged_Radio_button);
            // 
            // buttonSelect1
            // 
            this.buttonSelect1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect1.Location = new System.Drawing.Point(66, 276);
            this.buttonSelect1.Name = "buttonSelect1";
            this.buttonSelect1.Size = new System.Drawing.Size(372, 23);
            this.buttonSelect1.TabIndex = 0;
            this.buttonSelect1.Text = "Select camera...";
            this.buttonSelect1.UseVisualStyleBackColor = true;
            this.buttonSelect1.Click += new System.EventHandler(this.OnSelect1Click);
            // 
            // progressBarMeter
            // 
            this.progressBarMeter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBarMeter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarMeter.Location = new System.Drawing.Point(182, 347);
            this.progressBarMeter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.progressBarMeter.Name = "progressBarMeter";
            this.progressBarMeter.Size = new System.Drawing.Size(192, 24);
            this.progressBarMeter.Step = 2;
            this.progressBarMeter.TabIndex = 18;
            this.progressBarMeter.Visible = false;
            // 
            // buttonTalk
            // 
            this.buttonTalk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTalk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonTalk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonTalk.Location = new System.Drawing.Point(182, 377);
            this.buttonTalk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonTalk.Name = "buttonTalk";
            this.buttonTalk.Size = new System.Drawing.Size(93, 26);
            this.buttonTalk.TabIndex = 17;
            this.buttonTalk.Text = "Talk";
            this.buttonTalk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDownTalk);
            this.buttonTalk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUpTalk);
            // 
            // checkBoxSpeaker
            // 
            this.checkBoxSpeaker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSpeaker.AutoSize = true;
            this.checkBoxSpeaker.Enabled = false;
            this.checkBoxSpeaker.Location = new System.Drawing.Point(27, 347);
            this.checkBoxSpeaker.Name = "checkBoxSpeaker";
            this.checkBoxSpeaker.Size = new System.Drawing.Size(141, 17);
            this.checkBoxSpeaker.TabIndex = 6;
            this.checkBoxSpeaker.Text = "Enable Camera Speaker";
            this.checkBoxSpeaker.UseVisualStyleBackColor = true;
            this.checkBoxSpeaker.CheckedChanged += new System.EventHandler(this.onCheckedChanged_Speaker);
            // 
            // checkBoxAudio
            // 
            this.checkBoxAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAudio.AutoSize = true;
            this.checkBoxAudio.Enabled = false;
            this.checkBoxAudio.Location = new System.Drawing.Point(26, 324);
            this.checkBoxAudio.Name = "checkBoxAudio";
            this.checkBoxAudio.Size = new System.Drawing.Size(157, 17);
            this.checkBoxAudio.TabIndex = 5;
            this.checkBoxAudio.Text = "Enable Camera Microphone";
            this.checkBoxAudio.UseVisualStyleBackColor = true;
            this.checkBoxAudio.CheckedChanged += new System.EventHandler(this.OnAudio1CheckChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(63, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 250);
            this.panel1.TabIndex = 1;
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(182, 48);
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
            this.buttonForward.Location = new System.Drawing.Point(243, 48);
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
            this.buttonReverse.Location = new System.Drawing.Point(122, 48);
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
            this.textBoxTime.Location = new System.Drawing.Point(122, 77);
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
            this.groupBoxPlayback.Location = new System.Drawing.Point(12, 481);
            this.groupBoxPlayback.Name = "groupBoxPlayback";
            this.groupBoxPlayback.Size = new System.Drawing.Size(517, 103);
            this.groupBoxPlayback.TabIndex = 19;
            this.groupBoxPlayback.TabStop = false;
            this.groupBoxPlayback.Text = "Playback Control";
            // 
            // buttonMode
            // 
            this.buttonMode.Location = new System.Drawing.Point(123, 19);
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
            this.buttonOK.Location = new System.Drawing.Point(442, 594);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 23);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnClose);
            // 
            // errorProviderSpeaker
            // 
            this.errorProviderSpeaker.ContainerControl = this;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 629);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxPlayback);
            this.Controls.Add(this.groupBoxVideo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(350, 490);
            this.Name = "MainForm";
            this.Text = "Video Viewer with 2-way audio";
            this.groupBoxVideo.ResumeLayout(false);
            this.groupBoxVideo.PerformLayout();
            this.groupBoxPlayback.ResumeLayout(false);
            this.groupBoxPlayback.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSpeaker)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxVideo;
		private System.Windows.Forms.Button buttonSelect1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonForward;
		private System.Windows.Forms.Button buttonReverse;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.GroupBox groupBoxPlayback;
		private System.Windows.Forms.Button buttonMode;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkBoxAudio;
		private System.Windows.Forms.CheckBox checkBoxSpeaker;
		private System.Windows.Forms.ProgressBar progressBarMeter;
		private System.Windows.Forms.Button buttonTalk;
		private System.Windows.Forms.ErrorProvider errorProviderSpeaker;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton rb_pc_mic;
        private System.Windows.Forms.RadioButton rb_from_file;
        private System.Windows.Forms.Button btn_Select_WAV_File;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBoxSampleRate;
        private System.Windows.Forms.Label labelSampleRate;
	}
}

