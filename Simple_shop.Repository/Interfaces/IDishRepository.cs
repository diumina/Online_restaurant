using Simple_shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Simple_shop.Repository.Interfaces
{
	public interface IDishRepository : IRepository<Dish>
	{
		Task<IEnumerable<Dish>> GetDishesByGroupIdAsync(Expression<Func<Dish, bool>> predicate);

		Task<Dish> GetDishInfo(Expression<Func<Dish, bool>> predicate);
	}
}
