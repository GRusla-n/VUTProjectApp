using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VUTProjectApp.Dto
{
    public class ProductCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }        
    }
}
