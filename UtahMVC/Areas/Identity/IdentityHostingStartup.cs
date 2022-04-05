using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UtahMVC.Areas.Identity.Data;
using UtahMVC.Data;

[assembly: HostingStartup(typeof(UtahMVC.Areas.Identity.IdentityHostingStartup))]
namespace UtahMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UtahMVCContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UtahMVCContextConnection")));

                services.AddDefaultIdentity<UtahMVCUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = true;
                })
                    .AddEntityFrameworkStores<UtahMVCContext>();
            });
        }
    }
}