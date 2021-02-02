#define LogOn_With_DefaultNetworkCredentials

using System;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;


namespace DemoBookmarks
{
    class Program
    {
        private static readonly Guid IntegrationId = new Guid("C89ED57E-DB41-4ACA-BDFE-499D0D963864");
        private const string IntegrationName = "Bookmark Creator";
        private const string Version = "1.0";
        private const string ManufacturerName = "Sample Manufacturer";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Console.WriteLine("Milestone SDK Bookmarks demo (XProtect Corporate only)");
            Console.WriteLine("Creates 2 new bookmarks and retrieves them using ");
            Console.WriteLine("  1) BookmarkSearchTime ");
            Console.WriteLine("  2) BookmarkSearchFromBookmark");
            Console.WriteLine("  3) BookmarkGet");
            Console.WriteLine("");

            // Initialize the SDK - must be done in stand alone
            VideoOS.Platform.SDK.Environment.Initialize();
            VideoOS.Platform.SDK.UI.Environment.Initialize();		// Initialize ActiveX references, e.g. usage of ImageViewerActiveX etc

            #region Connect to the server
            VideoOS.Platform.SDK.UI.LoginDialog.DialogLoginForm loginForm = new VideoOS.Platform.SDK.UI.LoginDialog.DialogLoginForm(SetLoginResult, IntegrationId, IntegrationName, Version, ManufacturerName);
            Application.Run(loginForm);								// Show and complete the form and login to server
            if (!Connected)
            {
                Console.WriteLine("Failed to connect or login");
				Console.ReadKey();
				return;
            }

			if (EnvironmentManager.Instance.MasterSite.ServerId.ServerType == ServerId.EnterpriseServerType)
			{
				Console.WriteLine("Bookmark is not supported on this product.");
				Console.ReadKey();
				return;				
			}
            #endregion

            #region Select a camera
            Item _selectItem1 = null;
			VideoOS.Platform.UI.ItemPickerForm form = new VideoOS.Platform.UI.ItemPickerForm();
			form.KindFilter = Kind.Camera;
			form.AutoAccept = true;
			form.Init(Configuration.Instance.GetItems());
            if (form.ShowDialog() == DialogResult.OK)
            {
                _selectItem1 = form.SelectedItem;
            }
            if (_selectItem1 == null)
            {
                Console.WriteLine("Failed to pick a camera");
            	Console.ReadKey();
				return;
			}
			if (_selectItem1.FQID.ServerId.ServerType == ServerId.EnterpriseServerType)
			{
				Console.WriteLine("Bookmark is not supported on this product.");
				Console.ReadKey();
				return;
			}

            FQID cameraFqid = _selectItem1.FQID;

            #endregion

            DateTime timeNow = DateTime.Now;

            // get cameras for each recording server 


            // now-5:10min:                                       BookmarkSearchTime start
            // now-5:00min: (beginTime)                                         |                                                  BookmarkGet
            // now-4:59min: start recording 1                                   |                                       
            // now-4:55min: start bookmark 1                                    |
            // now-4:45min: end bookmark 1                                      |
            //                                                                  |                BookmarkSearchFromBookmark    
            // now-2:00min:                                                     |                            |
            // now-1:59min: start recording 2                                   |                            |
            // now-1:55min: start bookmark 2 (trigger time)                     |                            |
            // now-1:45min: end bookmark 2                                      |                            |
            // now                                                              v                            V

            Guid[] mediaDeviceTypes = new Guid[3];
            mediaDeviceTypes[0] = Kind.Camera;
            mediaDeviceTypes[1] = Kind.Microphone;
            mediaDeviceTypes[2] = Kind.Speaker;

            #region get bookmark reference

            Console.WriteLine("Asking for a new Bookmark Reference:");
            BookmarkReference bookmarkReference = BookmarkService.Instance.BookmarkGetNewReference(cameraFqid, true);
            Console.WriteLine(".... Received:"+bookmarkReference.Reference);

            #endregion

            #region create first bookmark
            Console.WriteLine("Creating the first bookmark");
            DateTime timeBegin = timeNow.AddMinutes(-5);

            StringBuilder bookmarkref= new StringBuilder();
            StringBuilder bookmarkHeader = new StringBuilder();
            StringBuilder bookmarkDesc = new StringBuilder();
            bookmarkref.AppendFormat("Mybookmark-{0}", timeBegin.ToLongTimeString());
            bookmarkHeader.AppendFormat("AutoBookmark-{0}", timeBegin.ToLongTimeString());
            bookmarkDesc.AppendFormat("AutoBookmark-{0} set for a duration of {1} seconds", timeBegin.ToLongTimeString(), (timeBegin.AddSeconds(10) - timeBegin.AddSeconds(1)).Seconds);


            Bookmark bookmark1 = null; 
			try
			{
				bookmark1 = BookmarkService.Instance.BookmarkCreate(
                                    cameraFqid,
                                    timeBegin.AddSeconds(1),
					                timeBegin.AddSeconds(5),
					                timeBegin.AddSeconds(10),
					                bookmarkref.ToString(),
					                bookmarkHeader.ToString(),
					                bookmarkDesc.ToString());
                Console.WriteLine("Created bookmark 1 = " + bookmark1.BookmarkFQID.ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine("BookmarkCreate 1 failed: " + ex.Message);
				Console.WriteLine("Press any Key");
				Console.ReadKey();
            }
            #endregion

            Console.WriteLine("");
            Console.WriteLine("Waiting 20sec ....");
            Console.WriteLine("");
            System.Threading.Thread.Sleep(20000);

            #region Create a second bookmark
            Console.WriteLine("Creating a second bookmark - 2 minutes after the first bookmark");
            DateTime timeBegin2 = timeBegin.AddMinutes(2);
            bookmarkHeader.Length = 0;
            bookmarkDesc.Length = 0;
            StringBuilder bookmarkref2 = new StringBuilder();
            bookmarkref2.AppendFormat("Mybookmark-{0}", timeBegin2.ToLongTimeString());
            bookmarkHeader.AppendFormat("AutoBookmark-{0}", timeBegin2.ToLongTimeString());
            bookmarkDesc.AppendFormat("AutoBookmark-{0} set for a duration of {1} seconds", timeBegin2.ToLongTimeString(), (timeBegin2.AddSeconds(10)-timeBegin2.AddSeconds(1)).Seconds);

            Bookmark bookmark2 = null;
            try
            {
                bookmark2 = BookmarkService.Instance.BookmarkCreate(
                                    cameraFqid,
                                    timeBegin2.AddSeconds(1), 
                                    timeBegin2.AddSeconds(5), 
                                    timeBegin2.AddSeconds(10),
                                    bookmarkref2.ToString(),
                                    bookmarkHeader.ToString(),
                                    bookmarkDesc.ToString());
                Console.WriteLine("Created bookmark 2 " + bookmark2.BookmarkFQID.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("BookmarkCreate 2 failed: " + ex.Message);
                Console.WriteLine("Press any Key");
                Console.ReadKey();
            }

            Console.WriteLine("-> trigger time = {0}", bookmark2.TimeTrigged);
            Console.WriteLine("");
            #endregion
            
            #region BookmarkSearchTime

            // Get max 10 the bookmarks created after the specified time
            Console.WriteLine("");
            Console.WriteLine("Looking for bookmarks using BookmarkSearchTime (finding the 2 newly created)");
            Bookmark[] bookmarkList = BookmarkService.Instance.BookmarkSearchTime(
                                cameraFqid.ServerId,
                                bookmark1.TimeBegin.AddSeconds(-10), 
                                1800000000, 
                                10, 
                                mediaDeviceTypes, 
                                null, 
                                null, 
                                null);
            if (bookmarkList.Length > 0)
            {
                Console.WriteLine("-> Found {0} bookmark(s)", bookmarkList.Length);
                int counter = 1;
                foreach (Bookmark bookmark in bookmarkList)
                {
                    Console.WriteLine("{0}:", counter);
                    Item camera = Configuration.Instance.GetItem(bookmark.BookmarkFQID.ParentId, Kind.Camera);

                    FQID parent = bookmark.BookmarkFQID.GetParent();

                    Item camera2 = Configuration.Instance.GetItem(parent);

                    Console.WriteLine("     Id  ={0} ", bookmark.BookmarkFQID.ToString());
                    Console.WriteLine("     Name={0} ", bookmark.Header);
                    Console.WriteLine("     Desc={0} ", bookmark.Description);
                    Console.WriteLine("     user={0} ", bookmark.User);
                    Console.WriteLine("     Device={0} Start={1} Stop={2}  ", bookmark.GetDeviceItem().FQID.ObjectId, bookmark.TimeBegin, bookmark.TimeEnd);
                    counter++;
                }
            }
            else
            {
                Console.WriteLine("sorry no bookmarks found");
            }
            Console.WriteLine("");
            #endregion 
            
            #region BookmarkSearchFromBookmark
            // Get the next (max 10) bookmarks after the first
            Console.WriteLine("Looking for bookmarks using BookmarSearchFromBookmark (finding the last of the 2 newly created)");
            Bookmark[] bookmarkListsFromBookmark = BookmarkService.Instance.BookmarkSearchFromBookmark(
                                bookmark1.BookmarkFQID,
                                1800000000, 
                                10, 
                                mediaDeviceTypes, 
                                null, 
                                null, 
                                null);
            if (bookmarkListsFromBookmark.Length > 0)
            {
                Console.WriteLine("-> Found {0} bookmark(s)", bookmarkListsFromBookmark.Length);
                int counter = 1;
                foreach (Bookmark bookmark in bookmarkListsFromBookmark)
                {
                    Console.WriteLine("{0}:", counter);
                    Console.WriteLine("     Id  ={0} ", bookmark.BookmarkFQID.ToString());
                    Console.WriteLine("     Name={0} ", bookmark.Header);
                    Console.WriteLine("     Desc={0} ", bookmark.Description);
                    Console.WriteLine("     user={0} ", bookmark.User);
                    Console.WriteLine("     Device={0} Start={1} Stop={2}  ", bookmark.GetDeviceItem().FQID.ObjectId, bookmark.TimeBegin, bookmark.TimeEnd);
                    counter++;
                }
            }
            else
            {
                Console.WriteLine("sorry no bookmarks found");
            }
            Console.WriteLine("");

            #endregion

            #region SearchTimeAsync
            Console.WriteLine("Looking for bookmarks using bookmarkSearchTimeAsync (finding the last of the 2 newly created)");
            _hasBeenCalledBack = false;

            BookmarkService.Instance.BookmarkSearchTimeAsync(
                                cameraFqid.ServerId,
                                bookmark2.TimeBegin.AddSeconds(-10),
                                1800000000,
                                10,
                                mediaDeviceTypes,
                                null,
                                null,
                                null,
                                MyAsyncCallbackHandler,
                                null);

            while (!_hasBeenCalledBack)
            {
                //Do something usefull while waiting
                System.Threading.Thread.Sleep(100);
            }
            #endregion

            #region SearchBookmarkAsync
            Console.WriteLine("Looking for bookmarks using BookmarkSearchFromBookmarkAsync (finding the last of the 2 newly created)");
            _hasBeenCalledBack = false;

            BookmarkService.Instance.BookmarkSearchFromBookmarkAsync(
                                bookmark1.BookmarkFQID,
                                1800000000,
                                10,
                                mediaDeviceTypes,
                                null,
                                null,
                                null,
                                MyAsyncCallbackHandler,
                                null);

            while (!_hasBeenCalledBack)
            {
                //Do something usefull while waiting
                System.Threading.Thread.Sleep(100);
            }
            #endregion

            #region BookmarkGet
            // Get first created bookmark
            Console.WriteLine("Looking for the first bookmarks using BookmarkGet (finding the first of the 2 newly created)");
            Bookmark newbookmarkFetched = BookmarkService.Instance.BookmarkGet(bookmark1.BookmarkFQID);
            if (newbookmarkFetched != null)
            {
                Console.WriteLine("-> A bookmarks is found");
                Console.WriteLine("     Id  ={0} ", newbookmarkFetched.BookmarkFQID.ToString());
                Console.WriteLine("     Name={0} ", newbookmarkFetched.Header);
                Console.WriteLine("     Desc={0} ", newbookmarkFetched.Description);
                Console.WriteLine("     user={0} ", newbookmarkFetched.User);
                Console.WriteLine("     Device={0} Start={1} Stop={2}  ", newbookmarkFetched.GetDeviceItem().FQID.ObjectId, newbookmarkFetched.TimeBegin, newbookmarkFetched.TimeEnd);
            }
            else
            {
                Console.WriteLine("Sorry no bookmarks found");
            }
            Console.WriteLine("");
            #endregion

            #region Update Bookmark1
            Console.WriteLine("Updating the bookmark just fetched using BookmarkUpdate");
            newbookmarkFetched.Description = "Now I have updated the description of this bookmark";
            Bookmark newbookmark1 = BookmarkService.Instance.BookmarkUpdate(newbookmarkFetched);
            if (newbookmarkFetched != null)
            {
                Console.WriteLine("-> Result =");
                Console.WriteLine("     Id  ={0} ", newbookmarkFetched.BookmarkFQID.ToString());
                Console.WriteLine("     Name={0} ", newbookmarkFetched.Header);
                Console.WriteLine("     Desc={0} ", newbookmarkFetched.Description);
                Console.WriteLine("     user={0} ", newbookmarkFetched.User);
                Console.WriteLine("     Device={0} Start={1} Stop={2}  ", newbookmarkFetched.GetDeviceItem().FQID.ObjectId, newbookmarkFetched.TimeBegin, newbookmarkFetched.TimeEnd);
            }
            else
            {
                Console.WriteLine("Sorry. Failed to update the bookmark");
            }
            Console.WriteLine("");

            #endregion

            #region Deleting bookmarks
            Console.WriteLine("Deleting 2 newly created bookmarks");
            BookmarkService.Instance.BookmarkDelete(bookmark1.BookmarkFQID);
            Console.WriteLine("   -> first deleted");
            BookmarkService.Instance.BookmarkDelete(bookmark2);
            Console.WriteLine("   -> second deleted");
            #endregion

            Console.WriteLine("");
            Console.WriteLine("Press any key");
            Console.ReadKey();
            
        }

        private static bool Connected = false;

        private static void SetLoginResult(bool connected)
        {
            Connected = connected;
        }

        private static bool _hasBeenCalledBack = false;

        private static void MyAsyncCallbackHandler(object result, object state)
        {

            BookmarkService.BookmarkSearchCompletedEventArgs sResult = result as BookmarkService.BookmarkSearchCompletedEventArgs;

            if (sResult != null)
            {
                if (sResult.Exception != null)
                {
                    Console.WriteLine("Got an exception: " + sResult.Exception.Message);
                }
                else if (sResult.Cancelled)
                {
                    Console.WriteLine("The operation was cancelled.");
                }
                else if (sResult.Result == null || sResult.Result.Length == 0)
                {
                    Console.WriteLine("The operation returned an empty result.");
                }
                else
                {
                    Bookmark[] bookmarks = sResult.Result;
                    Console.WriteLine("-> Found {0} bookmark(s)", bookmarks.Length);
                    int counter = 1;
                    foreach (Bookmark bookmark in bookmarks)
                    {
                        Console.WriteLine("{0}:", counter);
                        Console.WriteLine("     Id  ={0} ", bookmark.BookmarkFQID.ToString());
                        Console.WriteLine("     Name={0} ", bookmark.Header);
                        Console.WriteLine("     Desc={0} ", bookmark.Description);
                        Console.WriteLine("     user={0} ", bookmark.User);
                        Console.WriteLine("     Device={0} Start={1} Stop={2}  ", bookmark.GetDeviceItem().FQID.ObjectId, bookmark.TimeBegin, bookmark.TimeEnd);
                        counter++;
                    }
                }

            }
            else
            {
                Console.WriteLine("Did not get any valid result");
            }

            _hasBeenCalledBack = true;
        }

    }
}
