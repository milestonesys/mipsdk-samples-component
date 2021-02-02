using SurveillanceCloudSample.SharedObjects;
using System.Collections.Generic;

namespace SurveillanceCloudSampleService.Objects
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int NumberOfCameras { get; set; }

        public List<Camera> Cameras { get; set; }
    }
}