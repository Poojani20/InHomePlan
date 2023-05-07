using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace InHomePlanWeb.Areas.HomeOwner.Controllers
{
    [Area("HomeOwner")]
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
        public IActionResult Application(Application obj, IFormFile fileHomePlan, IFormFile fileLandPlan)
        {
            if (fileHomePlan != null && fileHomePlan.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileHomePlan.CopyTo(memoryStream);
                    byte[] fileData1 = memoryStream.ToArray();

                    fileLandPlan.CopyTo(memoryStream);
                    byte[] fileData2 = memoryStream.ToArray();

                    // Save the file data to the database or perform any other operations
                    // using the file data

                    // Assign the file data to the appropriate property in the model
                    obj.FileHomePlan = fileData1;
                    obj.FileLandPlan = fileData2;
                    //obj.FileHomePlanName = file.FileName;
                }

                _db.Application.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("ApplicationDisplay");
            }

            //if (ModelState.IsValid)
            //{
            //    _db.Application.Add(obj);
            //    _db.SaveChanges();
            //    return RedirectToAction("ApplicationDisplay");
            //}

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
