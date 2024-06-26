using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels.PropertyUserControls
{
    public partial class CheckedListUserControl : UserControl
    {
        public EventHandler CloseClicked;

        public CheckedListUserControl()
        {
            InitializeComponent();
        }

        public void FillList(ValueTypeInfo[] valueTypeInfos, string selectedString)
        {
            string[] selected = selectedString.Split(';');
            List<string> selectedList = new List<string>();
            foreach (string s in selected)
            {
                if (!string.IsNullOrWhiteSpace(s))
                    selectedList.Add(s.Trim());
            }

            foreach (var vti in valueTypeInfos)
            {
                checkedListBox1.Items.Add(new TagItem(vti.Name, vti.Value), selectedList.Contains(vti.Value));
            }
        }

        public string GetSelections()
        {
            string r = "";
            foreach (TagItem i in checkedListBox1.CheckedItems)
            {
                if (r != "")
                    r += ";";
                r += i.Value;
            }
            return r;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            CloseClicked(this, null);
        }
    }

}
