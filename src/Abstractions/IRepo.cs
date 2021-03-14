using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Models;

namespace VUTProjectApp.Data
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        void Create(TEntity category);
        void Update(TEntity category);
        void Delete(TEntity category);
        bool SaveChanges();
    }
}
