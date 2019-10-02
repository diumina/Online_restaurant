using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Simple_shop.Controllers
{
	public class BaseController : Controller
	{
		#region Public methods

		public IActionResult ErrorView(bool isPartial = false)
		{
			if (isPartial)
			{
				return PartialView("Error");
			}

			return View("Error");
		}

		public string MapContentPath(string virtualPath)
		{
			if (!virtualPath.Contains("~"))
			{
				virtualPath = $"~/wwwroot/{virtualPath}";
			}

			return Path.GetFullPath(virtualPath).Replace("~", "");
		}

		#endregion
	}
}