using Simple_shop.EF;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;

namespace Simple_shop.Repository.Repositories
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		public OrderRepository(AppDbContext appDbContext) 
			: base(appDbContext)
		{
		}
	}
}
