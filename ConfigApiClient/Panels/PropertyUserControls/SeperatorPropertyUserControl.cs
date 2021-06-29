using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels.PropertyUserControls
{
    public partial class SeperatorPropertyUserControl : PropertyUserControl
    {
        private int _origY;
        private Form _dropDownForm = null;

        public SeperatorPropertyUserControl()
        {
            InitializeComponent();
        }

        public SeperatorPropertyUserControl(Property property) : base(property)
        {
            InitializeComponent();

            labelOfProperty.Text = property.DisplayName;
            HasChanged = false;
            _origY = button1.Left;

            button1.Enabled = property.IsSettable;
            button1.Text = base.Property.Value??"Select ...";

            if (property.ValueTypeInfos == null)
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenList();
        }

        private void OpenList()
        {
            button1.Hide();

            CheckedListUserControl listBox = new CheckedListUserControl();
            _dropDownForm = new Form()
            {
                ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.None,
                ControlBox = false,
                StartPosition = FormStartPosition.Manual,
                TopMost = true,
                Location = this.PointToScreen(button1.Location),
                Width = button1.Width,
                Height = Math.Min(300, 100 + base.Property.ValueTypeInfos.Length * 18),
            };
            listBox.Size = _dropDownForm.Size;
            listBox.CloseClicked += (s, e) => { _dropDownForm.DialogResult = DialogResult.OK; _dropDownForm.Close(); };

            _dropDownForm.Controls.Add(listBox);

            listBox.FillList(base.Property.ValueTypeInfos, base.Property.Value);
            if (_dropDownForm.ShowDialog() == DialogResult.OK)
            {
                string newValue = listBox.GetSelections();
                if (base.Property.Value != newValue)
                {
                    base.Property.Value = newValue;
                    ValueChanged(this, new EventArgs());
                }
            }
            _dropDownForm.Dispose();
            button1.Text = base.Property.Value;
            button1.Show();
        }
        internal override int LeftIndent
        {
            set { button1.Left = _origY - value; }
        }

        private void OnCheckChanged(object sender, EventArgs e)
        {
            HasChanged = true;
            if (ValueChanged != null)
            {/*
                if (comboBox1.SelectedItem != null)
                {
                    Property.Value = ((TagItem)comboBox1.SelectedItem).Value.ToString();
                    ValueChanged(this, new EventArgs());
                }*/
            }
        }
    }

}
