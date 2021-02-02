namespace ConfigAPIClient.Panels.PropertyUserControls
{
	partial class SeperatorPropertyUserControl
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
            this.button1 = new System.Windows.Forms.Button();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(248, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Select ...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SeperatorPropertyUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelOfProperty);
            this.Name = "SeperatorPropertyUserControl";
            this.Size = new System.Drawing.Size(530, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelOfProperty;
        private System.Windows.Forms.Button button1;
    }
}
