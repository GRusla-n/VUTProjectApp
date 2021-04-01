using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VUTProjectApp.Models;

namespace VUTProjectApp.Data
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, object>> include=null);        
        Task<TEntity> GetById(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> include = null);
        void Create(TEntity category);
        void Update(TEntity category);
        void Delete(TEntity category);
        bool SaveChanges();
        bool SaveChangesAsync();
    }
}
