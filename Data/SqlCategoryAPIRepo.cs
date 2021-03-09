using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Models;

namespace VUTProjectApp.Data
{
    public class SqlCategoryAPIRepo : ICategoryAPIRepo
    {
        private readonly ApplicationDbContext context;

        public SqlCategoryAPIRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        async public Task<List<Category>> GetAllCategory()
        {
            return await context.Categories.ToListAsync();
        }

        async public Task<Category> GetCategoryById(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
