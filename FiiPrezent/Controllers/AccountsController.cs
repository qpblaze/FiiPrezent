using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FiiPrezent.Controllers
{
    public class AccountsController : Controller
    {
        [Route("login")]
        public IActionResult LogIn(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult FacebookLogIn()
        {
            string returnUrl = Convert.ToString(TempData["returnUrl"]);
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }

            var authProperties = new AuthenticationProperties
            {
                RedirectUri = returnUrl
            };

            return Challenge(authProperties, "Facebook");
        }
    }
}