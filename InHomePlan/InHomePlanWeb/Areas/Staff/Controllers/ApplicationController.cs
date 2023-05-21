using Microsoft.AspNetCore.Mvc;
using InHomePlanWeb.Models;
using System.Threading.Tasks;
using InHomePlanWeb.Data;
using InHomePlanWeb.Repository.IRepository;
using Stripe.Checkout;
using InHomePlanWeb.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using static InHomePlanWeb.Utility.Enums;
using Microsoft.AspNetCore.Authorization;
using InHomePlanWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InHomePlanWeb.Areas.Staff.Controllers 
{
    [Area("Staff")]
    [Authorize(Roles = SD.Role_Staff)]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public Models.Application applicationModel { get; set; }
        public ApplicationController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender, DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            _db = db;
            _dbContextOptions = dbContextOptions;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        public IActionResult Application()
        {
            return View();
        }

        // GET: All Applications
        public IActionResult ApplicationDisplay()
        {
            List<Application> objApplicationList = GetAllApplications();

            List<ApplicationStatusCount> statusCounts = GetApplicationStatusCounts();


            var viewModel = new ApplicationVM
            {
                ApplicationList = objApplicationList,
                StatusCounts = statusCounts
            };

            return View(viewModel);

        }

        // GET: A single application
        public IActionResult ApplicationDetails(int applicationID)
        {
            // Check if a applicationId is passed
            if (applicationID == 0)
            {
                return NotFound();
            }

            // Get the related application
            Application applicationFromDb = GetViewApplication(applicationID);

            // Check if the application exists
            if (applicationFromDb == null)
            {
                return NotFound();
            }

            // Get the latest status of the application
            applicationFromDb.LastStatus = GetLastStatus(applicationFromDb);

            // Load files
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string filePath1 = Path.Combine(wwwRootPath, applicationFromDb.HomePlanFileUrl);
            string filePath2 = Path.Combine(wwwRootPath, applicationFromDb.LandPlanFileUrl);

            applicationFromDb.HomePlanFileUrl = filePath1;  
            applicationFromDb.LandPlanFileUrl = filePath2;

            return View(applicationFromDb);
        }


        //POST: Update application status
        [HttpPost]
        public async Task<IActionResult> ApplicationDetailsAsync(Application obj, string PlanStatusComment, string InspectionStatusComment, string FinalStatusComment)
        {
            // Variable to hold the application status
            string statusText = ""; 

            if (ModelState.IsValid)
            {
                // Get the current application
                Application? applicationCurrent = GetViewApplication(obj.ApplicationID);

                // Get the latest status of the application
                applicationCurrent.LastStatus = GetLastStatus(applicationCurrent);

                if (applicationCurrent != null)
                {
                    if((applicationCurrent.LastStatus.PlanStatusComment != PlanStatusComment
                        || applicationCurrent.LastStatus.InspectionStatusComment != InspectionStatusComment
                        || applicationCurrent.LastStatus.FinalStatusComment != FinalStatusComment
                        || applicationCurrent.IsPlanApproved != obj.IsPlanApproved
                        || applicationCurrent.IsInspectionCompleted != obj.IsInspectionCompleted
                        || applicationCurrent.IsFinalApproved != obj.IsFinalApproved ) == false )
                    {
                        //no changes
                        TempData["Message"] = "No changes were made";
                        TempData["MessageType"] = "warning";

                        return RedirectToAction("ApplicationDetails", new { applicationID = obj.ApplicationID });

                    }

                    if (applicationCurrent.IsPlanApproved != obj.IsPlanApproved)
                    {
                        applicationCurrent.IsPlanApproved = obj.IsPlanApproved;
                        statusText = (bool)(applicationCurrent.IsPlanApproved == BoolOptions.Yes) ? SD.StatusPlanApprove : SD.StatusPlanReject;

                        // Send status update email
                        await SendStatusUpdateEmail(applicationCurrent, statusText, PlanStatusComment);
                    }

                    if (applicationCurrent.IsInspectionCompleted != obj.IsInspectionCompleted)
                    {
                        applicationCurrent.IsInspectionCompleted = obj.IsInspectionCompleted;
                        statusText = (bool)(applicationCurrent.IsInspectionCompleted == BoolOptions.Yes) ? SD.StatusInspectionApprove : SD.StatusInspectionReject;

                        // Send status update email
                        await SendStatusUpdateEmail(applicationCurrent, statusText, InspectionStatusComment);
                    }

                    if (applicationCurrent.IsFinalApproved != obj.IsFinalApproved)
                    {
                        applicationCurrent.IsFinalApproved = obj.IsFinalApproved;
                        statusText = (bool)(applicationCurrent.IsFinalApproved == BoolOptions.Yes) ? SD.StatusFinalApprove : SD.StatusFinalReject;

                        // Send status update email
                        await SendStatusUpdateEmail(applicationCurrent, statusText, FinalStatusComment);

                    }

                    // Add ApplicationStatus record
                    AddApplicationStatus(applicationCurrent, PlanStatusComment, InspectionStatusComment, FinalStatusComment);

                    // Update application status
                    applicationCurrent.ApplicationStatus = statusText;

                    using (var dbContext = new ApplicationDbContext(_dbContextOptions))
                    {
                        dbContext.Application.Update(applicationCurrent);
                        dbContext.SaveChanges();
                    }

                }

                // If changes made
                TempData["Message"] = "Application updated successfully";
                TempData["MessageType"] = "success";

                return RedirectToAction("ApplicationDetails", new { applicationID = obj.ApplicationID });
            }

            // If the model state is not valid, return the view with validation errors
            return RedirectToAction("ApplicationDetails", new { applicationID = obj.ApplicationID });
        }

        // Method to get all applications
        private List<Application> GetAllApplications()
        {
            return _db.Application
                .Include(a => a.Payment)
                .ToList();
        }

        // Method to get selected application
        private Application GetViewApplication(int applicationId)
        {
            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                return dbContext.Application
                    .Include(a => a.Payment)
                    .Include(a => a.ApplicationStatusDetails)
                    .FirstOrDefault(a => a.ApplicationID == applicationId);
            }
        }

        // Method to get the Latest application status
        public ApplicationStatus GetLastStatus(Application application)
        {
            return application.ApplicationStatusDetails
              .OrderByDescending(s => s.StatusChangedOn)
              .FirstOrDefault();
        }


        // Method to calculate status count
        private List<ApplicationStatusCount> GetApplicationStatusCounts()
        {
            var statusCounts = _db.Application
                .GroupBy(a => a.ApplicationStatus)
                .Select(g => new ApplicationStatusCount { StatusType = g.Key, Count = g.Count() })
                .ToList();

            return statusCounts;
        }


        // Method to get the current user's ID
        private string GetCurrentUserId()
        {
            string currentUserName = User.Identity.Name;

            string currentUserId = _db.Users
                .Where(u => u.UserName == currentUserName)
                .Select(u => u.Id)
                .FirstOrDefault();

            return currentUserId;
        }

        // Method to add a new ApplicationStatus record
        private void AddApplicationStatus(Application application, string commentPlan, string commentInspection, string commentFinal)
        {
            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                ApplicationStatus applicationStatus = new ApplicationStatus
                {
                    ApplicationID = application.ApplicationID,
                    IsPlanApproved = application.IsPlanApproved,
                    IsInspectionCompleted = application.IsInspectionCompleted,
                    IsFinalApproved = application.IsFinalApproved,
                    PlanStatusComment = commentPlan,
                    InspectionStatusComment = commentInspection,
                    FinalStatusComment = commentFinal,
                    StatusChangedBy = GetCurrentUserId(), // Set the appropriate value
                    StatusChangedOn = DateTime.Now
                };

                dbContext.ApplicationStatus.Add(applicationStatus);
                dbContext.SaveChanges();
            }
        }

        // Method to send status update email
        private async Task SendStatusUpdateEmail(Application application, string statusText, string comment)
        {
            string recipientEmail = application.Email;
            string subject = "Home Application - Status Update";

            EmailTemplateProvider templateProvider = new EmailTemplateProvider();
            string emailContent = templateProvider.GetApplicationStatusUpdateEmailTemplate(application, statusText, comment);

            await _emailSender.SendEmailAsync(recipientEmail, subject, emailContent);
        }

        // Method to download a file
        public IActionResult DownloadFile(string filePath)
        {
            // Ensure that the file path is valid and exists
            if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
            {
                // Get the file name from the file path
                string fileName = Path.GetFileName(filePath);

                // Return the file for download
                return PhysicalFile(filePath, "application/octet-stream", fileName);
            }

            // If the file path is invalid or the file doesn't exist, return an error or handle it accordingly
            return NotFound();
        }

    }
}
