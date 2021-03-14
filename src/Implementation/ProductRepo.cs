using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Data;
using VUTProjectApp.Models;

namespace VUTProjectApp.Implementation
{
    public class ProductRepo : Repo<Product>, IProductRepo
    {
        public ProductRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}
