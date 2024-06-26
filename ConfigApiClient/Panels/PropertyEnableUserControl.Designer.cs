namespace ConfigAPIClient.Panels
{
	partial class PropertyEnableUserControl
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
			this.EnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.panelContent = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// EnabledCheckBox
			// 
			this.EnabledCheckBox.AutoSize = true;
			this.EnabledCheckBox.Location = new System.Drawing.Point(10, 13);
			this.EnabledCheckBox.Name = "EnabledCheckBox";
			this.EnabledCheckBox.Size = new System.Drawing.Size(15, 14);
			this.EnabledCheckBox.TabIndex = 5;
			this.EnabledCheckBox.UseVisualStyleBackColor = true;
			this.EnabledCheckBox.CheckedChanged += new System.EventHandler(this.OnEnableChanged);
			// 
			// panelContent
			// 
			this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelContent.Location = new System.Drawing.Point(176, 3);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(324, 34);
			this.panelContent.TabIndex = 6;
			// 
			// PropertyEnableUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.panelContent);
			this.Controls.Add(this.EnabledCheckBox);
			this.Name = "PropertyEnableUserControl";
			this.Size = new System.Drawing.Size(500, 40);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox EnabledCheckBox;
		private System.Windows.Forms.Panel panelContent;
	}
}
