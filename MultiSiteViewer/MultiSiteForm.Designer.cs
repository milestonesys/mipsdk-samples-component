namespace MultiSiteViewer
{
	partial class MultiSiteForm
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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSelect1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonSelect2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeView1.Location = new System.Drawing.Point(13, 40);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(331, 425);
			this.treeView1.TabIndex = 0;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(557, 482);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAdd.Location = new System.Drawing.Point(13, 482);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(106, 23);
			this.buttonAdd.TabIndex = 2;
			this.buttonAdd.Text = "Add Site";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRemove.Location = new System.Drawing.Point(232, 482);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(112, 23);
			this.buttonRemove.TabIndex = 3;
			this.buttonRemove.Text = "Remove Site";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.ButtonRemoveClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Current Available Configuration:";
			// 
			// buttonSelect1
			// 
			this.buttonSelect1.Location = new System.Drawing.Point(403, 40);
			this.buttonSelect1.Name = "buttonSelect1";
			this.buttonSelect1.Size = new System.Drawing.Size(229, 23);
			this.buttonSelect1.TabIndex = 5;
			this.buttonSelect1.Text = "Select camera ...";
			this.buttonSelect1.UseVisualStyleBackColor = true;
			this.buttonSelect1.Click += new System.EventHandler(this.ButtonSelect1Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Black;
			this.panel1.Location = new System.Drawing.Point(403, 70);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(229, 161);
			this.panel1.TabIndex = 6;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Black;
			this.panel2.Location = new System.Drawing.Point(403, 304);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(229, 161);
			this.panel2.TabIndex = 8;
			// 
			// buttonSelect2
			// 
			this.buttonSelect2.Location = new System.Drawing.Point(403, 274);
			this.buttonSelect2.Name = "buttonSelect2";
			this.buttonSelect2.Size = new System.Drawing.Size(229, 23);
			this.buttonSelect2.TabIndex = 7;
			this.buttonSelect2.Text = "Select camera ...";
			this.buttonSelect2.UseVisualStyleBackColor = true;
			this.buttonSelect2.Click += new System.EventHandler(this.ButtonSelect2Click);
			// 
			// MultiSiteForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(669, 517);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.buttonSelect2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonSelect1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.treeView1);
			this.Name = "MultiSiteForm";
			this.Text = "Multi Site Viewer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonSelect1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonSelect2;
	}
}

