using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace SurveillanceCloudSampleService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{action}",
                defaults: new { controller = "Values" }
            );
        }
    }
}
