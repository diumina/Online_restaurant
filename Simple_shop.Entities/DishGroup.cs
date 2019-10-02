using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_shop.Entities
{
	[Table("DishGroups")]
	public class DishGroup
	{
		#region Constructors

		public DishGroup()
		{
			Dishes = new List<Dish>();
		}

		#endregion

		#region Properties

		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		#endregion

		#region References

		public virtual List<Dish> Dishes { get; set; }

		#endregion
	}
}
