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
    public class LlTestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public LlTestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlTestimonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlTestimonials.Include(l => l.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: LlTestimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llTestimonial = await _context.LlTestimonials
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (llTestimonial == null)
            {
                return NotFound();
            }

            return View(llTestimonial);
        }

        // GET: LlTestimonials/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId");
            return View();
        }

        // POST: LlTestimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestimonialId,Message,Status,BgImagePath,UserId")] LlTestimonial llTestimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llTestimonial.UserId);
            return View(llTestimonial);
        }

        // GET: LlTestimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llTestimonial = await _context.LlTestimonials.FindAsync(id);
            if (llTestimonial == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llTestimonial.UserId);
            return View(llTestimonial);
        }

        // POST: LlTestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TestimonialId,Message,Status,BgImagePath,UserId")] LlTestimonial llTestimonial)
        {
            if (id != llTestimonial.TestimonialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llTestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlTestimonialExists(llTestimonial.TestimonialId))
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
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llTestimonial.UserId);
            return View(llTestimonial);
        }

        // GET: LlTestimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llTestimonial = await _context.LlTestimonials
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (llTestimonial == null)
            {
                return NotFound();
            }

            return View(llTestimonial);
        }

        // POST: LlTestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llTestimonial = await _context.LlTestimonials.FindAsync(id);
            _context.LlTestimonials.Remove(llTestimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlTestimonialExists(decimal id)
        {
            return _context.LlTestimonials.Any(e => e.TestimonialId == id);
        }
    }
}
