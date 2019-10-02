using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_shop.Entities
{
	[Table("Orders")]
	public class Order
	{
		#region Constructors

		public Order()
		{
			OrdersDishesData = new List<OrderDishData>();
		}

		#endregion

		#region Properties

		[Key]
		public int Id { get; set; }

		[Required]
		public int Number { get; set; }

		#endregion

		#region References

		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		public virtual List<OrderDishData> OrdersDishesData { get; set; }

		#endregion
	}
}
