using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lama_Store.Models;
using Microsoft.AspNetCore.Http;

namespace Lama_Store.Controllers
{
    public class LlConatctusController : Controller
    {
        private readonly ModelContext _context;

        public LlConatctusController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlConatctus
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlConatctus.Include(l => l.User);
            return View(await modelContext.ToListAsync());
        }

        public IActionResult ContactUs()
        {
            return RedirectToAction("ContactUs", "Home");
        }

        // GET: LlConatctus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llConatctu = await _context.LlConatctus
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ContactusId == id);
            if (llConatctu == null)
            {
                return NotFound();
            }

            return View(llConatctu);
        }

        // GET: LlConatctus/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId");
            return View();
        }

        // POST: LlConatctus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactusId,Namee,Email,Message,UserId")] LlConatctu llConatctu)
        {
            int? user = HttpContext.Session.GetInt32("UserId");
            if (user != null)
            {
                llConatctu.UserId = user;
            }
            if (ModelState.IsValid)
            {
                _context.Add(llConatctu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llConatctu.UserId);
            return View(llConatctu);
        }

        // GET: LlConatctus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llConatctu = await _context.LlConatctus.FindAsync(id);
            if (llConatctu == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llConatctu.UserId);
            return View(llConatctu);
        }

        // POST: LlConatctus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ContactusId,Namee,Email,Message,UserId")] LlConatctu llConatctu)
        {
            if (id != llConatctu.ContactusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llConatctu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlConatctuExists(llConatctu.ContactusId))
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
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llConatctu.UserId);
            return View(llConatctu);
        }

        // GET: LlConatctus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llConatctu = await _context.LlConatctus
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ContactusId == id);
            if (llConatctu == null)
            {
                return NotFound();
            }

            return View(llConatctu);
        }

        // POST: LlConatctus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llConatctu = await _context.LlConatctus.FindAsync(id);
            _context.LlConatctus.Remove(llConatctu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlConatctuExists(decimal id)
        {
            return _context.LlConatctus.Any(e => e.ContactusId == id);
        }
    }
}
