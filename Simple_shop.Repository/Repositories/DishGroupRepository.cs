using Microsoft.EntityFrameworkCore;
using Simple_shop.EF;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_shop.Repository.Repositories
{
	public class DishGroupRepository : Repository<DishGroup>, IDishGroupRepository
	{
		public DishGroupRepository(AppDbContext appDbContext)
			: base(appDbContext)
		{
		}
	}
}
