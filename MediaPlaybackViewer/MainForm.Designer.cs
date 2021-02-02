namespace MediaPlaybackViewer
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
			this.groupBoxPlayback = new System.Windows.Forms.GroupBox();
			this.textBoxAsked = new System.Windows.Forms.TextBox();
			this.button6 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.textBoxTime = new System.Windows.Forms.TextBox();
			this.buttonStop = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.buttonForward = new System.Windows.Forms.Button();
			this.buttonReverse = new System.Windows.Forms.Button();
			this.buttonCamera = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.buttonSelectXPCO = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonSelectServer = new System.Windows.Forms.Button();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.checkBoxFill = new System.Windows.Forms.CheckBox();
			this.checkBoxAspect = new System.Windows.Forms.CheckBox();
			this.timeLineUserControl1 = new MediaPlaybackViewer.TimeLineUserControl();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.groupBoxPlayback.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.pictureBox);
			this.panel1.Location = new System.Drawing.Point(16, 99);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(550, 308); 
			this.panel1.TabIndex = 21;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(547, 308); 
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Resize += new System.EventHandler(this.OnReSizePictureBox);
			// 
			// groupBoxPlayback
			// 
			this.groupBoxPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBoxPlayback.Controls.Add(this.textBoxAsked);
			this.groupBoxPlayback.Controls.Add(this.button6);
			this.groupBoxPlayback.Controls.Add(this.button3);
			this.groupBoxPlayback.Controls.Add(this.button2);
			this.groupBoxPlayback.Controls.Add(this.button1);
			this.groupBoxPlayback.Controls.Add(this.button4);
			this.groupBoxPlayback.Controls.Add(this.textBoxTime);
			this.groupBoxPlayback.Controls.Add(this.buttonStop);
			this.groupBoxPlayback.Controls.Add(this.button5);
			this.groupBoxPlayback.Controls.Add(this.buttonForward);
			this.groupBoxPlayback.Controls.Add(this.buttonReverse);
			this.groupBoxPlayback.Location = new System.Drawing.Point(15, 428); 
			this.groupBoxPlayback.Name = "groupBoxPlayback";
			this.groupBoxPlayback.Size = new System.Drawing.Size(402, 106);
			this.groupBoxPlayback.TabIndex = 22;
			this.groupBoxPlayback.TabStop = false;
			this.groupBoxPlayback.Text = "Playback Control";
			// 
			// textBoxAsked
			// 
			this.textBoxAsked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxAsked.Location = new System.Drawing.Point(121, 76);
			this.textBoxAsked.Name = "textBoxAsked";
			this.textBoxAsked.ReadOnly = true;
			this.textBoxAsked.Size = new System.Drawing.Size(166, 21);
			this.textBoxAsked.TabIndex = 11;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(13, 73);
			this.button6.Name = "button7";
			this.button6.Size = new System.Drawing.Size(75, 23);
			this.button6.TabIndex = 2;
			this.button6.Text = "< Frame";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.OnPreviousFrame);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(314, 76);
			this.button3.Name = "button4";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 10;
			this.button3.Text = "Frame >";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.OnNextFrame);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(314, 17);
			this.button2.Name = "button3";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 8;
			this.button2.Text = "Sequence >";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.OnNextSequence);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(13, 17);
			this.button1.Name = "button2";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "< Sequence";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnPrevSequence);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(13, 46);
			this.button4.Name = "button5";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 1;
			this.button4.Text = "DB Start";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// textBoxTime
			// 
			this.textBoxTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxTime.Location = new System.Drawing.Point(121, 46);
			this.textBoxTime.Name = "textBoxTime";
			this.textBoxTime.ReadOnly = true;
			this.textBoxTime.Size = new System.Drawing.Size(166, 21);
			this.textBoxTime.TabIndex = 7;
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(181, 17);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(45, 23);
			this.buttonStop.TabIndex = 5;
			this.buttonStop.Text = "| |";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.Location = new System.Drawing.Point(314, 46);
			this.button5.Name = "button6";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 9;
			this.button5.Text = "DB End";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.buttonEnd_Click);
			// 
			// buttonForward
			// 
			this.buttonForward.Location = new System.Drawing.Point(242, 17);
			this.buttonForward.Name = "buttonForward";
			this.buttonForward.Size = new System.Drawing.Size(45, 23);
			this.buttonForward.TabIndex = 6;
			this.buttonForward.Text = ">>";
			this.buttonForward.UseVisualStyleBackColor = true;
			this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
			// 
			// buttonReverse
			// 
			this.buttonReverse.Location = new System.Drawing.Point(121, 17);
			this.buttonReverse.Name = "buttonReverse";
			this.buttonReverse.Size = new System.Drawing.Size(45, 23);
			this.buttonReverse.TabIndex = 4;
			this.buttonReverse.Text = "<<";
			this.buttonReverse.UseVisualStyleBackColor = true;
			this.buttonReverse.Click += new System.EventHandler(this.buttonReverse_Click);
			// 
			// buttonCamera
			// 
			this.buttonCamera.Enabled = false;
			this.buttonCamera.Location = new System.Drawing.Point(371, 38); 
			this.buttonCamera.Name = "buttonCamera";
			this.buttonCamera.Size = new System.Drawing.Size(192, 23);
			this.buttonCamera.TabIndex = 23;
			this.buttonCamera.Text = "Select camera...";
			this.buttonCamera.UseVisualStyleBackColor = true;
			this.buttonCamera.Click += new System.EventHandler(this.OnSelectCamera);
			// 
			// button7
			// 
			this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button7.Location = new System.Drawing.Point(578, 511); 
			this.button7.Name = "button8";
			this.button7.Size = new System.Drawing.Size(111, 23);
			this.button7.TabIndex = 26;
			this.button7.Text = "Close";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.OnClose);
            // 
            // buttonSelectXPCO
            // 
            this.buttonSelectXPCO.Enabled = false;
			this.buttonSelectXPCO.Location = new System.Drawing.Point(160, 9); 
			this.buttonSelectXPCO.Name = "buttonSelectXPCO";
			this.buttonSelectXPCO.Size = new System.Drawing.Size(192, 23);
			this.buttonSelectXPCO.TabIndex = 30;
			this.buttonSelectXPCO.Text = "Select database storage... (XML/SCP)";
			this.buttonSelectXPCO.UseVisualStyleBackColor = true;
			this.buttonSelectXPCO.Click += new System.EventHandler(this.SelectXPCOStorageClick);
			// 
			// buttonSelectServer
			// 
			this.buttonSelectServer.Location = new System.Drawing.Point(160, 38); 
			this.buttonSelectServer.Name = "buttonSelectServer";
			this.buttonSelectServer.Size = new System.Drawing.Size(192, 23);
			this.buttonSelectServer.TabIndex = 31;
			this.buttonSelectServer.Text = "Login to server...";
			this.buttonSelectServer.UseVisualStyleBackColor = true;
			this.buttonSelectServer.Click += new System.EventHandler(this.LoginClick);
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(16, 14); 
			this.radioButton1.Name = "radioButton2";
			this.radioButton1.Size = new System.Drawing.Size(105, 17);
			this.radioButton1.TabIndex = 33;
			this.radioButton1.Text = "Open file (XPCO)";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.SourceChanged);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Checked = true;
			this.radioButton2.Location = new System.Drawing.Point(16, 42); 
			this.radioButton2.Name = "radioButton3";
			this.radioButton2.Size = new System.Drawing.Size(104, 17);
			this.radioButton2.TabIndex = 34;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Login to a server";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.SourceChanged);
			// 
			// checkBoxFill
			// 
			this.checkBoxFill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxFill.AutoSize = true;
			this.checkBoxFill.Location = new System.Drawing.Point(578, 43); 
			this.checkBoxFill.Name = "checkBoxFill";
			this.checkBoxFill.Size = new System.Drawing.Size(89, 17);
			this.checkBoxFill.TabIndex = 36;
			this.checkBoxFill.Text = "Fill with black";
			this.checkBoxFill.UseVisualStyleBackColor = true;
			// 
			// checkBoxAspect
			// 
			this.checkBoxAspect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxAspect.AutoSize = true;
			this.checkBoxAspect.Location = new System.Drawing.Point(578, 25); 
			this.checkBoxAspect.Name = "checkBoxAspect";
			this.checkBoxAspect.Size = new System.Drawing.Size(110, 17);
			this.checkBoxAspect.TabIndex = 35;
			this.checkBoxAspect.Text = "Keep Aspect ratio";
			this.checkBoxAspect.UseVisualStyleBackColor = true;
			// 
			// timeLineUserControl1
			// 
			this.timeLineUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.timeLineUserControl1.BackColor = System.Drawing.Color.Transparent;
			this.timeLineUserControl1.Item = null;
			this.timeLineUserControl1.Location = new System.Drawing.Point(589, 81); 
			this.timeLineUserControl1.Name = "timeLineUserControl1";
			this.timeLineUserControl1.Size = new System.Drawing.Size(109, 400);
			this.timeLineUserControl1.TabIndex = 24;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(722, 551); 
			this.Controls.Add(this.checkBoxFill);
			this.Controls.Add(this.checkBoxAspect);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.buttonSelectServer);
			this.Controls.Add(this.buttonSelectXPCO);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.timeLineUserControl1);
			this.Controls.Add(this.buttonCamera);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBoxPlayback);
			this.Name = "MainForm";
			this.Text = "Media Playback Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			this.Resize += new System.EventHandler(this.OnResize);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.groupBoxPlayback.ResumeLayout(false);
			this.groupBoxPlayback.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBoxPlayback;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button buttonForward;
		private System.Windows.Forms.Button buttonReverse;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.TextBox textBoxAsked;
		private System.Windows.Forms.Button buttonCamera;
		private TimeLineUserControl timeLineUserControl1;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button buttonSelectXPCO;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button buttonSelectServer;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.CheckBox checkBoxFill;
		private System.Windows.Forms.CheckBox checkBoxAspect;
	}
}

