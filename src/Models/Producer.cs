using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VUTProjectApp.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Country { get; set; }

        public List<Product> Products { get; set; }
    }
}
