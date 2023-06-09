﻿using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace InHomePlanWeb.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }

        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email
            var client = new SendGridClient(SendGridSecret);
            //var from = new EmailAddress("10749097@students.plymouth.ac.uk", "InHomePlan");
            //var from = new EmailAddress("hasitha.ranasinghearachchige@flinders.edu.au", "InHomePlan");
            var from = new EmailAddress("info@inhomeplan.com", "InHomePlan");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            
            return client.SendEmailAsync(msg);
            //return Task.CompletedTask;
        }
    }
}
