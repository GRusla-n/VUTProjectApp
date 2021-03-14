using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Models;

namespace VUTProjectApp.Data
{
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected DbContext db { get; set; }        

        async public Task<List<TEntity>> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        async public Task<TEntity> GetById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public void Create(TEntity model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            db.Set<TEntity>().Add(model);
        }

        public bool SaveChanges()
        {
            return (db.SaveChanges() >= 0);
        }

        public void Update(TEntity model)
        {            
        }

        public void Delete(TEntity model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            db.Set<TEntity>().Remove(model);
        }
    }
}
