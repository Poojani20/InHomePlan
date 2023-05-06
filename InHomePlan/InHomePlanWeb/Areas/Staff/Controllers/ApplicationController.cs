using Microsoft.AspNetCore.Mvc;
using InHomePlanWeb.Models;
using System.Threading.Tasks;
using InHomePlanWeb.Data;
using InHomePlanWeb.Repository.IRepository;
using Stripe.Checkout;

namespace InHomePlanWeb.Areas.Staff.Controllers 
{
    [Area("Staff")]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Application
        public IActionResult ApplicationDisplay()
        {
            List<Application> objApplicationList = _db.Application.ToList();
            return View(objApplicationList);
        }

        public IActionResult Application()
        {
            return View();
        }

        // POST: Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Application(Application obj)
        {
            if(ModelState.IsValid)
            {
                _db.Application.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("ApplicationDisplay");
            }

            // If the model state is not valid, return the view with validation errors
            return RedirectToAction("Application");
        }


        //public string ReturnUrl { get; set; }

        //[HttpPost]
        //public async Task<IActionResult> Application1(Application application)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        _context.Add(application);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View(application);
        //}


        //   var DOMAIN = "https://localhost:7169/";
        //        var options = new SessionCreateOptions
        //{
        // SuccessUrl = domain$"/ApplicationConfirmation?id={ApplicationHeader.Id}",
        // CancelUrl = domain + "index",
        //    LineItems = new List<SessionLineItemOptions>

        //  {
        //    new SessionLineItemOptions
        //    {
        //      Price = "price_H5ggYwtDq4fbrJ",
        //      Quantity = 2,
        //    },
        //  },
        //    Mode = "payment",
        //};


        //        var service = new SessionService();
        //        service.Create(options);



    }
}
