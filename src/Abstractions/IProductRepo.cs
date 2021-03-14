using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Data;
using VUTProjectApp.Models;

namespace VUTProjectApp.Abstractions
{
    public interface IProductRepo : IRepo<Product>
    {
    }
}
