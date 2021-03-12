using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VUTProjectApp.Dto
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
