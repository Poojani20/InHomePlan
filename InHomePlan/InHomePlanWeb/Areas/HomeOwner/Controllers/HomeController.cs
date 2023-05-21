using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InHomePlanWeb.Controllers
{
    [Area("HomeOwner")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _db = db;
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();  
        }

        public IActionResult Register()
        {
            return View();  
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contactus()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEnquiry(Enquiry enquiry)
        {
            _db.Add(enquiry);
            _db.SaveChanges();

            // Send emails both user and staff
            await SendEnquiryEmail(enquiry.Name, enquiry.Email, enquiry.Phone, enquiry.Subject, enquiry.Message);
            await SendEnquiryEmailInternal(enquiry.Name, enquiry.Email, enquiry.Phone, enquiry.Subject, enquiry.Message);

            TempData["Message"] = "Enquiry sent successfully";
            TempData["MessageType"] = "success";

            return RedirectToAction("Contactus");
        }

        // Email to user
        private async Task SendEnquiryEmail(string name, string email, string phone, string subject, string message)
        {
            string recipientEmail = email;
            string emailSubject = "Enquiry";
            EmailTemplateProvider templateProvider = new EmailTemplateProvider();
            string emailContent = templateProvider.GetEnquiryEmailTemplate(name, email, phone, subject, message);

            await _emailSender.SendEmailAsync(recipientEmail, emailSubject, emailContent);
        }

        // Email to staff
        private async Task SendEnquiryEmailInternal(string name, string email, string phone, string subject, string message)
        {
            string recipientEmail = "info@inhomeplan.com";
            string emailSubject = "Enquiry";
            EmailTemplateProvider templateProvider = new EmailTemplateProvider();
            string emailContent = templateProvider.GetEnquiryEmailTemplateInternal(name, email, phone, subject, message);

            await _emailSender.SendEmailAsync(recipientEmail, emailSubject, emailContent);
        }


    }
}