using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VideoOS.Platform.SDK.Proxy.RecorderServices;
using VideoOS.Platform.UI;
using JPEGData = VideoOS.Platform.Data.JPEGData;

namespace SmartSearch
{
    public partial class MainForm : Form
    {
        private Item _selectItem;
        private JPEGVideoSource _jpegVideoSource;
        private bool _mouseIsDown = false;
        private int _normX1;
        private int _normX2;
        private int _normY1;
        private int _normY2;

        private TimeSpan _duration = TimeSpan.FromHours(1.0);
        private Bitmap _cameraBitmap;
        private const int GridSize = 4;
        private int[,] _searchMask = new int[GridSize, GridSize];
        private SearchResult _searchResult;

        public MainForm()
        {
            InitializeComponent();
            CheckServerVersionForVMDAreaMask();
        }

        private void OnClose(object sender, EventArgs e)
        {
            VideoOS.Platform.SDK.Environment.RemoveAllServers();
            Close();
        }

        /// <summary>
        /// Uses item picker for choosing camera
        /// Initializes a JpegVideoSource with that camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPickCamera_Click(object sender, EventArgs e)
        {
			if (_cameraBitmap!=null)
				_cameraBitmap.Dispose();
	        _cameraBitmap = null;

            ItemPickerForm form = new ItemPickerForm();
            form.KindFilter = Kind.Camera;
            form.AutoAccept = true;
            form.Init(Configuration.Instance.GetItems());
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                
                _selectItem = form.SelectedItem;
                buttonPickCamera.Text = _selectItem.Name; 
                _jpegVideoSource = new JPEGVideoSource(_selectItem);
                _jpegVideoSource.Init();
				ShowImageFrom(DateTime.UtcNow);
                buttonClear.Enabled = true;
                buttonSearch.Enabled = true;
            }
        }
        #region draw image
        /// <summary>
        /// Retrieves image using the _JpegVideoSource
        /// If succes converts the Jpeg to a bitmap and calls routine to draw it
        /// If no image is retrieved a routine to draw a error message is called
        /// No image means that the system has no recorded images from that camera
        /// </summary>
        /// <param name="time"></param>
	    private void ShowImageFrom(DateTime time)
	    {
			JPEGData jpegData = _jpegVideoSource.GetAtOrBefore(time) as JPEGData;
			if (jpegData != null)
			{
			    MemoryStream ms = new MemoryStream(jpegData.Bytes);
			    _cameraBitmap = new Bitmap(ms);
			    Drawgraphics();
			    ms.Dispose();
			}
			else
			{
			    DrawNoGraphics();
			}
	    }
        /// <summary>
        /// Draw an error message
        /// No image means that the system has no recorded images from that camera
        /// </summary>
        private void DrawNoGraphics()
        {
            Bitmap noImage = new Bitmap(160,100);
            Graphics g = Graphics.FromImage(noImage);
            g.Clear(Color.Black);
            Font myFont = new Font(FontFamily.GenericSansSerif, (float)10.0);
            TextRenderer.DrawText(g, "No image from camera.", myFont, new Point(0, 45),
                                    Color.White);
            
            if (noImage.Width != pictureBox.Width || noImage.Height != pictureBox.Height)
            {
                pictureBox.Image = new Bitmap(noImage, pictureBox.Size);
            }
            else
            {
                pictureBox.Image = noImage;
            }
        }
        /// <summary>
        /// Draw the image from the _cameraBitmap
        /// Draw an overlay grid
        /// Draw overlay hatch based on _searchMask 
        /// If mouse is down draw a rectangle, UI aid for picking out a rectangle
        /// If showing a search result and checkBoxOverlay.Checked draw overlay image with the result mask 
        /// which show where the motion was detected.
        /// </summary>
        private void Drawgraphics()
        {
            Bitmap finalImage = new Bitmap(_cameraBitmap.Width, _cameraBitmap.Height);
            using (Graphics g = Graphics.FromImage(finalImage))
            {
                g.DrawImage(_cameraBitmap, new Point(0, 0));
                for (int i = 1; i < GridSize; i++)
                {
                    g.DrawLine(new Pen(Color.Aqua, 1.0F), new Point(0, (int) _cameraBitmap.Height/GridSize*i),
                               new Point(_cameraBitmap.Width, (int) _cameraBitmap.Height/GridSize*i));
                    g.DrawLine(new Pen(Color.Aqua, 1.0F), new Point((int) _cameraBitmap.Width/GridSize*i, 0),
                               new Point(_cameraBitmap.Width/GridSize*i, (int) _cameraBitmap.Height));
                }


                for (int x = 0; x < GridSize; x++)
                {
                    for (int y = 0; y < GridSize; y++)
                    {
                        if (_searchMask[x, y] == 1)
                        {
                            g.FillRectangle(new HatchBrush(HatchStyle.Percent25, Color.Red, Color.Transparent),
                                            x*_cameraBitmap.Width/GridSize, y*_cameraBitmap.Height/GridSize,
                                            _cameraBitmap.Width/GridSize, _cameraBitmap.Height/GridSize);
                        }
                    }
                }

                if (_mouseIsDown == true)
                {
                    Rectangle rectangle = new Rectangle(_normX1*_cameraBitmap.Width/GridSize, _normY1*_cameraBitmap.Height/GridSize,
                                                        (_normX2 - _normX1+1)*_cameraBitmap.Width/GridSize,
                                                        (_normY2 - _normY1+1)*_cameraBitmap.Height/GridSize);
                    g.DrawRectangle(new Pen(Color.Red),rectangle );
                }
                if (checkBoxOverlay.Checked)
                {
                    if (_searchResult != null)
                    {
                        double heightRatio = (double)finalImage.Height / _searchResult.Resolution.Height;
                        double widthRatio = (double)finalImage.Width / _searchResult.Resolution.Width;
                        float penWidth = 3;
                        foreach (var result in _searchResult.MotionAreas)
                        {
                            var rectangle = new Rectangle((int)(result.X*widthRatio), (int)(result.Y*heightRatio), (int)(result.Width*widthRatio), (int)(result.Height*heightRatio));
                            g.DrawRectangle(new Pen(Color.Green, penWidth), rectangle);
                        }
                    }
                }

            }
            if (finalImage.Width != pictureBox.Width || finalImage.Height != pictureBox.Height)
            {
                pictureBox.Image = new Bitmap(finalImage, pictureBox.Size);
            }
            else
            {
                pictureBox.Image = finalImage;
            }
        }
        #endregion
        #region mouse control
        /// <summary>
        /// Used for marking area of the image displayed
        /// Defines starting point (_normX1,_normY1) and tells mouse is down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _searchResult = null;
            _mouseIsDown = true;
            _normX1 = (int) (((double) GridSize*(double) e.X)/(double) pictureBox.Width);
            _normY1 = (int) (Math.Round((double) GridSize*(double) e.Y)/(double) pictureBox.Height);
        }
        /// <summary>
        /// Defines ending point (_normX2,_normY2) refreshes images drawing a rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseIsDown == true)
            {
                _normX2 = (int) (Math.Round((double) GridSize*(double) e.X)/(double) pictureBox.Width);
                if (_normX2 >= GridSize) _normX2 = GridSize - 1;
                _normY2 = (int) (Math.Round((double) GridSize*(double) e.Y)/(double) pictureBox.Height);
                if (_normY2 >= GridSize) _normY2 = GridSize - 1;
                Drawgraphics();
            }
        }
        /// <summary>
        /// From the two points fills the _searchMask
        /// Refresh image with overlay of rectangle area chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseIsDown = false;
            for (int x = _normX1; x <= _normX2; x++)
            {
                for (int y = _normY1; y <= _normY2; y++)
                {
                     _searchMask[x, y] = 1;
                }
            }
            Drawgraphics();
        }
        #endregion
        #region search
        private Guid _searchId;
	    private RCSClient _rcsClient;
        /// <summary>
        /// Setup and start the search
        /// Start the timer that controls the fetching of status and results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
            _searchResult = null;
			try
			{
				listBoxResult.Items.Clear();

				_rcsClient = new RCSClient();
				StringBuilder sb = new StringBuilder();
				for (int y = 0; y < GridSize; y++)		// Note y first, then x
				{
					for (int x = 0; x < GridSize; x++)
					{
						sb.Append(_searchMask[x, y]);
					}
				}

				_searchId = _rcsClient.StartSearch(_selectItem, DateTime.UtcNow - _duration, DateTime.UtcNow, hScrollBar1.Value,
					TimeSpan.FromSeconds(2), sb.ToString(), GridSize, GridSize);
				timer1.Start();
				buttonSearch.Enabled = false;
				buttonClear.Enabled = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Timer handler gets status, when status is right it gets search results
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTimerTick(object sender, EventArgs e)
		{   
			try
			{
				// Notice: This will execute on the UI thread - not recommended for real use!
				var status = _rcsClient.GetStatus(_searchId);
				switch (status)
				{
					case SmartSearchStatusType.SearchResultReady:
						SearchResult result = _rcsClient.GetSearchResult(_searchId, true);
						if (result != null)
						{
							listBoxResult.Items.Add(result);
						}
						break;
					case SmartSearchStatusType.SearchEndTimeReached:
						listBoxResult.Items.Add("Done");
						SetSearchStopped();
						break;
					case SmartSearchStatusType.UnspecifiedError:
						listBoxResult.Items.Add("Error ...");
						SetSearchStopped();
						break;
					case SmartSearchStatusType.SearchCancelled:
						listBoxResult.Items.Add("Canceled ...");
						SetSearchStopped();
						break;
				}
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				SetSearchStopped();
				_searchId = Guid.Empty;
			}
		}
        
        private void SetSearchStopped()
	    {
			buttonSearch.Enabled = true;
			buttonClear.Enabled = true;
			timer1.Stop();    
	    }
        #endregion
        /// <summary>
        /// clear the searchMask and display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            _searchResult = null;
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    _searchMask[x, y] = 0;
                }
            }
            ShowImageFrom(DateTime.UtcNow);
            Drawgraphics();
        }

        /// <summary>
        /// If a time is chosen in the list box then an image from that time will be displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			int ix = listBoxResult.SelectedIndex;
			if (ix >= 0)
			{
				SearchResult result = listBoxResult.Items[ix] as SearchResult;
				if (result != null)
				{
				    _searchResult = result;
					ShowImageFrom(result.Time);
				}
			}
		}

        private void checkBoxOverlay_CheckedChanged(object sender, EventArgs e)
        {
            if(_searchResult!=null) ShowImageFrom(_searchResult.Time);
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            switch (timeUnitSelctor.Text)
            {
                case "Hours":
                    _duration = TimeSpan.FromHours((double)timeNumericSelector.Value);
                    break;
                case "Days":
                    _duration = TimeSpan.FromDays((double) timeNumericSelector.Value);
                    break;
                case "Minutes":
                    _duration = TimeSpan.FromMinutes((double) timeNumericSelector.Value);
                    break;

            }
        }
        /// <summary>
        /// In XProtect Corporate 7.0  and XProtect Expert 7.0 a new Smart Search was introduced.
        /// This uses another format for returning the areas with motion detected.
        /// This sample will only turn on the option to use overlay for the versions
        /// that use the newer format.
        /// </summary>
        private void CheckServerVersionForVMDAreaMask()
        {
            List<Item> lServ = Configuration.Instance.GetItemsByKind(Kind.Server);
            Item server = lServ[0];
            if (server.FQID.ServerId.ServerType == "XPCO") //best method?
            {
                //ProductVersion in XPE is formatted [0-9].[0-9][a-z] eg 8.5a
                //ProductVersion in XPCO is formatted [0-9].[0-9].[0-9].[0-9] eg 6.1.0.1
                //ServerType is Express = 250, Professional = 300, Enterprise = 400, ( Expert = 500, Corporate = 600 )
                string productVersionString = server.Properties["ProductVersion"];
                string servertypeString = server.Properties["ServerType"];
                string[] s = productVersionString.Split('.');
                int major = Convert.ToInt32(s[0]);
                int minor = Convert.ToInt32(s[1].Substring(0, 1));
                if (((servertypeString == "500" || servertypeString == "600") && (major >= 7)))
                {
                    checkBoxOverlay.Enabled = true;
                }
            }
        }
    }
}
