using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.LightingSystem.Feedback.Models
{
    public class UserFeedback
    {       
        public int Id { get; set; } 
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        public bool? IsHappyWithService { get; set; }
        public int LevelOfBrightness { get; set; }
        [NotMapped]
        public List<SelectListItem> BrightnessIndex = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "1" },
            new SelectListItem { Value = "2", Text = "2" },
            new SelectListItem { Value = "3", Text = "3"  },
            new SelectListItem { Value = "4", Text = "4"  },
            new SelectListItem { Value = "5", Text = "5"  },
            new SelectListItem { Value = "6", Text = "6"  },
            new SelectListItem { Value = "7", Text = "7"  },
            new SelectListItem { Value = "8", Text = "8"  },
            new SelectListItem { Value = "9", Text = "9"  },
            new SelectListItem { Value = "10", Text = "10"},
        };
       
    }
}
