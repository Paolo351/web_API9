using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using web_API9.Models;
using web_API9.Services;

namespace web_API9
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
            services.AddControllersWithViews();

            //Userz
            services.Configure<Userz_DatabaseSettings>(
                Configuration.GetSection(nameof(Userz_DatabaseSettings)));
            services.AddSingleton<IBDO_DatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<Userz_DatabaseSettings>>().Value);
            services.AddSingleton<UserzService>();

            //Deployment
            services.Configure<Deployment_DatabaseSettings>(
                Configuration.GetSection(nameof(Deployment_DatabaseSettings)));
            services.AddSingleton<IBDO_DatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<Deployment_DatabaseSettings>>().Value);
            services.AddSingleton<DeploymentService>();

            //Project
            services.Configure<Project_DatabaseSettings>(
                Configuration.GetSection(nameof(Project_DatabaseSettings)));
            services.AddSingleton<IBDO_DatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<Project_DatabaseSettings>>().Value);
            services.AddSingleton<ProjectService>();

            //Database
            services.Configure<Database_DatabaseSettings>(
                Configuration.GetSection(nameof(Database_DatabaseSettings)));
            services.AddSingleton<IBDO_DatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<Database_DatabaseSettings>>().Value);
            services.AddSingleton<DatabaseService>();

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
