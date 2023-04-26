using FormsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using FormsWebApp.Context;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Policy;

namespace FormsWebApp.Controllers
{
    //You can only enter the home if you're logged in.
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string _username = claimUser.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value.ToString();
            ViewData["username"] = _username;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Fields/Details/5
        public async Task<IActionResult> GetField(int? id)
        {
            if (id == null || _context.Field == null)
            {
                return NotFound();
            }

            var @field = await _context.Field
                .FirstOrDefaultAsync(m => m.id == id);
            if (@field == null)
            {
                return NotFound();
            }

            return View(@field);
        }

        // GET: Forms/Details/5
        public async Task<JsonResult> ShowForm(int? id)
        {
          
            if (id == null || _context.Form == null)
            {
                return Json(null);
            }

            var forms = await _context.Form
                .FirstOrDefaultAsync(m => m.id == id);
            if (forms == null)
            {
                return Json(null);
            }

            return Json(forms);
        }

        public IQueryable<Form> GetAllForms()
        {
            return _context.Form.AsQueryable();
        }

        // GET: Forms/Details/5
        public async Task<JsonResult> GetForms()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string username = claimUser.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value.ToString();
           
            if (username == null || _context.Form == null)
            {
                return Json(null);
            }

            //var forms = await _context.Form.ToArrayAsync(m => m.user_e_mail == username);
            var forms = GetAllForms().Where(m => m.user_e_mail == username).ToList();
            if (forms == null)
            {
                return Json(null);
            }
            
            return Json(forms);
        }

        [Route("api/AjaxAPI/AjaxMethod")]
        [HttpPost]
         public async Task<bool> AddNewForm([FromBody]Form new_form)
         {
            ClaimsPrincipal claimUser = HttpContext.User;
            string username = claimUser.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value.ToString();
            Form f = new Form();
            f.user_e_mail = username;
            f.created_at = DateTime.UtcNow;
            f.description = new_form.description;
            f.name = new_form.name;
            int id = 0;
            _context.Add(f);
            await _context.SaveChangesAsync();
            id = f.id;
            for (int i = 0; i < new_form.fields.Count(); i++)
            {
                Field field = new Field();
                field.required = new_form.fields.ElementAt(i).required;
                field.form_id = id;
                field.form = new_form;
                field.name = new_form.fields.ElementAt(i).name;
                field.dataType = new_form.fields.ElementAt(i).dataType;
                _context.Add(field);
                await _context.SaveChangesAsync();
            }
            return true;
         }

        

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(int id)
        {
            if (_context.Form == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Form'  is null.");
            }
            var form = await _context.Form.FindAsync(id);
            if (form != null)
            {
                _context.Form.Remove(form);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteField(int id)
        {
            if (_context.Field == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Field'  is null.");
            }
            var @field = await _context.Field.FindAsync(id);
            if (@field != null)
            {
                _context.Field.Remove(@field);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldExists(int id)
        {
            return (_context.Field?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private bool FormExists(int id)
        {
            return (_context.Form?.Any(e => e.id == id)).GetValueOrDefault();
        }

    }
}