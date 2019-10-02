using Microsoft.AspNetCore.Identity;
using Simple_shop.EF;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_shop.Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		#region Fields

		private readonly AppDbContext _dbContext;
		private readonly Dictionary<string, dynamic> _repositories;

		private readonly UserManager<User> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly SignInManager<User> signInManager;

		#endregion

		#region Properties

		public UserManager<User> UserManager { get { return userManager; } }

		public RoleManager<IdentityRole> RoleManager { get { return roleManager; } }

		public SignInManager<User> SignInManager { get { return signInManager; } }

		#endregion

		#region Constructors

		public UnitOfWork(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
			SignInManager<User> signInManager)
		{
			_dbContext = dbContext;
			_repositories = new Dictionary<string, dynamic>();
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.signInManager = signInManager;
		}

		#endregion

		#region Public Methods

		public IRepository<TEntity> Repository<TEntity>(Type repositoryType) where TEntity : class
		{
			string type = typeof(TEntity).Name;

			if (_repositories.Keys.Contains(type))
			{
				return _repositories[type] as IRepository<TEntity>;
			}

			_repositories.Add(type, Activator.CreateInstance(repositoryType, _dbContext));

			return _repositories[type] as IRepository<TEntity>;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public int SaveChanges()
		{
			return _dbContext.SaveChanges();
		}

		public void Rollback()
		{
			_dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}

		#endregion
	}
}

