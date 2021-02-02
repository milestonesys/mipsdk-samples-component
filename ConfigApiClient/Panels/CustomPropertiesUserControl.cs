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

namespace ConfigAPIClient.Panels
{
    public partial class CustomPropertiesUserControl : UserControl
    {
        ConfigurationItem _item;

        public CustomPropertiesUserControl(ConfigurationItem item)
        {
            InitializeComponent();

            _item = item;
            foreach (ConfigurationItem child in item.Children)
            {
                listView1.Items.Add("--- Item: " + child.Path);
                foreach (Property p in child.Properties)
                {
                    listView1.Items.Add("Key=" + p.Key + " = " + p.Value);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //_item.Children.Add(new ConfigurationItem() )
        }
    }
}
