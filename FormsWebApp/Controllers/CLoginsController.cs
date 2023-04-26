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
    public class CLoginsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CLoginsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CLogins
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }

        // GET: CLogins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var cLogin = await _context.Users
                .FirstOrDefaultAsync(m => m.e_mail == id);
            if (cLogin == null)
            {
                return NotFound();
            }

            return View(cLogin);
        }

        // GET: CLogins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("e_mail,password")] CLogin cLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cLogin);
        }

        // GET: CLogins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var cLogin = await _context.Users.FindAsync(id);
            if (cLogin == null)
            {
                return NotFound();
            }
            return View(cLogin);
        }

        // POST: CLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("e_mail,password")] CLogin cLogin)
        {
            if (id != cLogin.e_mail)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CLoginExists(cLogin.e_mail))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cLogin);
        }

        // GET: CLogins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var cLogin = await _context.Users
                .FirstOrDefaultAsync(m => m.e_mail == id);
            if (cLogin == null)
            {
                return NotFound();
            }

            return View(cLogin);
        }

        // POST: CLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var cLogin = await _context.Users.FindAsync(id);
            if (cLogin != null)
            {
                _context.Users.Remove(cLogin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CLoginExists(string id)
        {
            return (_context.Users?.Any(e => e.e_mail == id)).GetValueOrDefault();
        }
    }
}
