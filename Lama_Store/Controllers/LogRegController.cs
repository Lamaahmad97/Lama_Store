using Lama_Store.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lama_Store.Controllers
{
    public class LogRegController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnviroment;

        public LogRegController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {

            _context = context;
            _webHostEnviroment = webHostEnviroment; //مشان اوصل للباث تاع w3


        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("UserName,LoginPass")] LlLogin lLogin)
        {

            var auth = _context.LlLogins.Where(x => x.UserName == lLogin.UserName && x.LoginPass == lLogin.LoginPass).SingleOrDefault();
            {
                if (auth != null)
                    switch (auth.RoleId)
                    {
                        case 1: // role = customer 
                            HttpContext.Session.SetInt32("UserI d", (int)auth.UserId);
                           var result = _context.LlUsers.Where(x => x.UserId == auth.UserId).SingleOrDefault();
                            HttpContext.Session.SetString("Fname", result.UserFname);
                            HttpContext.Session.SetString("Lname", result.UserLname);
                            HttpContext.Session.SetString("Picture", result.ImagePath);

                            return RedirectToAction("Index", "Home");
                        case 2: // role = Admin
                            HttpContext.Session.SetString("AdminName", auth.UserName);
                            HttpContext.Session.SetString("AdminPassword", auth.LoginPass);

                            HttpContext.Session.SetInt32("AdminId", (int)auth.UserId);


                            return RedirectToAction("Index", "Admin");

                    }
            }
            return View();
        }
        public IActionResult registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> registration([Bind("UserId,UserFname,UserLname,ImagePath,ImageFile")] LlUser lUser, string username, string password)
        {
            if (ModelState.IsValid)
            {
                //1- get w3rootpath
                string w3rootpath = _webHostEnviroment.WebRootPath;//C:\Users\d.kanaan.ext\Desktop\Rest\Rest\wwwroot\
                                                                   //Guid.newguid : generate unique string before image name                                           ////2- generate image name and add unique string
                string fileName = Guid.NewGuid().ToString() + "_" + lUser.ImageFile.FileName;//96522555_img1.jpg //ex : affscdw5635edvcywydfew_Aseel.jpg
                                                                                             //3- path
                string path = Path.Combine(w3rootpath + "/Image/" + fileName);//C:\Users\d.kanaan.ext\Desktop\Rest\Rest\wwwroot\images/96522555_img1 .jpg
                                                                               //4-create Image inside image file in w3root folder
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await lUser.ImageFile.CopyToAsync(fileStream);
                }
                lUser.ImagePath = fileName;
                _context.Add(lUser);
                await _context.SaveChangesAsync();
                var lastId = _context.LlUsers.OrderByDescending(l => l.UserId).FirstOrDefault().UserId;
                //new row of table LLogin 
                LlLogin user1 = new Models.LlLogin();
                user1.RoleId = 1;//customer 
                user1.UserId = lastId;//200
                user1.UserName = username;//AhmadAli@gmail.com
                user1.LoginPass = password;//1234567890
                _context.Add(user1);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "LogReg");
            }
            return View(lUser);
        }


    }
}
