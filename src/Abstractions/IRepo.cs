using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;

namespace VUTProjectApp.Data
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll(HttpContext httpContext, PaginationDto pagination, params Expression<Func<TEntity, object>>[] includes);        
        Task<TEntity> GetById(Expression<Func<TEntity, bool>> filterExpression, params Expression<Func<TEntity, object>>[] includes);
        void Create(TEntity category);
        void Update(TEntity category);
        void Delete(TEntity category);
        bool SaveChanges();
        bool SaveChangesAsync();
    }
}
