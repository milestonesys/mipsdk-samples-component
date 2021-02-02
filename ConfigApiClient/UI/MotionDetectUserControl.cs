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
    public partial class MotionDetectUserControl : UserControl
    {
        private ConfigurationItem _item, _privacyMaskItem;

        private BitmapLiveImages _bitmapLiveImages;

        public MotionDetectUserControl(ConfigurationItem item, ConfigurationItem privacyMask, ConfigApiClient configApiClient)
        {
            InitializeComponent();

            _item = item;
            _privacyMaskItem = privacyMask;
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

            BitmapFormatting.MotionDetectMaskOverlay(_item, bitmap);
            BitmapFormatting.PrivacyMaskOverlay(_privacyMaskItem, bitmap, true);

            pictureBox1.Image = new Bitmap(bitmap, pictureBox1.Width, pictureBox1.Height);

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
