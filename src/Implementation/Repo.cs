using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace VUTProjectApp.Data
{
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected DbContext db { get; set; }

        async public Task<List<TEntity>> GetAll(Expression<Func<TEntity, object>> include=null)
        {                    
            if (include != null)
            {
                return await db.Set<TEntity>().Include(include).ToListAsync();
            }
            return await db.Set<TEntity>().ToListAsync();
        }        

        async public Task<TEntity> GetById(int id, Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> include = null)
        {
            if(include != null)
            {
                return await db.Set<TEntity>().Include(include).FirstOrDefaultAsync(filterExpression);
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
