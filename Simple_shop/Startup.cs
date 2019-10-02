using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple_shop.EF;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using Simple_shop.Repository.Repositories;
using Simple_shop.Repository.UnitOfWork;
using Simple_shop.Services.Interfaces;
using Simple_shop.Services.Services;

namespace Simple_shop
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<AppDbContext>(options =>
			   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			//DI start
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			services.AddTransient<IOrderService, OrderService>();
			services.AddTransient<IDishService, DishService>();
			services.AddTransient<IUserService, UserService>();

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			//DI end

			services.AddIdentity <User, IdentityRole> ()
				.AddEntityFrameworkStores<AppDbContext>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Dish}/{action=Main}/{id?}");
			});
		}
	}
}
