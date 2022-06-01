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

namespace Lama_Store.Controllers
{
    public class LlStoresController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;


        public LlStoresController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: LlStores
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.LlStores.Include(l => l.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: LlStores/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llStore = await _context.LlStores
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (llStore == null)
            {
                return NotFound();
            }

            return View(llStore);
        }

        // GET: LlStores/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.LlCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: LlStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreName,ImagePathS,CategoryId,ImageFile")] LlStore llStore)
        {
            if (ModelState.IsValid)
            {
                if (llStore.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = _webHostEnviroment.WebRootPath;
                    //Guid.newguid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + llStore.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                      //3- path
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await llStore.ImageFile.CopyToAsync(fileStream);
                    }


                    llStore.ImagePathS = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                }
                _context.Add(llStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.LlCategories, "CategoryId", "CategoryName", llStore.CategoryId);
            return View(llStore);
        }

        // GET: LlStores/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llStore = await _context.LlStores.FindAsync(id);
            if (llStore == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.LlCategories, "CategoryId", "CategoryId", llStore.CategoryId);
            return View(llStore);
        }

        // POST: LlStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("StoreId,StoreName,ImagePathS,CategoryId,ImageFile")] LlStore llStore)
        {
            if (id != llStore.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (llStore.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = _webHostEnviroment.WebRootPath;
                        //Guid.newguid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + llStore.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                          //3- path
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await llStore.ImageFile.CopyToAsync(fileStream);
                        }


                        llStore.ImagePathS = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                    }
                    _context.Update(llStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlStoreExists(llStore.StoreId))
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
            ViewData["CategoryId"] = new SelectList(_context.LlCategories, "CategoryId", "CategoryId", llStore.CategoryId);
            return View(llStore);
        }

        // GET: LlStores/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llStore = await _context.LlStores
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (llStore == null)
            {
                return NotFound();
            }

            return View(llStore);
        }

        // POST: LlStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llStore = await _context.LlStores.FindAsync(id);
            _context.LlStores.Remove(llStore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlStoreExists(decimal id)
        {
            return _context.LlStores.Any(e => e.StoreId == id);
        }
    }
}
