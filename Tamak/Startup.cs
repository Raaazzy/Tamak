using Tamak.Data.Interfaces;
using Tamak.Data;
using Microsoft.EntityFrameworkCore;
using Tamak.Data.Repository;
using Tamak.Migrations;
using Tamak.Data.Models;
using Tamak.Service.Interfaces;
using Tamak.Service.Implementations;

namespace Tamak
{
    public class Startup
    {
        private IConfigurationRoot _configString;

        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnvironment) {
            _configString = new ConfigurationBuilder().SetBasePath(hostEnvironment.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_configString.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddTransient<IProductsCategory, CategoryRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Data.Models.ShopCart.GetCart(sp));
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "defalut", template: "{controller=Home}/{action=Index}/{id?}");

            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }
        }
    }
}
