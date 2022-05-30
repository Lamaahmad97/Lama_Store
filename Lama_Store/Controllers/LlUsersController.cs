using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lama_Store.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Lama_Store.Controllers
{
    public class LlUsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LlUsersController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: LlUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.LlUsers.ToListAsync());
        }

        // GET: LlUsers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llUser = await _context.LlUsers
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (llUser == null)
            {
                return NotFound();
            }

            return View(llUser);
        }

        // GET: LlUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LlUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserFname,UserLname,ImagePath,ImageFile")] LlUser llUser)
        {
            if (ModelState.IsValid)
            {
                if (llUser.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = _webHostEnviroment.WebRootPath;
                    //Guid.newguid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + llUser.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                    //3- path
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await llUser.ImageFile.CopyToAsync(fileStream);
                    }


                    llUser.ImagePath = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                }
                _context.Add(llUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(llUser);
        }

        // GET: LlUsers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llUser = await _context.LlUsers.FindAsync(id);
            if (llUser == null)
            {
                return NotFound();
            }
            return View(llUser);
        }

        // POST: LlUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,UserFname,UserLname,ImagePath")] LlUser llUser)
        {
            if (id != llUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (llUser.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = _webHostEnviroment.WebRootPath;
                        //Guid.newguid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + llUser.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                                       //3- path
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await llUser.ImageFile.CopyToAsync(fileStream);
                        }


                        llUser.ImagePath= fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
                    }
                    _context.Update(llUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LlUserExists(llUser.UserId))
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
            return View(llUser);
        }

        // GET: LlUsers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var llUser = await _context.LlUsers
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (llUser == null)
            {
                return NotFound();
            }

            return View(llUser);
        }

        // POST: LlUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var llUser = await _context.LlUsers.FindAsync(id);
            _context.LlUsers.Remove(llUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LlUserExists(decimal id)
        {
            return _context.LlUsers.Any(e => e.UserId == id);
        }
    }
}
