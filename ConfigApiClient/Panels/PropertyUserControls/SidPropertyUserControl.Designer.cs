namespace ConfigAPIClient.Panels.PropertyUserControls
{
    partial class SidPropertyUserControl
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.radioButtonBasic = new System.Windows.Forms.RadioButton();
            this.radioButtonADUser = new System.Windows.Forms.RadioButton();
            this.buttonAdSelect = new System.Windows.Forms.Button();
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
            this.labelOfProperty.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this.labelOfProperty.MouseHover += new System.EventHandler(this.OnMouseHover);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(280, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(236, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // radioButtonBasic
            // 
            this.radioButtonBasic.AutoSize = true;
            this.radioButtonBasic.Location = new System.Drawing.Point(157, 8);
            this.radioButtonBasic.Name = "radioButtonBasic";
            this.radioButtonBasic.Size = new System.Drawing.Size(76, 17);
            this.radioButtonBasic.TabIndex = 4;
            this.radioButtonBasic.TabStop = true;
            this.radioButtonBasic.Text = "Basic User";
            this.radioButtonBasic.UseVisualStyleBackColor = true;
            this.radioButtonBasic.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // radioButtonADUser
            // 
            this.radioButtonADUser.AutoSize = true;
            this.radioButtonADUser.Location = new System.Drawing.Point(157, 31);
            this.radioButtonADUser.Name = "radioButtonADUser";
            this.radioButtonADUser.Size = new System.Drawing.Size(65, 17);
            this.radioButtonADUser.TabIndex = 5;
            this.radioButtonADUser.TabStop = true;
            this.radioButtonADUser.Text = "AD-User";
            this.radioButtonADUser.UseVisualStyleBackColor = true;
            this.radioButtonADUser.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // buttonAdSelect
            // 
            this.buttonAdSelect.Location = new System.Drawing.Point(280, 34);
            this.buttonAdSelect.Name = "buttonAdSelect";
            this.buttonAdSelect.Size = new System.Drawing.Size(236, 23);
            this.buttonAdSelect.TabIndex = 6;
            this.buttonAdSelect.Text = "...";
            this.buttonAdSelect.UseVisualStyleBackColor = true;
            this.buttonAdSelect.Click += new System.EventHandler(this.OnAdClick);
            // 
            // BasicSidPropertyUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.buttonAdSelect);
            this.Controls.Add(this.radioButtonADUser);
            this.Controls.Add(this.radioButtonBasic);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.labelOfProperty);
            this.Name = "BasicSidPropertyUserControl";
            this.Size = new System.Drawing.Size(530, 65);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelOfProperty;
		private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton radioButtonBasic;
        private System.Windows.Forms.RadioButton radioButtonADUser;
        private System.Windows.Forms.Button buttonAdSelect;
	}
}
