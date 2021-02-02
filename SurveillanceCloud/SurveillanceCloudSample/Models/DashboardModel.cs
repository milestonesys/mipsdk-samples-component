using SurveillanceCloudSample.SharedObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveillanceCloudSample.Models
{
    public class DashboardModel
    {
        public int NumberOfCameras
        { get; set; }

        public List<Camera> Cameras
        { get; set; }

        public DashboardModel(int numberOfCameras, List<Camera> cameras)
        {
            NumberOfCameras = numberOfCameras;
            Cameras = cameras;
        }
    }
}