﻿using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Repository.IRepository;
using InHomePlanWeb.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public Models.Application applicationModel { get; set; }

        public ApplicationController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        //// GET: Application
        //[HttpGet]
        //public IActionResult PaymentConfirmation()
        //{
        //    List<Models.Application> objApplicationList = _db.Application.ToList();
        //    return View(objApplicationList);
        //}


        //Update payments details

        

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

                // Stripe payment setup

                Guid paymentId = Guid.NewGuid();

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
                            UnitAmount = 50000, // Set the amount in the smallest currency unit (e.g., cents)
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
                Stripe.Checkout.Session session = service.Create(options); // currently using Stripe session

                obj.PaymentId = paymentId;
                obj.SessionId = session.Id;

                _db.Application.Add(obj);
                _db.SaveChanges();

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
                
                //return RedirectToAction("ApplicationDisplay");
            }

            // If the model state is not valid, return the view with validation errors
            return RedirectToAction("Application");
        }

        public async Task<IActionResult> PaymentConfirmationAsync(Guid? id)
        {
            if (id != null)
            {
                Models.Application? applicationFromDb = _db.Application.FirstOrDefault(u => u.PaymentId == id);

                var service = new SessionService();
                Stripe.Checkout.Session session = service.Get(applicationFromDb.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    applicationFromDb.PaymentIntentId = session.PaymentIntentId;
                    applicationFromDb.ApplicationStatus = SD.StatusPending;
                    applicationFromDb.PaymentStatus = SD.PaymentStatusApproved;
                    applicationFromDb.PaymentDate = DateTime.Now;

                    //sending confirmation email

                    string recipientEmail = applicationFromDb.Email;
                    string subject = "Home Plan Approval";
                    EmailTemplateProvider templateProvider = new EmailTemplateProvider();
                    string emailContent = templateProvider.GetHomePlanApprovalEmailTemplate(applicationFromDb.FirstName, applicationFromDb.PlanNo, applicationFromDb.PaymentDate);

                    await _emailSender.SendEmailAsync(recipientEmail, subject, emailContent);

                    //updating database
                    _db.Application.Update(applicationFromDb);
                    _db.SaveChanges();

                }
            }

            List<Models.Application> objApplicationList = _db.Application.ToList();
            return View(objApplicationList);
        }

    }
}
