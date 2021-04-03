using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VUTProjectApp.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Country { get; set; }

        public List<Product> Products { get; set; }
    }
}
