namespace MediaRGBVideoEnhancementLive
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
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.hScrollBarExpose = new System.Windows.Forms.HScrollBar();
			this.hScrollBarOffset = new System.Windows.Forms.HScrollBar();
			this.label2 = new System.Windows.Forms.Label();
			this.vScrollBarB = new System.Windows.Forms.VScrollBar();
			this.vScrollBarG = new System.Windows.Forms.VScrollBar();
			this.vScrollBarR = new System.Windows.Forms.VScrollBar();
			this.labelCount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.pictureBoxEnhanced = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonSelect1 = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonRestart = new System.Windows.Forms.Button();
			this.groupBoxVideo.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnhanced)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxVideo
			// 
			this.groupBoxVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxVideo.Controls.Add(this.label4);
			this.groupBoxVideo.Controls.Add(this.label3);
			this.groupBoxVideo.Controls.Add(this.hScrollBarExpose);
			this.groupBoxVideo.Controls.Add(this.hScrollBarOffset);
			this.groupBoxVideo.Controls.Add(this.label2);
			this.groupBoxVideo.Controls.Add(this.vScrollBarB);
			this.groupBoxVideo.Controls.Add(this.vScrollBarG);
			this.groupBoxVideo.Controls.Add(this.vScrollBarR);
			this.groupBoxVideo.Controls.Add(this.labelCount);
			this.groupBoxVideo.Controls.Add(this.label1);
			this.groupBoxVideo.Controls.Add(this.panel2);
			this.groupBoxVideo.Controls.Add(this.panel1);
			this.groupBoxVideo.Controls.Add(this.buttonSelect1);
			this.groupBoxVideo.Location = new System.Drawing.Point(12, 4);
			this.groupBoxVideo.Name = "groupBoxVideo";
			this.groupBoxVideo.Size = new System.Drawing.Size(637, 305);
			this.groupBoxVideo.TabIndex = 12;
			this.groupBoxVideo.TabStop = false;
			this.groupBoxVideo.Text = "Video";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(317, 276);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Expose:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(318, 260);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Offset:";
			// 
			// hScrollBarExpose
			// 
			this.hScrollBarExpose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBarExpose.LargeChange = 1;
			this.hScrollBarExpose.Location = new System.Drawing.Point(388, 274);
			this.hScrollBarExpose.Maximum = 10;
			this.hScrollBarExpose.Minimum = 1;
			this.hScrollBarExpose.Name = "hScrollBarExpose";
			this.hScrollBarExpose.Size = new System.Drawing.Size(172, 17);
			this.hScrollBarExpose.TabIndex = 11;
			this.hScrollBarExpose.Value = 1;
			this.hScrollBarExpose.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
			// 
			// hScrollBarOffset
			// 
			this.hScrollBarOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBarOffset.Location = new System.Drawing.Point(388, 257);
			this.hScrollBarOffset.Maximum = 255;
			this.hScrollBarOffset.Minimum = -255;
			this.hScrollBarOffset.Name = "hScrollBarOffset";
			this.hScrollBarOffset.Size = new System.Drawing.Size(172, 17);
			this.hScrollBarOffset.TabIndex = 10;
			this.hScrollBarOffset.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(565, 232);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "R   G   B";
			// 
			// vScrollBarB
			// 
			this.vScrollBarB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBarB.Location = new System.Drawing.Point(597, 38);
			this.vScrollBarB.Name = "vScrollBarB";
			this.vScrollBarB.Size = new System.Drawing.Size(17, 183);
			this.vScrollBarB.TabIndex = 8;
			this.vScrollBarB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
			// 
			// vScrollBarG
			// 
			this.vScrollBarG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBarG.Location = new System.Drawing.Point(580, 38);
			this.vScrollBarG.Name = "vScrollBarG";
			this.vScrollBarG.Size = new System.Drawing.Size(17, 183);
			this.vScrollBarG.TabIndex = 7;
			this.vScrollBarG.Value = 50;
			this.vScrollBarG.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
			// 
			// vScrollBarR
			// 
			this.vScrollBarR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBarR.Location = new System.Drawing.Point(563, 38);
			this.vScrollBarR.Name = "vScrollBarR";
			this.vScrollBarR.Size = new System.Drawing.Size(17, 183);
			this.vScrollBarR.TabIndex = 6;
			this.vScrollBarR.Value = 100;
			this.vScrollBarR.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrollChange);
			// 
			// labelCount
			// 
			this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelCount.AutoSize = true;
			this.labelCount.Location = new System.Drawing.Point(439, 232);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new System.Drawing.Size(13, 13);
			this.labelCount.TabIndex = 5;
			this.labelCount.Text = "..";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(368, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Frame count:";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.Black;
			this.panel2.Controls.Add(this.pictureBoxEnhanced);
			this.panel2.Location = new System.Drawing.Point(320, 38);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(240, 183);
			this.panel2.TabIndex = 3;
			// 
			// pictureBoxEnhanced
			// 
			this.pictureBoxEnhanced.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxEnhanced.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxEnhanced.Name = "pictureBoxEnhanced";
			this.pictureBoxEnhanced.Size = new System.Drawing.Size(240, 183);
			this.pictureBoxEnhanced.TabIndex = 0;
			this.pictureBoxEnhanced.TabStop = false;
			this.pictureBoxEnhanced.Resize += new System.EventHandler(this.OnResizePictureBox);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.BackColor = System.Drawing.Color.Black;
			this.panel1.Location = new System.Drawing.Point(19, 20);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(271, 201);
			this.panel1.TabIndex = 1;
			// 
			// buttonSelect1
			// 
			this.buttonSelect1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonSelect1.Location = new System.Drawing.Point(19, 227);
			this.buttonSelect1.Name = "buttonSelect1";
			this.buttonSelect1.Size = new System.Drawing.Size(271, 23);
			this.buttonSelect1.TabIndex = 0;
			this.buttonSelect1.Text = "Select camera...";
			this.buttonSelect1.UseVisualStyleBackColor = true;
			this.buttonSelect1.Click += new System.EventHandler(this.OnSelect);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(562, 336);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(87, 23);
			this.buttonOK.TabIndex = 20;
			this.buttonOK.Text = "Close";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.OnClose);
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonStop.Enabled = false;
			this.buttonStop.Location = new System.Drawing.Point(112, 336);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 21;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.OnStop);
			// 
			// buttonRestart
			// 
			this.buttonRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRestart.Enabled = false;
			this.buttonRestart.Location = new System.Drawing.Point(31, 336);
			this.buttonRestart.Name = "buttonRestart";
			this.buttonRestart.Size = new System.Drawing.Size(75, 23);
			this.buttonRestart.TabIndex = 22;
			this.buttonRestart.Text = "Restart";
			this.buttonRestart.UseVisualStyleBackColor = true;
			this.buttonRestart.Click += new System.EventHandler(this.OnRestart);
			// 
			// MainForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 371);
			this.Controls.Add(this.buttonRestart);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.groupBoxVideo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "MainForm";
			this.Text = "Toolkit RGB VideoEnhancement";
			this.groupBoxVideo.ResumeLayout(false);
			this.groupBoxVideo.PerformLayout();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnhanced)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxVideo;
		private System.Windows.Forms.Button buttonSelect1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label labelCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBoxEnhanced;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonRestart;
		private System.Windows.Forms.VScrollBar vScrollBarB;
		private System.Windows.Forms.VScrollBar vScrollBarG;
		private System.Windows.Forms.VScrollBar vScrollBarR;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.HScrollBar hScrollBarExpose;
		private System.Windows.Forms.HScrollBar hScrollBarOffset;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
	}
}

