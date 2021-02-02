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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Clearing the session object when we load the Login Page
            Session.Clear();
            return View("HomeView", new HomeModel());
        }

        [HttpPost]
        public ActionResult Login(HomeModel homeModel)
        {
            LoginResult result = null;

            //Calling the SurveillanceCloudSampleService's Login method with the parameters filled by the user
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8083/Login");
            request.Method = "POST";

            byte[] byteArray = new UTF8Encoding().GetBytes(JsonConvert.SerializeObject(new { userName = homeModel.Username, password = homeModel.Password }));

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
                    result = JsonConvert.DeserializeObject<LoginResult>(reader.ReadToEnd());
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
                        return RedirectToAction("Index");
                    }
                }
                catch
                {

                }

                TempData["Error"] = HttpUtility.HtmlEncode("Invalid or no response from the service");
                return RedirectToAction("Index");
            }

            //Setting the userId in the session
            Session["userId"] = result.UserId;
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public ActionResult Register(HomeModel homeModel)
        {
            LoginResult result = null;

            //Calling the SurveillanceCloudSampleService's Register method with the parameters filled by the user
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8083/Register");
            request.Method = "POST";

            byte[] byteArray = new UTF8Encoding().GetBytes(JsonConvert.SerializeObject(new { userName = homeModel.Username, password = homeModel.Password, code = homeModel.Code }));

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
                    result = JsonConvert.DeserializeObject<LoginResult>(reader.ReadToEnd());
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
                        return RedirectToAction("Index", new { register = 1 });
                    }
                }
                catch
                {

                }

                TempData["Error"] = HttpUtility.HtmlEncode("Invalid or no response from the service");
                return RedirectToAction("Index", new { register = 1 });
            }

            //Setting the userId in the session
            Session["userId"] = result.UserId;
            return RedirectToAction("Index", "Dashboard");
        }
    }
}