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
    public class LlCategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LlCategoriesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: LlCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.LlCategories.ToListAsync());
        }



        // GET: LlCategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llCategory = await _context.LlCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (llCategory == null)
            {
                return NotFound();
            }

            return View(llCategory);
        }

        // GET: LlCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LlCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,ImagePath,ImageFile")] LlCategory llCategory)
        {
            if (ModelState.IsValid)
            {
                if (llCategory.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = _webHostEnviroment.WebRootPath;
                    //Guid.newguid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + llCategory.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                       //3- path
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await llCategory.ImageFile.CopyToAsync(fileStream);
                    }


                    llCategory.ImagePath = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                }
                _context.Add(llCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(llCategory);
        }

        // GET: LlCategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llCategory = await _context.LlCategories.FindAsync(id);
            if (llCategory == null)
            {
                return NotFound();
            }
            return View(llCategory);
        }

        // POST: LlCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("CategoryId,CategoryName,ImagePath,ImageFile")] LlCategory llCategory)
        {
            if (id != llCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (llCategory.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = _webHostEnviroment.WebRootPath;
                    //Guid.newguid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + llCategory.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                       //3- path
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await llCategory.ImageFile.CopyToAsync(fileStream);
                    }


                    llCategory.ImagePath = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                }
                try
                {
                    _context.Update(llCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlCategoryExists(llCategory.CategoryId))
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
            return View(llCategory);
        }

        // GET: LlCategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llCategory = await _context.LlCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (llCategory == null)
            {
                return NotFound();
            }

            return View(llCategory);
        }

        // POST: LlCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llCategory = await _context.LlCategories.FindAsync(id);
            _context.LlCategories.Remove(llCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlCategoryExists(decimal id)
        {
            return _context.LlCategories.Any(e => e.CategoryId == id);
        }
    }
}
