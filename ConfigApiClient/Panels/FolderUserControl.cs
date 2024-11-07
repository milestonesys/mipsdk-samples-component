using System.Windows.Forms;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
    public partial class FolderUserControl : UserControl
    {
        public FolderUserControl(ConfigurationItem item, ConfigurationItem[] childrens)
        {
            InitializeComponent();

            if (childrens != null && childrens.Length > 0)
            {
                ConfigurationItem first = childrens[0];
                int cols = 0;
                int colsNormal = 0;
                int importanceToShow = 2;
                if (first.Properties != null)
                {
                    foreach (Property pi in first.Properties)       // We assume here that the children have same settings!
                    {
                        if (pi.UIImportance == 2)
                        {
                            cols++;
                        }
                        if (pi.UIImportance == 0)
                        {
                            colsNormal++;
                        }
                    }
                    if (cols == 0)
                    {
                        importanceToShow = 0;
                        cols = colsNormal;
                    }
                }

                if (cols > 0)
                {
                    tableLayoutPanel1.ColumnCount = cols;
                    tableLayoutPanel1.RowCount = childrens.Length + 1;

                    for (int col = 0; col < cols; col++)
                    {
                        if (col < tableLayoutPanel1.ColumnStyles.Count)
                            tableLayoutPanel1.ColumnStyles[col] = new ColumnStyle(SizeType.AutoSize);
                        else
                            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    }

                    for (int ix = 0; ix < childrens.Length; ix++)
                    {
                        ConfigurationItem child = childrens[ix];
                        if (MainForm._navItemTypes.Contains(child.ItemType))    // Do not repeat what is on the navigation tree
                            continue;

                        int iy = 0;

                        if (ix < tableLayoutPanel1.RowStyles.Count)
                        {
                            tableLayoutPanel1.RowStyles[ix] = new RowStyle(SizeType.AutoSize);
                        }
                        else
                        {
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        }
                        foreach (Property pi in child.Properties) // We assume here that the children have same settings!
                        {
                            if (pi.UIImportance == importanceToShow)
                            {
                                if (ix == 0)
                                {
                                    tableLayoutPanel1.Controls.Add(MakeControlName(pi), iy, 0);
                                }
                                tableLayoutPanel1.Controls.Add(MakeControl(pi), iy, ix + 1);
                                iy++;
                            }
                        }
                    }
                    tableLayoutPanel1.Height = 25 * (childrens.Length + 1) + 1;
                }
            }
        }

        private Control MakeControl(Property pi)
        {
            TextBox tb = new TextBox();
            tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb.Dock = DockStyle.Fill;
            tb.Text = pi?.Value?.ToString() ?? "";
            tb.ReadOnly = !pi.IsSettable;
            return tb;
        }
        private Control MakeControlName(Property pi)
        {
            TextBox tb = new TextBox();
            tb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tb.Dock = DockStyle.Fill;
            tb.Text = pi.DisplayName;
            tb.ReadOnly = true;
            return tb;
        }
    }
}
