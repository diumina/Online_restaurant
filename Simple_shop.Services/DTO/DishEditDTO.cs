using System.Collections.Generic;

namespace Simple_shop.Services.DTO
{
	public class DishEditDTO
	{
		#region Constructor

		public DishEditDTO()
		{
			DishGroups = new List<ListItemDTO>();
		}

		#endregion

		#region Properties

		public int Id { get; set; }

		public int DishGroupId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public double Weight { get; set; }

		public double Price { get; set; }

		public string ImageURL { get; set; }

		public List<ListItemDTO> DishGroups { get; set; }

		#endregion
	}
}
