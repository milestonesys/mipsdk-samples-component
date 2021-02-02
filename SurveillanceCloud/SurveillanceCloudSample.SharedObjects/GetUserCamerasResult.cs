using System.Collections.Generic;

namespace SurveillanceCloudSample.SharedObjects
{
    public class GetUserCamerasResult
    {
        public int NumberOfCameras { get; set; }
        public List<Camera> Cameras { get; set; }
    }
}
