using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.LightingSystem.Feedback.Models
{
    public class Model3
    {
        [Required]
        public string HomeAddress { get; set; }
    }
}
