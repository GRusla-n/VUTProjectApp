using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Data;
using VUTProjectApp.Implementation;
using VUTProjectApp.Services;

namespace VUTProjectApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection")));
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddTransient<IFileStorage, AzureStorage>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
