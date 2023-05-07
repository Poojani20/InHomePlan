using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Application(Application obj, IFormFile? fileHomePlan, IFormFile? fileLandPlan)
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
