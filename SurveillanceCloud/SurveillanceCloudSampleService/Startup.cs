using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SurveillanceCloudSampleService.Startup))]

namespace SurveillanceCloudSampleService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
