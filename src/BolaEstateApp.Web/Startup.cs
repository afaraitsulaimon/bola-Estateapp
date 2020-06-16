  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolaEstateApp.Data.DatabaseContexts.AuthenticationDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BolaEstateApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //this services.AddDbContextPool<AuthenticationDbContext> , says add a DbContext called the ApplicationDbContext
               //the sqlServerOptions is vey powerful
               //and we just used it , to say, when creating the database migration for the AuthenticationDbContext, create it inside BolaEstateApp.Data
              // which is sqlServerOptions => {
                  // sqlServerOptions.MigrationsAssembly("BolaEstateApp.Data");


            services.AddDbContextPool<AuthenticationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("AuthenticationConnection"),
                   sqlServerOptions => {

                       sqlServerOptions.MigrationsAssembly("BolaEstateApp.Data");
                   }
                
                ));


               
               //in here , we want to make sure all our database are in the BolaEstateApp.Data folder
               //this services.AddDbContextPool<ApplicationDbContext> , says add a DbContext called the ApplicationDbContext
               //the sqlServerOptions is vey powerful
               //and we just used it , to say, when creating the database migration for ApplicationDbContext, create it inside BolaEstateApp.Data
              // which is sqlServerOptions => {
                  // sqlServerOptions.MigrationsAssembly("BolaEstateApp.Data");

               services.AddDbContextPool<ApplicationDbContext>(Options =>
                Options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection"),

               sqlServerOptions => {
                   sqlServerOptions.MigrationsAssembly("BolaEstateApp.Data");
               }));



               services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
