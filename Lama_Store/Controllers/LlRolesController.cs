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
    public class LlRolesController : Controller
    {
        private readonly ModelContext _context;

        public LlRolesController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.LlRoles.ToListAsync());
        }

        // GET: LlRoles/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llRole = await _context.LlRoles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (llRole == null)
            {
                return NotFound();
            }

            return View(llRole);
        }

        // GET: LlRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LlRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName")] LlRole llRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(llRole);
        }

        // GET: LlRoles/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llRole = await _context.LlRoles.FindAsync(id);
            if (llRole == null)
            {
                return NotFound();
            }
            return View(llRole);
        }

        // POST: LlRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RoleId,RoleName")] LlRole llRole)
        {
            if (id != llRole.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlRoleExists(llRole.RoleId))
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
            return View(llRole);
        }

        // GET: LlRoles/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llRole = await _context.LlRoles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (llRole == null)
            {
                return NotFound();
            }

            return View(llRole);
        }

        // POST: LlRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llRole = await _context.LlRoles.FindAsync(id);
            _context.LlRoles.Remove(llRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlRoleExists(decimal id)
        {
            return _context.LlRoles.Any(e => e.RoleId == id);
        }
    }
}
