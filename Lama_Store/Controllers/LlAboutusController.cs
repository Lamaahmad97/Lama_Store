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
    public class LlAboutusController : Controller
    {
        private readonly ModelContext _context;

        public LlAboutusController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlAboutus
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlAboutus.Include(l => l.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: LlAboutus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llAboutu = await _context.LlAboutus
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.AboutusId == id);
            if (llAboutu == null)
            {
                return NotFound();
            }

            return View(llAboutu);
        }

        // GET: LlAboutus/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId");
            return View();
        }

        // POST: LlAboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutusId,Descriptionn,UserId")] LlAboutu llAboutu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llAboutu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llAboutu.UserId);
            return View(llAboutu);
        }

        // GET: LlAboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llAboutu = await _context.LlAboutus.FindAsync(id);
            if (llAboutu == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llAboutu.UserId);
            return View(llAboutu);
        }

        // POST: LlAboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AboutusId,Descriptionn,UserId")] LlAboutu llAboutu)
        {
            if (id != llAboutu.AboutusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llAboutu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlAboutuExists(llAboutu.AboutusId))
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
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llAboutu.UserId);
            return View(llAboutu);
        }

        // GET: LlAboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llAboutu = await _context.LlAboutus
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.AboutusId == id);
            if (llAboutu == null)
            {
                return NotFound();
            }

            return View(llAboutu);
        }

        // POST: LlAboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llAboutu = await _context.LlAboutus.FindAsync(id);
            _context.LlAboutus.Remove(llAboutu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlAboutuExists(decimal id)
        {
            return _context.LlAboutus.Any(e => e.AboutusId == id);
        }
    }
}
