namespace ConfigAccessViaSDK
{
	partial class ConfigAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigAccess));
            this.buttonOK = new System.Windows.Forms.Button();
            this.dumpConfigurationUserControl1 = new ConfigAccessViaSDK.DumpConfigurationUserControl();
            this.buttonLicense = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonTrigger = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(618, 487);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 23);
            this.buttonOK.TabIndex = 21;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnClose);
            // 
            // dumpConfigurationUserControl1
            // 
            this.dumpConfigurationUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpConfigurationUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dumpConfigurationUserControl1.Location = new System.Drawing.Point(4, 4);
            this.dumpConfigurationUserControl1.MinimumSize = new System.Drawing.Size(300, 200);
            this.dumpConfigurationUserControl1.Name = "dumpConfigurationUserControl1";
            this.dumpConfigurationUserControl1.Size = new System.Drawing.Size(701, 456);
            this.dumpConfigurationUserControl1.TabIndex = 5;
            // 
            // buttonLicense
            // 
            this.buttonLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLicense.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonLicense.Location = new System.Drawing.Point(486, 487);
            this.buttonLicense.Name = "buttonLicense";
            this.buttonLicense.Size = new System.Drawing.Size(110, 23);
            this.buttonLicense.TabIndex = 22;
            this.buttonLicense.Text = "Show License";
            this.buttonLicense.UseVisualStyleBackColor = true;
            this.buttonLicense.Click += new System.EventHandler(this.ShowLicense);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 376);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(113, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "Physical Hierarchy";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.OnHierarchyChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(355, 487);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(262, 487);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Logout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonTrigger
            // 
            this.buttonTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTrigger.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonTrigger.Location = new System.Drawing.Point(13, 487);
            this.buttonTrigger.Name = "buttonTrigger";
            this.buttonTrigger.Size = new System.Drawing.Size(87, 23);
            this.buttonTrigger.TabIndex = 26;
            this.buttonTrigger.Text = "Trigger";
            this.buttonTrigger.UseVisualStyleBackColor = true;
            this.buttonTrigger.Click += new System.EventHandler(this.buttonTrigger_Click);
            // 
            // ConfigAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 522);
            this.Controls.Add(this.buttonTrigger);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonLicense);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dumpConfigurationUserControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "ConfigAccess";
            this.Text = "ConfigAccess";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private DumpConfigurationUserControl dumpConfigurationUserControl1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonLicense;
		private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonTrigger;
	}
}

