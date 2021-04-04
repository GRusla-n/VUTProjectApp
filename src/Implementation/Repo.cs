using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using VUTProjectApp.Extension;
using VUTProjectApp.Dto;

namespace VUTProjectApp.Data
{
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected DbContext db { get; set; }

        async public Task<List<TEntity>> GetAll(HttpContext httpContext, PaginationDto pagination, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = db.Set<TEntity>().AsQueryable();
            await httpContext.InsertPaginationParametersInResponse(query, pagination);
            query = query.Pagination(pagination);
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }           

            return await query.ToListAsync();
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
