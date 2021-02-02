using System;
using System.Drawing;
using System.Linq;
using VideoOS.ConfigurationAPI;
using VideoOS.Platform;
using VideoOS.Platform.Client;

namespace ConfigAPIClient.Util
{
    public class BitmapLiveImages
    {
        private ConfigurationItem _item;
        private BitmapSource _bitmapSource;
        private ConfigApiClient _configApiClient;
        private Bitmap _lastBitmap;

        public delegate void ImageReceived();
        public event ImageReceived ImageReceivedEvent;

        public object _imageLock = new object();

        public BitmapLiveImages(ConfigurationItem item, ConfigApiClient configApiClient)
        {
            _item = item;
            _configApiClient = configApiClient;
            lock (_imageLock)
            {
                _lastBitmap = new Bitmap(320, 240);

                Graphics g = Graphics.FromImage(_lastBitmap);
                g.FillRectangle(Brushes.Black, 0, 0, _lastBitmap.Width, _lastBitmap.Height);
                g.DrawString("no images", new Font(FontFamily.GenericMonospace, 12), Brushes.White, new PointF(20, 100));
                g.Dispose();
            }
        }

        public void Init()
        {
            ConfigurationItem privacyFolder = _configApiClient.GetItem(_item.ParentPath);
            ConfigurationItem camera = _configApiClient.GetItem(privacyFolder.ParentPath);
            Property cameraId = camera.Properties.FirstOrDefault<Property>(p => p.Key == "Id");

            Item cameraItem = Configuration.Instance.GetItem(new Guid(cameraId.Value), Kind.Camera);

            _bitmapSource = new BitmapSource();
            _bitmapSource.Init();
            _bitmapSource.Item = cameraItem;
            _bitmapSource.NewBitmapEvent += BitmapSource_NewBitmapEvent;
            _bitmapSource.LiveStart();
        }


        private void BitmapSource_NewBitmapEvent(Bitmap bitmap)
        {
            try
            {
                Bitmap oldBitmap;
                lock (_imageLock)
                {
                    oldBitmap = _lastBitmap;
                    _lastBitmap = new Bitmap(bitmap);
                }
                ImageReceivedEvent?.Invoke();

                oldBitmap?.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnImageReceived, Exception:" + ex.Message);
            }
        }


        public void Close()
        {
            _bitmapSource.LiveStop();
        }


        public Bitmap GetBitmap(Size imageSize)
        {
            lock (_imageLock)
            {
                return new Bitmap(_lastBitmap, imageSize);
            }
        }
    }
}
