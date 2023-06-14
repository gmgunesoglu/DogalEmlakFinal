using DogalEmlak.Models;
using DogalEmlak.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogalEmlak.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MembersController : Controller
	{
		UserService userService = new UserService();


		// memberslar listelenecek ve aksiyonlar => [Details] [Block/Unblock]
		public async Task<ActionResult> IndexAsync()
		{
			// ama sadece memberlar olmalı
			List<UserListModel> models = await userService.GetUsersInRoleAsync("Member");
			return View(models);
		}

		//Admin/Members/Details/{id}
		public async Task<ActionResult> DetailsAsync(Guid id)
		{
			UserDetailModel model = await userService.GetUserDetailModelAsync(id);
            return View(model);
		}


		// block
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Block(Guid id)
		{
			try
			{
				userService.Block(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}


		// unblock
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UnBlock(Guid id)
		{
			try
			{
				userService.UnBlock(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
    }
}
