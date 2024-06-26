namespace ConfigAPIClient.Panels
{
	partial class TickPropertyUserControl
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
            this.labelOfProperty = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelOfProperty
            // 
            this.labelOfProperty.AutoSize = true;
            this.labelOfProperty.Location = new System.Drawing.Point(10, 10);
            this.labelOfProperty.Name = "labelOfProperty";
            this.labelOfProperty.Size = new System.Drawing.Size(16, 13);
            this.labelOfProperty.TabIndex = 2;
            this.labelOfProperty.Text = "...";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(280, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // TickPropertyUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labelOfProperty);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "TickPropertyUserControl";
            this.Size = new System.Drawing.Size(465, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelOfProperty;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}
