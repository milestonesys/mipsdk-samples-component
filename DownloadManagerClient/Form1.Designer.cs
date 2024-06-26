namespace MyDownloadManagerClient
{
    partial class Form1
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
            this._addButton = new System.Windows.Forms.Button();
            this._installerPathLabel = new System.Windows.Forms.Label();
            this._installerPathTextBox = new System.Windows.Forms.TextBox();
            this._displayNameLabel = new System.Windows.Forms.Label();
            this._displayNameTextBox = new System.Windows.Forms.TextBox();
            this._groupNameTextBox = new System.Windows.Forms.TextBox();
            this._groupNameLabel = new System.Windows.Forms.Label();
            this._versionTextBox = new System.Windows.Forms.TextBox();
            this._versionLabel = new System.Windows.Forms.Label();
            this._removeButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _addButton
            // 
            this._addButton.Location = new System.Drawing.Point(24, 235);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(244, 24);
            this._addButton.TabIndex = 0;
            this._addButton.Text = "Add this to the Download Manager";
            this._addButton.UseVisualStyleBackColor = true;
            this._addButton.Click += new System.EventHandler(this._addButton_Click);
            // 
            // _installerPathLabel
            // 
            this._installerPathLabel.AutoSize = true;
            this._installerPathLabel.Location = new System.Drawing.Point(22, 90);
            this._installerPathLabel.Name = "_installerPathLabel";
            this._installerPathLabel.Size = new System.Drawing.Size(49, 13);
            this._installerPathLabel.TabIndex = 9;
            this._installerPathLabel.Text = "Filename";
            // 
            // _installerPathTextBox
            // 
            this._installerPathTextBox.Location = new System.Drawing.Point(133, 84);
            this._installerPathTextBox.Name = "_installerPathTextBox";
            this._installerPathTextBox.Size = new System.Drawing.Size(418, 20);
            this._installerPathTextBox.TabIndex = 3;
            this._installerPathTextBox.Text = "MyPlugin.msi";
            // 
            // _displayNameLabel
            // 
            this._displayNameLabel.AutoSize = true;
            this._displayNameLabel.Location = new System.Drawing.Point(22, 148);
            this._displayNameLabel.Name = "_displayNameLabel";
            this._displayNameLabel.Size = new System.Drawing.Size(70, 13);
            this._displayNameLabel.TabIndex = 11;
            this._displayNameLabel.Text = "Display name";
            // 
            // _displayNameTextBox
            // 
            this._displayNameTextBox.Location = new System.Drawing.Point(133, 142);
            this._displayNameTextBox.Name = "_displayNameTextBox";
            this._displayNameTextBox.Size = new System.Drawing.Size(418, 20);
            this._displayNameTextBox.TabIndex = 5;
            this._displayNameTextBox.Text = "MyPlugin";
            // 
            // _groupNameTextBox
            // 
            this._groupNameTextBox.Location = new System.Drawing.Point(134, 113);
            this._groupNameTextBox.Name = "_groupNameTextBox";
            this._groupNameTextBox.Size = new System.Drawing.Size(417, 20);
            this._groupNameTextBox.TabIndex = 4;
            this._groupNameTextBox.Text = "Smart Client Plugins";
            // 
            // _groupNameLabel
            // 
            this._groupNameLabel.AutoSize = true;
            this._groupNameLabel.Location = new System.Drawing.Point(22, 119);
            this._groupNameLabel.Name = "_groupNameLabel";
            this._groupNameLabel.Size = new System.Drawing.Size(65, 13);
            this._groupNameLabel.TabIndex = 10;
            this._groupNameLabel.Text = "Group name";
            // 
            // _versionTextBox
            // 
            this._versionTextBox.Location = new System.Drawing.Point(133, 171);
            this._versionTextBox.Name = "_versionTextBox";
            this._versionTextBox.Size = new System.Drawing.Size(418, 20);
            this._versionTextBox.TabIndex = 6;
            this._versionTextBox.Text = "1.0";
            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Location = new System.Drawing.Point(22, 177);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(42, 13);
            this._versionLabel.TabIndex = 12;
            this._versionLabel.Text = "Version";
            // 
            // _removeButton
            // 
            this._removeButton.Location = new System.Drawing.Point(307, 235);
            this._removeButton.Name = "_removeButton";
            this._removeButton.Size = new System.Drawing.Size(244, 24);
            this._removeButton.TabIndex = 1;
            this._removeButton.Text = "Remove this from the Download Manager";
            this._removeButton.UseVisualStyleBackColor = true;
            this._removeButton.Click += new System.EventHandler(this._removeButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.textBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox1.Location = new System.Drawing.Point(24, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(527, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "This program must be run on a computer with an XProtect Download Manager installe" +
                "d";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 280);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this._removeButton);
            this.Controls.Add(this._versionTextBox);
            this.Controls.Add(this._versionLabel);
            this.Controls.Add(this._groupNameTextBox);
            this.Controls.Add(this._groupNameLabel);
            this.Controls.Add(this._displayNameTextBox);
            this.Controls.Add(this._displayNameLabel);
            this.Controls.Add(this._installerPathTextBox);
            this.Controls.Add(this._installerPathLabel);
            this.Controls.Add(this._addButton);
            this.Name = "Form1";
            this.Text = "Download Manager Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _addButton;
        private System.Windows.Forms.Label _installerPathLabel;
        private System.Windows.Forms.TextBox _installerPathTextBox;
        private System.Windows.Forms.Label _displayNameLabel;
        private System.Windows.Forms.TextBox _displayNameTextBox;
        private System.Windows.Forms.TextBox _groupNameTextBox;
        private System.Windows.Forms.Label _groupNameLabel;
        private System.Windows.Forms.TextBox _versionTextBox;
        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.Button _removeButton;
        private System.Windows.Forms.TextBox textBox1;
    }
}

