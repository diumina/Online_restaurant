using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simple_shop.Entities;

namespace Simple_shop.EF
{
	public class AppDbContext : IdentityDbContext<User>
	{
		#region Constructors

		public AppDbContext(DbContextOptions<AppDbContext> options)
			:base(options)
		{
			Database.EnsureCreated();
		}

		#endregion

		#region Protected methods	

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>()
				.Property(u => u.Number)
				.HasDefaultValue(1000);

			base.OnModelCreating(modelBuilder);
		}

		#endregion

		#region Entities

		public DbSet<Dish> Dishes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDishData> OrdersDishesData { get; set; }
		public DbSet<DishGroup> DishGroups { get; set; }

		#endregion
	}
}
