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
            this.components = new System.ComponentModel.Container();
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
            this.resultLabel = new System.Windows.Forms.Label();
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
            this.radioButtonAVI = new System.Windows.Forms.RadioButton();
            this.checkBoxRelated = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonDB = new System.Windows.Forms.RadioButton();
            this.tabControlExportSource = new System.Windows.Forms.TabControl();
            this.resultLabelToolTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.buttonClose.Location = new System.Drawing.Point(306, 678);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.OnClose);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(123, 42);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(250, 20);
            this.dateTimePickerEnd.TabIndex = 6;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStart.Location = new System.Drawing.Point(123, 15);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(250, 20);
            this.dateTimePickerStart.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
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
            this.groupBox2.Location = new System.Drawing.Point(8, 511);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 104);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Selection";
            // 
            // buttonDestination
            // 
            this.buttonDestination.Location = new System.Drawing.Point(123, 72);
            this.buttonDestination.Name = "buttonDestination";
            this.buttonDestination.Size = new System.Drawing.Size(250, 23);
            this.buttonDestination.TabIndex = 5;
            this.buttonDestination.Text = "Select...";
            this.buttonDestination.UseVisualStyleBackColor = true;
            this.buttonDestination.Click += new System.EventHandler(this.buttonDestination_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Destination folder:";
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(101, 678);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 3;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 623);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(380, 23);
            this.progressBar.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(215, 678);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(9, 657);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Export result:";
            // 
            // resultLabel
            // 
            this.resultLabel.Location = new System.Drawing.Point(104, 657);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(280, 13);
            this.resultLabel.TabIndex = 10;
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
            this.tabPageVideo.Controls.Add(this.radioButtonAVI);
            this.tabPageVideo.Controls.Add(this.checkBoxRelated);
            this.tabPageVideo.Controls.Add(this.label4);
            this.tabPageVideo.Controls.Add(this.radioButtonDB);
            this.tabPageVideo.Location = new System.Drawing.Point(4, 22);
            this.tabPageVideo.Name = "tabPageVideo";
            this.tabPageVideo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVideo.Size = new System.Drawing.Size(374, 466);
            this.tabPageVideo.TabIndex = 0;
            this.tabPageVideo.Text = "Export Video";
            // 
            // listBoxCameras
            // 
            this.listBoxCameras.FormattingEnabled = true;
            this.listBoxCameras.Location = new System.Drawing.Point(134, 31);
            this.listBoxCameras.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxCameras.Name = "listBoxCameras";
            this.listBoxCameras.Size = new System.Drawing.Size(237, 121);
            this.listBoxCameras.TabIndex = 15;
            // 
            // buttonRemoveCamera
            // 
            this.buttonRemoveCamera.Location = new System.Drawing.Point(8, 60);
            this.buttonRemoveCamera.Name = "buttonRemoveCamera";
            this.buttonRemoveCamera.Size = new System.Drawing.Size(104, 23);
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
            this.groupBox3.Location = new System.Drawing.Point(190, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(180, 147);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // buttonOverlayImage
            // 
            this.buttonOverlayImage.Enabled = false;
            this.buttonOverlayImage.Location = new System.Drawing.Point(29, 115);
            this.buttonOverlayImage.Name = "buttonOverlayImage";
            this.buttonOverlayImage.Size = new System.Drawing.Size(140, 23);
            this.buttonOverlayImage.TabIndex = 12;
            this.buttonOverlayImage.Text = "Select...";
            this.buttonOverlayImage.UseVisualStyleBackColor = true;
            this.buttonOverlayImage.Click += new System.EventHandler(this.buttonOverlayImage_Click);
            // 
            // checkBoxIncludeOverlayImage
            // 
            this.checkBoxIncludeOverlayImage.AutoSize = true;
            this.checkBoxIncludeOverlayImage.Enabled = false;
            this.checkBoxIncludeOverlayImage.Location = new System.Drawing.Point(10, 95);
            this.checkBoxIncludeOverlayImage.Name = "checkBoxIncludeOverlayImage";
            this.checkBoxIncludeOverlayImage.Size = new System.Drawing.Size(129, 17);
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
            this.comboBoxSampleRate.Location = new System.Drawing.Point(10, 69);
            this.comboBoxSampleRate.Name = "comboBoxSampleRate";
            this.comboBoxSampleRate.Size = new System.Drawing.Size(161, 21);
            this.comboBoxSampleRate.TabIndex = 10;
            // 
            // textBoxVideoFilename
            // 
            this.textBoxVideoFilename.Enabled = false;
            this.textBoxVideoFilename.Location = new System.Drawing.Point(10, 15);
            this.textBoxVideoFilename.Name = "textBoxVideoFilename";
            this.textBoxVideoFilename.Size = new System.Drawing.Size(161, 20);
            this.textBoxVideoFilename.TabIndex = 9;
            this.textBoxVideoFilename.Text = "Type your avi file name here ...";
            // 
            // comboBoxCodec
            // 
            this.comboBoxCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodec.Enabled = false;
            this.comboBoxCodec.FormattingEnabled = true;
            this.comboBoxCodec.Location = new System.Drawing.Point(10, 42);
            this.comboBoxCodec.Name = "comboBoxCodec";
            this.comboBoxCodec.Size = new System.Drawing.Size(161, 21);
            this.comboBoxCodec.TabIndex = 10;
            // 
            // _liftPrivacyMask
            // 
            this._liftPrivacyMask.Location = new System.Drawing.Point(10, 439);
            this._liftPrivacyMask.Name = "_liftPrivacyMask";
            this._liftPrivacyMask.Size = new System.Drawing.Size(170, 23);
            this._liftPrivacyMask.TabIndex = 7;
            this._liftPrivacyMask.Text = "Lift privacy mask";
            this._liftPrivacyMask.UseVisualStyleBackColor = true;
            this._liftPrivacyMask.Click += new System.EventHandler(this._liftPrivacyMask_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera:";
            // 
            // radioButtonMKV
            // 
            this.radioButtonMKV.AutoSize = true;
            this.radioButtonMKV.Location = new System.Drawing.Point(138, 324);
            this.radioButtonMKV.Name = "radioButtonMKV";
            this.radioButtonMKV.Size = new System.Drawing.Size(48, 17);
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
            this.groupBoxDbSettings.Location = new System.Drawing.Point(190, 185);
            this.groupBoxDbSettings.Name = "groupBoxDbSettings";
            this.groupBoxDbSettings.Size = new System.Drawing.Size(180, 129);
            this.groupBoxDbSettings.TabIndex = 12;
            this.groupBoxDbSettings.TabStop = false;
            // 
            // checkBoxIncludeBookmark
            // 
            this.checkBoxIncludeBookmark.AutoSize = true;
            this.checkBoxIncludeBookmark.Location = new System.Drawing.Point(10, 105);
            this.checkBoxIncludeBookmark.Name = "checkBoxIncludeBookmark";
            this.checkBoxIncludeBookmark.Size = new System.Drawing.Size(116, 17);
            this.checkBoxIncludeBookmark.TabIndex = 6;
            this.checkBoxIncludeBookmark.Text = "Include bookmarks";
            this.checkBoxIncludeBookmark.UseVisualStyleBackColor = true;
            // 
            // checkBoxReExport
            // 
            this.checkBoxReExport.AutoSize = true;
            this.checkBoxReExport.Location = new System.Drawing.Point(10, 83);
            this.checkBoxReExport.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxReExport.Name = "checkBoxReExport";
            this.checkBoxReExport.Size = new System.Drawing.Size(107, 17);
            this.checkBoxReExport.TabIndex = 5;
            this.checkBoxReExport.Text = "Prevent re-export";
            this.checkBoxReExport.UseVisualStyleBackColor = true;
            // 
            // textBoxEncryptPassword
            // 
            this.textBoxEncryptPassword.Enabled = false;
            this.textBoxEncryptPassword.Location = new System.Drawing.Point(29, 53);
            this.textBoxEncryptPassword.Name = "textBoxEncryptPassword";
            this.textBoxEncryptPassword.Size = new System.Drawing.Size(119, 20);
            this.textBoxEncryptPassword.TabIndex = 4;
            // 
            // checkBoxEncrypt
            // 
            this.checkBoxEncrypt.AutoSize = true;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(10, 34);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(94, 17);
            this.checkBoxEncrypt.TabIndex = 3;
            this.checkBoxEncrypt.Text = "Encrypt export";
            this.checkBoxEncrypt.UseVisualStyleBackColor = true;
            this.checkBoxEncrypt.CheckedChanged += new System.EventHandler(this.OnEncryptChanged);
            // 
            // checkBoxSign
            // 
            this.checkBoxSign.AutoSize = true;
            this.checkBoxSign.Location = new System.Drawing.Point(10, 14);
            this.checkBoxSign.Name = "checkBoxSign";
            this.checkBoxSign.Size = new System.Drawing.Size(79, 17);
            this.checkBoxSign.TabIndex = 2;
            this.checkBoxSign.Text = "Sign export";
            this.checkBoxSign.UseVisualStyleBackColor = true;
            // 
            // buttonCameraAdd
            // 
            this.buttonCameraAdd.Location = new System.Drawing.Point(8, 31);
            this.buttonCameraAdd.Name = "buttonCameraAdd";
            this.buttonCameraAdd.Size = new System.Drawing.Size(104, 23);
            this.buttonCameraAdd.TabIndex = 0;
            this.buttonCameraAdd.Text = "Add Camera";
            this.buttonCameraAdd.UseVisualStyleBackColor = true;
            this.buttonCameraAdd.Click += new System.EventHandler(this.buttonAddCamera_Click);
            // 
            // radioButtonAVI
            // 
            this.radioButtonAVI.AutoSize = true;
            this.radioButtonAVI.Location = new System.Drawing.Point(91, 324);
            this.radioButtonAVI.Name = "radioButtonAVI";
            this.radioButtonAVI.Size = new System.Drawing.Size(42, 17);
            this.radioButtonAVI.TabIndex = 8;
            this.radioButtonAVI.Text = "AVI";
            this.radioButtonAVI.UseVisualStyleBackColor = true;
            this.radioButtonAVI.CheckedChanged += new System.EventHandler(this.radioButtonAVI_CheckedChanged);
            // 
            // checkBoxRelated
            // 
            this.checkBoxRelated.AutoSize = true;
            this.checkBoxRelated.Location = new System.Drawing.Point(134, 162);
            this.checkBoxRelated.Name = "checkBoxRelated";
            this.checkBoxRelated.Size = new System.Drawing.Size(136, 17);
            this.checkBoxRelated.TabIndex = 2;
            this.checkBoxRelated.Text = "Include related devices";
            this.checkBoxRelated.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Export format:";
            // 
            // radioButtonDB
            // 
            this.radioButtonDB.AutoSize = true;
            this.radioButtonDB.Checked = true;
            this.radioButtonDB.Location = new System.Drawing.Point(91, 194);
            this.radioButtonDB.Name = "radioButtonDB";
            this.radioButtonDB.Size = new System.Drawing.Size(71, 17);
            this.radioButtonDB.TabIndex = 6;
            this.radioButtonDB.TabStop = true;
            this.radioButtonDB.Text = "Database";
            this.radioButtonDB.UseVisualStyleBackColor = true;
            this.radioButtonDB.CheckedChanged += new System.EventHandler(this.OnDatabaseChanged);
            // 
            // tabControlExportSource
            // 
            this.tabControlExportSource.Controls.Add(this.tabPageVideo);
            this.tabControlExportSource.Location = new System.Drawing.Point(5, 12);
            this.tabControlExportSource.Name = "tabControlExportSource";
            this.tabControlExportSource.SelectedIndex = 0;
            this.tabControlExportSource.Size = new System.Drawing.Size(382, 492);
            this.tabControlExportSource.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 708);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabControlExportSource);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.Label resultLabel;
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
        private System.Windows.Forms.RadioButton radioButtonAVI;
        private System.Windows.Forms.CheckBox checkBoxRelated;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonDB;
        private System.Windows.Forms.TabControl tabControlExportSource;
        private System.Windows.Forms.Button buttonOverlayImage;
        private System.Windows.Forms.CheckBox checkBoxIncludeOverlayImage;
        private System.Windows.Forms.ListBox listBoxCameras;
        private System.Windows.Forms.Button buttonRemoveCamera;
        private System.Windows.Forms.ToolTip resultLabelToolTip;
    }
}