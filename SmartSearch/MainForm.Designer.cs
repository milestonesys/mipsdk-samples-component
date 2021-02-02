namespace SmartSearch
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
            this.buttonPickCamera = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonClear = new System.Windows.Forms.Button();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxOverlay = new System.Windows.Forms.CheckBox();
            this.timeNumericSelector = new System.Windows.Forms.NumericUpDown();
            this.timeUnitSelctor = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumericSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(593, 358);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.OnClose);
            // 
            // buttonPickCamera
            // 
            this.buttonPickCamera.Location = new System.Drawing.Point(13, 13);
            this.buttonPickCamera.Name = "buttonPickCamera";
            this.buttonPickCamera.Size = new System.Drawing.Size(207, 23);
            this.buttonPickCamera.TabIndex = 1;
            this.buttonPickCamera.Text = "Camera..";
            this.buttonPickCamera.UseVisualStyleBackColor = true;
            this.buttonPickCamera.Click += new System.EventHandler(this.buttonPickCamera_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Location = new System.Drawing.Point(13, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 262);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(380, 259);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSearch.Enabled = false;
            this.buttonSearch.Location = new System.Drawing.Point(16, 358);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // listBoxResult
            // 
            this.listBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.Location = new System.Drawing.Point(417, 46);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(251, 251);
            this.listBoxResult.TabIndex = 5;
            this.listBoxResult.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Enabled = false;
            this.buttonClear.Location = new System.Drawing.Point(97, 358);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hScrollBar1.Location = new System.Drawing.Point(255, 335);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(144, 22);
            this.hScrollBar1.TabIndex = 7;
            this.hScrollBar1.Value = 75;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Sensitivity:";
            // 
            // checkBoxOverlay
            // 
            this.checkBoxOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOverlay.AutoSize = true;
            this.checkBoxOverlay.Location = new System.Drawing.Point(419, 312);
            this.checkBoxOverlay.Name = "checkBoxOverlay";
            this.checkBoxOverlay.Size = new System.Drawing.Size(111, 17);
            this.checkBoxOverlay.TabIndex = 9;
            this.checkBoxOverlay.Text = "Show motion area";
            this.checkBoxOverlay.UseVisualStyleBackColor = true;
            this.checkBoxOverlay.CheckedChanged += new System.EventHandler(this.checkBoxOverlay_CheckedChanged);
            // 
            // timeNumericSelector
            // 
            this.timeNumericSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timeNumericSelector.Location = new System.Drawing.Point(153, 312);
            this.timeNumericSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timeNumericSelector.Name = "timeNumericSelector";
            this.timeNumericSelector.Size = new System.Drawing.Size(120, 20);
            this.timeNumericSelector.TabIndex = 10;
            this.timeNumericSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timeNumericSelector.ValueChanged += new System.EventHandler(this.OnValueChanged);
            // 
            // timeUnitSelctor
            // 
            this.timeUnitSelctor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timeUnitSelctor.Items.Add("Days");
            this.timeUnitSelctor.Items.Add("Hours");
            this.timeUnitSelctor.Items.Add("Minutes");
            this.timeUnitSelctor.Location = new System.Drawing.Point(279, 312);
            this.timeUnitSelctor.Name = "timeUnitSelctor";
            this.timeUnitSelctor.Size = new System.Drawing.Size(120, 20);
            this.timeUnitSelctor.TabIndex = 11;
            this.timeUnitSelctor.Text = "Days";
            this.timeUnitSelctor.TextChanged += new System.EventHandler(this.OnValueChanged);
            this.timeUnitSelctor.SelectedIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Search duration";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 393);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeUnitSelctor);
            this.Controls.Add(this.timeNumericSelector);
            this.Controls.Add(this.checkBoxOverlay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.listBoxResult);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonPickCamera);
            this.Controls.Add(this.buttonClose);
            this.Name = "MainForm";
            this.Text = "SmartSearch Application";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumericSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonPickCamera;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.ListBox listBoxResult;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxOverlay;
        private System.Windows.Forms.NumericUpDown timeNumericSelector;
        private System.Windows.Forms.DomainUpDown timeUnitSelctor;
        private System.Windows.Forms.Label label1;
    }
}