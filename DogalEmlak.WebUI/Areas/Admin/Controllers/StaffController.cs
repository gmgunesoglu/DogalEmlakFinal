using DogalEmlak.Models;
using DogalEmlak.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace DogalEmlak.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
	{
        UserService userService = new UserService();


        // staff listelenecek ve aksiyonlar => [Details] [Block/Unblock]
        // /Admin/Staff
        public async Task<ActionResult> IndexAsync()
        {
            // ama sadece memberlar olmalı
            List<UserListModel> models = await userService.GetUsersInRoleAsync("Staff");
            return View(models);
        }

        //Admin/Staff/Details/{id}
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

        // Admin/Staff/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                userService.Add(model,"Staff");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
