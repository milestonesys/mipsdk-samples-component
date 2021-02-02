namespace TransactClient
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonPick = new System.Windows.Forms.Button();
            this.labelSelectedItem = new System.Windows.Forms.Label();
            this.textTransactions = new System.Windows.Forms.TextBox();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonLive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(713, 715);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.OnClose);
            // 
            // buttonPick
            // 
            this.buttonPick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPick.Location = new System.Drawing.Point(12, 715);
            this.buttonPick.Name = "buttonPick";
            this.buttonPick.Size = new System.Drawing.Size(145, 23);
            this.buttonPick.TabIndex = 1;
            this.buttonPick.Text = "Pick transact source";
            this.buttonPick.UseVisualStyleBackColor = true;
            this.buttonPick.Click += new System.EventHandler(this.buttonPick_Click);
            // 
            // labelSelectedItem
            // 
            this.labelSelectedItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedItem.AutoSize = true;
            this.labelSelectedItem.Location = new System.Drawing.Point(163, 720);
            this.labelSelectedItem.Name = "labelSelectedItem";
            this.labelSelectedItem.Size = new System.Drawing.Size(64, 13);
            this.labelSelectedItem.TabIndex = 2;
            this.labelSelectedItem.Text = "Source: n/a";
            // 
            // textTransactions
            // 
            this.textTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTransactions.BackColor = System.Drawing.SystemColors.Info;
            this.textTransactions.Location = new System.Drawing.Point(12, 12);
            this.textTransactions.Multiline = true;
            this.textTransactions.Name = "textTransactions";
            this.textTransactions.ReadOnly = true;
            this.textTransactions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textTransactions.Size = new System.Drawing.Size(776, 672);
            this.textTransactions.TabIndex = 3;
            // 
            // textSearch
            // 
            this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textSearch.Location = new System.Drawing.Point(12, 690);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(490, 20);
            this.textSearch.TabIndex = 4;
            this.textSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textSearch_KeyUp);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSearch.Location = new System.Drawing.Point(508, 690);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(96, 21);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonLive
            // 
            this.buttonLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLive.Location = new System.Drawing.Point(610, 690);
            this.buttonLive.Name = "buttonLive";
            this.buttonLive.Size = new System.Drawing.Size(96, 21);
            this.buttonLive.TabIndex = 6;
            this.buttonLive.Text = "Back to live";
            this.buttonLive.UseVisualStyleBackColor = true;
            this.buttonLive.Click += new System.EventHandler(this.buttonLive_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 750);
            this.Controls.Add(this.buttonLive);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textSearch);
            this.Controls.Add(this.textTransactions);
            this.Controls.Add(this.labelSelectedItem);
            this.Controls.Add(this.buttonPick);
            this.Controls.Add(this.buttonClose);
            this.Name = "MainForm";
            this.Text = "TransactClient Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonPick;
        private System.Windows.Forms.Label labelSelectedItem;
        private System.Windows.Forms.TextBox textTransactions;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonLive;
    }
}