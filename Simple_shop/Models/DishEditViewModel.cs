using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Simple_shop.Models
{
	public class DishEditViewModel
	{
		#region Constructor 

		public DishEditViewModel()
		{
			DishGroups = new List<SelectListItem>();
		}

		#endregion

		#region Properties

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public double Weight { get; set; }

		[Required]
		public double Price { get; set; }

		public string ImageURL { get; set; }

		[Required]
		[Display(Name = "Image")]
		public IFormFile UploadedImage { get; set; }

		public int DishGroupId { get; set; }

		[Display(Name = "Group")]
		public List<SelectListItem> DishGroups { get; set; } 

		#endregion
	}
}
