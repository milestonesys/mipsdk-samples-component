namespace AudioDemo
{
    partial class AudioForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioForm));
            this._panel = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._audioDevicesGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this._volumeTrackBar = new System.Windows.Forms.TrackBar();
            this._muteCheckBox = new System.Windows.Forms.CheckBox();
            this._connectionLabel = new System.Windows.Forms.Label();
            this._connectButton = new System.Windows.Forms.Button();
            this._serverUrlTextBox = new System.Windows.Forms.TextBox();
            this._serverUrlLabel = new System.Windows.Forms.Label();
            this._secureOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this._panel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this._audioDevicesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._volumeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // _panel
            // 
            this._panel.Controls.Add(this._secureOnlyCheckBox);
            this._panel.Controls.Add(this.statusStrip1);
            this._panel.Controls.Add(this._audioDevicesGroupBox);
            this._panel.Controls.Add(this._connectionLabel);
            this._panel.Controls.Add(this._connectButton);
            this._panel.Controls.Add(this._serverUrlTextBox);
            this._panel.Controls.Add(this._serverUrlLabel);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(0, 0);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(286, 200);
            this._panel.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 178);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(286, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(91, 17);
            this.toolStripStatusLabel1.Text = "Not Connected ";
            // 
            // _audioDevicesGroupBox
            // 
            this._audioDevicesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._audioDevicesGroupBox.Controls.Add(this.buttonSelect);
            this._audioDevicesGroupBox.Controls.Add(this._volumeTrackBar);
            this._audioDevicesGroupBox.Controls.Add(this._muteCheckBox);
            this._audioDevicesGroupBox.Enabled = false;
            this._audioDevicesGroupBox.Location = new System.Drawing.Point(6, 74);
            this._audioDevicesGroupBox.Name = "_audioDevicesGroupBox";
            this._audioDevicesGroupBox.Size = new System.Drawing.Size(271, 101);
            this._audioDevicesGroupBox.TabIndex = 6;
            this._audioDevicesGroupBox.TabStop = false;
            this._audioDevicesGroupBox.Text = "Audio devices";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(7, 20);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(246, 23);
            this.buttonSelect.TabIndex = 3;
            this.buttonSelect.Text = "Select a microphone...";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.OnSelectMic);
            // 
            // _volumeTrackBar
            // 
            this._volumeTrackBar.Location = new System.Drawing.Point(63, 46);
            this._volumeTrackBar.Name = "_volumeTrackBar";
            this._volumeTrackBar.Size = new System.Drawing.Size(202, 45);
            this._volumeTrackBar.TabIndex = 2;
            this._volumeTrackBar.Value = 10;
            this._volumeTrackBar.ValueChanged += new System.EventHandler(this.OnVolumeTrackBarValueChanged);
            // 
            // _muteCheckBox
            // 
            this._muteCheckBox.AutoSize = true;
            this._muteCheckBox.Location = new System.Drawing.Point(7, 47);
            this._muteCheckBox.Name = "_muteCheckBox";
            this._muteCheckBox.Size = new System.Drawing.Size(50, 17);
            this._muteCheckBox.TabIndex = 1;
            this._muteCheckBox.Text = "Mute";
            this._muteCheckBox.UseVisualStyleBackColor = true;
            this._muteCheckBox.CheckedChanged += new System.EventHandler(this.OnMuteCheckBoxCheckedChanged);
            // 
            // _connectionLabel
            // 
            this._connectionLabel.AutoSize = true;
            this._connectionLabel.Location = new System.Drawing.Point(546, 22);
            this._connectionLabel.Name = "_connectionLabel";
            this._connectionLabel.Size = new System.Drawing.Size(0, 13);
            this._connectionLabel.TabIndex = 3;
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(202, 20);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(75, 23);
            this._connectButton.TabIndex = 1;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this.OnConnectClick);
            // 
            // _serverUrlTextBox
            // 
            this._serverUrlTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AudioDemo.Properties.Settings.Default, "ServerUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._serverUrlTextBox.Location = new System.Drawing.Point(6, 22);
            this._serverUrlTextBox.Name = "_serverUrlTextBox";
            this._serverUrlTextBox.Size = new System.Drawing.Size(190, 20);
            this._serverUrlTextBox.TabIndex = 0;
            this._serverUrlTextBox.Text = global::AudioDemo.Properties.Settings.Default.ServerUrl;
            // 
            // _serverUrlLabel
            // 
            this._serverUrlLabel.AutoSize = true;
            this._serverUrlLabel.Location = new System.Drawing.Point(3, 6);
            this._serverUrlLabel.Name = "_serverUrlLabel";
            this._serverUrlLabel.Size = new System.Drawing.Size(57, 13);
            this._serverUrlLabel.TabIndex = 0;
            this._serverUrlLabel.Text = "Server Url:";
            // 
            // _secureOnlyCheckBox
            // 
            this._secureOnlyCheckBox.AutoSize = true;
            this._secureOnlyCheckBox.Checked = true;
            this._secureOnlyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._secureOnlyCheckBox.Location = new System.Drawing.Point(6, 49);
            this._secureOnlyCheckBox.Name = "_secureOnlyCheckBox";
            this._secureOnlyCheckBox.Size = new System.Drawing.Size(82, 17);
            this._secureOnlyCheckBox.TabIndex = 9;
            this._secureOnlyCheckBox.Text = "Secure only";
            this._secureOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // AudioForm
            // 
            this.AcceptButton = this._connectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 200);
            this.Controls.Add(this._panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AudioForm";
            this.Text = "Milestone XProtect Audio Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClose);
            this._panel.ResumeLayout(false);
            this._panel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this._audioDevicesGroupBox.ResumeLayout(false);
            this._audioDevicesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._volumeTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panel;
        private System.Windows.Forms.Label _connectionLabel;
		private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.TextBox _serverUrlTextBox;
		private System.Windows.Forms.Label _serverUrlLabel;
        private System.Windows.Forms.GroupBox _audioDevicesGroupBox;
        private System.Windows.Forms.CheckBox _muteCheckBox;
		private System.Windows.Forms.TrackBar _volumeTrackBar;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.CheckBox _secureOnlyCheckBox;
    }
}

