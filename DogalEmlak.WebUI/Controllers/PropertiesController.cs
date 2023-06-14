using DogalEmlak.Models;
using DogalEmlak.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogalEmlak.WebUI.Controllers
{
	[Authorize]
	public class PropertiesController : Controller
	{
		PropertyService propertyService = new PropertyService();

		// Properties
		public async Task<ActionResult> IndexAsync()
		{
			List<PropertyListModel> properties = await propertyService.PropertyListModelsAsync(HttpContext.User.FindFirstValue("Id"));

			return View(properties);
		}


		// Properties/Add
		public ActionResult Add()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddAsync(PropertyCreateModel model)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Model is not valid");
				return View(model);
			}
			//kontrol
			foreach (IFormFile file in model.Files)
			{
				if (file == null || file.Length == 0)
				{
					return BadRequest("Please select an image file");
				}

				if (!file.ContentType.StartsWith("image/"))
				{
					return BadRequest("Only image files are allowed");
				}
			}
			//eklemeye çalış
			try
			{
				Guid id = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
				await propertyService.AddAsync(model,id);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}
		}


		// Properties/Update/{x}
		public async Task<ActionResult> UpdateAsync(Guid id)
		{
			PropertyUpdateModel model = await propertyService.GetPropertyUpdateModelAsync(id);
            return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> UpdateAsync(PropertyUpdateModel model)
		{
			try
			{
				await propertyService.UpdateProperty(model);			
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}
			return RedirectToAction(nameof(Index));
		}


		// POST: PropertiesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(Guid id)
		{
			try
			{
				await propertyService.RemoveAsync(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
