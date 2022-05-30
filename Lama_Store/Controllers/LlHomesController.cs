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
    public class LlHomesController : Controller
    {
        private readonly ModelContext _context;

        public LlHomesController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlHomes
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlHomes.Include(l => l.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: LlHomes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llHome = await _context.LlHomes
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (llHome == null)
            {
                return NotFound();
            }

            return View(llHome);
        }

        // GET: LlHomes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId");
            return View();
        }

        // POST: LlHomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeId,BgImagePath,BbgImagePath,BggImagePath,Texitt,Texittt,UserId")] LlHome llHome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llHome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llHome.UserId);
            return View(llHome);
        }

        // GET: LlHomes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llHome = await _context.LlHomes.FindAsync(id);
            if (llHome == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llHome.UserId);
            return View(llHome);
        }

        // POST: LlHomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("HomeId,BgImagePath,BbgImagePath,BggImagePath,Texitt,Texittt,UserId")] LlHome llHome)
        {
            if (id != llHome.HomeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llHome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlHomeExists(llHome.HomeId))
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
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llHome.UserId);
            return View(llHome);
        }

        // GET: LlHomes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llHome = await _context.LlHomes
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (llHome == null)
            {
                return NotFound();
            }

            return View(llHome);
        }

        // POST: LlHomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llHome = await _context.LlHomes.FindAsync(id);
            _context.LlHomes.Remove(llHome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlHomeExists(decimal id)
        {
            return _context.LlHomes.Any(e => e.HomeId == id);
        }
    }
}
