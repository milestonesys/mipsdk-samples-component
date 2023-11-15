using System;
using System.Windows.Controls;
using VideoOS.Platform;

namespace MatrixServer
{
    /// <summary>
    /// Interaction logic for MatrixViewItem.xaml
    /// </summary>
    public partial class MatrixViewItem : UserControl
    {
        private Item _item;

        public MatrixViewItem()
        {
            InitializeComponent();
        }

        public Item CameraItem
        {
            get { return _item; }
            set
            {
                if (value != null || _item != null)
                {
                    // Get to UI thread
                    Dispatcher.Invoke(() =>
                    {
                        PerformDisconnect();
                        _item = value;
                        if (value != null)
                        {
                            PerformConnect();
                        }
                    });
                }
            }
        }

        private void PerformDisconnect()
        {
            _imageViewer.Disconnect();
            _imageViewer.Close();
        }

        private void PerformConnect()
        {
            _imageViewer.CameraFQID = _item.FQID;
            _imageViewer.EnableVisibleHeader = true;
            _imageViewer.EnableMousePtzEmbeddedHandler = true;
            _imageViewer.Initialize();
            _imageViewer.Connect();
            _imageViewer.Selected = true;
            _imageViewer.EnableDigitalZoom = true;
        }
    }
}
