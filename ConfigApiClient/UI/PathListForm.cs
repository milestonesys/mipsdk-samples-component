using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
    public partial class PathListForm : Form
    {
        private Property _property;
        private ConfigApiClient _configApiClient;
        private List<string> itemTypes = new List<string>();
        private bool _allowAll = false;

        internal EventHandler ValueChanged = delegate { };

        public PathListForm()
        {
            InitializeComponent();
        }

        public void Init(Property property, ConfigApiClient configApiClient)
        {
            _property = property;
            _configApiClient = configApiClient;

            this.Text = property.DisplayName;

            if (property.IsSettable)
            {
                if (property.ValueTypeInfos == null)
                    throw new Exception("ValueTypeInfo is null on Path");

                string text = "";
                foreach (ValueTypeInfo valueTypeInfo in property.ValueTypeInfos)
                {
                    if (valueTypeInfo.Name == ValueTypeInfoNames.PathItemType)
                    {
                        var valueTypes = valueTypeInfo.Value.Split(',');
                        foreach (var valueType in valueTypes)
                        {
                            var itemType = valueType;
                            if (itemType.EndsWith("Folder"))
                                itemType = itemType.Substring(0, itemType.Length - 6);
                            itemTypes.Add(itemType);
                            if (!String.IsNullOrEmpty(text))
                                text += ", ";
                            text += itemType;
                        }
                    }
                    if (valueTypeInfo.Name == ValueTypeInfoNames.PathAllowAllItemType)
                        _allowAll = true;
                }
                buttonAdd.Enabled = true;
                buttonRemove.Enabled = false;
            }
            else
            {
                buttonAdd.Enabled = false;
                buttonRemove.Enabled = false;
            }
            if (!string.IsNullOrEmpty(property.Value))
            {
                string[] paths = property.Value.Split(';');
                foreach (string path in paths)
                {
                    listBox1.Items.Add(path);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            List<ConfigurationItem> top = new List<ConfigurationItem>();
            if (itemTypes.Count == 0 && _allowAll)
            {
                itemTypes.Add("Camera");
                itemTypes.Add("Microphone");
                itemTypes.Add("Speaker");
                itemTypes.Add("Input");
                itemTypes.Add("Output");
                itemTypes.Add("Metadata");
            }
            foreach (string itemType in itemTypes)
            {
                List<ConfigurationItem> top2 = _configApiClient.GetTopForItemType(itemType);
                foreach (var newItem in top2)
                {
                    bool found = false;
                    foreach (var currItem in top)
                    {
                        if (currItem.Path == newItem.Path)
                            found = true;
                    }
                    if (!found)
                        top.Add(newItem);
                }
            }

            MemberPicker picker = new MemberPicker(top, itemTypes, _allowAll, _configApiClient);
            if (picker.ShowDialog() == DialogResult.OK)
            {
                ConfigurationItem selectedItem = picker.SelectedConfigurationItem;
                if (selectedItem != null)
                {
                    listBox1.Items.Add(selectedItem.Path);      // selectedItem.DisplayName;
                }
                else
                if (picker.SelectedAllItem != null)
                {
                    listBox1.Items.Add(picker.SelectedAllItem);
                }
                SaveProperty();
                ValueChanged(this, e);
            }

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string path = (string)listBox1.SelectedItem;
                listBox1.Items.Remove(listBox1.SelectedItem);
                SaveProperty();
                ValueChanged(this, e);
            }
        }

        private void SaveProperty()
        {
            string v = "";
            foreach (string p in listBox1.Items)
            {
                if (v != "") v += ";";
                v += p;
            }
            _property.Value = v;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemove.Enabled = _property.IsSettable && listBox1.SelectedItem != null;
        }
    }
}
