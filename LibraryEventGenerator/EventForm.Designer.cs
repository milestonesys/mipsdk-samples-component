namespace LibraryEventGenerator
{
    partial class EventForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventForm));
            this._labelEvent = new System.Windows.Forms.Label();
            this._textBoxEventName = new System.Windows.Forms.TextBox();
            this._textBoxEventType = new System.Windows.Forms.TextBox();
            this._labelEventType = new System.Windows.Forms.Label();
            this._textBoxMessage = new System.Windows.Forms.TextBox();
            this._labelMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._buttonSelect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBoxSample = new System.Windows.Forms.PictureBox();
            this.FireEventButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _labelEvent
            // 
            this._labelEvent.AutoSize = true;
            this._labelEvent.Location = new System.Drawing.Point(12, 78);
            this._labelEvent.Name = "_labelEvent";
            this._labelEvent.Size = new System.Drawing.Size(67, 13);
            this._labelEvent.TabIndex = 3;
            this._labelEvent.Text = "Event name:";
            // 
            // _textBoxEventName
            // 
            this._textBoxEventName.Location = new System.Drawing.Point(143, 75);
            this._textBoxEventName.Name = "_textBoxEventName";
            this._textBoxEventName.Size = new System.Drawing.Size(219, 20);
            this._textBoxEventName.TabIndex = 3;
            this._textBoxEventName.Text = "My event";
            // 
            // _textBoxEventType
            // 
            this._textBoxEventType.Location = new System.Drawing.Point(143, 106);
            this._textBoxEventType.Name = "_textBoxEventType";
            this._textBoxEventType.Size = new System.Drawing.Size(219, 20);
            this._textBoxEventType.TabIndex = 4;
            this._textBoxEventType.Text = "My type of event";
            // 
            // _labelEventType
            // 
            this._labelEventType.AutoSize = true;
            this._labelEventType.Location = new System.Drawing.Point(12, 109);
            this._labelEventType.Name = "_labelEventType";
            this._labelEventType.Size = new System.Drawing.Size(61, 13);
            this._labelEventType.TabIndex = 5;
            this._labelEventType.Text = "Event type:";
            // 
            // _textBoxMessage
            // 
            this._textBoxMessage.Location = new System.Drawing.Point(143, 138);
            this._textBoxMessage.Name = "_textBoxMessage";
            this._textBoxMessage.Size = new System.Drawing.Size(219, 20);
            this._textBoxMessage.TabIndex = 5;
            this._textBoxMessage.Text = "MyAnalyticsEvent";
            // 
            // _labelMessage
            // 
            this._labelMessage.AutoSize = true;
            this._labelMessage.Location = new System.Drawing.Point(12, 141);
            this._labelMessage.Name = "_labelMessage";
            this._labelMessage.Size = new System.Drawing.Size(83, 13);
            this._labelMessage.TabIndex = 7;
            this._labelMessage.Text = "Event message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Source:";
            // 
            // _buttonSelect
            // 
            this._buttonSelect.Location = new System.Drawing.Point(144, 25);
            this._buttonSelect.Name = "_buttonSelect";
            this._buttonSelect.Size = new System.Drawing.Size(218, 23);
            this._buttonSelect.TabIndex = 2;
            this._buttonSelect.Text = "Select an Item...";
            this._buttonSelect.UseVisualStyleBackColor = true;
            this._buttonSelect.Click += new System.EventHandler(this.OnSelect);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(327, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnImageSelect);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Image:";
            // 
            // pictureBoxSample
            // 
            this.pictureBoxSample.Location = new System.Drawing.Point(143, 173);
            this.pictureBoxSample.Name = "pictureBoxSample";
            this.pictureBoxSample.Size = new System.Drawing.Size(117, 84);
            this.pictureBoxSample.TabIndex = 19;
            this.pictureBoxSample.TabStop = false;
            // 
            // FireEventButton
            // 
            this.FireEventButton.Location = new System.Drawing.Point(143, 284);
            this.FireEventButton.Name = "FireEventButton";
            this.FireEventButton.Size = new System.Drawing.Size(218, 25);
            this.FireEventButton.TabIndex = 10;
            this.FireEventButton.Text = "Send Event to Event Server";
            this.FireEventButton.UseVisualStyleBackColor = true;
            this.FireEventButton.Click += new System.EventHandler(this.FireEventToRule_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FireEventButton);
            this.groupBox1.Controls.Add(this._labelEvent);
            this.groupBox1.Controls.Add(this.pictureBoxSample);
            this.groupBox1.Controls.Add(this._textBoxEventName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this._labelEventType);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this._textBoxEventType);
            this.groupBox1.Controls.Add(this._labelMessage);
            this.groupBox1.Controls.Add(this._buttonSelect);
            this.groupBox1.Controls.Add(this._textBoxMessage);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 354);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(143, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(218, 25);
            this.button2.TabIndex = 20;
            this.button2.Text = "Send Alarm to Event Server";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.FireSendAlarmClick);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(289, 368);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 23);
            this.buttonOK.TabIndex = 21;
            this.buttonOK.Text = "Close";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnClose);
            // 
            // EventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 403);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 440);
            this.Name = "EventForm";
            this.Text = "Library Event Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Label _labelEvent;
        private System.Windows.Forms.TextBox _textBoxEventName;
        private System.Windows.Forms.TextBox _textBoxEventType;
        private System.Windows.Forms.Label _labelEventType;
        private System.Windows.Forms.TextBox _textBoxMessage;
		private System.Windows.Forms.Label _labelMessage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button _buttonSelect;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox pictureBoxSample;
		private System.Windows.Forms.Button FireEventButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button button2;
    }
}

