using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BlazorApp2.Areas.Identity.IdentityHostingStartup))]
namespace BlazorApp2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("ApplicationDbContextConnection")));

            //    services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<ApplicationDbContext>();
            //});
        }
    }
}