using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Simple_shop.Areas.Identity.IdentityHostingStartup))]
namespace Simple_shop.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}