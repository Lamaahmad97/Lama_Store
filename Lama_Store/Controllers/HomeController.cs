using Lama_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lama_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;


        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }




        public IActionResult Index()
        {
            int? user = HttpContext.Session.GetInt32("UserId");
            if (user == null) {
                int? CartCount = _context.LlOrders.Where(x => x.UserId == user).Count();
                if (CartCount == 0 || CartCount == null)
                    ViewBag.CartCount = 0;
                else {
                    ViewBag.CartCount = CartCount;
                }
            }

            var categories = _context.LlCategories.ToList();
            return View(categories);
        }

        public IActionResult Indexstore(int id)
        {
            var stores = _context.LlStores.Where(x => x.CategoryId == id).ToList();
            return View(stores);
           
        }
        public IActionResult Indexproducts(int id)
        {
            var products = _context.LlProducts.Where(x => x.StoreId == id).ToList();
            return View(products);

        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
