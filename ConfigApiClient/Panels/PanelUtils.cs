using System;
using System.Drawing;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;
using ConfigAPIClient.Panels.PropertyUserControls;

namespace ConfigAPIClient.Panels
{
	public class PanelUtils
	{
        internal static int BuildPropertiesUI(ConfigurationItem item, int top, int offset, Control parent, EventHandler valueChangedHandler, ConfigApiClient configApiClient, string toHaveFocus = null)
        {
            if (item.Properties != null)
            {
                foreach (Property property in item.Properties)
                {
                    if (property.UIImportance == 2 || MainForm.ShowHiddenProperties || property.UIImportance == 0 && MainForm.Advanced)
                    {
                        PropertyUserControl uc;
                        switch (property.ValueType)
                        {
                            case ValueTypes.IntType:
                                uc = new IntPropertyUserControl(property);
                                break;
                            case ValueTypes.DoubleType:
                                uc = new DoublePropertyUserControl(property);
                                break;
                            case ValueTypes.TickType:
                                uc = new TickPropertyUserControl(property);
                                break;
                            case ValueTypes.EnumType:
                                uc = new EnumPropertyUserControl(property);
                                break;
                            case ValueTypes.SliderType:
                                uc = new SliderPropertyUserControl(property);
                                break;
                            case ValueTypes.Path:
                                uc = new PathPropertyUserControl(property, configApiClient);
                                break;
                            case ValueTypes.PathList:
                                uc = new PathListPropertyUserControl(property, configApiClient);
                                break;
                            case ValueTypes.DateTimeType:
                            case "Date":
                            case "Time":
                                if (property.IsSettable)
                                    uc = new DateTimePickerPropertyUserControl(property);
                                else
                                    uc = new DateTimeDisplayPropertyUserControl(property);
                                break;

                            case ValueTypes.SeparatorType:
                                uc = new SeperatorPropertyUserControl(property);
                                break;

                            case ValueTypes.Array:
                                uc = new ArrayPropertyUserControl(property);
                                break;

                            case ValueTypes.ProgressType:
                            case ValueTypes.StringType:
                            default:
                                uc = new StringPropertyUserControl(property);
                                break;
                        }
                        uc.Location = new Point(0, top);
                        uc.Tag = property;
                        uc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        uc.ValueChanged += new EventHandler(valueChangedHandler);
                        uc.LeftIndent = offset;
                        if (property.ToolTipTranslationId != null)
                        {
                            uc.ToolTip = configApiClient.Translate(property.ToolTipTranslationId);
                        }

                        ScrollPanel sp = parent as ScrollPanel;
                        if (sp != null)
                        {
                            sp.Add(uc);
                        }
                        else
                        {
                            parent.Controls.Add(uc);
                            if (uc.Top + uc.Height > parent.Height)
                            {
                                parent.Height = uc.Top + uc.Height;
                            }
                        }

                        top += uc.Height;

                        if (property.Key == toHaveFocus)
                            uc.WeGotFocus(null, null);
                    }
                }
            }
            return top;
        }
	}
}
