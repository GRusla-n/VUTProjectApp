﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VUTProjectApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }        
        public string Image { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        
        [Required]
        public int CategoryId {get; set; }
        public Category Category {get; set; }
        
        [Required]
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

        public List<int> RatingId { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
