using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_shop.Services.DTO
{
	public class LoginDTO
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
