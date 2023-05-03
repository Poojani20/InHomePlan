using Microsoft.AspNetCore.Mvc;
using InHomePlanWeb.Models;
using System.Threading.Tasks;
using InHomePlanWeb.Data;
using InHomePlanWeb.Repository.IRepository;
using Stripe.Checkout;
using Stripe;

namespace InHomePlanWeb.Controllers
{
    public class ApplicationController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ApplicationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public ApplicationController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public IActionResult Application1()
        {
            return View();
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

        public IActionResult ApplicationConfirmation(int id) 
        {
            return View(id);
        }

    }
}
