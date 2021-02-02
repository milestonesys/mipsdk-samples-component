namespace TriggerAnalyticsEventSDK
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
            this.btnSelectEventSource = new System.Windows.Forms.Button();
            this.lblAnalyticsEventName = new System.Windows.Forms.Label();
            this.txtAnalyticsEventName = new System.Windows.Forms.TextBox();
            this.btnSendAnalyticsEvent = new System.Windows.Forms.Button();
            this.chkIncludeOverlay = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSelectEventSource
            // 
            this.btnSelectEventSource.Location = new System.Drawing.Point(12, 12);
            this.btnSelectEventSource.Name = "btnSelectEventSource";
            this.btnSelectEventSource.Size = new System.Drawing.Size(228, 23);
            this.btnSelectEventSource.TabIndex = 0;
            this.btnSelectEventSource.Text = "Select event source";
            this.btnSelectEventSource.UseVisualStyleBackColor = true;
            this.btnSelectEventSource.Click += new System.EventHandler(this.btnSelectEventSource_Click);
            // 
            // lblAnalyticsEventName
            // 
            this.lblAnalyticsEventName.AutoSize = true;
            this.lblAnalyticsEventName.Location = new System.Drawing.Point(12, 61);
            this.lblAnalyticsEventName.Name = "lblAnalyticsEventName";
            this.lblAnalyticsEventName.Size = new System.Drawing.Size(108, 13);
            this.lblAnalyticsEventName.TabIndex = 1;
            this.lblAnalyticsEventName.Text = "Analytics event name";
            // 
            // txtAnalyticsEventName
            // 
            this.txtAnalyticsEventName.Location = new System.Drawing.Point(12, 77);
            this.txtAnalyticsEventName.Name = "txtAnalyticsEventName";
            this.txtAnalyticsEventName.Size = new System.Drawing.Size(191, 20);
            this.txtAnalyticsEventName.TabIndex = 2;
            this.txtAnalyticsEventName.Text = "MyAnalyticsEvent01";
            // 
            // btnSendAnalyticsEvent
            // 
            this.btnSendAnalyticsEvent.Location = new System.Drawing.Point(12, 122);
            this.btnSendAnalyticsEvent.Name = "btnSendAnalyticsEvent";
            this.btnSendAnalyticsEvent.Size = new System.Drawing.Size(130, 23);
            this.btnSendAnalyticsEvent.TabIndex = 3;
            this.btnSendAnalyticsEvent.Text = "&Send analytics event";
            this.btnSendAnalyticsEvent.UseVisualStyleBackColor = true;
            this.btnSendAnalyticsEvent.Click += new System.EventHandler(this.btnSendAnalyticsEvent_Click);
            // 
            // chkIncludeOverlay
            // 
            this.chkIncludeOverlay.AutoSize = true;
            this.chkIncludeOverlay.Location = new System.Drawing.Point(148, 126);
            this.chkIncludeOverlay.Name = "chkIncludeOverlay";
            this.chkIncludeOverlay.Size = new System.Drawing.Size(98, 17);
            this.chkIncludeOverlay.TabIndex = 4;
            this.chkIncludeOverlay.Text = "Include &overlay";
            this.chkIncludeOverlay.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 192);
            this.Controls.Add(this.chkIncludeOverlay);
            this.Controls.Add(this.btnSendAnalyticsEvent);
            this.Controls.Add(this.txtAnalyticsEventName);
            this.Controls.Add(this.lblAnalyticsEventName);
            this.Controls.Add(this.btnSelectEventSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Trigger Analytics Event";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSelectEventSource;
		private System.Windows.Forms.Label lblAnalyticsEventName;
		private System.Windows.Forms.TextBox txtAnalyticsEventName;
		private System.Windows.Forms.Button btnSendAnalyticsEvent;
		private System.Windows.Forms.CheckBox chkIncludeOverlay;
	}
}

