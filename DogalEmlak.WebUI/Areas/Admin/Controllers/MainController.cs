using DogalEmlak.Models;
using DogalEmlak.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogalEmlak.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
	public class MainController : Controller
	{
        UserService userService = new UserService();

        //Admin
        public async Task<IActionResult> IndexAsync()
		{
            UserDetailModel model = await userService.GetUserDetailModelAsync(HttpContext.User.FindFirstValue("Id"));
            return View(model);
        }
    }
}
