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
    public class LlProductOrdersController : Controller
    {
        private readonly ModelContext _context;

        public LlProductOrdersController(ModelContext context)
        {
            _context = context;
        }

        // GET: LlProductOrders
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlProductOrders.Include(l => l.Order).Include(l => l.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: LlProductOrders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llProductOrder = await _context.LlProductOrders
                .Include(l => l.Order)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.ProductOrderId == id);
            if (llProductOrder == null)
            {
                return NotFound();
            }

            return View(llProductOrder);
        }

        // GET: LlProductOrders/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.LlOrders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.LlProducts, "ProductId", "ProductId");
            return View();
        }

        // POST: LlProductOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductOrderId,Quantity,DateFrom,DateTo,OrderId,ProductId")] LlProductOrder llProductOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(llProductOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.LlOrders, "OrderId", "OrderId", llProductOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.LlProducts, "ProductId", "ProductId", llProductOrder.ProductId);
            return View(llProductOrder);
        }

        // GET: LlProductOrders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llProductOrder = await _context.LlProductOrders.FindAsync(id);
            if (llProductOrder == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.LlOrders, "OrderId", "OrderId", llProductOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.LlProducts, "ProductId", "ProductId", llProductOrder.ProductId);
            return View(llProductOrder);
        }

        // POST: LlProductOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ProductOrderId,Quantity,DateFrom,DateTo,OrderId,ProductId")] LlProductOrder llProductOrder)
        {
            if (id != llProductOrder.ProductOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(llProductOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlProductOrderExists(llProductOrder.ProductOrderId))
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
            ViewData["OrderId"] = new SelectList(_context.LlOrders, "OrderId", "OrderId", llProductOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.LlProducts, "ProductId", "ProductId", llProductOrder.ProductId);
            return View(llProductOrder);
        }

        // GET: LlProductOrders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llProductOrder = await _context.LlProductOrders
                .Include(l => l.Order)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.ProductOrderId == id);
            if (llProductOrder == null)
            {
                return NotFound();
            }

            return View(llProductOrder);
        }

        // POST: LlProductOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llProductOrder = await _context.LlProductOrders.FindAsync(id);
            _context.LlProductOrders.Remove(llProductOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlProductOrderExists(decimal id)
        {
            return _context.LlProductOrders.Any(e => e.ProductOrderId == id);
        }
    }
}
