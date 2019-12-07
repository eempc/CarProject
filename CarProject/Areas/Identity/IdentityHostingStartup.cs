using System;
using CarProject.Areas.Identity.Data;
using CarProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CarProject.Areas.Identity.IdentityHostingStartup))]
namespace CarProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CarProjectContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CarProjectContextConnection")));

                services.AddDefaultIdentity<CarProjectUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<CarProjectContext>();
            });
        }
    }
}