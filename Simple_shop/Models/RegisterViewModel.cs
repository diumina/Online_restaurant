using System.ComponentModel.DataAnnotations;

namespace Simple_shop.Models
{
	public class RegisterViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)] 
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		[Display(Name = "Confirm password")]
		public string PasswordConfirm { get; set; }

		[Required]
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last name")]
		public string LastName { get; set; }

		[Required]
		public string Address { get; set; }
	}
}
