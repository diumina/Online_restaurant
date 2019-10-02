using Simple_shop.EF;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;

namespace Simple_shop.Repository.Repositories
{
	public class OrderDishDataRepository : Repository<OrderDishData>, IOrderDishDataRepository
	{
		public OrderDishDataRepository(AppDbContext appDbContext) 
			: base(appDbContext)
		{
		}
	}
}
