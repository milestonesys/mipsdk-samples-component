namespace MediaLiveViewer
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
            this.groupBoxVideo = new System.Windows.Forms.GroupBox();
            this.comboBoxStreamSelection = new System.Windows.Forms.ComboBox();
            this.buttonLift = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDecodingStatus = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxfps = new System.Windows.Forms.ComboBox();
            this.checkBoxKeyFramesOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxFill = new System.Windows.Forms.CheckBox();
            this.checkBoxAspect = new System.Windows.Forms.CheckBox();
            this.labelCropRect = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxCrop = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCompression = new System.Windows.Forms.ComboBox();
            this.buttonPause = new System.Windows.Forms.Button();
            this.checkBoxClientLive = new System.Windows.Forms.CheckBox();
            this.checkBoxDiskFull = new System.Windows.Forms.CheckBox();
            this.checkBoxDBFail = new System.Windows.Forms.CheckBox();
            this.checkBoxLiveFeed = new System.Windows.Forms.CheckBox();
            this.checkBoxRec = new System.Windows.Forms.CheckBox();
            this.checkBoxNotification = new System.Windows.Forms.CheckBox();
            this.checkBoxOffline = new System.Windows.Forms.CheckBox();
            this.checkBoxMotion = new System.Windows.Forms.CheckBox();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.labelResolution = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonSelect1 = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxVideo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxVideo
            // 
            this.groupBoxVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVideo.Controls.Add(this.comboBoxStreamSelection);
            this.groupBoxVideo.Controls.Add(this.buttonLift);
            this.groupBoxVideo.Controls.Add(this.label7);
            this.groupBoxVideo.Controls.Add(this.textBoxDecodingStatus);
            this.groupBoxVideo.Controls.Add(this.label5);
            this.groupBoxVideo.Controls.Add(this.comboBoxfps);
            this.groupBoxVideo.Controls.Add(this.checkBoxKeyFramesOnly);
            this.groupBoxVideo.Controls.Add(this.checkBoxFill);
            this.groupBoxVideo.Controls.Add(this.checkBoxAspect);
            this.groupBoxVideo.Controls.Add(this.labelCropRect);
            this.groupBoxVideo.Controls.Add(this.label6);
            this.groupBoxVideo.Controls.Add(this.checkBoxCrop);
            this.groupBoxVideo.Controls.Add(this.label4);
            this.groupBoxVideo.Controls.Add(this.labelSize);
            this.groupBoxVideo.Controls.Add(this.label2);
            this.groupBoxVideo.Controls.Add(this.comboBoxCompression);
            this.groupBoxVideo.Controls.Add(this.buttonPause);
            this.groupBoxVideo.Controls.Add(this.checkBoxClientLive);
            this.groupBoxVideo.Controls.Add(this.checkBoxDiskFull);
            this.groupBoxVideo.Controls.Add(this.checkBoxDBFail);
            this.groupBoxVideo.Controls.Add(this.checkBoxLiveFeed);
            this.groupBoxVideo.Controls.Add(this.checkBoxRec);
            this.groupBoxVideo.Controls.Add(this.checkBoxNotification);
            this.groupBoxVideo.Controls.Add(this.checkBoxOffline);
            this.groupBoxVideo.Controls.Add(this.checkBoxMotion);
            this.groupBoxVideo.Controls.Add(this.comboBoxResolution);
            this.groupBoxVideo.Controls.Add(this.labelResolution);
            this.groupBoxVideo.Controls.Add(this.label3);
            this.groupBoxVideo.Controls.Add(this.labelCount);
            this.groupBoxVideo.Controls.Add(this.label1);
            this.groupBoxVideo.Controls.Add(this.panel1);
            this.groupBoxVideo.Controls.Add(this.buttonSelect1);
            this.groupBoxVideo.Location = new System.Drawing.Point(12, 4);
            this.groupBoxVideo.Name = "groupBoxVideo";
            this.groupBoxVideo.Size = new System.Drawing.Size(673, 425);
            this.groupBoxVideo.TabIndex = 12;
            this.groupBoxVideo.TabStop = false;
            this.groupBoxVideo.Text = "Video";
            // 
            // comboBoxStreamSelection
            // 
            this.comboBoxStreamSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxStreamSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStreamSelection.FormattingEnabled = true;
            this.comboBoxStreamSelection.Items.AddRange(new object[] {
            "Default stream",
            "Max resolution stream",
            "Adaptive stream"});
            this.comboBoxStreamSelection.Location = new System.Drawing.Point(534, 338);
            this.comboBoxStreamSelection.Name = "comboBoxStreamSelection";
            this.comboBoxStreamSelection.Size = new System.Drawing.Size(114, 21);
            this.comboBoxStreamSelection.TabIndex = 35;
            this.comboBoxStreamSelection.SelectedIndexChanged += new System.EventHandler(this.comboBoxStreamSelection_SelectedIndexChanged);
            // 
            // buttonLift
            // 
            this.buttonLift.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLift.Enabled = false;
            this.buttonLift.Location = new System.Drawing.Point(534, 363);
            this.buttonLift.Name = "buttonLift";
            this.buttonLift.Size = new System.Drawing.Size(114, 23);
            this.buttonLift.TabIndex = 34;
            this.buttonLift.Text = "Lift privacy mask";
            this.buttonLift.UseVisualStyleBackColor = true;
            this.buttonLift.Click += new System.EventHandler(this.buttonLift_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(366, 314);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Hardware Decoding Status:";
            // 
            // textBoxDecodingStatus
            // 
            this.textBoxDecodingStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDecodingStatus.Location = new System.Drawing.Point(534, 311);
            this.textBoxDecodingStatus.Name = "textBoxDecodingStatus";
            this.textBoxDecodingStatus.ReadOnly = true;
            this.textBoxDecodingStatus.Size = new System.Drawing.Size(100, 20);
            this.textBoxDecodingStatus.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 368);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "FPS:";
            // 
            // comboBoxfps
            // 
            this.comboBoxfps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxfps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxfps.Items.AddRange(new object[] {
            "Default",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.comboBoxfps.Location = new System.Drawing.Point(412, 365);
            this.comboBoxfps.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxfps.Name = "comboBoxfps";
            this.comboBoxfps.Size = new System.Drawing.Size(92, 21);
            this.comboBoxfps.TabIndex = 30;
            this.comboBoxfps.SelectedIndexChanged += new System.EventHandler(this.comboBoxfps_SelectedIndexChanged);
            // 
            // checkBoxKeyFramesOnly
            // 
            this.checkBoxKeyFramesOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxKeyFramesOnly.AutoSize = true;
            this.checkBoxKeyFramesOnly.Location = new System.Drawing.Point(241, 313);
            this.checkBoxKeyFramesOnly.Name = "checkBoxKeyFramesOnly";
            this.checkBoxKeyFramesOnly.Size = new System.Drawing.Size(105, 17);
            this.checkBoxKeyFramesOnly.TabIndex = 29;
            this.checkBoxKeyFramesOnly.Text = "Key Frames Only";
            this.checkBoxKeyFramesOnly.UseVisualStyleBackColor = true;
            // 
            // checkBoxFill
            // 
            this.checkBoxFill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxFill.AutoSize = true;
            this.checkBoxFill.Location = new System.Drawing.Point(138, 313);
            this.checkBoxFill.Name = "checkBoxFill";
            this.checkBoxFill.Size = new System.Drawing.Size(89, 17);
            this.checkBoxFill.TabIndex = 28;
            this.checkBoxFill.Text = "Fill with black";
            this.checkBoxFill.UseVisualStyleBackColor = true;
            // 
            // checkBoxAspect
            // 
            this.checkBoxAspect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAspect.AutoSize = true;
            this.checkBoxAspect.Location = new System.Drawing.Point(19, 313);
            this.checkBoxAspect.Name = "checkBoxAspect";
            this.checkBoxAspect.Size = new System.Drawing.Size(110, 17);
            this.checkBoxAspect.TabIndex = 27;
            this.checkBoxAspect.Text = "Keep Aspect ratio";
            this.checkBoxAspect.UseVisualStyleBackColor = true;
            // 
            // labelCropRect
            // 
            this.labelCropRect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCropRect.AutoSize = true;
            this.labelCropRect.Location = new System.Drawing.Point(482, 82);
            this.labelCropRect.Name = "labelCropRect";
            this.labelCropRect.Size = new System.Drawing.Size(13, 13);
            this.labelCropRect.TabIndex = 26;
            this.labelCropRect.Text = "--";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(391, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Cropping in frame:";
            // 
            // checkBoxCrop
            // 
            this.checkBoxCrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCrop.AutoSize = true;
            this.checkBoxCrop.Location = new System.Drawing.Point(549, 78);
            this.checkBoxCrop.Name = "checkBoxCrop";
            this.checkBoxCrop.Size = new System.Drawing.Size(99, 17);
            this.checkBoxCrop.TabIndex = 24;
            this.checkBoxCrop.Text = "Crop lower right";
            this.checkBoxCrop.UseVisualStyleBackColor = true;
            this.checkBoxCrop.CheckedChanged += new System.EventHandler(this.OnCropChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(391, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Bytes:";
            // 
            // labelSize
            // 
            this.labelSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(482, 43);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(13, 13);
            this.labelSize.TabIndex = 22;
            this.labelSize.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "JPEG Quality:";
            // 
            // comboBoxCompression
            // 
            this.comboBoxCompression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxCompression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompression.FormattingEnabled = true;
            this.comboBoxCompression.Items.AddRange(new object[] {
            "80",
            "75",
            "50",
            "25",
            "10",
            "5",
            "1"});
            this.comboBoxCompression.Location = new System.Drawing.Point(259, 365);
            this.comboBoxCompression.Name = "comboBoxCompression";
            this.comboBoxCompression.Size = new System.Drawing.Size(80, 21);
            this.comboBoxCompression.TabIndex = 20;
            this.comboBoxCompression.SelectedIndexChanged += new System.EventHandler(this.OnQualityChanged);
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPause.Enabled = false;
            this.buttonPause.Location = new System.Drawing.Point(534, 271);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 19;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.OnClick);
            // 
            // checkBoxClientLive
            // 
            this.checkBoxClientLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxClientLive.AutoSize = true;
            this.checkBoxClientLive.Enabled = false;
            this.checkBoxClientLive.Location = new System.Drawing.Point(393, 275);
            this.checkBoxClientLive.Name = "checkBoxClientLive";
            this.checkBoxClientLive.Size = new System.Drawing.Size(111, 17);
            this.checkBoxClientLive.TabIndex = 18;
            this.checkBoxClientLive.Text = "We have paused ";
            this.checkBoxClientLive.UseVisualStyleBackColor = true;
            // 
            // checkBoxDiskFull
            // 
            this.checkBoxDiskFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDiskFull.AutoSize = true;
            this.checkBoxDiskFull.Enabled = false;
            this.checkBoxDiskFull.Location = new System.Drawing.Point(393, 252);
            this.checkBoxDiskFull.Name = "checkBoxDiskFull";
            this.checkBoxDiskFull.Size = new System.Drawing.Size(63, 17);
            this.checkBoxDiskFull.TabIndex = 17;
            this.checkBoxDiskFull.Text = "Disk full";
            this.checkBoxDiskFull.UseVisualStyleBackColor = true;
            // 
            // checkBoxDBFail
            // 
            this.checkBoxDBFail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDBFail.AutoSize = true;
            this.checkBoxDBFail.Enabled = false;
            this.checkBoxDBFail.Location = new System.Drawing.Point(393, 229);
            this.checkBoxDBFail.Name = "checkBoxDBFail";
            this.checkBoxDBFail.Size = new System.Drawing.Size(60, 17);
            this.checkBoxDBFail.TabIndex = 16;
            this.checkBoxDBFail.Text = "DB Fail";
            this.checkBoxDBFail.UseVisualStyleBackColor = true;
            // 
            // checkBoxLiveFeed
            // 
            this.checkBoxLiveFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLiveFeed.AutoSize = true;
            this.checkBoxLiveFeed.Enabled = false;
            this.checkBoxLiveFeed.Location = new System.Drawing.Point(394, 206);
            this.checkBoxLiveFeed.Name = "checkBoxLiveFeed";
            this.checkBoxLiveFeed.Size = new System.Drawing.Size(73, 17);
            this.checkBoxLiveFeed.TabIndex = 15;
            this.checkBoxLiveFeed.Text = "Live Feed";
            this.checkBoxLiveFeed.UseVisualStyleBackColor = true;
            // 
            // checkBoxRec
            // 
            this.checkBoxRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRec.AutoSize = true;
            this.checkBoxRec.Enabled = false;
            this.checkBoxRec.Location = new System.Drawing.Point(393, 183);
            this.checkBoxRec.Name = "checkBoxRec";
            this.checkBoxRec.Size = new System.Drawing.Size(48, 17);
            this.checkBoxRec.TabIndex = 14;
            this.checkBoxRec.Text = "REC";
            this.checkBoxRec.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotification
            // 
            this.checkBoxNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxNotification.AutoSize = true;
            this.checkBoxNotification.Enabled = false;
            this.checkBoxNotification.Location = new System.Drawing.Point(392, 160);
            this.checkBoxNotification.Name = "checkBoxNotification";
            this.checkBoxNotification.Size = new System.Drawing.Size(79, 17);
            this.checkBoxNotification.TabIndex = 13;
            this.checkBoxNotification.Text = "Notification";
            this.checkBoxNotification.UseVisualStyleBackColor = true;
            // 
            // checkBoxOffline
            // 
            this.checkBoxOffline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOffline.AutoSize = true;
            this.checkBoxOffline.Enabled = false;
            this.checkBoxOffline.Location = new System.Drawing.Point(392, 137);
            this.checkBoxOffline.Name = "checkBoxOffline";
            this.checkBoxOffline.Size = new System.Drawing.Size(95, 17);
            this.checkBoxOffline.TabIndex = 12;
            this.checkBoxOffline.Text = "Camera Offline";
            this.checkBoxOffline.UseVisualStyleBackColor = true;
            // 
            // checkBoxMotion
            // 
            this.checkBoxMotion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMotion.AutoSize = true;
            this.checkBoxMotion.Enabled = false;
            this.checkBoxMotion.Location = new System.Drawing.Point(395, 114);
            this.checkBoxMotion.Name = "checkBoxMotion";
            this.checkBoxMotion.Size = new System.Drawing.Size(58, 17);
            this.checkBoxMotion.TabIndex = 11;
            this.checkBoxMotion.Text = "Motion";
            this.checkBoxMotion.UseVisualStyleBackColor = true;
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Items.AddRange(new object[] {
            "160x120",
            "320x240",
            "640x480",
            "1048x780",
            "1920x1080"});
            this.comboBoxResolution.Location = new System.Drawing.Point(368, 338);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(136, 21);
            this.comboBoxResolution.TabIndex = 10;
            this.comboBoxResolution.SelectedIndexChanged += new System.EventHandler(this.OnResolutionChanged);
            // 
            // labelResolution
            // 
            this.labelResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelResolution.AutoSize = true;
            this.labelResolution.Location = new System.Drawing.Point(482, 63);
            this.labelResolution.Name = "labelResolution";
            this.labelResolution.Size = new System.Drawing.Size(48, 13);
            this.labelResolution.TabIndex = 9;
            this.labelResolution.Text = "320x240";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Resolution:";
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(482, 25);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(13, 13);
            this.labelCount.TabIndex = 7;
            this.labelCount.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(391, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Frame Count:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(19, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 271);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 271);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Resize += new System.EventHandler(this.OnResizePictureBox);
            // 
            // buttonSelect1
            // 
            this.buttonSelect1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelect1.Location = new System.Drawing.Point(19, 336);
            this.buttonSelect1.Name = "buttonSelect1";
            this.buttonSelect1.Size = new System.Drawing.Size(320, 23);
            this.buttonSelect1.TabIndex = 0;
            this.buttonSelect1.Text = "Select camera...";
            this.buttonSelect1.UseVisualStyleBackColor = true;
            this.buttonSelect1.Click += new System.EventHandler(this.OnSelect1Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(598, 448);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 23);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnClose);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 483);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxVideo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(699, 488);
            this.Name = "MainForm";
            this.Text = "Media Live Viewer";
            this.groupBoxVideo.ResumeLayout(false);
            this.groupBoxVideo.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxVideo;
		private System.Windows.Forms.Button buttonSelect1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label labelCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelResolution;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxResolution;
		private System.Windows.Forms.CheckBox checkBoxNotification;
		private System.Windows.Forms.CheckBox checkBoxOffline;
		private System.Windows.Forms.CheckBox checkBoxMotion;
		private System.Windows.Forms.CheckBox checkBoxRec;
		private System.Windows.Forms.CheckBox checkBoxClientLive;
		private System.Windows.Forms.CheckBox checkBoxDiskFull;
		private System.Windows.Forms.CheckBox checkBoxDBFail;
		private System.Windows.Forms.CheckBox checkBoxLiveFeed;
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxCompression;
		private System.Windows.Forms.Label labelSize;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBoxCrop;
		private System.Windows.Forms.Label labelCropRect;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkBoxFill;
		private System.Windows.Forms.CheckBox checkBoxAspect;
		private System.Windows.Forms.CheckBox checkBoxKeyFramesOnly;
        private System.Windows.Forms.ComboBox comboBoxfps;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxDecodingStatus;
        private System.Windows.Forms.Button buttonLift;
        private System.Windows.Forms.ComboBox comboBoxStreamSelection;
    }
}

