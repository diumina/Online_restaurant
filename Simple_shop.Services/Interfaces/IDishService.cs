using Simple_shop.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simple_shop.Services.Interfaces
{
	public interface IDishService
	{
		Task<IEnumerable<DishListItemDTO>> GetDishesByGroupIdAsync(int id);

		Task<DishInfoDTO> GetDishInfoByIdAsync(int id);

		Task<DishEditDTO> GetCreateDishEditDTOAsync();

		Task<bool> CreateNewDishAsync(DishEditDTO dishEditDTO);

		Task<bool> EditDishImageAsync(DishEditDTO dishEditDTO);

		Task<DishEditDTO> GetDishByIdForEditAsync(int id);

		Task<bool> EditDishAsync(DishEditDTO dishEditDTO, string webRootPath);
	}
}
