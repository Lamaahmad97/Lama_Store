using Lama_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lama_Store.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        public AdminController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.usernumber = _context.LlUsers.Count();
            ViewBag.sales = _context.LlProducts.Sum(x => x.ProductCost);
            ViewBag.Prouducts = _context.LlProducts.Count();

            var order = _context.LlOrders.ToList();
            var ordpro = _context.LlProductOrders.ToList();
            var products = _context.LlProducts.ToList();
            var users = _context.LlUsers.ToList();
            var result = from o in order
                         join op in ordpro on o.OrderId equals op.OrderId
                         join p in products on op.ProductId equals p.ProductId
                         join u in users on o.UserId equals u.UserId
                         select new JoinTables { Product = p, order = o, users = u, ordpro = op };
            var model = Tuple.Create<IEnumerable<LlOrder>, IEnumerable<LlProduct>, IEnumerable<LlUser>, IEnumerable<JoinTables>>(order, products, users, result);
            return View(model);



        }

        public IActionResult ViewandUpdate()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminPassword = HttpContext.Session.GetString("AdminPassword");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");

            var cus = _context.LlUsers.Where(x => x.UserId == 27).SingleOrDefault();

            ViewBag.FName = cus.UserFname; //ضربت ايرور هون 

            ViewBag.LName = cus.UserLname;



            return View();
        }

        [HttpPost]
        public IActionResult ViewandUpdate(string FirstName,string LastName)
        {

           // _context.LlUsers.Update(lUser);
            _context.SaveChanges();

            return RedirectToAction("index", "Admin");
        }
        

        public IActionResult JoinTable()
        {
            var order = _context.LlOrders.ToList();
            var ordpro = _context.LlProductOrders.ToList();
            var products = _context.LlProducts.ToList();
            var users = _context.LlUsers.ToList();

            var result = from o in order
                         join op in ordpro on o.OrderId equals op.OrderId
                         join p in products on op.ProductId equals p.ProductId
                         join u in users on o.UserId equals u.UserId
                         select new JoinTables { Product = p, order = o, users = u, ordpro = op };


            return View(result);
        }
        public IActionResult Search()
        {
            var modelcontext = _context.LlProductOrders.Include(x => x.Product).Include(x => x.Order).ToList();
            return View(modelcontext);

        }
        [HttpPost]
        public IActionResult Search(DateTime? startDate, DateTime? endDate)
        {
            var modelcontext = _context.LlProductOrders.Include(x => x.Product).Include(x => x.Order).ToList();

            if (startDate == null && endDate == null)
            {
                return View(modelcontext);
            }
            else if (startDate != null && endDate == null)
            {
                var result = modelcontext.Where(x => x.DateFrom.Value.Date == startDate);
                return View(result);
            }
            else if (startDate == null && endDate != null)
            {
                var result = modelcontext.Where(x => x.DateFrom.Value.Date == endDate);
                return View(result);
            }
            else
            {
                var result = modelcontext.Where(x => x.DateFrom.Value.Date >= startDate && x.DateFrom.Value.Date <= endDate);
                return View(result);
            }
        }


        public IActionResult Report()
        {
            var order = _context.LlOrders.ToList();

            var ordpro = _context.LlProductOrders.ToList();
            var products = _context.LlProducts.ToList();
            var users = _context.LlUsers.ToList();

            var result = from o in order
                         join op in ordpro on o.OrderId equals op.OrderId
                         join p in products on op.ProductId equals p.ProductId
                         join u in users on o.UserId equals u.UserId
                         select new JoinTables { Product = p, order = o, users = u, ordpro = op };
            ViewBag.TotalQuantity = _context.LlProductOrders.Sum(x => x.Quantity);
            ViewBag.TotalPrice = _context.LlProductOrders.Sum(x => x.Quantity * x.Product.ProductPrice);
            var modelContext = _context.LlProductOrders.Include(p => p.Order).Include(p => p.Product).ToList();
            var model = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<LlProductOrder>>(result, modelContext);
            return View(model);
        }
        [HttpPost]
        public IActionResult Report(DateTime? startDate, DateTime? endDate)
        {
            var order = _context.LlOrders.ToList();

            var ordpro = _context.LlProductOrders.ToList();
            var products = _context.LlProducts.ToList();
            var users = _context.LlUsers.ToList();

            var join = from o in order
                       join op in ordpro on o.OrderId equals op.OrderId
                       join p in products on op.ProductId equals p.ProductId
                       join u in users on o.UserId equals u.UserId
                       select new JoinTables { Product = p, order = o, users = u, ordpro = op };
            var modelContext = _context.LlProductOrders.Include(p => p.Order).Include(p => p.Product).ToList();

            if (startDate == null && endDate == null)
            {
                var model = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<LlProductOrder>>(join, modelContext);
                ViewBag.TotalQuantity = _context.LlProductOrders.Sum(x => x.Quantity);
                ViewBag.TotalPrice = _context.LlProductOrders.Sum(x => x.Quantity * x.Product.ProductPrice);
                return View(model);
            }
            else if (startDate != null && endDate == null)
            {

                var result = modelContext.Where(x => x.DateFrom.Value.Date == startDate);
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.TotalPrice = result.Sum(x => x.Quantity * x.Product.ProductPrice);
                var model = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<LlProductOrder>>(join, result);

                return View(model);
            }
            else if (startDate == null && endDate != null)
            {
                var result = modelContext.Where(x => x.DateFrom.Value.Date == endDate);
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.TotalPrice = result.Sum(x => x.Quantity * x.Product.ProductPrice);
                var model = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<LlProductOrder>>(join, modelContext);

                return View(model);
            }
            else
            {
                var result = modelContext.Where(x => x.DateFrom.Value.Date >= startDate && x.DateFrom.Value.Date <= endDate);
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.TotalPrice = result.Sum(x => x.Quantity * x.Product.ProductPrice);
                var model = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<LlProductOrder>>(join, result);

                return View(model);
            }
        }
    }
}
