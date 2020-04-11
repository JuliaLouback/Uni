using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Uni.Areas.Identity.IdentityHostingStartup))]
namespace Uni.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}