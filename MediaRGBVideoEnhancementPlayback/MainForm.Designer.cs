namespace MediaRGBEnhancementPlayback
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
            this.buttonSelectDBCamera = new System.Windows.Forms.Button();
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
            this.buttonCameraPick = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hScrollBarExpose = new System.Windows.Forms.HScrollBar();
            this.hScrollBarOffset = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.vScrollBarB = new System.Windows.Forms.VScrollBar();
            this.vScrollBarG = new System.Windows.Forms.VScrollBar();
            this.vScrollBarR = new System.Windows.Forms.VScrollBar();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
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
            this.panel1.Location = new System.Drawing.Point(16, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 224);
            this.panel1.TabIndex = 21;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(398, 224);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // buttonSelectDBCamera
            // 
            this.buttonSelectDBCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelectDBCamera.Location = new System.Drawing.Point(28, 330);
            this.buttonSelectDBCamera.Name = "buttonSelectDBCamera";
            this.buttonSelectDBCamera.Size = new System.Drawing.Size(168, 23);
            this.buttonSelectDBCamera.TabIndex = 20;
            this.buttonSelectDBCamera.Text = "Select database file...";
            this.buttonSelectDBCamera.UseVisualStyleBackColor = true;
            this.buttonSelectDBCamera.Click += new System.EventHandler(this.OnOpenMediaFiles);
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
            this.groupBoxPlayback.Location = new System.Drawing.Point(15, 392);
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
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(13, 73);
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
            this.button4.Location = new System.Drawing.Point(314, 76);
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
            this.button3.Location = new System.Drawing.Point(314, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Sequence >";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnNextSequence);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "< Sequence";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnPrevSequence);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 46);
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
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(314, 46);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "DB End";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.buttonEnd_Click);
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
            // openFileDialog
            // 
            this.openFileDialog.Filter = "PTZ files (*.PQZ)|*.pqz";
            // 
            // buttonCameraPick
            // 
            this.buttonCameraPick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCameraPick.Enabled = false;
            this.buttonCameraPick.Location = new System.Drawing.Point(29, 359);
            this.buttonCameraPick.Name = "buttonCameraPick";
            this.buttonCameraPick.Size = new System.Drawing.Size(341, 23);
            this.buttonCameraPick.TabIndex = 23;
            this.buttonCameraPick.Text = "Select camera...";
            this.buttonCameraPick.UseVisualStyleBackColor = true;
            this.buttonCameraPick.Click += new System.EventHandler(this.OnSelectCamera);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(451, 475);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(111, 23);
            this.button8.TabIndex = 26;
            this.button8.Text = "Close";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.OnClose);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Expose:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Offset:";
            // 
            // hScrollBarExpose
            // 
            this.hScrollBarExpose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hScrollBarExpose.LargeChange = 1;
            this.hScrollBarExpose.Location = new System.Drawing.Point(136, 288);
            this.hScrollBarExpose.Maximum = 10;
            this.hScrollBarExpose.Minimum = 1;
            this.hScrollBarExpose.Name = "hScrollBarExpose";
            this.hScrollBarExpose.Size = new System.Drawing.Size(172, 17);
            this.hScrollBarExpose.TabIndex = 38;
            this.hScrollBarExpose.Value = 1;
            this.hScrollBarExpose.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
            // 
            // hScrollBarOffset
            // 
            this.hScrollBarOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hScrollBarOffset.Location = new System.Drawing.Point(136, 273);
            this.hScrollBarOffset.Maximum = 255;
            this.hScrollBarOffset.Minimum = -255;
            this.hScrollBarOffset.Name = "hScrollBarOffset";
            this.hScrollBarOffset.Size = new System.Drawing.Size(172, 17);
            this.hScrollBarOffset.TabIndex = 37;
            this.hScrollBarOffset.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(454, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "R   G   B";
            // 
            // vScrollBarB
            // 
            this.vScrollBarB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBarB.Location = new System.Drawing.Point(485, 36);
            this.vScrollBarB.Name = "vScrollBarB";
            this.vScrollBarB.Size = new System.Drawing.Size(17, 224);
            this.vScrollBarB.TabIndex = 35;
            this.vScrollBarB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
            // 
            // vScrollBarG
            // 
            this.vScrollBarG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBarG.Location = new System.Drawing.Point(468, 36);
            this.vScrollBarG.Name = "vScrollBarG";
            this.vScrollBarG.Size = new System.Drawing.Size(17, 224);
            this.vScrollBarG.TabIndex = 34;
            this.vScrollBarG.Value = 50;
            this.vScrollBarG.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
            // 
            // vScrollBarR
            // 
            this.vScrollBarR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBarR.Location = new System.Drawing.Point(451, 36);
            this.vScrollBarR.Name = "vScrollBarR";
            this.vScrollBarR.Size = new System.Drawing.Size(17, 224);
            this.vScrollBarR.TabIndex = 33;
            this.vScrollBarR.Value = 100;
            this.vScrollBarR.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLogin.Location = new System.Drawing.Point(202, 330);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(168, 23);
            this.buttonLogin.TabIndex = 41;
            this.buttonLogin.Text = "Connect to server (login)..";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.OnLogin);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 515);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hScrollBarExpose);
            this.Controls.Add(this.hScrollBarOffset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.vScrollBarB);
            this.Controls.Add(this.vScrollBarG);
            this.Controls.Add(this.vScrollBarR);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.buttonCameraPick);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSelectDBCamera);
            this.Controls.Add(this.groupBoxPlayback);
            this.Name = "MainForm";
            this.Text = "Media RGB Video Enhancement Playback";
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
		private System.Windows.Forms.Button buttonSelectDBCamera;
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
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.TextBox textBoxAsked;
		private System.Windows.Forms.Button buttonCameraPick;
        private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.HScrollBar hScrollBarExpose;
		private System.Windows.Forms.HScrollBar hScrollBarOffset;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.VScrollBar vScrollBarB;
		private System.Windows.Forms.VScrollBar vScrollBarG;
		private System.Windows.Forms.VScrollBar vScrollBarR;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

