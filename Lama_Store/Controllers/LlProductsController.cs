using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lama_Store.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Lama_Store.Controllers
{
    public class LlProductsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LlProductsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: LlProducts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlProducts.Include(l => l.Store);
            return View(await modelContext.ToListAsync());
        }

        public  IActionResult SinglePage(int id)
        {
            int? user = HttpContext.Session.GetInt32("UserId");
            if (user == null)
            {
                int? CartCount = _context.LlOrders.Where(x => x.UserId == user).Count();
                if (CartCount == 0 || CartCount == null)
                    ViewBag.CartCount = 0;
                else
                {
                    ViewBag.CartCount = CartCount;
                }
            }
            var modelContext = _context.LlProducts.Where(l => l.ProductId == id).ToList();
            return View(modelContext);
        }

        public async Task<IActionResult> Sales(decimal? id)
        {
            int? user = HttpContext.Session.GetInt32("UserId");
            if (user == null)
            {
                int? CartCount = _context.LlOrders.Where(x => x.UserId == user).Count();
                if (CartCount == 0 || CartCount == null)
                    ViewBag.CartCount = 0;
                else
                {
                    ViewBag.CartCount = CartCount;
                }
            }
            if (id != null) {
                var result = _context.LlProducts.Where(x=>x.StoreId == (int)id).Include(i => i.Store);
                return View(await result.ToListAsync());
            }
            var modelContext = _context.LlProducts.Include(l => l.Store);
            return View(await modelContext.ToListAsync());
        }
        // GET: LlProducts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llProduct = await _context.LlProducts
                .Include(l => l.Store)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (llProduct == null)
            {
                return NotFound();
            }

            return View(llProduct);
        }

        // GET: LlProducts/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.LlStores, "StoreId", "StoreName");
            return View();
        }

        // POST: LlProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ImagePathP,ProductCost,StoreId,ImageFile")] LlProduct llProduct)
        {
            if (ModelState.IsValid)
            {
                if (llProduct.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = _webHostEnviroment.WebRootPath;
                    //Guid.newguid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + llProduct.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                       //3- path
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await llProduct.ImageFile.CopyToAsync(fileStream);
                    }


                    llProduct.ImagePathP = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                }
                _context.Add(llProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.LlStores, "StoreId", "StoreName", llProduct.StoreId);
            return View(llProduct);
        }

        // GET: LlProducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llProduct = await _context.LlProducts.FindAsync(id);
            if (llProduct == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.LlStores, "StoreId", "StoreId", llProduct.StoreId);
            return View(llProduct);
        }

        // POST: LlProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ProductId,ProductName,ProductPrice,ImagePathP,ProductCost,StoreId,ImageFile")] LlProduct llProduct)
        {
            if (id != llProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (llProduct.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = _webHostEnviroment.WebRootPath;
                        //Guid.newguid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + llProduct.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                          //3- path
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await llProduct.ImageFile.CopyToAsync(fileStream);
                        }


                        llProduct.ImagePathP = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                    }
                    _context.Update(llProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlProductExists(llProduct.ProductId))
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
            ViewData["StoreId"] = new SelectList(_context.LlStores, "StoreId", "StoreId", llProduct.StoreId);
            return View(llProduct);
        }

        // GET: LlProducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llProduct = await _context.LlProducts
                .Include(l => l.Store)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (llProduct == null)
            {
                return NotFound();
            }

            return View(llProduct);
        }

        // POST: LlProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llProduct = await _context.LlProducts.FindAsync(id);
            _context.LlProducts.Remove(llProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlProductExists(decimal id)
        {
            return _context.LlProducts.Any(e => e.ProductId == id);
        }
    }
}
