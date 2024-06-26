using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Util
{
    public static class BitmapFormatting
    {
        public static void PrivacyMaskOverlay(ConfigurationItem item, Bitmap bitmap, bool isShowingMotionDetect)
        {
            if (item == null)
                return;

            Property maskProperty = item.Properties.FirstOrDefault<Property>(p => p.Key == "PrivacyMaskRegions");
            Property sizeProperty = item.Properties.FirstOrDefault<Property>(p => p.Key == "GridSize");
            bool enabled = item.EnableProperty != null && item.EnableProperty.Enabled;

            string sizeString = sizeProperty.Value.Substring(sizeProperty.Value.Length - 1);
            int size10 = 0;
            int size = Int32.Parse(sizeString, System.Globalization.CultureInfo.InvariantCulture);
            if (Int32.TryParse(sizeProperty.Value.Substring(sizeProperty.Value.Length - 2), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out size10))
                size = size10;
            String mask = maskProperty.Value;

            if (enabled)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    int maskIx = 0;
                    int boxWidth = bitmap.Width / size;
                    int boxHeight = bitmap.Height / size;
                    Brush fillBrush = new SolidBrush(Color.FromArgb(0x40, Color.Red));
                    Pen fillPen = new Pen(Color.FromArgb(0x20, Color.Red), 1);
                    if (!isShowingMotionDetect)
                        g.DrawRectangle(Pens.Red, 0, 0, bitmap.Width - 1, bitmap.Height - 1);

                    for (int iy = 0; iy < size; iy++)
                    {
                        for (int ix = 0; ix < size; ix++)
                        {
                            int x1 = ix * bitmap.Width / size;
                            int y1 = iy * bitmap.Height / size;
                            if (mask.Length > maskIx && mask[maskIx] == '1')
                                g.FillRectangle(fillBrush, x1, y1, boxWidth, boxHeight);
                            else if (!isShowingMotionDetect)
                                g.DrawRectangle(fillPen, x1, y1, boxWidth, boxHeight);
                            maskIx++;
                        }
                    }

                }
            }

        }

        public static void MotionDetectMaskOverlay(ConfigurationItem item, Bitmap bitmap)
        {
            Property maskProperty = item.Properties.FirstOrDefault<Property>(p => p.Key == "ExcludeRegions");
            Property sizeProperty = item.Properties.FirstOrDefault<Property>(p => p.Key == "GridSize");
            bool enabled = item.EnableProperty != null && item.EnableProperty.Enabled;

            string sizeString = sizeProperty.Value.Substring(sizeProperty.Value.Length - 1);
            int size10 = 0;
            int size = Int32.Parse(sizeString, System.Globalization.CultureInfo.InvariantCulture);
            if (Int32.TryParse(sizeProperty.Value.Substring(sizeProperty.Value.Length - 2), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out size10))
                size = size10;
            String mask = maskProperty.Value;

            if (enabled)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    int maskIx = 0;
                    int boxWidth = bitmap.Width / size;
                    int boxHeight = bitmap.Height / size;
                    Brush fillBrush = new SolidBrush(Color.FromArgb(0x40, Color.Blue));
                    Pen fillPen = new Pen(Color.FromArgb(0x20, Color.Blue), 1);
                    g.DrawRectangle(Pens.Red, 0, 0, bitmap.Width - 1, bitmap.Height - 1);

                    for (int iy = 0; iy < size; iy++)
                    {
                        for (int ix = 0; ix < size; ix++)
                        {
                            int x1 = ix * bitmap.Width / size;
                            int y1 = iy * bitmap.Height / size;
                            if (mask.Length > maskIx && mask[maskIx] == '1')
                                g.FillRectangle(fillBrush, x1, y1, boxWidth, boxHeight);
                            else
                                g.DrawRectangle(fillPen, x1, y1, boxWidth, boxHeight);
                            maskIx++;
                        }
                    }

                }
            }
        }
    }
}
