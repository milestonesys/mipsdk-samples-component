using Newtonsoft.Json;
using SurveillanceCloudSample.Models;
using SurveillanceCloudSample.SharedObjects;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SurveillanceCloudSample.Controllers
{
    public class DashboardController : Controller
    {
        private GetUserCamerasResult userCameras;

        public ActionResult Index()
        {
            //Calling the SurveillanceCloudSampleService's GetUserCameras method with the parameters from the session
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8083/GetUserCameras");
            request.Method = "POST";

            byte[] byteArray = new UTF8Encoding().GetBytes(JsonConvert.SerializeObject(new { userId = Session["userId"] }));

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    //Deserializing the response
                    userCameras = JsonConvert.DeserializeObject<GetUserCamerasResult>(reader.ReadToEnd());
                }
            }
            catch (Exception)
            {

            }

            //Creating the Model and returning the View
            DashboardModel dashboardModel = new DashboardModel(userCameras.NumberOfCameras, userCameras.Cameras);
            return View("DashboardView", dashboardModel);
        }

        [HttpPost]
        public ActionResult AddDevice(string name, string address, string userName, string password, int cameraId)
        {
            AddDeviceResult result = null;

            //Calling the SurveillanceCloudSampleService's AddDevice method with the parameters filled by the user
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8083/AddDevice");
            request.Method = "POST";
            request.Timeout = 900000;

            byte[] byteArray = new UTF8Encoding().GetBytes(JsonConvert.SerializeObject(new { userId = Session["userId"], cameraId = cameraId, name = name, userName = userName, password = password, ipAddress = address }));

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    //Deserializing the response
                    result = JsonConvert.DeserializeObject<AddDeviceResult>(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (ex is WebException && (ex as WebException).Response != null && (ex as WebException).Response is HttpWebResponse && (((ex as WebException).Response as HttpWebResponse).StatusCode == HttpStatusCode.BadRequest || ((ex as WebException).Response as HttpWebResponse).StatusCode == HttpStatusCode.InternalServerError))
                    {
                        //Getting the error message and setting it in the TempData object for later visualization
                        string description = JsonConvert.DeserializeObject<string>(new StreamReader(((ex as WebException).Response as HttpWebResponse).GetResponseStream()).ReadToEnd());
                        TempData["Error"] = HttpUtility.HtmlEncode(description);
                        return RedirectToAction("Index", "Dashboard", new { cameraId = cameraId });
                    }
                }
                finally
                {

                }

                TempData["Error"] = HttpUtility.HtmlEncode("Invalid or no response from the service");
                return RedirectToAction("Index", "Dashboard", new { cameraId = cameraId });
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}