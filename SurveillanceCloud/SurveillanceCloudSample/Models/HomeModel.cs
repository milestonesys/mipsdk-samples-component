using System.ComponentModel.DataAnnotations;

namespace SurveillanceCloudSample.Models
{
    public class HomeModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Code")]
        public string Code
        {
            get;
            set;
        }
    }
}