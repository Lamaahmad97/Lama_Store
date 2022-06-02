using Lama_Store.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lama_Store.Controllers
{
    public class LlTestimonialsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LlTestimonialsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment; //مشان اوصل للباث تاع w3

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("TestimonialId,Message,Status,BgImagePath,UserId")] LlTestimonial llTestimonial, IFormFile ImageFile)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                RedirectToAction("Login", "LogReg");
            }
            llTestimonial.Status = "false";
            llTestimonial.UserId = HttpContext.Session.GetInt32("UserId");
            string w3rootpath = _webHostEnviroment.WebRootPath;//C:\Users\d.kanaan.ext\Desktop\Rest\Rest\wwwroot\
                                                               //Guid.newguid : generate unique string before image name                                           ////2- generate image name and add unique string
            string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;//96522555_img1.jpg //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                         //3- path
            string path = Path.Combine(w3rootpath + "/Image/" + fileName);//C:\Users\d.kanaan.ext\Desktop\Rest\Rest\wwwroot\images/96522555_img1 .jpg
                                                                          //4-create Image inside image file in w3root folder
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            llTestimonial.BgImagePath = fileName;
            _context.Add(llTestimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

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
