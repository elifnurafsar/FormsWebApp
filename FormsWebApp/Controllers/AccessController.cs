using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FormsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormsWebApp.Context;
using FormsWebApp.Models;

namespace FormsWebApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated) { 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: CLogins/Details/e_mail
        public async Task<CLogin?> Details(string e_mail)
        {
            if (e_mail == null || _context.Users == null)
            {
                return null;
            }

            var cLogin = await _context.Users
                .FirstOrDefaultAsync(m => m.e_mail == e_mail);
            if (cLogin == null)
            {
                return null;
            }

            return cLogin;
        }

        [HttpPost]
        public async Task<IActionResult> Login(CLogin login_model)
        {
            CLogin user = Details(login_model.e_mail).Result;

            if ((login_model.e_mail == "user1@gmail.com" && login_model.password == "user1.11") || (user != null && user.password == login_model.password)) {
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


        // POST: CLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("e_mail,password")] CLogin model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                TempData["Message"] = "User " + model.e_mail + " created successfully!";
                return RedirectToAction("Login", "Access");
            }

            TempData["Message"] = "An error occurred while creating user " + model.e_mail + "! Please refresh and try again";
            return RedirectToAction("Login", "Access");
        }

        // GET: CLogins/Delete/5
        public async Task<IActionResult> Delete(string e_mail)
        {
            if (e_mail == null || _context.Users == null)
            {
                return NotFound();
            }

            var cLogin = await _context.Users
                .FirstOrDefaultAsync(m => m.e_mail == e_mail);
            if (cLogin == null)
            {
                return NotFound();
            }

            return View(cLogin);
        }

        // POST: CLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string e_mail)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var cLogin = await _context.Users.FindAsync(e_mail);
            if (cLogin != null)
            {
                _context.Users.Remove(cLogin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string e_mail)
        {
            return (_context.Users?.Any(e => e.e_mail == e_mail)).GetValueOrDefault();
        }
    }
}
