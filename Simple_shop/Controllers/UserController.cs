using Microsoft.AspNetCore.Mvc;
using Simple_shop.Models;
using Simple_shop.Services.DTO;
using Simple_shop.Services.Infrastructure;
using Simple_shop.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Simple_shop.Controllers
{
	public class UserController : BaseController
	{
		#region Fields

		private readonly IUserService _userService;

		#endregion

		#region Constructors

		public UserController(IUserService userService)
		{
			this._userService = userService;
		}

		#endregion

		#region Public methods

		[HttpGet]
		[RequireHttps]
		public IActionResult Register()
		{
			return View(new RegisterViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(registerViewModel);
				}

				RegisterDTO registerDTO = new RegisterDTO()
				{
					Email = registerViewModel.Email,
					Password = registerViewModel.Password,
					FirstName = registerViewModel.FirstName,
					LastName = registerViewModel.LastName,
					Address = registerViewModel.Address
				};

				OperationDetails result = await _userService.CreateNewUserAsync(registerDTO);

				if (result.Succedeed)
				{
					return RedirectToAction("Login");
				}
			}
			catch (Exception ex)
			{
				//logging
			}

			return ErrorView();
		}

		[HttpGet]
		[RequireHttps]
		public IActionResult Login(string returnUrl = null)
		{
			return View(new LoginViewModel(){ ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(loginViewModel);
				}

				LoginDTO loginDTO = new LoginDTO()
				{
					Email = loginViewModel.Email,
					Password = loginViewModel.Password,
					RememberMe = loginViewModel.RememberMe
				};

				OperationDetails result = await _userService.SignInUserAsync(loginDTO);

				if (result.Succedeed)
				{
					if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl)&&Url.IsLocalUrl(loginViewModel.ReturnUrl))
					{
						return Redirect(loginViewModel.ReturnUrl);
					}

					return RedirectToAction("List", "Dish");
				}
			}
			catch (Exception ex)
			{
				//logging
			}

			return ErrorView();
		}



		#endregion

	}
}