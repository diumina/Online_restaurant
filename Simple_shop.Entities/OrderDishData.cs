using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_shop.Entities
{
	[Table("OrdersDishesData")]
	public class OrderDishData
	{
		#region Properties

		[Key]
		public int Id { get; set; }

		public int Amount { get; set; }

		#endregion

		#region References

		public int OrderId { get; set; }

		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }

		public int DishId { get; set; }

		[ForeignKey("DishId")]
		public virtual Dish Dish { get; set; }

		#endregion
	}
}
