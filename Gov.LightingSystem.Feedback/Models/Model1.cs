using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.LightingSystem.Feedback.Models
{
    public class Model1
    {
        [Required]
        public string UserName { get; set; }
    }
}
