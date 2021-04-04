using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Dto;

namespace VUTProjectApp.Extension
{
    public static class HttpContextExtension
    {
        public async static Task InsertPaginationParametersInResponse<T>(this HttpContext httpContext, IQueryable<T> queryable, PaginationDto pagination)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double count = await queryable.CountAsync();
            double totalAmountPage = Math.Ceiling(count / pagination.RecordsPerPage);
            httpContext.Response.Headers.Add("totalAmountPages", totalAmountPage.ToString());
        }
    }
}
