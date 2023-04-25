using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FormsWebApp.Models;

namespace FormsWebApp.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated) { 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CLogin login_model)
        {
            if(login_model.e_mail == "user1@gmail.com" && login_model.password == "user1.11") {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, login_model.e_mail),
                    new Claim("Role", "User")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = login_model.keep_logged_in
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "Wrong credentials provided!";
            return View();
        }
    }
}
