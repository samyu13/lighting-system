using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.LightingSystem.Feedback.Models
{
    public class Model2
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
