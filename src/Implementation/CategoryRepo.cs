using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Data;
using VUTProjectApp.Models;

namespace VUTProjectApp.Implementation
{
    public class CategoryRepo : Repo<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}
