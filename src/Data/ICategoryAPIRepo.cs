using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Models;

namespace VUTProjectApp.Data
{
    public interface ICategoryAPIRepo
    {
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int id);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        bool SaveChanges();
    }
}
