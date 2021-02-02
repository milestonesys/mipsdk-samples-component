namespace ExportSample
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDestination = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label10 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.tabPageVideo = new System.Windows.Forms.TabPage();
            this.listBoxCameras = new System.Windows.Forms.ListBox();
            this.buttonRemoveCamera = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonOverlayImage = new System.Windows.Forms.Button();
            this.checkBoxIncludeOverlayImage = new System.Windows.Forms.CheckBox();
            this.comboBoxSampleRate = new System.Windows.Forms.ComboBox();
            this.textBoxVideoFilename = new System.Windows.Forms.TextBox();
            this.comboBoxCodec = new System.Windows.Forms.ComboBox();
            this._liftPrivacyMask = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonMKV = new System.Windows.Forms.RadioButton();
            this.groupBoxDbSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxIncludeBookmark = new System.Windows.Forms.CheckBox();
            this.checkBoxReExport = new System.Windows.Forms.CheckBox();
            this.textBoxEncryptPassword = new System.Windows.Forms.TextBox();
            this.checkBoxEncrypt = new System.Windows.Forms.CheckBox();
            this.checkBoxSign = new System.Windows.Forms.CheckBox();
            this.buttonCameraAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonAVI = new System.Windows.Forms.RadioButton();
            this.checkBoxRelated = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonDB = new System.Windows.Forms.RadioButton();
            this.tabControlExportSource = new System.Windows.Forms.TabControl();
            this.groupBox2.SuspendLayout();
            this.tabPageVideo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxDbSettings.SuspendLayout();
            this.tabControlExportSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(408, 834);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 28);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.OnClose);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(164, 52);
            this.dateTimePickerEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(332, 22);
            this.dateTimePickerEnd.TabIndex = 6;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStart.Location = new System.Drawing.Point(164, 18);
            this.dateTimePickerStart.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(332, 22);
            this.dateTimePickerStart.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Export interval:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateTimePickerEnd);
            this.groupBox2.Controls.Add(this.buttonDestination);
            this.groupBox2.Controls.Add(this.dateTimePickerStart);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(11, 629);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(507, 128);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Selection";
            // 
            // buttonDestination
            // 
            this.buttonDestination.Location = new System.Drawing.Point(164, 89);
            this.buttonDestination.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDestination.Name = "buttonDestination";
            this.buttonDestination.Size = new System.Drawing.Size(333, 28);
            this.buttonDestination.TabIndex = 5;
            this.buttonDestination.Text = "Select...";
            this.buttonDestination.UseVisualStyleBackColor = true;
            this.buttonDestination.Click += new System.EventHandler(this.buttonDestination_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Destination folder:";
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(135, 834);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(100, 28);
            this.buttonExport.TabIndex = 3;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(11, 767);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(507, 28);
            this.progressBar.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(287, 834);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 809);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Export result:";
            // 
            // labelError
            // 
            this.labelError.Location = new System.Drawing.Point(139, 809);
            this.labelError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(373, 16);
            this.labelError.TabIndex = 10;
            // 
            // tabPageVideo
            // 
            this.tabPageVideo.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageVideo.Controls.Add(this.listBoxCameras);
            this.tabPageVideo.Controls.Add(this.buttonRemoveCamera);
            this.tabPageVideo.Controls.Add(this.groupBox3);
            this.tabPageVideo.Controls.Add(this._liftPrivacyMask);
            this.tabPageVideo.Controls.Add(this.label1);
            this.tabPageVideo.Controls.Add(this.radioButtonMKV);
            this.tabPageVideo.Controls.Add(this.groupBoxDbSettings);
            this.tabPageVideo.Controls.Add(this.buttonCameraAdd);
            this.tabPageVideo.Controls.Add(this.label2);
            this.tabPageVideo.Controls.Add(this.radioButtonAVI);
            this.tabPageVideo.Controls.Add(this.checkBoxRelated);
            this.tabPageVideo.Controls.Add(this.label4);
            this.tabPageVideo.Controls.Add(this.radioButtonDB);
            this.tabPageVideo.Location = new System.Drawing.Point(4, 25);
            this.tabPageVideo.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageVideo.Name = "tabPageVideo";
            this.tabPageVideo.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageVideo.Size = new System.Drawing.Size(501, 577);
            this.tabPageVideo.TabIndex = 0;
            this.tabPageVideo.Text = "Export Video";
            // 
            // listBoxCameras
            // 
            this.listBoxCameras.FormattingEnabled = true;
            this.listBoxCameras.ItemHeight = 16;
            this.listBoxCameras.Location = new System.Drawing.Point(178, 38);
            this.listBoxCameras.Name = "listBoxCameras";
            this.listBoxCameras.Size = new System.Drawing.Size(315, 148);
            this.listBoxCameras.TabIndex = 15;
            // 
            // buttonRemoveCamera
            // 
            this.buttonRemoveCamera.Location = new System.Drawing.Point(11, 74);
            this.buttonRemoveCamera.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveCamera.Name = "buttonRemoveCamera";
            this.buttonRemoveCamera.Size = new System.Drawing.Size(138, 28);
            this.buttonRemoveCamera.TabIndex = 14;
            this.buttonRemoveCamera.Text = "Remove Camera";
            this.buttonRemoveCamera.UseVisualStyleBackColor = true;
            this.buttonRemoveCamera.Click += new System.EventHandler(this.buttonRemoveCamera_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonOverlayImage);
            this.groupBox3.Controls.Add(this.checkBoxIncludeOverlayImage);
            this.groupBox3.Controls.Add(this.comboBoxSampleRate);
            this.groupBox3.Controls.Add(this.textBoxVideoFilename);
            this.groupBox3.Controls.Add(this.comboBoxCodec);
            this.groupBox3.Location = new System.Drawing.Point(253, 388);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(240, 181);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // buttonOverlayImage
            // 
            this.buttonOverlayImage.Enabled = false;
            this.buttonOverlayImage.Location = new System.Drawing.Point(39, 142);
            this.buttonOverlayImage.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOverlayImage.Name = "buttonOverlayImage";
            this.buttonOverlayImage.Size = new System.Drawing.Size(187, 28);
            this.buttonOverlayImage.TabIndex = 12;
            this.buttonOverlayImage.Text = "Select...";
            this.buttonOverlayImage.UseVisualStyleBackColor = true;
            this.buttonOverlayImage.Click += new System.EventHandler(this.buttonOverlayImage_Click);
            // 
            // checkBoxIncludeOverlayImage
            // 
            this.checkBoxIncludeOverlayImage.AutoSize = true;
            this.checkBoxIncludeOverlayImage.Enabled = false;
            this.checkBoxIncludeOverlayImage.Location = new System.Drawing.Point(13, 117);
            this.checkBoxIncludeOverlayImage.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxIncludeOverlayImage.Name = "checkBoxIncludeOverlayImage";
            this.checkBoxIncludeOverlayImage.Size = new System.Drawing.Size(167, 21);
            this.checkBoxIncludeOverlayImage.TabIndex = 11;
            this.checkBoxIncludeOverlayImage.Text = "Include overlay image";
            this.checkBoxIncludeOverlayImage.UseVisualStyleBackColor = true;
            // 
            // comboBoxSampleRate
            // 
            this.comboBoxSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampleRate.Enabled = false;
            this.comboBoxSampleRate.FormattingEnabled = true;
            this.comboBoxSampleRate.Items.AddRange(new object[] {
            "8000",
            "16000",
            "44100"});
            this.comboBoxSampleRate.Location = new System.Drawing.Point(13, 85);
            this.comboBoxSampleRate.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSampleRate.Name = "comboBoxSampleRate";
            this.comboBoxSampleRate.Size = new System.Drawing.Size(213, 24);
            this.comboBoxSampleRate.TabIndex = 10;
            // 
            // textBoxVideoFilename
            // 
            this.textBoxVideoFilename.Enabled = false;
            this.textBoxVideoFilename.Location = new System.Drawing.Point(13, 18);
            this.textBoxVideoFilename.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxVideoFilename.Name = "textBoxVideoFilename";
            this.textBoxVideoFilename.Size = new System.Drawing.Size(213, 22);
            this.textBoxVideoFilename.TabIndex = 9;
            this.textBoxVideoFilename.Text = "Type your avi file name here ...";
            // 
            // comboBoxCodec
            // 
            this.comboBoxCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodec.Enabled = false;
            this.comboBoxCodec.FormattingEnabled = true;
            this.comboBoxCodec.Location = new System.Drawing.Point(13, 52);
            this.comboBoxCodec.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCodec.Name = "comboBoxCodec";
            this.comboBoxCodec.Size = new System.Drawing.Size(213, 24);
            this.comboBoxCodec.TabIndex = 10;
            // 
            // _liftPrivacyMask
            // 
            this._liftPrivacyMask.Location = new System.Drawing.Point(13, 540);
            this._liftPrivacyMask.Margin = new System.Windows.Forms.Padding(4);
            this._liftPrivacyMask.Name = "_liftPrivacyMask";
            this._liftPrivacyMask.Size = new System.Drawing.Size(227, 28);
            this._liftPrivacyMask.TabIndex = 7;
            this._liftPrivacyMask.Text = "Lift privacy mask";
            this._liftPrivacyMask.UseVisualStyleBackColor = true;
            this._liftPrivacyMask.Click += new System.EventHandler(this._liftPrivacyMask_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera:";
            // 
            // radioButtonMKV
            // 
            this.radioButtonMKV.AutoSize = true;
            this.radioButtonMKV.Location = new System.Drawing.Point(184, 399);
            this.radioButtonMKV.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonMKV.Name = "radioButtonMKV";
            this.radioButtonMKV.Size = new System.Drawing.Size(58, 21);
            this.radioButtonMKV.TabIndex = 11;
            this.radioButtonMKV.Text = "MKV";
            this.radioButtonMKV.UseVisualStyleBackColor = true;
            this.radioButtonMKV.CheckedChanged += new System.EventHandler(this.radioButtonMKV_CheckedChanged);
            // 
            // groupBoxDbSettings
            // 
            this.groupBoxDbSettings.Controls.Add(this.checkBoxIncludeBookmark);
            this.groupBoxDbSettings.Controls.Add(this.checkBoxReExport);
            this.groupBoxDbSettings.Controls.Add(this.textBoxEncryptPassword);
            this.groupBoxDbSettings.Controls.Add(this.checkBoxEncrypt);
            this.groupBoxDbSettings.Controls.Add(this.checkBoxSign);
            this.groupBoxDbSettings.Location = new System.Drawing.Point(253, 228);
            this.groupBoxDbSettings.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxDbSettings.Name = "groupBoxDbSettings";
            this.groupBoxDbSettings.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxDbSettings.Size = new System.Drawing.Size(240, 159);
            this.groupBoxDbSettings.TabIndex = 12;
            this.groupBoxDbSettings.TabStop = false;
            // 
            // checkBoxIncludeBookmark
            // 
            this.checkBoxIncludeBookmark.AutoSize = true;
            this.checkBoxIncludeBookmark.Location = new System.Drawing.Point(13, 129);
            this.checkBoxIncludeBookmark.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxIncludeBookmark.Name = "checkBoxIncludeBookmark";
            this.checkBoxIncludeBookmark.Size = new System.Drawing.Size(148, 21);
            this.checkBoxIncludeBookmark.TabIndex = 6;
            this.checkBoxIncludeBookmark.Text = "Include bookmarks";
            this.checkBoxIncludeBookmark.UseVisualStyleBackColor = true;
            // 
            // checkBoxReExport
            // 
            this.checkBoxReExport.AutoSize = true;
            this.checkBoxReExport.Location = new System.Drawing.Point(13, 102);
            this.checkBoxReExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxReExport.Name = "checkBoxReExport";
            this.checkBoxReExport.Size = new System.Drawing.Size(140, 21);
            this.checkBoxReExport.TabIndex = 5;
            this.checkBoxReExport.Text = "Prevent re-export";
            this.checkBoxReExport.UseVisualStyleBackColor = true;
            // 
            // textBoxEncryptPassword
            // 
            this.textBoxEncryptPassword.Enabled = false;
            this.textBoxEncryptPassword.Location = new System.Drawing.Point(39, 65);
            this.textBoxEncryptPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEncryptPassword.Name = "textBoxEncryptPassword";
            this.textBoxEncryptPassword.Size = new System.Drawing.Size(157, 22);
            this.textBoxEncryptPassword.TabIndex = 4;
            // 
            // checkBoxEncrypt
            // 
            this.checkBoxEncrypt.AutoSize = true;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(13, 42);
            this.checkBoxEncrypt.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(121, 21);
            this.checkBoxEncrypt.TabIndex = 3;
            this.checkBoxEncrypt.Text = "Encrypt export";
            this.checkBoxEncrypt.UseVisualStyleBackColor = true;
            this.checkBoxEncrypt.CheckedChanged += new System.EventHandler(this.OnEncryptChanged);
            // 
            // checkBoxSign
            // 
            this.checkBoxSign.AutoSize = true;
            this.checkBoxSign.Location = new System.Drawing.Point(13, 17);
            this.checkBoxSign.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSign.Name = "checkBoxSign";
            this.checkBoxSign.Size = new System.Drawing.Size(101, 21);
            this.checkBoxSign.TabIndex = 2;
            this.checkBoxSign.Text = "Sign export";
            this.checkBoxSign.UseVisualStyleBackColor = true;
            // 
            // buttonCameraAdd
            // 
            this.buttonCameraAdd.Location = new System.Drawing.Point(11, 38);
            this.buttonCameraAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCameraAdd.Name = "buttonCameraAdd";
            this.buttonCameraAdd.Size = new System.Drawing.Size(138, 28);
            this.buttonCameraAdd.TabIndex = 0;
            this.buttonCameraAdd.Text = "Add Camera";
            this.buttonCameraAdd.UseVisualStyleBackColor = true;
            this.buttonCameraAdd.Click += new System.EventHandler(this.buttonAddCamera_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 202);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Audio:";
            // 
            // radioButtonAVI
            // 
            this.radioButtonAVI.AutoSize = true;
            this.radioButtonAVI.Location = new System.Drawing.Point(121, 399);
            this.radioButtonAVI.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonAVI.Name = "radioButtonAVI";
            this.radioButtonAVI.Size = new System.Drawing.Size(50, 21);
            this.radioButtonAVI.TabIndex = 8;
            this.radioButtonAVI.Text = "AVI";
            this.radioButtonAVI.UseVisualStyleBackColor = true;
            this.radioButtonAVI.CheckedChanged += new System.EventHandler(this.radioButtonAVI_CheckedChanged);
            // 
            // checkBoxRelated
            // 
            this.checkBoxRelated.AutoSize = true;
            this.checkBoxRelated.Location = new System.Drawing.Point(178, 200);
            this.checkBoxRelated.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRelated.Name = "checkBoxRelated";
            this.checkBoxRelated.Size = new System.Drawing.Size(214, 21);
            this.checkBoxRelated.TabIndex = 2;
            this.checkBoxRelated.Text = "Include related audio devices";
            this.checkBoxRelated.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 241);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Export format:";
            // 
            // radioButtonDB
            // 
            this.radioButtonDB.AutoSize = true;
            this.radioButtonDB.Checked = true;
            this.radioButtonDB.Location = new System.Drawing.Point(121, 239);
            this.radioButtonDB.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonDB.Name = "radioButtonDB";
            this.radioButtonDB.Size = new System.Drawing.Size(90, 21);
            this.radioButtonDB.TabIndex = 6;
            this.radioButtonDB.TabStop = true;
            this.radioButtonDB.Text = "Database";
            this.radioButtonDB.UseVisualStyleBackColor = true;
            this.radioButtonDB.CheckedChanged += new System.EventHandler(this.OnDatabaseChanged);
            // 
            // tabControlExportSource
            // 
            this.tabControlExportSource.Controls.Add(this.tabPageVideo);
            this.tabControlExportSource.Location = new System.Drawing.Point(7, 15);
            this.tabControlExportSource.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlExportSource.Name = "tabControlExportSource";
            this.tabControlExportSource.SelectedIndex = 0;
            this.tabControlExportSource.Size = new System.Drawing.Size(509, 606);
            this.tabControlExportSource.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 872);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabControlExportSource);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "ExportSample Application";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageVideo.ResumeLayout(false);
            this.tabPageVideo.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxDbSettings.ResumeLayout(false);
            this.groupBoxDbSettings.PerformLayout();
            this.tabControlExportSource.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonDestination;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonExport;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
		private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.TabPage tabPageVideo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxSampleRate;
        private System.Windows.Forms.TextBox textBoxVideoFilename;
        private System.Windows.Forms.ComboBox comboBoxCodec;
        private System.Windows.Forms.Button _liftPrivacyMask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonMKV;
        private System.Windows.Forms.GroupBox groupBoxDbSettings;
        private System.Windows.Forms.CheckBox checkBoxIncludeBookmark;
        private System.Windows.Forms.CheckBox checkBoxReExport;
        private System.Windows.Forms.TextBox textBoxEncryptPassword;
        private System.Windows.Forms.CheckBox checkBoxEncrypt;
        private System.Windows.Forms.CheckBox checkBoxSign;
        private System.Windows.Forms.Button buttonCameraAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonAVI;
        private System.Windows.Forms.CheckBox checkBoxRelated;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonDB;
        private System.Windows.Forms.TabControl tabControlExportSource;
        private System.Windows.Forms.Button buttonOverlayImage;
        private System.Windows.Forms.CheckBox checkBoxIncludeOverlayImage;
        private System.Windows.Forms.ListBox listBoxCameras;
        private System.Windows.Forms.Button buttonRemoveCamera;
    }
}