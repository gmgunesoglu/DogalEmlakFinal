using DogalEmlak.Models;
using DogalEmlak.Service;
using Microsoft.AspNetCore.Mvc;

namespace DogalEmlak.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		//servis için...
		PropertyService propertyService = new PropertyService();

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> IndexAsync()
		{
			List< PropertyListModel> models = await propertyService.PropertyListModelsAsync();
			return View(models);
		}

		[HttpPost]
		public async Task<IActionResult> DetailsAsync(Guid id)
		{
			PropertyModel model = await propertyService.GetPropertyModelAsync(id);
			return View(model);
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
    }
}