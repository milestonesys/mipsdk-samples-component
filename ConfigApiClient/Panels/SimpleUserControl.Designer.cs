namespace ConfigAPIClient.Panels
{
	partial class SimpleUserControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panelId = new System.Windows.Forms.Panel();
            this.EnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scrollPanel1 = new ConfigAPIClient.Panels.ScrollPanel();
            this.panelId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelId
            // 
            this.panelId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelId.Controls.Add(this.EnabledCheckBox);
            this.panelId.Controls.Add(this.pictureBox1);
            this.panelId.Controls.Add(this.textBoxName);
            this.panelId.Controls.Add(this.label1);
            this.panelId.Controls.Add(this.panel2);
            this.panelId.Location = new System.Drawing.Point(5, 5);
            this.panelId.Name = "panelId";
            this.panelId.Size = new System.Drawing.Size(507, 62);
            this.panelId.TabIndex = 0;
            // 
            // EnabledCheckBox
            // 
            this.EnabledCheckBox.AutoSize = true;
            this.EnabledCheckBox.Location = new System.Drawing.Point(10, 37);
            this.EnabledCheckBox.Name = "EnabledCheckBox";
            this.EnabledCheckBox.Size = new System.Drawing.Size(15, 14);
            this.EnabledCheckBox.TabIndex = 3;
            this.EnabledCheckBox.UseVisualStyleBackColor = true;
            this.EnabledCheckBox.CheckedChanged += new System.EventHandler(this.OnEnabledChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.ForeColor = System.Drawing.Color.Black;
            this.textBoxName.Location = new System.Drawing.Point(94, 11);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(314, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(36, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Navy;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 1);
            this.panel2.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.Controls.Add(this.buttonSave);
            this.panelBottom.Controls.Add(this.panel1);
            this.panelBottom.Location = new System.Drawing.Point(0, 364);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(514, 55);
            this.panelBottom.TabIndex = 3;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Enabled = false;
            this.buttonSave.ForeColor = System.Drawing.Color.Black;
            this.buttonSave.Location = new System.Drawing.Point(411, 18);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.OnSave);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 1);
            this.panel1.TabIndex = 1;
            // 
            // scrollPanel1
            // 
            this.scrollPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.scrollPanel1.ForeColor = System.Drawing.Color.Black;
            this.scrollPanel1.Location = new System.Drawing.Point(4, 74);
            this.scrollPanel1.Name = "scrollPanel1";
            this.scrollPanel1.Size = new System.Drawing.Size(507, 284);
            this.scrollPanel1.TabIndex = 4;
            // 
            // SimpleUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.scrollPanel1);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelId);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "SimpleUserControl";
            this.Size = new System.Drawing.Size(514, 419);
            this.panelId.ResumeLayout(false);
            this.panelId.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonSave;
		private ScrollPanel scrollPanel1;
		private System.Windows.Forms.CheckBox EnabledCheckBox;
	}
}
