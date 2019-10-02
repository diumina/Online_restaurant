using Simple_shop.Entities;
using System.Collections.Generic;

namespace Simple_shop.Models
{
	public class DishListItemViewModel
	{
		#region Properties

		public int Id { get; set; }

		public string Name { get; set; }

		public double Weight { get; set; }

		public double Price { get; set; }

		public string ImageURL { get; set; }

		#endregion
	}
}


