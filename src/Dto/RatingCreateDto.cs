using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VUTProjectApp.Dto
{
    public class RatingCreateDto
    {   
        [Required]
        public int Point { get; set; }
        public string Description { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
