using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_shop.Entities
{
	[Table("Dishes")]
	public class Dish
	{
		#region Constructors

		public Dish()
		{
			OrdersDishesData = new List<OrderDishData>();
		}

		#endregion

		#region Properties

		[Key]
		public int Id { get; set; }

		[StringLength(maximumLength: 50, MinimumLength = 5)]
		[Required]
		public string Name { get; set; }

		[StringLength(maximumLength: 500, MinimumLength = 10)]
		[Required]
		public string Description { get; set; }

		[Required]
		public double Weight { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public string ImageURL { get; set; }

		#endregion

		#region References

		public int DishGroupId { get; set; }

		[ForeignKey("DishGroupId")]
		public virtual DishGroup DishGroup { get; set; }

		public virtual List<OrderDishData> OrdersDishesData { get; set; }

		#endregion
	}
}
