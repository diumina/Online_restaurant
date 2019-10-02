using Microsoft.EntityFrameworkCore;
using Simple_shop.EF;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Simple_shop.Repository.Repositories
{
	public class DishRepository : Repository<Dish>, IDishRepository
	{
		public DishRepository(AppDbContext appDbContext) 
			: base(appDbContext)
		{
		}

		#region Public Methods

		public async Task<IEnumerable<Dish>> GetDishesByGroupIdAsync(Expression<Func<Dish, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}

		public async Task<Dish> GetDishInfo(Expression<Func<Dish, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate);
		}

		#endregion
	}
}
