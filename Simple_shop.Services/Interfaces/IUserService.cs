using Microsoft.AspNetCore.Identity;
using Simple_shop.Services.DTO;
using Simple_shop.Services.Infrastructure;
using System.Threading.Tasks;

namespace Simple_shop.Services.Interfaces
{
	public interface IUserService
	{
		Task<OperationDetails> CreateNewUserAsync(RegisterDTO registerDTO);
		Task<OperationDetails> SignInUserAsync(LoginDTO loginDTO);
	}
}
