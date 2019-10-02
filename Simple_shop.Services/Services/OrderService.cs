using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using Simple_shop.Repository.Repositories;
using Simple_shop.Repository.UnitOfWork;
using Simple_shop.Services.Interfaces;
using System.Collections.Generic;

namespace Simple_shop.Services.Services
{
	public class OrderService : IOrderService
	{
		#region Fields

		private readonly IUnitOfWork unitOfWork;
		private readonly IOrderRepository orderRepository;

		#endregion

		#region Constructors

		public OrderService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			this.orderRepository = unitOfWork.Repository<Order>(typeof(OrderRepository)) as IOrderRepository;
		}

		#endregion

		#region Public methods

		

		#endregion
	}
}
