using Microsoft.AspNetCore.Mvc;
using InHomePlanWeb.Models;
using System.Threading.Tasks;
using InHomePlanWeb.Data;
using InHomePlanWeb.Repository.IRepository;
using Stripe.Checkout;
using InHomePlanWeb.Utility;

namespace InHomePlanWeb.Areas.Staff.Controllers 
{
    [Area("Staff")]
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
        
        // GET: Get single application
        public IActionResult ApplicationDetails(int? applicationID)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (applicationID == 0)
            {
                return NotFound();
            }

            // Find - only works with a Primary Key
            Application? applicationFromDb = _db.Application.Find(applicationID);

            // FirstOrDefault - works with any field
            Application? applicationFromDb2 = _db.Application.FirstOrDefault(u => u.ApplicationID == applicationID);

            // Where - use if we have more filtering to do
            Application? applicationFromDb3 = _db.Application.Where(u => u.ApplicationID == applicationID).FirstOrDefault();

            if (applicationFromDb == null)
            {
                return NotFound();
            }

            string filePath1 = Path.Combine(wwwRootPath, applicationFromDb.HomePlanFileUrl);
            string filePath2 = Path.Combine(wwwRootPath, applicationFromDb.LandPlanFileUrl);

            applicationFromDb.HomePlanFileUrl = filePath1;  
            applicationFromDb.LandPlanFileUrl = filePath2;

            // using option1:Find here
            return View(applicationFromDb);
        }

        // POST: Application/Update
        [HttpPost]
        public IActionResult ApplicationDetails(Application obj)
        {
            if (ModelState.IsValid)
            {
                _db.Application.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("ApplicationDetails", new { applicationID = obj.ApplicationID });
            }

            // If the model state is not valid, return the view with validation errors
            return RedirectToAction("ApplicationDetails", obj);
        }

        public IActionResult DownloadFile(string filePath)
        {
            // Ensure that the file path is valid and exists
            if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
            {
                // Get the file name from the file path
                string fileName = Path.GetFileName(filePath);

                // Return the file for download
                return File(filePath, "application/octet-stream", fileName);
            }

            // If the file path is invalid or the file doesn't exist, return an error or handle it accordingly
            return NotFound();
        }

    }
}
