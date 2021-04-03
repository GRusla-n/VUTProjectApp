using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Models;

namespace VUTProjectApp.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Image { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }        
        public CategoryDto Category {get; set; }
        public ProducerForProductDto Producer { get; set; }
        public List<RatingDto> Ratings { get; set; }
    }
}
