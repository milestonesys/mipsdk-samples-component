namespace SurveillanceCloudSampleService.Objects
{
    public class SurveillanceSettings
    {
        public bool SecureOnly { get; set; }
        public string SurveillanceScheme { get; set; }
        public string SurveillanceAddress { get; set; }
        public int SurveillancePort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdUser { get; set; }
    }
}