namespace MultiSiteViewer
{
	partial class SiteAddForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.secureOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.radioButtonCurrent = new System.Windows.Forms.RadioButton();
            this.radioButtonBasic = new System.Windows.Forms.RadioButton();
            this.radioButtonAD = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.treeViewSites = new System.Windows.Forms.TreeView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.radioButtonSDKLoadedChildSites = new System.Windows.Forms.RadioButton();
            this.radioButtonSampleLoadedChildSites = new System.Windows.Forms.RadioButton();
            this.radioButtonIncludeNone = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(351, 397);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(81, 58);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(180, 20);
            this.textBoxUsername.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.secureOnlyCheckBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxServer);
            this.groupBox1.Controls.Add(this.radioButtonCurrent);
            this.groupBox1.Controls.Add(this.radioButtonBasic);
            this.groupBox1.Controls.Add(this.radioButtonAD);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxUsername);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server address and credentials";
            // 
            // secureOnlyCheckBox
            // 
            this.secureOnlyCheckBox.AutoSize = true;
            this.secureOnlyCheckBox.Checked = true;
            this.secureOnlyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secureOnlyCheckBox.Location = new System.Drawing.Point(81, 132);
            this.secureOnlyCheckBox.Name = "secureOnlyCheckBox";
            this.secureOnlyCheckBox.Size = new System.Drawing.Size(82, 17);
            this.secureOnlyCheckBox.TabIndex = 12;
            this.secureOnlyCheckBox.Text = "Secure only";
            this.secureOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Server:";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(81, 24);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(180, 20);
            this.textBoxServer.TabIndex = 0;
            // 
            // radioButtonCurrent
            // 
            this.radioButtonCurrent.AutoSize = true;
            this.radioButtonCurrent.Location = new System.Drawing.Point(202, 109);
            this.radioButtonCurrent.Name = "radioButtonCurrent";
            this.radioButtonCurrent.Size = new System.Drawing.Size(59, 17);
            this.radioButtonCurrent.TabIndex = 5;
            this.radioButtonCurrent.Text = "Current";
            this.radioButtonCurrent.UseVisualStyleBackColor = true;
            this.radioButtonCurrent.CheckedChanged += new System.EventHandler(this.radioButtonCurrent_CheckedChanged);
            // 
            // radioButtonBasic
            // 
            this.radioButtonBasic.AutoSize = true;
            this.radioButtonBasic.Location = new System.Drawing.Point(135, 109);
            this.radioButtonBasic.Name = "radioButtonBasic";
            this.radioButtonBasic.Size = new System.Drawing.Size(51, 17);
            this.radioButtonBasic.TabIndex = 4;
            this.radioButtonBasic.Text = "Basic";
            this.radioButtonBasic.UseVisualStyleBackColor = true;
            // 
            // radioButtonAD
            // 
            this.radioButtonAD.AutoSize = true;
            this.radioButtonAD.Checked = true;
            this.radioButtonAD.Location = new System.Drawing.Point(81, 109);
            this.radioButtonAD.Name = "radioButtonAD";
            this.radioButtonAD.Size = new System.Drawing.Size(40, 17);
            this.radioButtonAD.TabIndex = 3;
            this.radioButtonAD.TabStop = true;
            this.radioButtonAD.Text = "AD";
            this.radioButtonAD.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(81, 83);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(180, 20);
            this.textBoxPassword.TabIndex = 2;
            // 
            // buttonValidate
            // 
            this.buttonValidate.Location = new System.Drawing.Point(93, 182);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(180, 23);
            this.buttonValidate.TabIndex = 1;
            this.buttonValidate.Text = "Validate";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // treeViewSites
            // 
            this.treeViewSites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSites.Location = new System.Drawing.Point(13, 211);
            this.treeViewSites.Name = "treeViewSites";
            this.treeViewSites.Size = new System.Drawing.Size(413, 136);
            this.treeViewSites.TabIndex = 2;
            this.treeViewSites.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AfterSelect);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(207, 397);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(132, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Add selected site";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // radioButtonSDKLoadedChildSites
            // 
            this.radioButtonSDKLoadedChildSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSDKLoadedChildSites.AutoSize = true;
            this.radioButtonSDKLoadedChildSites.Location = new System.Drawing.Point(11, 386);
            this.radioButtonSDKLoadedChildSites.Name = "radioButtonSDKLoadedChildSites";
            this.radioButtonSDKLoadedChildSites.Size = new System.Drawing.Size(137, 17);
            this.radioButtonSDKLoadedChildSites.TabIndex = 3;
            this.radioButtonSDKLoadedChildSites.Text = "SDK loads all child sites";
            this.radioButtonSDKLoadedChildSites.UseVisualStyleBackColor = true;
            // 
            // radioButtonSampleLoadedChildSites
            // 
            this.radioButtonSampleLoadedChildSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSampleLoadedChildSites.AutoSize = true;
            this.radioButtonSampleLoadedChildSites.Checked = true;
            this.radioButtonSampleLoadedChildSites.Location = new System.Drawing.Point(11, 363);
            this.radioButtonSampleLoadedChildSites.Name = "radioButtonSampleLoadedChildSites";
            this.radioButtonSampleLoadedChildSites.Size = new System.Drawing.Size(150, 17);
            this.radioButtonSampleLoadedChildSites.TabIndex = 6;
            this.radioButtonSampleLoadedChildSites.TabStop = true;
            this.radioButtonSampleLoadedChildSites.Text = "Sample loads all child sites";
            this.radioButtonSampleLoadedChildSites.UseVisualStyleBackColor = true;
            // 
            // radioButtonIncludeNone
            // 
            this.radioButtonIncludeNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonIncludeNone.AutoSize = true;
            this.radioButtonIncludeNone.Location = new System.Drawing.Point(11, 409);
            this.radioButtonIncludeNone.Name = "radioButtonIncludeNone";
            this.radioButtonIncludeNone.Size = new System.Drawing.Size(149, 17);
            this.radioButtonIncludeNone.TabIndex = 7;
            this.radioButtonIncludeNone.Text = "Do not load any child sites";
            this.radioButtonIncludeNone.UseVisualStyleBackColor = true;
            // 
            // SiteAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 432);
            this.Controls.Add(this.radioButtonIncludeNone);
            this.Controls.Add(this.radioButtonSampleLoadedChildSites);
            this.Controls.Add(this.radioButtonSDKLoadedChildSites);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.treeViewSites);
            this.Controls.Add(this.buttonValidate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.MinimumSize = new System.Drawing.Size(295, 284);
            this.Name = "SiteAddForm";
            this.Text = "Site add form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxUsername;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.RadioButton radioButtonCurrent;
		private System.Windows.Forms.RadioButton radioButtonBasic;
		private System.Windows.Forms.RadioButton radioButtonAD;
		private System.Windows.Forms.Button buttonValidate;
		private System.Windows.Forms.TreeView treeViewSites;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxServer;
		private System.Windows.Forms.RadioButton radioButtonSDKLoadedChildSites;
        private System.Windows.Forms.RadioButton radioButtonSampleLoadedChildSites;
        private System.Windows.Forms.RadioButton radioButtonIncludeNone;
        private System.Windows.Forms.CheckBox secureOnlyCheckBox;
    }
}