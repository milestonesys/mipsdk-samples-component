using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SurveillanceCloudSample.SharedObjects
{
    public class Camera
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "IP")]
        public string Address { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }

        [JsonIgnore]
        public bool IsFilled
        {
            get
            {
                return Name != default(string) || Address != default(string) || Username != default(string) || Password != default(string);
            }
        }
    }
}