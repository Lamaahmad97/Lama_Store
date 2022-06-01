using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lama_Store.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;
using EASendMail;

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
        public async Task<IActionResult> AddToCart(int id)
        {
            LlProduct resultItem = _context.LlProducts.Where(x => x.ProductId == id).SingleOrDefault();
            LlOrder order = new LlOrder();
            order.OrderName = resultItem.ProductName;
            order.UserId = HttpContext.Session.GetInt32("UserId");
            _context.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Sales","LlProducts");
        }

        public IActionResult ViewCart() {
            int? currentUser = HttpContext.Session.GetInt32("UserId");

            var result = _context.LlOrders.Where(x => x.UserId == currentUser);
            var result2 = _context.LlProducts.Where(x => x.ProductId > 0);
            var eventsQry = from Or in result
                            join Pr in result2 on Or.OrderName equals Pr.ProductName
                            where Or.OrderName == Pr.ProductName select new NewClass(Or.OrderName, 
                            Pr.ImagePathP,
                            Pr.ProductCost,
                            Pr.ProductPrice,
                            Pr.ProductId
                            ); 
            return View(eventsQry.ToList());    
        }

        public IActionResult Checkout()
        {
            int? currentUser = HttpContext.Session.GetInt32("UserId");

            var result = _context.LlOrders.Where(x => x.UserId == currentUser);
            var result2 = _context.LlProducts.Where(x => x.ProductId > 0);
            var eventsQry = from Or in result
                            join Pr in result2 on Or.OrderName equals Pr.ProductName
                            where Or.OrderName == Pr.ProductName
                            select new NewClass(Or.OrderName,
                            Pr.ImagePathP,
                            Pr.ProductCost,
                            Pr.ProductPrice,
                            Pr.ProductId
            );
            return View(eventsQry.ToList());
        }


        [HttpPost]
        public IActionResult SendEmail(string add1,string city,string zip, string Email)
        {
            int? currentUser = HttpContext.Session.GetInt32("UserId");

            var result = _context.LlOrders.Where(x => x.UserId == currentUser).ToList();
            string items = "";
            for (int i = 0; i < result.Count(); i++) {
                items += " 1x "+ result.ElementAt(i) + "\r\n ";
            }
            string message = "Thank you for your purchase from EStore \r\n" +
                "Your order is as follows:\r\n" + items +
                " Which will be Delived to \r\n add1";

            try
            {
                if (ModelState.IsValid)
                {
                    SmtpMail oMail = new SmtpMail("TryIt");

                    // Your email address
                    oMail.From = "khaledxz150@hotmail.com";

                    // Set recipient email address
                    oMail.To = Email;

                    // Set email subject
                    oMail.Subject = "Purchase from EStore";

                    // Set email body
                    oMail.TextBody = message;

                    // Hotmail/Outlook SMTP server address
                    SmtpServer oServer = new SmtpServer("smtp.office365.com");

                    // If your account is office 365, please change to Office 365 SMTP server
                    // SmtpServer oServer = new SmtpServer("smtp.office365.com");

                    // User authentication should use your
                    // email address as the user name.
                    oServer.User = "khaledxz150@hotmail.com";

                    // If you got authentication error, try to create an app password instead of your user password.
                    // https://support.microsoft.com/en-us/account-billing/using-app-passwords-with-apps-that-don-t-support-two-step-verification-5896ed9b-4263-e681-128a-a6f2979a7944
                    oServer.Password = "-=Shmnga7bsh";

                    // use 587 TLS port
                    oServer.Port = 587;

                    // detect SSL/TLS connection automatically
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


                    EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                    oSmtp.SendMail(oServer, oMail);
                    return RedirectToAction("index","Home");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
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

    public class NewClass
    {
        public string ProductName { get; }
        public string Image { get; }
        public decimal? ProductCost { get; }
        public decimal? ProductPrice { get; }
        public decimal ProductId { get; }   
        public NewClass(string productName, string image, decimal? productCost, decimal? productPrice,
            decimal ProductId)
        {
            ProductName = productName;
            Image = image;
            ProductCost = productCost;
            ProductPrice = productPrice;
            this.ProductId = ProductId;

        }

        public override bool Equals(object obj)
        {
            return obj is NewClass other &&
                   ProductName == other.ProductName &&
                   Image == other.Image &&
                   ProductCost == other.ProductCost &&
                   ProductPrice == other.ProductPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductName, Image, ProductCost, ProductPrice);
        }
    }
}
