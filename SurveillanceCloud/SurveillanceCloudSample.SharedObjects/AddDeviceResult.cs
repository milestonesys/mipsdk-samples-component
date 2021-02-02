namespace SurveillanceCloudSample.SharedObjects
{
    public class AddDeviceResult
    {
        public AddDeviceStatus Status { get; set; }
    }

    public enum AddDeviceStatus
    {
        Success,
        Problem
    }
}
