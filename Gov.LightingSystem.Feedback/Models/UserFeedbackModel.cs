using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Gov.LightingSystem.Feedback.Models
{
    public class UserFeedbackModel
    {
        public int FeedbackId { get; set; }  
        //public Model1 Model1 { get; set; }
        //public Model2 Model2 { get; set; }
        //public Model3 Model3 { get; set; }
        //public Model4 Model4 { get; set; }
        //public Model5 Model5 { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        public bool IsHappyWithService { get; set; }
        public int LevelOfBrightness { get; set; }
    }
}
