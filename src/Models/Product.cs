using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VUTProjectApp.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }        
    }
}
