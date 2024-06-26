namespace MediaPlaybackViewer
{
	partial class TimeLineUserControl
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
				if (_graphics != null)
				{
					_graphics.Dispose();
					_graphics = null;
				}
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.labelEndTime = new System.Windows.Forms.Label();
			this.labelStartTime = new System.Windows.Forms.Label();
			this.labelCurrent = new System.Windows.Forms.Label();
			this.buttonDump = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox.Location = new System.Drawing.Point(7, 20);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(24, 306);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
			this.pictureBox.MouseLeave += new System.EventHandler(this.OnMouseLeave);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
			// 
			// labelEndTime
			// 
			this.labelEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelEndTime.AutoSize = true;
			this.labelEndTime.Location = new System.Drawing.Point(5, 329);
			this.labelEndTime.Name = "labelEndTime";
			this.labelEndTime.Size = new System.Drawing.Size(26, 13);
			this.labelEndTime.TabIndex = 1;
			this.labelEndTime.Text = "End";
			// 
			// labelStartTime
			// 
			this.labelStartTime.AutoSize = true;
			this.labelStartTime.Location = new System.Drawing.Point(4, 4);
			this.labelStartTime.Name = "labelStartTime";
			this.labelStartTime.Size = new System.Drawing.Size(34, 13);
			this.labelStartTime.TabIndex = 2;
			this.labelStartTime.Text = "Begin";
			// 
			// labelCurrent
			// 
			this.labelCurrent.AutoSize = true;
			this.labelCurrent.Location = new System.Drawing.Point(35, 133);
			this.labelCurrent.Name = "labelCurrent";
			this.labelCurrent.Size = new System.Drawing.Size(0, 13);
			this.labelCurrent.TabIndex = 3;
			// 
			// buttonDump
			// 
			this.buttonDump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDump.Location = new System.Drawing.Point(8, 345);
			this.buttonDump.Name = "buttonDump";
			this.buttonDump.Size = new System.Drawing.Size(75, 45);
			this.buttonDump.TabIndex = 4;
			this.buttonDump.Text = "Show Cache";
			this.buttonDump.UseVisualStyleBackColor = true;
			this.buttonDump.Click += new System.EventHandler(this.OnDump);
			// 
			// TimeLineUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.buttonDump);
			this.Controls.Add(this.labelCurrent);
			this.Controls.Add(this.labelStartTime);
			this.Controls.Add(this.labelEndTime);
			this.Controls.Add(this.pictureBox);
			this.Name = "TimeLineUserControl";
			this.Size = new System.Drawing.Size(90, 397);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label labelEndTime;
		private System.Windows.Forms.Label labelStartTime;
		private System.Windows.Forms.Label labelCurrent;
		private System.Windows.Forms.Button buttonDump;
	}
}
