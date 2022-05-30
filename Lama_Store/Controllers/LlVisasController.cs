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
    public class LlVisasController : Controller
    {
        private readonly ModelContext _context;

        public LlVisasController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlVisas
        public async Task<IActionResult> Index()
        {
            return View(await _context.LlVisas.ToListAsync());
        }

        // GET: LlVisas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llVisa = await _context.LlVisas
                .FirstOrDefaultAsync(m => m.VisaId == id);
            if (llVisa == null)
            {
                return NotFound();
            }

            return View(llVisa);
        }

        // GET: LlVisas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LlVisas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisaId,VisaName,VisaPass,VisaBalance")] LlVisa llVisa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llVisa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(llVisa);
        }

        // GET: LlVisas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llVisa = await _context.LlVisas.FindAsync(id);
            if (llVisa == null)
            {
                return NotFound();
            }
            return View(llVisa);
        }

        // POST: LlVisas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("VisaId,VisaName,VisaPass,VisaBalance")] LlVisa llVisa)
        {
            if (id != llVisa.VisaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llVisa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlVisaExists(llVisa.VisaId))
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
            return View(llVisa);
        }

        // GET: LlVisas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llVisa = await _context.LlVisas
                .FirstOrDefaultAsync(m => m.VisaId == id);
            if (llVisa == null)
            {
                return NotFound();
            }

            return View(llVisa);
        }

        // POST: LlVisas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llVisa = await _context.LlVisas.FindAsync(id);
            _context.LlVisas.Remove(llVisa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlVisaExists(decimal id)
        {
            return _context.LlVisas.Any(e => e.VisaId == id);
        }
    }
}
