using DogalEmlak.Entities;
using DogalEmlak.Models;
using DogalEmlak.Service;
using DogalEmlak.Service.Abstract;
using DogalEmlak.Service.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Security.Claims;

namespace DogalEmlak.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
	{
        UserService userService = new UserService();

        //Admin
        public async Task<IActionResult> IndexAsync()
        {
            UserDetailModel model = await userService.GetUserDetailModelAsync(HttpContext.User.FindFirstValue("Id"));
            return View(model);
        }


        // Account/Register
        [AllowAnonymous]
        public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
		public ActionResult Register(UserRegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			try
			{
                userService.Add(model, "Member");
                return RedirectToAction(nameof(Login));
            }
			catch (Exception ex)
			{
                ModelState.AddModelError("", ex.Message);
                return View(model);
			}
		}


        // Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
				User user = userService.GetLogin(model);

				//cookie ile yetkilendir
				List<Claim> claims = new List<Claim>();
				claims.Add(new Claim("Id", user.Id.ToString()));
				claims.Add(new Claim("FirstName", user.FirstName));
				claims.Add(new Claim("LastName", user.LastName));
				claims.Add(new Claim("UserName", user.UserName));
				claims.Add(new Claim("Password", user.Password));
				claims.Add(new Claim("Email", user.Email));
				claims.Add(new Claim("Phone", user.Phone));
				claims.Add(new Claim(ClaimTypes.Role, user.Role.Authority));
				ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookies");
				ClaimsPrincipal principal = new ClaimsPrincipal(identity);
				HttpContext.SignInAsync("Cookies", principal);

				if (user.Role.Authority.Equals("Admin"))
				{
					return RedirectToAction("Index", "Admin");
				}

				return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
				return View(model);
			}
        }


		// Account/Logout
		public ActionResult Logout()
		{
			HttpContext.SignOutAsync("Cookies");
            return RedirectToAction(nameof(Login));
        }


		// Account/Update
		public ActionResult Update()
		{
            UserUpdateModel model = userService.GetUserUpdateModel(HttpContext.User.FindFirstValue("Id"));
            return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(UserUpdateModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                User user = userService.Update(model, HttpContext.User.FindFirstValue("Id"));
                //cookie ile yeniden yetkilendir (isim numara vs değişti)
                HttpContext.SignOutAsync("Cookies");
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Id", user.Id.ToString()));
                claims.Add(new Claim("FirstName", user.FirstName));
                claims.Add(new Claim("LastName", user.LastName));
                claims.Add(new Claim("UserName", user.UserName));
                claims.Add(new Claim("Password", user.Password));
                claims.Add(new Claim("Email", user.Email));
                claims.Add(new Claim("Phone", user.Phone));
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Authority));
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookies");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("Cookies", principal);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }



		// Account/Delete
		// cookie den user ı bulup siler
		public ActionResult Delete()
		{
			try
			{
				userService.Delete(HttpContext.User.FindFirstValue("Id"));
				HttpContext.SignOutAsync("Cookies");
				return RedirectToAction(nameof(Register));
			}
			catch
			{
				return View();
			}
		}
	}
}
