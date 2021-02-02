using System.Collections.Generic;
using VideoOS.Platform;

namespace ConfigAccessViaSDK
{ 
	partial class DevicePickerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevicePickerForm));
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.itemPickerUserControl = new VideoOS.Platform.UI.ItemPickerUserControl();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(536, 422);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.OnCancel);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(442, 422);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.OnOK);
			// 
			// itemPickerUserControl
			// 
			this.itemPickerUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.itemPickerUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.itemPickerUserControl.BackColor = System.Drawing.Color.WhiteSmoke;
			this.itemPickerUserControl.CategoryUserSelectable = true;
			this.itemPickerUserControl.Font = new System.Drawing.Font("Arial", 9.25F);
			this.itemPickerUserControl.GroupTabVisible = true;
			this.itemPickerUserControl.ItemsSelected = ((System.Collections.Generic.List<VideoOS.Platform.Item>)(resources.GetObject("itemPickerUserControl.ItemsSelected")));
			this.itemPickerUserControl.KindUserSelectable = true;
			this.itemPickerUserControl.Location = new System.Drawing.Point(0, 0);
			this.itemPickerUserControl.Name = "itemPickerUserControl";
			this.itemPickerUserControl.ServerTabVisible = true;
			this.itemPickerUserControl.ShowDisabledItems = false;
			this.itemPickerUserControl.SingleSelect = false;
			this.itemPickerUserControl.Size = new System.Drawing.Size(632, 408);
			this.itemPickerUserControl.TabIndex = 0;
			// 
			// DevicePickerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(633, 460);
			this.Controls.Add(this.itemPickerUserControl);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Name = "DevicePickerForm";
			this.Text = "Device Picker";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private VideoOS.Platform.UI.ItemPickerUserControl itemPickerUserControl;
	}
}