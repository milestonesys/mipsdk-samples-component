namespace PTZandPresets
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
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxPresets = new System.Windows.Forms.ComboBox();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonleft = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBoxSequences = new System.Windows.Forms.ComboBox();
            this.buttonViewProperties = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonCreatePreset = new System.Windows.Forms.Button();
            this.textBoxPresetName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDeletePreset = new System.Windows.Forms.Button();
            this.buttonUpdatePreset = new System.Windows.Forms.Button();
            this.labelPresetAddName = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonStopMove = new System.Windows.Forms.Button();
            this.buttonStartMove = new System.Windows.Forms.Button();
            this.trackBarZoomS = new System.Windows.Forms.TrackBar();
            this.trackBarZoom = new System.Windows.Forms.TrackBar();
            this.trackBarTiltS = new System.Windows.Forms.TrackBar();
            this.trackBarTilt = new System.Windows.Forms.TrackBar();
            this.trackBarPanS = new System.Windows.Forms.TrackBar();
            this.trackBarPan = new System.Windows.Forms.TrackBar();
            this.t2ZoomSpeed = new System.Windows.Forms.TextBox();
            this.t2Pan = new System.Windows.Forms.TextBox();
            this.t2Tilt = new System.Windows.Forms.TextBox();
            this.t2Zoom = new System.Windows.Forms.TextBox();
            this.t2PanSpeed = new System.Windows.Forms.TextBox();
            this.t2TiltSpeed = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonSendAbs = new System.Windows.Forms.Button();
            this.buttonGetAbs = new System.Windows.Forms.Button();
            this.textBoxGetAbsZoom = new System.Windows.Forms.TextBox();
            this.textBoxGetAbsTilt = new System.Windows.Forms.TextBox();
            this.textBoxGetAbsPan = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoomS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTiltS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTilt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPanS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPan)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(701, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnClose);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(917, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(561, 17);
            this.toolStripStatusLabel1.Text = "This is a developer\'s code sample only. It is neither a test program nor an attem" +
    "pt to make an application.";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(27, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 410);
            this.panel1.TabIndex = 5;
            // 
            // comboBoxPresets
            // 
            this.comboBoxPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPresets.Enabled = false;
            this.comboBoxPresets.FormattingEnabled = true;
            this.comboBoxPresets.Location = new System.Drawing.Point(6, 19);
            this.comboBoxPresets.Name = "comboBoxPresets";
            this.comboBoxPresets.Size = new System.Drawing.Size(185, 21);
            this.comboBoxPresets.TabIndex = 9;
            this.comboBoxPresets.SelectedIndexChanged += new System.EventHandler(this.comboBoxPresets_SelectedIndexChanged);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(83, 12);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(33, 23);
            this.buttonUp.TabIndex = 10;
            this.buttonUp.Text = "^";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(83, 69);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(33, 23);
            this.buttonDown.TabIndex = 11;
            this.buttonDown.Text = "v";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Location = new System.Drawing.Point(120, 41);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(33, 23);
            this.buttonRight.TabIndex = 12;
            this.buttonRight.Text = ">";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonleft
            // 
            this.buttonleft.Location = new System.Drawing.Point(48, 41);
            this.buttonleft.Name = "buttonleft";
            this.buttonleft.Size = new System.Drawing.Size(33, 23);
            this.buttonleft.TabIndex = 13;
            this.buttonleft.Text = "<";
            this.buttonleft.UseVisualStyleBackColor = true;
            this.buttonleft.Click += new System.EventHandler(this.buttonleft_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.buttonDown);
            this.groupBox2.Controls.Add(this.buttonUp);
            this.groupBox2.Controls.Add(this.buttonleft);
            this.groupBox2.Controls.Add(this.buttonRight);
            this.groupBox2.Location = new System.Drawing.Point(442, 358);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 100);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Move in steps";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(83, 41);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "C";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBoxSequences
            // 
            this.comboBoxSequences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSequences.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSequences.Enabled = false;
            this.comboBoxSequences.FormattingEnabled = true;
            this.comboBoxSequences.Location = new System.Drawing.Point(701, 25);
            this.comboBoxSequences.Name = "comboBoxSequences";
            this.comboBoxSequences.Size = new System.Drawing.Size(198, 21);
            this.comboBoxSequences.TabIndex = 26;
            // 
            // buttonViewProperties
            // 
            this.buttonViewProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonViewProperties.Enabled = false;
            this.buttonViewProperties.Location = new System.Drawing.Point(701, 450);
            this.buttonViewProperties.Name = "buttonViewProperties";
            this.buttonViewProperties.Size = new System.Drawing.Size(198, 23);
            this.buttonViewProperties.TabIndex = 27;
            this.buttonViewProperties.Text = "View Camera Properties";
            this.buttonViewProperties.UseVisualStyleBackColor = true;
            this.buttonViewProperties.Click += new System.EventHandler(this.buttonViewProperties_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect.Location = new System.Drawing.Point(27, 23);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(397, 23);
            this.buttonSelect.TabIndex = 28;
            this.buttonSelect.Text = "Select Camera...";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelectClick);
            // 
            // buttonCreatePreset
            // 
            this.buttonCreatePreset.Location = new System.Drawing.Point(120, 73);
            this.buttonCreatePreset.Name = "buttonCreatePreset";
            this.buttonCreatePreset.Size = new System.Drawing.Size(71, 23);
            this.buttonCreatePreset.TabIndex = 29;
            this.buttonCreatePreset.Text = "Add preset";
            this.buttonCreatePreset.UseVisualStyleBackColor = true;
            this.buttonCreatePreset.Visible = false;
            this.buttonCreatePreset.Click += new System.EventHandler(this.buttonCreatePreset_Click);
            // 
            // textBoxPresetName
            // 
            this.textBoxPresetName.Location = new System.Drawing.Point(83, 46);
            this.textBoxPresetName.Name = "textBoxPresetName";
            this.textBoxPresetName.Size = new System.Drawing.Size(109, 20);
            this.textBoxPresetName.TabIndex = 30;
            this.textBoxPresetName.Text = "new preset name";
            this.textBoxPresetName.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonDeletePreset);
            this.groupBox3.Controls.Add(this.buttonUpdatePreset);
            this.groupBox3.Controls.Add(this.labelPresetAddName);
            this.groupBox3.Controls.Add(this.comboBoxPresets);
            this.groupBox3.Controls.Add(this.buttonCreatePreset);
            this.groupBox3.Controls.Add(this.textBoxPresetName);
            this.groupBox3.Location = new System.Drawing.Point(442, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 102);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PTZ Presets";
            // 
            // buttonDeletePreset
            // 
            this.buttonDeletePreset.Enabled = false;
            this.buttonDeletePreset.Location = new System.Drawing.Point(75, 73);
            this.buttonDeletePreset.Name = "buttonDeletePreset";
            this.buttonDeletePreset.Size = new System.Drawing.Size(41, 23);
            this.buttonDeletePreset.TabIndex = 33;
            this.buttonDeletePreset.Text = "Del";
            this.buttonDeletePreset.UseVisualStyleBackColor = true;
            this.buttonDeletePreset.Visible = false;
            this.buttonDeletePreset.Click += new System.EventHandler(this.OnDeletePreset);
            // 
            // buttonUpdatePreset
            // 
            this.buttonUpdatePreset.Enabled = false;
            this.buttonUpdatePreset.Location = new System.Drawing.Point(9, 73);
            this.buttonUpdatePreset.Name = "buttonUpdatePreset";
            this.buttonUpdatePreset.Size = new System.Drawing.Size(60, 23);
            this.buttonUpdatePreset.TabIndex = 32;
            this.buttonUpdatePreset.Text = "Update preset";
            this.buttonUpdatePreset.UseVisualStyleBackColor = true;
            this.buttonUpdatePreset.Visible = false;
            this.buttonUpdatePreset.Click += new System.EventHandler(this.OnUpdatePreset);
            // 
            // labelPresetAddName
            // 
            this.labelPresetAddName.AutoSize = true;
            this.labelPresetAddName.Location = new System.Drawing.Point(8, 48);
            this.labelPresetAddName.Name = "labelPresetAddName";
            this.labelPresetAddName.Size = new System.Drawing.Size(35, 13);
            this.labelPresetAddName.TabIndex = 31;
            this.labelPresetAddName.Text = "Name";
            this.labelPresetAddName.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.buttonStopMove);
            this.groupBox4.Controls.Add(this.buttonStartMove);
            this.groupBox4.Controls.Add(this.trackBarZoomS);
            this.groupBox4.Controls.Add(this.trackBarZoom);
            this.groupBox4.Controls.Add(this.trackBarTiltS);
            this.groupBox4.Controls.Add(this.trackBarTilt);
            this.groupBox4.Controls.Add(this.trackBarPanS);
            this.groupBox4.Controls.Add(this.trackBarPan);
            this.groupBox4.Controls.Add(this.t2ZoomSpeed);
            this.groupBox4.Controls.Add(this.t2Pan);
            this.groupBox4.Controls.Add(this.t2Tilt);
            this.groupBox4.Controls.Add(this.t2Zoom);
            this.groupBox4.Controls.Add(this.t2PanSpeed);
            this.groupBox4.Controls.Add(this.t2TiltSpeed);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(650, 72);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(255, 321);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Move continuously";
            // 
            // buttonStopMove
            // 
            this.buttonStopMove.Location = new System.Drawing.Point(75, 282);
            this.buttonStopMove.Name = "buttonStopMove";
            this.buttonStopMove.Size = new System.Drawing.Size(171, 23);
            this.buttonStopMove.TabIndex = 43;
            this.buttonStopMove.Text = "Stop Move";
            this.buttonStopMove.UseVisualStyleBackColor = true;
            this.buttonStopMove.Click += new System.EventHandler(this.buttonStopMove_Click);
            // 
            // buttonStartMove
            // 
            this.buttonStartMove.Location = new System.Drawing.Point(75, 253);
            this.buttonStartMove.Name = "buttonStartMove";
            this.buttonStartMove.Size = new System.Drawing.Size(171, 23);
            this.buttonStartMove.TabIndex = 42;
            this.buttonStartMove.Text = "Start Move";
            this.buttonStartMove.UseVisualStyleBackColor = true;
            this.buttonStartMove.Click += new System.EventHandler(this.buttonStartMove_Click);
            // 
            // trackBarZoomS
            // 
            this.trackBarZoomS.Location = new System.Drawing.Point(169, 159);
            this.trackBarZoomS.Name = "trackBarZoomS";
            this.trackBarZoomS.Size = new System.Drawing.Size(77, 45);
            this.trackBarZoomS.TabIndex = 40;
            this.trackBarZoomS.ValueChanged += new System.EventHandler(this.trackBarZoomS_ValueChanged);
            // 
            // trackBarZoom
            // 
            this.trackBarZoom.Location = new System.Drawing.Point(169, 133);
            this.trackBarZoom.Maximum = 1;
            this.trackBarZoom.Minimum = -1;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.Size = new System.Drawing.Size(77, 45);
            this.trackBarZoom.TabIndex = 41;
            this.trackBarZoom.Scroll += new System.EventHandler(this.trackBarZoom_Scroll);
            // 
            // trackBarTiltS
            // 
            this.trackBarTiltS.Location = new System.Drawing.Point(169, 105);
            this.trackBarTiltS.Name = "trackBarTiltS";
            this.trackBarTiltS.Size = new System.Drawing.Size(77, 45);
            this.trackBarTiltS.TabIndex = 38;
            this.trackBarTiltS.ValueChanged += new System.EventHandler(this.trackBarTiltS_ValueChanged);
            // 
            // trackBarTilt
            // 
            this.trackBarTilt.Location = new System.Drawing.Point(169, 79);
            this.trackBarTilt.Maximum = 1;
            this.trackBarTilt.Minimum = -1;
            this.trackBarTilt.Name = "trackBarTilt";
            this.trackBarTilt.Size = new System.Drawing.Size(77, 45);
            this.trackBarTilt.TabIndex = 39;
            this.trackBarTilt.Scroll += new System.EventHandler(this.trackBarTilt_Scroll);
            // 
            // trackBarPanS
            // 
            this.trackBarPanS.Location = new System.Drawing.Point(169, 51);
            this.trackBarPanS.Name = "trackBarPanS";
            this.trackBarPanS.Size = new System.Drawing.Size(77, 45);
            this.trackBarPanS.TabIndex = 36;
            this.trackBarPanS.ValueChanged += new System.EventHandler(this.trackBarPanS_ValueChanged);
            // 
            // trackBarPan
            // 
            this.trackBarPan.Location = new System.Drawing.Point(169, 25);
            this.trackBarPan.Maximum = 1;
            this.trackBarPan.Minimum = -1;
            this.trackBarPan.Name = "trackBarPan";
            this.trackBarPan.Size = new System.Drawing.Size(77, 45);
            this.trackBarPan.TabIndex = 37;
            this.trackBarPan.Scroll += new System.EventHandler(this.trackBarPan_Scroll);
            // 
            // t2ZoomSpeed
            // 
            this.t2ZoomSpeed.Location = new System.Drawing.Point(75, 159);
            this.t2ZoomSpeed.Name = "t2ZoomSpeed";
            this.t2ZoomSpeed.Size = new System.Drawing.Size(88, 20);
            this.t2ZoomSpeed.TabIndex = 35;
            this.t2ZoomSpeed.Text = "0.0";
            this.t2ZoomSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.t2ZoomSpeed_Validating);
            // 
            // t2Pan
            // 
            this.t2Pan.Location = new System.Drawing.Point(75, 25);
            this.t2Pan.Name = "t2Pan";
            this.t2Pan.Size = new System.Drawing.Size(88, 20);
            this.t2Pan.TabIndex = 34;
            this.t2Pan.Text = "0.0";
            this.t2Pan.Validating += new System.ComponentModel.CancelEventHandler(this.t2Pan_Validating);
            // 
            // t2Tilt
            // 
            this.t2Tilt.Location = new System.Drawing.Point(75, 79);
            this.t2Tilt.Name = "t2Tilt";
            this.t2Tilt.Size = new System.Drawing.Size(88, 20);
            this.t2Tilt.TabIndex = 33;
            this.t2Tilt.Text = "0.0";
            this.t2Tilt.Validating += new System.ComponentModel.CancelEventHandler(this.t2Tilt_Validating);
            // 
            // t2Zoom
            // 
            this.t2Zoom.Location = new System.Drawing.Point(75, 133);
            this.t2Zoom.Name = "t2Zoom";
            this.t2Zoom.Size = new System.Drawing.Size(88, 20);
            this.t2Zoom.TabIndex = 32;
            this.t2Zoom.Text = "0.0";
            this.t2Zoom.Validating += new System.ComponentModel.CancelEventHandler(this.t2Zoom_Validating);
            // 
            // t2PanSpeed
            // 
            this.t2PanSpeed.Location = new System.Drawing.Point(75, 51);
            this.t2PanSpeed.Name = "t2PanSpeed";
            this.t2PanSpeed.Size = new System.Drawing.Size(88, 20);
            this.t2PanSpeed.TabIndex = 31;
            this.t2PanSpeed.Text = "0.0";
            this.t2PanSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.t2PanSpeed_Validating);
            // 
            // t2TiltSpeed
            // 
            this.t2TiltSpeed.Location = new System.Drawing.Point(75, 105);
            this.t2TiltSpeed.Name = "t2TiltSpeed";
            this.t2TiltSpeed.Size = new System.Drawing.Size(88, 20);
            this.t2TiltSpeed.TabIndex = 30;
            this.t2TiltSpeed.Text = "0.0";
            this.t2TiltSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.t2TiltSpeed_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Zoom-speed:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Tilt-speed:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Pan:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Pan-speed:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Tilt:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Zoom:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.buttonSendAbs);
            this.groupBox5.Controls.Add(this.buttonGetAbs);
            this.groupBox5.Controls.Add(this.textBoxGetAbsZoom);
            this.groupBox5.Controls.Add(this.textBoxGetAbsTilt);
            this.groupBox5.Controls.Add(this.textBoxGetAbsPan);
            this.groupBox5.Location = new System.Drawing.Point(442, 180);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(198, 172);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Absolute PTZ positioning";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Zoom";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Tilt";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Pan:";
            // 
            // buttonSendAbs
            // 
            this.buttonSendAbs.Location = new System.Drawing.Point(6, 133);
            this.buttonSendAbs.Name = "buttonSendAbs";
            this.buttonSendAbs.Size = new System.Drawing.Size(179, 23);
            this.buttonSendAbs.TabIndex = 4;
            this.buttonSendAbs.Text = "Send absolute position command";
            this.buttonSendAbs.UseVisualStyleBackColor = true;
            this.buttonSendAbs.Click += new System.EventHandler(this.buttonSendAbs_Click);
            // 
            // buttonGetAbs
            // 
            this.buttonGetAbs.Location = new System.Drawing.Point(6, 104);
            this.buttonGetAbs.Name = "buttonGetAbs";
            this.buttonGetAbs.Size = new System.Drawing.Size(179, 23);
            this.buttonGetAbs.TabIndex = 3;
            this.buttonGetAbs.Text = "Get absolute position from camera";
            this.buttonGetAbs.UseVisualStyleBackColor = true;
            this.buttonGetAbs.Click += new System.EventHandler(this.buttonGetAbs_Click);
            // 
            // textBoxGetAbsZoom
            // 
            this.textBoxGetAbsZoom.Location = new System.Drawing.Point(76, 75);
            this.textBoxGetAbsZoom.Name = "textBoxGetAbsZoom";
            this.textBoxGetAbsZoom.Size = new System.Drawing.Size(109, 20);
            this.textBoxGetAbsZoom.TabIndex = 2;
            // 
            // textBoxGetAbsTilt
            // 
            this.textBoxGetAbsTilt.Location = new System.Drawing.Point(76, 49);
            this.textBoxGetAbsTilt.Name = "textBoxGetAbsTilt";
            this.textBoxGetAbsTilt.Size = new System.Drawing.Size(109, 20);
            this.textBoxGetAbsTilt.TabIndex = 1;
            // 
            // textBoxGetAbsPan
            // 
            this.textBoxGetAbsPan.Location = new System.Drawing.Point(76, 23);
            this.textBoxGetAbsPan.Name = "textBoxGetAbsPan";
            this.textBoxGetAbsPan.Size = new System.Drawing.Size(109, 20);
            this.textBoxGetAbsPan.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(75, 197);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 17);
            this.checkBox1.TabIndex = 44;
            this.checkBox1.Text = "Reverse Tilt values";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.OnTiltReverse);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 555);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.buttonViewProperties);
            this.Controls.Add(this.comboBoxSequences);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "MainForm";
            this.Text = "PTZandPresets MIP SDK Sample";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoomS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTiltS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTilt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPanS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPan)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxPresets;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonleft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxSequences;
        private System.Windows.Forms.Button buttonViewProperties;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCreatePreset;
        private System.Windows.Forms.TextBox textBoxPresetName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.TrackBar trackBarPan;
        private System.Windows.Forms.TrackBar trackBarPanS;
        private System.Windows.Forms.TextBox t2ZoomSpeed;
        private System.Windows.Forms.TextBox t2Pan;
        private System.Windows.Forms.TextBox t2Tilt;
        private System.Windows.Forms.TextBox t2Zoom;
        private System.Windows.Forms.TextBox t2PanSpeed;
        private System.Windows.Forms.TextBox t2TiltSpeed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBarTiltS;
        internal System.Windows.Forms.TrackBar trackBarTilt;
        private System.Windows.Forms.TrackBar trackBarZoomS;
        internal System.Windows.Forms.TrackBar trackBarZoom;
        private System.Windows.Forms.Button buttonStartMove;
        private System.Windows.Forms.Button buttonStopMove;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonGetAbs;
        private System.Windows.Forms.TextBox textBoxGetAbsZoom;
        private System.Windows.Forms.TextBox textBoxGetAbsTilt;
        private System.Windows.Forms.TextBox textBoxGetAbsPan;
        private System.Windows.Forms.Button buttonSendAbs;
        private System.Windows.Forms.Label labelPresetAddName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button buttonUpdatePreset;
		private System.Windows.Forms.Button buttonDeletePreset;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}