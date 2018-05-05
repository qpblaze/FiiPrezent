using System;
using System.Threading.Tasks;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FiiPrezent.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [Route("login")]
        public IActionResult LogIn(string returnUrl = "/")
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
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(LogInCallBack), "Accounts")
            };

            return Challenge(authProperties, "Facebook");
        }

        public async Task<IActionResult> LogInCallBack()
        {
            if (!(await _accountsService.Exists(User.GetNameIdentifier())))
            {
                Account account = new Account
                {
                    Id = Guid.NewGuid(),
                    NameIdentifier = User.GetNameIdentifier(),
                    Name = User.GetName(),
                    Email = User.GetEmail(),
                    Picture = User.GetProfileImage()
                };

                await _accountsService.CreateAccount(account);
            }

            return Redirect(TempData["returnUrl"].ToString());
        }
    }
}