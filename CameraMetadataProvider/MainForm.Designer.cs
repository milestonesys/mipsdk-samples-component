namespace CameraMetadataProvider
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
            this.panelTimeDisplay = new System.Windows.Forms.Panel();
            this.lstPTZCommands = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSessionCount = new System.Windows.Forms.TextBox();
            this.hScrollBarQuality = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.labelQuality = new System.Windows.Forms.Label();
            this.comboBoxFPS = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonIdle = new System.Windows.Forms.Button();
            this.buttonPushToTalk = new System.Windows.Forms.Button();
            this.panelTimeDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(433, 295);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // panelTimeDisplay
            // 
            this.panelTimeDisplay.BackColor = System.Drawing.Color.Azure;
            this.panelTimeDisplay.Controls.Add(this.lstPTZCommands);
            this.panelTimeDisplay.Controls.Add(this.label4);
            this.panelTimeDisplay.Controls.Add(this.textBoxTime);
            this.panelTimeDisplay.Location = new System.Drawing.Point(167, 23);
            this.panelTimeDisplay.Name = "panelTimeDisplay";
            this.panelTimeDisplay.Size = new System.Drawing.Size(341, 240);
            this.panelTimeDisplay.TabIndex = 1;
            // 
            // lstPTZCommands
            // 
            this.lstPTZCommands.FormattingEnabled = true;
            this.lstPTZCommands.HorizontalScrollbar = true;
            this.lstPTZCommands.Location = new System.Drawing.Point(6, 55);
            this.lstPTZCommands.Name = "lstPTZCommands";
            this.lstPTZCommands.Size = new System.Drawing.Size(332, 173);
            this.lstPTZCommands.TabIndex = 14;
            this.lstPTZCommands.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = " PTZ commands:";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(6, 16);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(118, 20);
            this.textBoxTime.TabIndex = 0;
            this.textBoxTime.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(13, 23);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(132, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Accept Sessions";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnectClick);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(12, 52);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(133, 23);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Disconnect Sessions";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnectClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sessions:";
            // 
            // textBoxSessionCount
            // 
            this.textBoxSessionCount.Location = new System.Drawing.Point(101, 203);
            this.textBoxSessionCount.Name = "textBoxSessionCount";
            this.textBoxSessionCount.ReadOnly = true;
            this.textBoxSessionCount.Size = new System.Drawing.Size(44, 20);
            this.textBoxSessionCount.TabIndex = 5;
            this.textBoxSessionCount.TabStop = false;
            this.textBoxSessionCount.Text = "0";
            // 
            // hScrollBarQuality
            // 
            this.hScrollBarQuality.Location = new System.Drawing.Point(15, 161);
            this.hScrollBarQuality.Name = "hScrollBarQuality";
            this.hScrollBarQuality.Size = new System.Drawing.Size(130, 17);
            this.hScrollBarQuality.TabIndex = 3;
            this.hScrollBarQuality.TabStop = true;
            this.hScrollBarQuality.Value = 75;
            this.hScrollBarQuality.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Max Quality:";
            // 
            // labelQuality
            // 
            this.labelQuality.AutoSize = true;
            this.labelQuality.Location = new System.Drawing.Point(77, 146);
            this.labelQuality.Name = "labelQuality";
            this.labelQuality.Size = new System.Drawing.Size(19, 13);
            this.labelQuality.TabIndex = 8;
            this.labelQuality.Text = "75";
            // 
            // comboBoxFPS
            // 
            this.comboBoxFPS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFPS.FormattingEnabled = true;
            this.comboBoxFPS.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "60"});
            this.comboBoxFPS.Location = new System.Drawing.Point(80, 106);
            this.comboBoxFPS.Name = "comboBoxFPS";
            this.comboBoxFPS.Size = new System.Drawing.Size(65, 21);
            this.comboBoxFPS.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max FPS:";
            // 
            // buttonIdle
            // 
            this.buttonIdle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonIdle.Location = new System.Drawing.Point(15, 295);
            this.buttonIdle.Name = "buttonIdle";
            this.buttonIdle.Size = new System.Drawing.Size(130, 23);
            this.buttonIdle.TabIndex = 4;
            this.buttonIdle.Text = "Start sending Video";
            this.buttonIdle.UseVisualStyleBackColor = true;
            this.buttonIdle.Click += new System.EventHandler(this.OnIdle);
            // 
            // buttonPushToTalk
            // 
            this.buttonPushToTalk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPushToTalk.Location = new System.Drawing.Point(15, 266);
            this.buttonPushToTalk.Name = "buttonPushToTalk";
            this.buttonPushToTalk.Size = new System.Drawing.Size(130, 23);
            this.buttonPushToTalk.TabIndex = 11;
            this.buttonPushToTalk.Text = "Push to Talk";
            this.buttonPushToTalk.UseVisualStyleBackColor = true;
            this.buttonPushToTalk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnButtonPushToTalkMouseDown);
            this.buttonPushToTalk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnButtonPushToTalkMouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(520, 330);
            this.Controls.Add(this.buttonPushToTalk);
            this.Controls.Add(this.buttonIdle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxFPS);
            this.Controls.Add(this.labelQuality);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hScrollBarQuality);
            this.Controls.Add(this.textBoxSessionCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.panelTimeDisplay);
            this.Controls.Add(this.buttonClose);
            this.Name = "MainForm";
            this.Text = "Video, Audio and Metadata Provider";
            this.panelTimeDisplay.ResumeLayout(false);
            this.panelTimeDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Panel panelTimeDisplay;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonDisconnect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxSessionCount;
		private System.Windows.Forms.HScrollBar hScrollBarQuality;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelQuality;
		private System.Windows.Forms.ComboBox comboBoxFPS;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonIdle;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox lstPTZCommands;
        private System.Windows.Forms.Button buttonPushToTalk;
    }
}

