using DogalEmlak.Models;
using DogalEmlak.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogalEmlak.WebUI.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Admin,Staff")]
    public class MainController : Controller
    {
        UserService userService = new UserService();

        //Staff
        public async Task<IActionResult> IndexAsync()
        {
            UserDetailModel model = await userService.GetUserDetailModelAsync(HttpContext.User.FindFirstValue("Id"));
            return View(model);
        }
    }
}
