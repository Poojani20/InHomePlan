using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using Session = Stripe.Checkout.Session;
using static InHomePlanWeb.Utility.Enums;

namespace InHomePlanWeb.Areas.HomeOwner.Controllers
{
    [Area("HomeOwner")]
    [Authorize(Roles = SD.Role_Houseowner)]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;// for file path
        private readonly IEmailSender _emailSender;

        [BindProperty] // bind data between form elements and the model properties
        public Models.Application ApplicationModel { get; set; }

        public ApplicationController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        public IActionResult Application()
        {
            return View();
        }

        // POST: Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Application(Models.Application obj, IFormFile fileHomePlan, IFormFile fileLandPlan)
        {
            // Generate a unique payment ID and set the application fee
            Guid paymentId = Guid.NewGuid();
            int applicationFee = 150000;

            if (ModelState.IsValid)
            {
                // Save uploaded files if they exist
                if (fileHomePlan != null && fileLandPlan != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath; 

                    string homePlanFilePath = SaveUploadedFile(fileHomePlan, @"Uploads\Plans\Home", wwwRootPath);
                    string landPlanFilePath = SaveUploadedFile(fileLandPlan, @"Uploads\Plans\Land", wwwRootPath);

                    obj.HomePlanFileUrl = homePlanFilePath;
                    obj.LandPlanFileUrl = landPlanFilePath;

                }

                // Create a Stripe session for payment
                var session = CreateStripeSession(paymentId, applicationFee);

                // Create a payment object
                var payment = CreatePayment(paymentId, session.Id);

                // Set the current user ID for the application
                obj.ApplicationUserID = GetCurrentUserId();

                // Associate the payment with the application
                obj.Payment = payment;


                // Add the application to the database
                _db.Application.Add(obj);
                _db.SaveChanges();


                // Redirect to the Stripe checkout page
                return RedirectStripeCheckout(session.Url);

            }

            // If the model state is not valid, return the view with validation errors
            return RedirectToAction("Application");
        }


        public async Task<IActionResult> PaymentConfirmationAsync(Guid? id)
        {
            if (id != null)
            {
                // Retrieve the payment from the database based on the provided ID
                Models.Payment? PaymentFromDb = _db.Payment.FirstOrDefault(u => u.PaymentId == id);

                var service = new SessionService();
                Stripe.Checkout.Session session = service.Get(PaymentFromDb.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    // Update the payment details
                    var payment = UpdatePayment(PaymentFromDb, session);

                    // Update the application status
                    var application = UpdateApplicationStatus(PaymentFromDb.ApplicationID);

                    // Create new ApplicationStatus object 
                    var applicationStatus = CreateApplicationStatus(PaymentFromDb.ApplicationID);

                    // Send confirmation email
                    await SendConfirmationEmail(application, PaymentFromDb);

                    // Update the database
                    _db.Payment.Update(PaymentFromDb);
                    _db.Application.Update(application);
                    _db.ApplicationStatus.Add(applicationStatus);
                    _db.SaveChanges();

                }
            }

            // Retrieve applications for the current user
            List<Application> objApplicationList = GetApplicationsForCurrentUser();


            return View(objApplicationList);

        }


        // GET: Get single application
        public IActionResult ApplicationDetails(int? applicationID)
        {
            if (applicationID == null)
            {
                return NotFound();
            }

            Models.Application? applicationFromDb = _db.Application.Find(applicationID);

            if (applicationFromDb == null)
            {
                return NotFound();
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string filePath1 = Path.Combine(wwwRootPath, applicationFromDb.HomePlanFileUrl);
            string filePath2 = Path.Combine(wwwRootPath, applicationFromDb.LandPlanFileUrl);

            applicationFromDb.HomePlanFileUrl = filePath1;
            applicationFromDb.LandPlanFileUrl = filePath2;

            return View(applicationFromDb);
        }


        // Method to save an uploaded file
        private string SaveUploadedFile(IFormFile file, string folderPath, string wwwRootPath)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(wwwRootPath, folderPath);

            using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return Path.Combine(folderPath, fileName);
        }

        // Method to create a Stripe session for payment
        private Stripe.Checkout.Session CreateStripeSession(Guid paymentId, int applicationFee)
        {
            var domain = "https://localhost:7169/";

            var options = new SessionCreateOptions
            {
                // Set Stripe session options

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
                            UnitAmount = applicationFee, // Set the amount in the smallest currency unit (e.g., cents)
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Product Name" // Set the name of the product or service
                            }
                        },
                        Quantity = 1 // Set the quantity of the product or service
                    }
                },
                Mode = "payment",

                SuccessUrl = domain + $"homeowner/application/PaymentConfirmation?id={paymentId}", // Set the URL to redirect to after successful payment
                CancelUrl = domain + $"homeowner/application/PaymentConfirmation", // Set the URL to redirect to if the payment is canceled

            };

            var service = new SessionService();
            var session = service.Create(options);

            return session;
        }

        // Method to redirect to the Stripe checkout page
        private IActionResult RedirectStripeCheckout(string url)
        {
            Response.Headers.Add("Location", url);
            return new StatusCodeResult(303);
        }


        // Method to create a payment object
        private Payment CreatePayment(Guid paymentId, string sessionId)
        {
            var payment = new Payment
            {
                PaymentId = paymentId,
                SessionId = sessionId,
                Amount = 150000/100
            };

            return payment;
        }

        // Method to update a payment object
        private Payment UpdatePayment(Payment payment, Session session)
        {
            payment.PaymentIntentId = session.PaymentIntentId;
            payment.PaymentStatus = SD.PaymentStatusApproved;
            payment.PaymentDate = DateTime.Now;

            return payment;
        }

        // Method to update the application status
        private Application UpdateApplicationStatus(int applicationId)
        {
            Application? applicationFromDb = _db.Application.FirstOrDefault(u => u.ApplicationID == applicationId);

            applicationFromDb.ApplicationStatus = SD.StatusPending;

            return applicationFromDb;
        }


        // Method to create a ApplicationStatus object
        private ApplicationStatus CreateApplicationStatus(int applicationId)
        {
            // Create a new ApplicationStatus record with default values
            ApplicationStatus applicationStatus = new ApplicationStatus
            {
                ApplicationID = applicationId,
                IsPlanApproved = BoolOptions.No,
                IsInspectionCompleted = BoolOptions.No,
                IsFinalApproved = BoolOptions.No,
                PlanStatusComment = "No Comments",
                InspectionStatusComment = "No Comments",
                FinalStatusComment = "No Comments",
                StatusChangedBy = GetCurrentUserId(), // Set the appropriate value
                StatusChangedOn = DateTime.Now
            };

            return applicationStatus;
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

        // Method to get all applications for current user
        private List<Application> GetApplicationsForCurrentUser()
        {
            return _db.Application
                .Include(a => a.Payment)
                .Where(a => a.ApplicationUserID == GetCurrentUserId())
                .ToList();
        }

        // Method to send confirmation email
        private async Task SendConfirmationEmail(Application application, Payment payment)
        {
            string recipientEmail = application.Email;
            string subject = "Home Plan Approval";
            EmailTemplateProvider templateProvider = new EmailTemplateProvider();
            string emailContent = templateProvider.GetApplicationSubmitEmailTemplate(application.FirstName, application.PlanNo, payment.PaymentDate);

            await _emailSender.SendEmailAsync(recipientEmail, subject, emailContent);
        }

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
