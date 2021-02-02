namespace MultiUserEnvironment
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
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.textBoxPasswordMain = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxUserMain = new System.Windows.Forms.TextBox();
            this.textBoxUri = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxUserContext1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panelView = new System.Windows.Forms.Panel();
            this.pictureBoxRC1 = new System.Windows.Forms.PictureBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.radioButtonAD = new System.Windows.Forms.RadioButton();
            this.radioButtonBasic = new System.Windows.Forms.RadioButton();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxUserContext2 = new System.Windows.Forms.GroupBox();
            this.panelVideo2 = new System.Windows.Forms.Panel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxUC2 = new System.Windows.Forms.PictureBox();
            this.buttonSelectCamera2 = new System.Windows.Forms.Button();
            this.radioButtonAD2 = new System.Windows.Forms.RadioButton();
            this.radioButtonBasic2 = new System.Windows.Forms.RadioButton();
            this.textBoxPassword2 = new System.Windows.Forms.TextBox();
            this.textBoxUSerName2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxUserContext1.SuspendLayout();
            this.panelVideo.SuspendLayout();
            this.panelView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRC1)).BeginInit();
            this.groupBoxUserContext2.SuspendLayout();
            this.panelVideo2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUC2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(766, 528);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.OnClose);
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Controls.Add(this.textBoxPasswordMain);
            this.groupBoxServer.Controls.Add(this.buttonSave);
            this.groupBoxServer.Controls.Add(this.label6);
            this.groupBoxServer.Controls.Add(this.textBoxUserMain);
            this.groupBoxServer.Controls.Add(this.textBoxUri);
            this.groupBoxServer.Controls.Add(this.label7);
            this.groupBoxServer.Controls.Add(this.label1);
            this.groupBoxServer.Location = new System.Drawing.Point(13, 13);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(415, 112);
            this.groupBoxServer.TabIndex = 0;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Logon Server";
            // 
            // textBoxPasswordMain
            // 
            this.textBoxPasswordMain.Location = new System.Drawing.Point(121, 77);
            this.textBoxPasswordMain.Name = "textBoxPasswordMain";
            this.textBoxPasswordMain.PasswordChar = '*';
            this.textBoxPasswordMain.Size = new System.Drawing.Size(100, 20);
            this.textBoxPasswordMain.TabIndex = 10;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(337, 25);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(67, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Username:";
            // 
            // textBoxUserMain
            // 
            this.textBoxUserMain.Location = new System.Drawing.Point(121, 52);
            this.textBoxUserMain.Name = "textBoxUserMain";
            this.textBoxUserMain.Size = new System.Drawing.Size(100, 20);
            this.textBoxUserMain.TabIndex = 9;
            // 
            // textBoxUri
            // 
            this.textBoxUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUri.Location = new System.Drawing.Point(122, 26);
            this.textBoxUri.Name = "textBoxUri";
            this.textBoxUri.Size = new System.Drawing.Size(203, 20);
            this.textBoxUri.TabIndex = 1;
            this.textBoxUri.Text = "http://localhost";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server address:";
            // 
            // groupBoxUserContext1
            // 
            this.groupBoxUserContext1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxUserContext1.Controls.Add(this.button2);
            this.groupBoxUserContext1.Controls.Add(this.panelVideo);
            this.groupBoxUserContext1.Controls.Add(this.radioButtonAD);
            this.groupBoxUserContext1.Controls.Add(this.radioButtonBasic);
            this.groupBoxUserContext1.Controls.Add(this.textBoxPassword);
            this.groupBoxUserContext1.Controls.Add(this.button1);
            this.groupBoxUserContext1.Controls.Add(this.label2);
            this.groupBoxUserContext1.Controls.Add(this.textBoxUserName);
            this.groupBoxUserContext1.Controls.Add(this.label3);
            this.groupBoxUserContext1.Enabled = false;
            this.groupBoxUserContext1.Location = new System.Drawing.Point(13, 131);
            this.groupBoxUserContext1.Name = "groupBoxUserContext1";
            this.groupBoxUserContext1.Size = new System.Drawing.Size(415, 383);
            this.groupBoxUserContext1.TabIndex = 1;
            this.groupBoxUserContext1.TabStop = false;
            this.groupBoxUserContext1.Text = "User 1 context";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(165, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Logout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnLogoutUC1);
            // 
            // panelVideo
            // 
            this.panelVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVideo.Controls.Add(this.listBox1);
            this.panelVideo.Controls.Add(this.panelView);
            this.panelVideo.Controls.Add(this.buttonSelect);
            this.panelVideo.Enabled = false;
            this.panelVideo.Location = new System.Drawing.Point(7, 115);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(402, 262);
            this.panelVideo.TabIndex = 7;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(235, 58);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(162, 186);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.OnCameraListSelect1);
            // 
            // panelView
            // 
            this.panelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelView.Controls.Add(this.pictureBoxRC1);
            this.panelView.Location = new System.Drawing.Point(14, 13);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(200, 156);
            this.panelView.TabIndex = 1;
            // 
            // pictureBoxRC1
            // 
            this.pictureBoxRC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxRC1.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRC1.Name = "pictureBoxRC1";
            this.pictureBoxRC1.Size = new System.Drawing.Size(200, 156);
            this.pictureBoxRC1.TabIndex = 0;
            this.pictureBoxRC1.TabStop = false;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelect.Location = new System.Drawing.Point(14, 231);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(200, 23);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Select camera...";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.OnSelectCameraUC1);
            // 
            // radioButtonAD
            // 
            this.radioButtonAD.AutoSize = true;
            this.radioButtonAD.Checked = true;
            this.radioButtonAD.Location = new System.Drawing.Point(216, 44);
            this.radioButtonAD.Name = "radioButtonAD";
            this.radioButtonAD.Size = new System.Drawing.Size(100, 17);
            this.radioButtonAD.TabIndex = 3;
            this.radioButtonAD.TabStop = true;
            this.radioButtonAD.Text = "Active Directory";
            this.radioButtonAD.UseVisualStyleBackColor = true;
            // 
            // radioButtonBasic
            // 
            this.radioButtonBasic.AutoSize = true;
            this.radioButtonBasic.Location = new System.Drawing.Point(216, 20);
            this.radioButtonBasic.Name = "radioButtonBasic";
            this.radioButtonBasic.Size = new System.Drawing.Size(51, 17);
            this.radioButtonBasic.TabIndex = 2;
            this.radioButtonBasic.Text = "Basic";
            this.radioButtonBasic.UseVisualStyleBackColor = true;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(84, 44);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Logon";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnLogonUC1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(84, 19);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(100, 20);
            this.textBoxUserName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password:";
            // 
            // groupBoxUserContext2
            // 
            this.groupBoxUserContext2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUserContext2.Controls.Add(this.button4);
            this.groupBoxUserContext2.Controls.Add(this.panelVideo2);
            this.groupBoxUserContext2.Controls.Add(this.radioButtonAD2);
            this.groupBoxUserContext2.Controls.Add(this.radioButtonBasic2);
            this.groupBoxUserContext2.Controls.Add(this.textBoxPassword2);
            this.groupBoxUserContext2.Controls.Add(this.textBoxUSerName2);
            this.groupBoxUserContext2.Controls.Add(this.button3);
            this.groupBoxUserContext2.Controls.Add(this.label4);
            this.groupBoxUserContext2.Controls.Add(this.label5);
            this.groupBoxUserContext2.Enabled = false;
            this.groupBoxUserContext2.Location = new System.Drawing.Point(434, 131);
            this.groupBoxUserContext2.Name = "groupBoxUserContext2";
            this.groupBoxUserContext2.Size = new System.Drawing.Size(415, 383);
            this.groupBoxUserContext2.TabIndex = 2;
            this.groupBoxUserContext2.TabStop = false;
            this.groupBoxUserContext2.Text = "User 2 context";
            // 
            // panelVideo2
            // 
            this.panelVideo2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVideo2.Controls.Add(this.listBox2);
            this.panelVideo2.Controls.Add(this.panel2);
            this.panelVideo2.Controls.Add(this.buttonSelectCamera2);
            this.panelVideo2.Enabled = false;
            this.panelVideo2.Location = new System.Drawing.Point(7, 115);
            this.panelVideo2.Name = "panelVideo2";
            this.panelVideo2.Size = new System.Drawing.Size(402, 262);
            this.panelVideo2.TabIndex = 7;
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(234, 58);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(162, 186);
            this.listBox2.TabIndex = 1;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.OnCameraListSelect2);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pictureBoxUC2);
            this.panel2.Location = new System.Drawing.Point(14, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 156);
            this.panel2.TabIndex = 1;
            // 
            // pictureBoxUC2
            // 
            this.pictureBoxUC2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxUC2.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxUC2.Name = "pictureBoxUC2";
            this.pictureBoxUC2.Size = new System.Drawing.Size(200, 156);
            this.pictureBoxUC2.TabIndex = 0;
            this.pictureBoxUC2.TabStop = false;
            // 
            // buttonSelectCamera2
            // 
            this.buttonSelectCamera2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelectCamera2.Location = new System.Drawing.Point(14, 231);
            this.buttonSelectCamera2.Name = "buttonSelectCamera2";
            this.buttonSelectCamera2.Size = new System.Drawing.Size(200, 23);
            this.buttonSelectCamera2.TabIndex = 0;
            this.buttonSelectCamera2.Text = "Select camera...";
            this.buttonSelectCamera2.UseVisualStyleBackColor = true;
            this.buttonSelectCamera2.Click += new System.EventHandler(this.OnSelectCameraUC2);
            // 
            // radioButtonAD2
            // 
            this.radioButtonAD2.AutoSize = true;
            this.radioButtonAD2.Location = new System.Drawing.Point(216, 44);
            this.radioButtonAD2.Name = "radioButtonAD2";
            this.radioButtonAD2.Size = new System.Drawing.Size(100, 17);
            this.radioButtonAD2.TabIndex = 3;
            this.radioButtonAD2.Text = "Active Directory";
            this.radioButtonAD2.UseVisualStyleBackColor = true;
            // 
            // radioButtonBasic2
            // 
            this.radioButtonBasic2.AutoSize = true;
            this.radioButtonBasic2.Checked = true;
            this.radioButtonBasic2.Location = new System.Drawing.Point(216, 20);
            this.radioButtonBasic2.Name = "radioButtonBasic2";
            this.radioButtonBasic2.Size = new System.Drawing.Size(51, 17);
            this.radioButtonBasic2.TabIndex = 2;
            this.radioButtonBasic2.TabStop = true;
            this.radioButtonBasic2.Text = "Basic";
            this.radioButtonBasic2.UseVisualStyleBackColor = true;
            // 
            // textBoxPassword2
            // 
            this.textBoxPassword2.Location = new System.Drawing.Point(84, 44);
            this.textBoxPassword2.Name = "textBoxPassword2";
            this.textBoxPassword2.PasswordChar = '*';
            this.textBoxPassword2.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword2.TabIndex = 1;
            this.textBoxPassword2.Text = "admin";
            // 
            // textBoxUSerName2
            // 
            this.textBoxUSerName2.Location = new System.Drawing.Point(84, 19);
            this.textBoxUSerName2.Name = "textBoxUSerName2";
            this.textBoxUSerName2.Size = new System.Drawing.Size(100, 20);
            this.textBoxUSerName2.TabIndex = 0;
            this.textBoxUSerName2.Text = "admin";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(84, 73);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Logon";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnLogonUC2);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Username:";
            // 
            // listBox3
            // 
            this.listBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(458, 13);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(379, 108);
            this.listBox3.TabIndex = 4;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(165, 73);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Logout";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnLogoutUC2);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 563);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.groupBoxUserContext2);
            this.Controls.Add(this.groupBoxUserContext1);
            this.Controls.Add(this.groupBoxServer);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Multi-User Environment Application";
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxServer.PerformLayout();
            this.groupBoxUserContext1.ResumeLayout(false);
            this.groupBoxUserContext1.PerformLayout();
            this.panelVideo.ResumeLayout(false);
            this.panelView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRC1)).EndInit();
            this.groupBoxUserContext2.ResumeLayout(false);
            this.groupBoxUserContext2.PerformLayout();
            this.panelVideo2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUC2)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.GroupBox groupBoxServer;
		private System.Windows.Forms.TextBox textBoxUri;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.GroupBox groupBoxUserContext1;
		private System.Windows.Forms.RadioButton radioButtonAD;
		private System.Windows.Forms.RadioButton radioButtonBasic;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panelVideo;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.Panel panelView;
		private System.Windows.Forms.PictureBox pictureBoxRC1;
		private System.Windows.Forms.GroupBox groupBoxUserContext2;
		private System.Windows.Forms.Panel panelVideo2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PictureBox pictureBoxUC2;
		private System.Windows.Forms.Button buttonSelectCamera2;
		private System.Windows.Forms.RadioButton radioButtonAD2;
		private System.Windows.Forms.RadioButton radioButtonBasic2;
		private System.Windows.Forms.TextBox textBoxPassword2;
		private System.Windows.Forms.TextBox textBoxUSerName2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox textBoxPasswordMain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxUserMain;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}