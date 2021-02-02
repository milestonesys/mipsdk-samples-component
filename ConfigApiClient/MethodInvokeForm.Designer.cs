namespace ConfigAPIClient
{
    partial class MethodInvokeForm
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelActions = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.scrollPanel1 = new ConfigAPIClient.Panels.ScrollPanel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMain.Controls.Add(this.scrollPanel1);
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(556, 395);
            this.panelMain.TabIndex = 1;
            // 
            // panelActions
            // 
            this.panelActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelActions.Location = new System.Drawing.Point(0, 393);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(556, 51);
            this.panelActions.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // scrollPanel1
            // 
            this.scrollPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scrollPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.scrollPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel1.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel1.Name = "scrollPanel1";
            this.scrollPanel1.Size = new System.Drawing.Size(556, 395);
            this.scrollPanel1.TabIndex = 0;
            // 
            // MethodInvokeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 442);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelMain);
            this.Name = "MethodInvokeForm";
            this.Text = "WizardForm";
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Panel panelActions;
		private System.Windows.Forms.Timer timer1;
        private Panels.ScrollPanel scrollPanel1;
    }
}