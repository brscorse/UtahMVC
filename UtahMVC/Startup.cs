using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtahMVC.Data;
using UtahMVC.Models;

namespace UtahMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<CrashesDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CrashesDbConnection")));


            services.AddDbContext<AuthDbContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:AuthDbContextConnection"]));

            services.AddScoped<IUtahMVCRepository, EFUtahMVCRepository>();

            //services.AddSingleton<InferenceSession>(
            //   new InferenceSession("../UtahMVC/wwwroot/lib/intexModel.onnx"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
//Added CSP Headers
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self', bootstrap.css;");
                await next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            //endpoints babyyyyyy
            app.UseEndpoints(endpoints =>
            {

                //ADMIN ENDPOINTS

                // county plus a page number
                endpoints.MapControllerRoute(
                    name: "adminCountyPage",
                    pattern: "Admin/Admin/{countyNames}/Page{pageNum}",
                    defaults: new { Controller = "Admin", action = "Admin" });

                // only a page is passed through with all filters as defaults
                endpoints.MapControllerRoute(
                    name: "adminPaging",
                    pattern: "Admin/Admin/Page{pageNum}",
                    defaults: new { Controller = "Admin", action = "Admin", pageNum = 1 });

                // only a county is passed through 
                endpoints.MapControllerRoute(
                    name: "adminCounty",
                    pattern: "Admin/Admin/{countyNames}",
                    defaults: new { Controller = "Admin", action = "Admin", pageNum = 1 });

                // county plus a page number
                endpoints.MapControllerRoute(
                    name: "countyPage",
                    pattern: "{countyNames}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Crashes" });

                // only a page is passed through with all filters as defaults
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Crashes", pageNum = 1 });

                // only a county is passed through 
                endpoints.MapControllerRoute(
                    name: "county",
                    pattern: "{countyNames}",
                    defaults: new { Controller = "Home", action = "Crashes", pageNum = 1 });

                //default endpoint
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");




                endpoints.MapDefaultControllerRoute();

                //Connects to razor pages
                endpoints.MapRazorPages();
            });

 
        }
    }
}
