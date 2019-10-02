using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simple_shop.Entities;
using Simple_shop.Repository.Interfaces;
using Simple_shop.Repository.Repositories;
using Simple_shop.Repository.UnitOfWork;
using Simple_shop.Services.DTO;
using Simple_shop.Services.Interfaces;

namespace Simple_shop.Services.Services
{
	public class DishService : IDishService
	{
		#region Fields

		private readonly IUnitOfWork _unitOfWork;
		private readonly IDishRepository _dishRepository;
		private readonly IDishGroupRepository _dishGroupRepository;

		#endregion

		#region Constructors

		public DishService(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
			this._dishRepository = unitOfWork.Repository<Dish>(typeof(DishRepository)) as IDishRepository;
			this._dishGroupRepository = unitOfWork.Repository<DishGroup>(typeof(DishGroupRepository)) as IDishGroupRepository;
		}

		#endregion

		#region Public methods

		public async Task<IEnumerable<DishListItemDTO>> GetDishesByGroupIdAsync(int id)
		{
			IEnumerable<Dish> dishesByGroup = await _dishRepository.GetDishesByGroupIdAsync(x => x.DishGroupId == id);

			return dishesByGroup.Select(x => new DishListItemDTO()
			{
				Id = x.Id,
				Name = x.Name,
				Weight = x.Weight,
				Price = x.Price,
				ImageURL = x.ImageURL
			});
		}

		public async Task<DishInfoDTO> GetDishInfoByIdAsync(int id)
		{
			Dish dish = await _dishRepository.GetAsync(id);

			if (dish == null)
			{
				throw new System.Exception();
			}

			return new DishInfoDTO
			{
				Id = dish.Id,
				Name = dish.Name,
				Description = dish.Description,
				Weight = dish.Weight,
				Price = dish.Price,
				ImageURL = dish.ImageURL,
				Amount = dish.OrdersDishesData.Count
			};
		}

		/// <summary>
		/// Get DishEditDTO to create new dish
		/// </summary>
		/// <returns></returns>
		public async Task<DishEditDTO> GetCreateDishEditDTOAsync()
		{
			DishEditDTO dishEditDTO = new DishEditDTO()
			{
				DishGroups = (await _dishGroupRepository.GetAllAsync())
				.Select(x => new ListItemDTO()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList()
			};

			return dishEditDTO;
		}

		public async Task<bool> CreateNewDishAsync(DishEditDTO dishEditDTO)
		{
			Dish dishToCreate = new Dish()
			{
				Name = dishEditDTO.Name,
				Description = dishEditDTO.Description,
				Weight = dishEditDTO.Weight,
				Price = dishEditDTO.Price,
				DishGroupId = dishEditDTO.DishGroupId,
				ImageURL = dishEditDTO.ImageURL
			};
			
			_dishRepository.Add(dishToCreate);

			if (await _unitOfWork.SaveChangesAsync() > 0)
			{
				dishEditDTO.Id = dishToCreate.Id;

				return true;
			}

			return false;
		}

		public async Task<bool> EditDishImageAsync(DishEditDTO dishEditDTO)
		{
			Dish dishToUpdateImage = await _dishRepository.GetAsync(dishEditDTO.Id);

			if (dishToUpdateImage != null)
			{
				dishToUpdateImage.ImageURL = dishEditDTO.ImageURL;
				_dishRepository.Update(dishToUpdateImage);

				return (await _unitOfWork.SaveChangesAsync() > 0);
			}

			return false;
		}

		public async Task<DishEditDTO> GetDishByIdForEditAsync(int id)
		{
			Dish dish = await _dishRepository.GetAsync(id);

			if (dish != null)
			{
				return new DishEditDTO
				{
					Id = dish.Id,
					Name = dish.Name,
					Description = dish.Description,
					Weight = dish.Weight,
					Price = dish.Price,
					ImageURL = dish.ImageURL,
					DishGroupId = dish.DishGroupId,
					DishGroups = (await _dishGroupRepository.GetAllAsync())
					.Select(x => new ListItemDTO()
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}).ToList()
				};
			}

			return null;
		}

		public async Task<bool> EditDishAsync(DishEditDTO dishEditDTO, string webRootPath)
		{
			Dish dishToUpdate = await _dishRepository.GetAsync(dishEditDTO.Id);

			if (dishToUpdate != null)
			{
				dishToUpdate.Name = dishEditDTO.Name;
				dishToUpdate.Description = dishEditDTO.Description;
				dishToUpdate.Weight = dishEditDTO.Weight;
				dishToUpdate.Price = dishEditDTO.Price;
				dishToUpdate.DishGroupId = dishEditDTO.DishGroupId;

				string oldImageUrl = dishToUpdate.ImageURL;

				if (!String.IsNullOrEmpty(dishEditDTO.ImageURL))
				{
					dishToUpdate.ImageURL = dishEditDTO.ImageURL;
				}

				_dishRepository.Update(dishToUpdate);

				if (await _unitOfWork.SaveChangesAsync() > 0)
				{
					if (!String.IsNullOrEmpty(dishEditDTO.ImageURL) && File.Exists($"{webRootPath}/{oldImageUrl}"))
					{
						File.Delete($"{webRootPath}/{oldImageUrl}");
					}

					return true;
				}
			}

			return false;
		}

		#endregion

		#region Private methods

		

		#endregion
	}
}
