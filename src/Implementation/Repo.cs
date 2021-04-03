using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VUTProjectApp.Extension;

namespace VUTProjectApp.Data
{
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected DbContext db { get; set; }

        async public Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {               
            if (includes != null)
            {
                return await includes.Aggregate(db.Set<TEntity>().AsQueryable(), (current, include) => current.Include(include)).ToListAsync();
            }
            return await db.Set<TEntity>().ToListAsync();
        }        

        async public Task<TEntity> GetById(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                return await includes.Aggregate(db.Set<TEntity>().AsQueryable(), (current, include) => current.Include(include)).FirstOrDefaultAsync(filterExpression);
            }
            return await db.Set<TEntity>().FirstOrDefaultAsync(filterExpression);
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

        public bool SaveChangesAsync()
        {
            return (db.SaveChangesAsync().Result >= 0);
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
