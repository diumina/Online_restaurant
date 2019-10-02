using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_shop.Entities
{
	[Table("Users")]
	public class User : IdentityUser
	{
		#region Constructors

		public User()
		{
			Orders = new List<Order>();
		}

		#endregion
		
		#region Properties

		[StringLength(maximumLength: 100, MinimumLength = 3)]
		[Required]
		public string FirstName { get; set; }

		[StringLength(maximumLength: 100, MinimumLength = 3)]
		[Required]
		public string LastName { get; set; }

		public string Address { get; set; }

		#endregion

		#region References

		public virtual List<Order> Orders { get; set; }

		#endregion
	}
}
