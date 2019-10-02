using Microsoft.AspNetCore.Identity;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Simple_shop.Repository.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		UserManager<User> UserManager { get; }
		RoleManager<IdentityRole> RoleManager { get; }
		SignInManager<User> SignInManager { get; }

		IRepository<TEntity> Repository<TEntity>(Type repositoryType) where TEntity : class;
		Task<int> SaveChangesAsync();
		int SaveChanges();
		void Rollback();
	}
}
