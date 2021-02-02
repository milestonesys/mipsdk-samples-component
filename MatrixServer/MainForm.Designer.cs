namespace MatrixServer
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
			this.buttonClose = new System.Windows.Forms.Button();
			this.labelError = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.matrixViewItem1 = new MatrixServer.MatrixViewItem();
			this.matrixViewItem2 = new MatrixServer.MatrixViewItem();
			this.matrixViewItem3 = new MatrixServer.MatrixViewItem();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(339, 388);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.OnClose);
			// 
			// labelError
			// 
			this.labelError.AutoSize = true;
			this.labelError.Location = new System.Drawing.Point(5, 335);
			this.labelError.Name = "labelError";
			this.labelError.Size = new System.Drawing.Size(0, 13);
			this.labelError.TabIndex = 5;
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(13, 43);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(386, 212);
			this.listBox1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Matrix Commands:";
			// 
			// matrixViewItem1
			// 
			this.matrixViewItem1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.matrixViewItem1.CameraItem = null;
			this.matrixViewItem1.Location = new System.Drawing.Point(16, 272);
			this.matrixViewItem1.Name = "matrixViewItem1";
			this.matrixViewItem1.Size = new System.Drawing.Size(120, 92);
			this.matrixViewItem1.TabIndex = 8;
			this.matrixViewItem1.Load += new System.EventHandler(this.OnLoad);
			// 
			// matrixViewItem2
			// 
			this.matrixViewItem2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.matrixViewItem2.CameraItem = null;
			this.matrixViewItem2.Location = new System.Drawing.Point(153, 272);
			this.matrixViewItem2.Name = "matrixViewItem2";
			this.matrixViewItem2.Size = new System.Drawing.Size(120, 92);
			this.matrixViewItem2.TabIndex = 9;
			// 
			// matrixViewItem3
			// 
			this.matrixViewItem3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.matrixViewItem3.CameraItem = null;
			this.matrixViewItem3.Location = new System.Drawing.Point(285, 272);
			this.matrixViewItem3.Name = "matrixViewItem3";
			this.matrixViewItem3.Size = new System.Drawing.Size(120, 92);
			this.matrixViewItem3.TabIndex = 10;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 425);
			this.Controls.Add(this.matrixViewItem3);
			this.Controls.Add(this.matrixViewItem2);
			this.Controls.Add(this.matrixViewItem1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.labelError);
			this.Controls.Add(this.buttonClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "Matrix Server Application";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label labelError;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label1;
		private MatrixViewItem matrixViewItem1;
		private MatrixViewItem matrixViewItem2;
		private MatrixViewItem matrixViewItem3;
	}
}