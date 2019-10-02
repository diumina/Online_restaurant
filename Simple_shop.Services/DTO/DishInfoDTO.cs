using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_shop.Services.DTO
{
	public class DishInfoDTO
	{
		#region Properties

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public double Weight { get; set; }

		public double Price { get; set; }

		public string ImageURL { get; set; }

		public int Amount { get; set; }

		#endregion
	}
}
