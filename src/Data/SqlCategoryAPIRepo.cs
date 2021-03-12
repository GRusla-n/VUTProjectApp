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

        public void CreateCategory(Category category)
        {
            if(category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            context.Categories.Add(category);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }

        public void UpdateCategory(Category category)
        {
            //We don't need to do anything here
        }

        public void DeleteCategory(Category category)
        {
            if(category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            context.Categories.Remove(category);
        }
    }
}
