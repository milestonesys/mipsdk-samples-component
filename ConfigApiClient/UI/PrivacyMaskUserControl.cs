using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;
using ConfigAPIClient.Util;

namespace ConfigAPIClient.UI
{
    public partial class PrivacyMaskUserControl : UserControl
    {
        private ConfigurationItem _item;
        private BitmapLiveImages _bitmapLiveImages;

        public PrivacyMaskUserControl(ConfigurationItem item, ConfigApiClient configApiClient)
        {
            InitializeComponent();

            _item = item;
            _bitmapLiveImages = new BitmapLiveImages(_item, configApiClient);
            _bitmapLiveImages.ImageReceivedEvent += _bitmapLiveImages_ImageReceivedEvent;
            _bitmapLiveImages.Init();
        }

        void _bitmapLiveImages_ImageReceivedEvent()
        {
            BeginInvoke(new MethodInvoker(Refresh));
        }

        private bool _refreshInProgress = false;
        public new void Refresh()
        {
            if (pictureBox1.Width == 0 || pictureBox1.Height==0)
                return;

            if (_refreshInProgress) return;
            _refreshInProgress = true;
            Bitmap bitmap = _bitmapLiveImages.GetBitmap(pictureBox1.Size);

            BitmapFormatting.PrivacyMaskOverlay(_item, bitmap, false);
            pictureBox1.Image = bitmap;

            _refreshInProgress = false;
        }

        public void Close()
        {
            if (_bitmapLiveImages != null)
                _bitmapLiveImages.Close();
            _bitmapLiveImages = null;
        }
    }
}
