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
    public class LlOrdersController : Controller
    {
        private readonly ModelContext _context;

        public LlOrdersController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlOrders
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlOrders.Include(l => l.User).Include(l => l.Visa);
            return View(await modelContext.ToListAsync());
        }

        // GET: LlOrders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llOrder = await _context.LlOrders
                .Include(l => l.User)
                .Include(l => l.Visa)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (llOrder == null)
            {
                return NotFound();
            }

            return View(llOrder);
        }

        // GET: LlOrders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId");
            ViewData["VisaId"] = new SelectList(_context.LlVisas, "VisaId", "VisaId");
            return View();
        }

        // POST: LlOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderName,UserId,VisaId")] LlOrder llOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llOrder.UserId);
            ViewData["VisaId"] = new SelectList(_context.LlVisas, "VisaId", "VisaId", llOrder.VisaId);
            return View(llOrder);
        }

        // GET: LlOrders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llOrder = await _context.LlOrders.FindAsync(id);
            if (llOrder == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llOrder.UserId);
            ViewData["VisaId"] = new SelectList(_context.LlVisas, "VisaId", "VisaId", llOrder.VisaId);
            return View(llOrder);
        }

        // POST: LlOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("OrderId,OrderName,UserId,VisaId")] LlOrder llOrder)
        {
            if (id != llOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlOrderExists(llOrder.OrderId))
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
            ViewData["UserId"] = new SelectList(_context.LlUsers, "UserId", "UserId", llOrder.UserId);
            ViewData["VisaId"] = new SelectList(_context.LlVisas, "VisaId", "VisaId", llOrder.VisaId);
            return View(llOrder);
        }

        // GET: LlOrders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llOrder = await _context.LlOrders
                .Include(l => l.User)
                .Include(l => l.Visa)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (llOrder == null)
            {
                return NotFound();
            }

            return View(llOrder);
        }

        // POST: LlOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llOrder = await _context.LlOrders.FindAsync(id);
            _context.LlOrders.Remove(llOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlOrderExists(decimal id)
        {
            return _context.LlOrders.Any(e => e.OrderId == id);
        }
    }
}
