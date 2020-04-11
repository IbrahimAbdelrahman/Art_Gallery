using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration config;

        // inject some basic services to get configuration
        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // inject DutchSeeder as a service
            services.AddTransient<DutchSeeder>();

            // inject the repository in the service layer to use it 
            services.AddScoped<IDutchRepository, DutchRepository>();
            // add service to our project to tell him what database to use and make the 
            // DbContext part of service collection, so you can inject in different services 
            // like controller
            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("DutchConnectionString"));
            });
            // support for real mail service 
            services.AddTransient<IMailService, NullMailService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // use this method to configure the order of the middleware to run through the HTTP request.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // show error page when you are not in the development mood
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseDefaultFiles();
            // make the application enable to navigate files
            // the default behaviour tha the app can only navigate files inside wwwroot
            app.UseStaticFiles();

            // use a middleware to get node package manager in your project
            app.UseNodeModules();

            // Matches request to an endpoint.
            app.UseRouting();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" }
                    );
                cfg.MapRazorPages();
            });


        }
    }
}
