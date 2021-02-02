using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveillanceCloudSample.SharedObjects
{
    public class RegisterResult
    {
        public bool Error { get; set; }
        public int? UserId { get; set; }
        public int NumberOfCameras { get; set; }
    }
}
