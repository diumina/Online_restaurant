using Simple_shop.EF;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;

namespace Simple_shop.Repository.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(AppDbContext appDbContext) 
			: base(appDbContext)
		{
		}
	}
}
