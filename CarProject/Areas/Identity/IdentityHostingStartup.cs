using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CarProject.Areas.Identity.IdentityHostingStartup))]
namespace CarProject.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<CarProjectContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("CarProjectContextConnection")));

            //    services.AddDefaultIdentity<CarProjectUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<CarProjectContext>();
            //});
        }
    }
}