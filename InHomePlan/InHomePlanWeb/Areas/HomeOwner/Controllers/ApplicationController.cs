using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;


namespace InHomePlanWeb.Areas.HomeOwner.Controllers
{
    [Area("HomeOwner")]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicationController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;  
        }

        // GET: Application
        public IActionResult ApplicationDisplay()
        {
            List<Models.Application> objApplicationList = _db.Application.ToList();
            return View(objApplicationList);
        }

        public IActionResult Application()
        {
            return View();
        }

        // POST: Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Application(Models.Application obj, IFormFile? fileHomePlan, IFormFile? fileLandPlan)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(fileHomePlan != null)
                {
                    string fileName1 = Guid.NewGuid().ToString() + Path.GetExtension(fileHomePlan.FileName);
                    string filePath1 = Path.Combine(wwwRootPath, @"Uploads\Plans\Home");

                    string fileName2 = Guid.NewGuid().ToString() + Path.GetExtension(fileLandPlan.FileName);
                    string filePath2 = Path.Combine(wwwRootPath, @"Uploads\Plans\Land");

                    using (var fileStream1 = new FileStream(Path.Combine(filePath1, fileName1), FileMode.Create))
                    {
                        fileHomePlan.CopyTo(fileStream1);
                    }

                    using (var fileStream2 = new FileStream(Path.Combine(filePath2, fileName2), FileMode.Create))
                    {
                        fileLandPlan.CopyTo(fileStream2);
                    }

                    obj.HomePlanFileUrl = @"Uploads\Plans\Home\" + fileName1;
                    obj.LandPlanFileUrl = @"Uploads\Plans\Land\" + fileName2;

                }

                _db.Application.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("ApplicationDisplay");
            }

            // If the model state is not valid, return the view with validation errors
            return RedirectToAction("Application");
        }

        // POST: Stripe payment
        [HttpPost]
        public IActionResult Payment()
        {
            StripeConfiguration.ApiKey = "sk_test_51N3aLlSJhkQUz078VdybXfM0GTMYEAcaHdflkPHcMYhEQykWZD3gZ5wQxEvjsNFCzIqlXbMwUme8lEKUxXRCIMNP00z8pgJkML";

            var domain = "https://localhost:7169/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card" // Allow card payments
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "lkr", // Set the currency code
                            UnitAmount = 750000, // Set the amount in the smallest currency unit (e.g., cents)
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Product Name" // Set the name of the product or service
                            }
                        },
                        Quantity = 1 // Set the quantity of the product or service
                    }
                },
                Mode = "payment",

                //SuccessUrl = "https://example.com/success", // Set the URL to redirect to after successful payment
                //CancelUrl = "https://example.com/cancel" // Set the URL to redirect to if the payment is canceled

                SuccessUrl = domain + $"homeowner/application/applicationdisplay", // Set the URL to redirect to after successful payment
                CancelUrl = domain + $"homeowner/application/applicationdisplay", // Set the URL to redirect to if the payment is canceled
            };

            var service = new SessionService();
            var session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

            //return View(session); // Pass the session object to the view
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
