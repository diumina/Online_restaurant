using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Simple_shop.Models;
using Simple_shop.Services.DTO;
using Simple_shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_shop.Controllers
{
	public class DishController : BaseController
	{
		#region Fields

		private readonly IDishService _dishService;
		private readonly IHostingEnvironment _hostingEnvironment;

		#endregion

		#region Constructors

		public DishController(IDishService dishService, IHostingEnvironment hostingEnvironment)
		{
			this._dishService = dishService;
			_hostingEnvironment = hostingEnvironment;
		}

		#endregion

		#region Public methods

		public IActionResult Main()
		{
			return View();
		}

		public async Task<IActionResult> List(int id)
		{
			try
			{
				IEnumerable<DishListItemViewModel> dishList = (await _dishService.GetDishesByGroupIdAsync(id))
					.Select(x => new DishListItemViewModel
					{
						Id = x.Id,
						Name = x.Name,
						Weight = x.Weight,
						Price = x.Price,
						ImageURL = x.ImageURL
					});

				return View(dishList);
			}
			catch (Exception)
			{
				//logging
			}

			return ErrorView();
		}

		public async Task<IActionResult> Info(int id)
		{
			try
			{
				DishInfoDTO dish = await _dishService.GetDishInfoByIdAsync(id);

				if (dish != null)
				{
					DishInfoViewModel dishInfo = new DishInfoViewModel
					{
						Id = dish.Id,
						Name = dish.Name,
						Description = dish.Description,
						Weight = dish.Weight,
						Price = dish.Price,
						ImageURL = dish.ImageURL,
						Amount = dish.Amount
					};

					return View(dishInfo);
				}
			}
			catch (Exception)
			{
			}

			return ErrorView();
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			try
			{
				DishEditDTO dishEditDTO = await _dishService.GetCreateDishEditDTOAsync();

				DishEditViewModel dishEditViewModel = new DishEditViewModel()
				{
					DishGroups = dishEditDTO.DishGroups
					.Select(x => new SelectListItem()
					{
						Text = x.Text,
						Value = x.Value
					}).ToList()
				};

				return View(dishEditViewModel);
			}
			catch (Exception)
			{
				//logging
			}

			return ErrorView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DishEditViewModel dishEditViewModel, IFormFile uploadedImage)
		{
			try
			{
				DishEditDTO dishEditDTO = new DishEditDTO()
				{
					Name = dishEditViewModel.Name,
					Description = dishEditViewModel.Description,
					Weight = dishEditViewModel.Weight,
					Price = dishEditViewModel.Price,
					DishGroupId = dishEditViewModel.DishGroupId,
					ImageURL = "/images/No_Image_Available.jpg"
				};

				bool response = await _dishService.CreateNewDishAsync(dishEditDTO);

				if (response)
				{
					dishEditDTO.ImageURL = await SaveDishImageAsync(uploadedImage, dishEditDTO.Id);

					bool responseImg = await _dishService.EditDishImageAsync(dishEditDTO);

					if (responseImg)
					{
						return RedirectToAction("Info", new { id = dishEditDTO.Id });
					}
				}

			}
			catch (Exception ex)
			{
				//logging
			}

			return ErrorView();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				DishEditDTO dishEditDTO = await _dishService.GetDishByIdForEditAsync(id);

				if (dishEditDTO != null)
				{
					DishEditViewModel dishEditViewModel = new DishEditViewModel()
					{
						Name = dishEditDTO.Name,
						Description = dishEditDTO.Description,
						Weight = dishEditDTO.Weight,
						Price = dishEditDTO.Price,
						DishGroupId = dishEditDTO.DishGroupId,
						ImageURL = dishEditDTO.ImageURL,
						DishGroups = dishEditDTO.DishGroups
						.Select(x => new SelectListItem()
						{
							Text = x.Text,
							Value = x.Value
						}).ToList()
					};

					return View(dishEditViewModel);
				}
			}
			catch (Exception)
			{
				//logging
			}

			return ErrorView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DishEditViewModel dishEditViewModel, IFormFile uploadedImage)
		{
			try
			{
				DishEditDTO dishEditDTO = new DishEditDTO()
				{
					Id = dishEditViewModel.Id,
					Name = dishEditViewModel.Name,
					Description = dishEditViewModel.Description,
					Weight = dishEditViewModel.Weight,
					Price = dishEditViewModel.Price,
					DishGroupId = dishEditViewModel.DishGroupId,
					ImageURL = await SaveDishImageAsync(uploadedImage, dishEditViewModel.Id)
				};

				bool response = await _dishService.EditDishAsync(dishEditDTO, _hostingEnvironment.WebRootPath);

				if (response)
				{
					return RedirectToAction("Info", new { id = dishEditDTO.Id });
				}
			}
			catch (Exception ex)
			{
				//logging
			}

			return ErrorView();
		}

		#endregion

		#region Private methods

		private async Task<string> SaveDishImageAsync(IFormFile uploadedImage, int dishId)
		{
			if (uploadedImage != null)
			{
				string dishDirectoryVirtualPath = $"/images/dishes/dish-{dishId}";

				if (!Directory.Exists(MapContentPath(dishDirectoryVirtualPath)))
				{
					Directory.CreateDirectory(MapContentPath(dishDirectoryVirtualPath));
				}

				string newImageVirtualUrl = $"{dishDirectoryVirtualPath}/{Guid.NewGuid()}.{uploadedImage.ContentType.Split("/")[1]}";
				string newImagePhysicalUrl = $"{_hostingEnvironment.WebRootPath}{newImageVirtualUrl}";

				using (FileStream fileStream = new FileStream(newImagePhysicalUrl, FileMode.Create))
				{
					await uploadedImage.CopyToAsync(fileStream);

					if (System.IO.File.Exists(newImagePhysicalUrl))
					{
						return newImageVirtualUrl;
					}
				}
			}

			return null;
		}

		#endregion
	}
}