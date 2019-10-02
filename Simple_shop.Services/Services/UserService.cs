using Microsoft.AspNetCore.Identity;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using Simple_shop.Repository.Repositories;
using Simple_shop.Repository.UnitOfWork;
using Simple_shop.Services.DTO;
using Simple_shop.Services.Infrastructure;
using Simple_shop.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Simple_shop.Services.Services
{
	public class UserService : IUserService
	{
		#region Fields

		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;

		#endregion

		#region Constructors

		public UserService(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			this._userRepository = unitOfWork.Repository<User>(typeof(UserRepository)) as IUserRepository;
		}

		#endregion

		#region Public methods

		public async Task<OperationDetails> CreateNewUserAsync(RegisterDTO registerDTO)
		{
			User userToCreate = new User()
			{
				Email = registerDTO.Email,
				UserName = registerDTO.Email,
				FirstName = registerDTO.FirstName,
				LastName = registerDTO.LastName,
				Address = registerDTO.Address
			};

			IdentityResult result = await _unitOfWork.UserManager.CreateAsync(userToCreate, registerDTO.Password);

			if (result.Succeeded)
			{
				await _unitOfWork.SignInManager.SignInAsync(userToCreate, true);
			}

			return new OperationDetails(result.Succeeded);
		}

		public async Task<OperationDetails> SignInUserAsync(LoginDTO loginDTO)
		{
			SignInResult result = await _unitOfWork.SignInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
				loginDTO.RememberMe, false);

			return new OperationDetails(result.Succeeded);
		}

		#endregion

	}
}
