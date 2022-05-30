using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lama_Store.Models;

namespace Lama_Store.Controllers
{
    public class LlLoginsController : Controller
    {
        private readonly ModelContext _context;

        public LlLoginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlLogins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlLogins.Include(l => l.Role).Include(l => l.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: LlLogins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llLogin = await _context.LlLogins
                .Include(l => l.Role)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LoginId == id);
            if (llLogin == null)
            {
                return NotFound();
            }

            return View(llLogin);
        }

        // GET: LlLogins/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.LlRoles, "RoleId", "RoleId");
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId");
            return View();
        }

        // POST: LlLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoginId,UserName,LoginPass,RoleId,UserId")] LlLogin llLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.LlRoles, "RoleId", "RoleId", llLogin.RoleId);
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llLogin.UserId);
            return View(llLogin);
        }

        // GET: LlLogins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llLogin = await _context.LlLogins.FindAsync(id);
            if (llLogin == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.LlRoles, "RoleId", "RoleId", llLogin.RoleId);
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llLogin.UserId);
            return View(llLogin);
        }

        // POST: LlLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("LoginId,UserName,LoginPass,RoleId,UserId")] LlLogin llLogin)
        {
            if (id != llLogin.LoginId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlLoginExists(llLogin.LoginId))
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
            ViewData["RoleId"] = new SelectList(_context.LlRoles, "RoleId", "RoleId", llLogin.RoleId);
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llLogin.UserId);
            return View(llLogin);
        }

        // GET: LlLogins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llLogin = await _context.LlLogins
                .Include(l => l.Role)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LoginId == id);
            if (llLogin == null)
            {
                return NotFound();
            }

            return View(llLogin);
        }

        // POST: LlLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llLogin = await _context.LlLogins.FindAsync(id);
            _context.LlLogins.Remove(llLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlLoginExists(decimal id)
        {
            return _context.LlLogins.Any(e => e.LoginId == id);
        }
    }
}
