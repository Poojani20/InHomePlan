using InHomePlanWeb.Models;
using System.Text.Encodings.Web;

namespace InHomePlanWeb.Utility
{
    public class EmailTemplateProvider
    {
        //Register account
        public string GetRegistrationEmailTemplate(string firstName, string email, string confirmationLink)
        {
            string emailContent = $@"
                            <!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Registration Confirmation</title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <h2>Registration Confirmation</h2>
                                <p>Dear {firstName},</p>
                                <p>Thank you for registering to InHomePlan. Your account has been successfully created.</p>
                                <p>Email: {email}</p>
                                <p>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.</p>
                                <p>We look forward to serving you. If you have any questions or need assistance, please feel free to contact us.</p>
                                <p>Thank you!</p>
                            </body>
                            </html>";

            return emailContent;
        }

        //Submit application
        public string GetApplicationSubmitEmailTemplate(string firstName, string planId, DateTime submittedDate)
        {
            string emailContent = $@"
                            <!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Home Plan Approval</title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <h2>Application Submission</h2>
                                <p>Dear {firstName},</p>
                                <p>Your home plan has been successfully submitted for approval. We will review it and notify you of the decision shortly.</p>
                                <p>Plan Details:</p>
                                <ul>
                                    <li><strong>Plan ID:</strong> {planId}</li>
                                    <li><strong>Submitted Date:</strong> {submittedDate:yyyy-MM-dd}</li>
                                </ul>
                                <p>If you have any questions or need further assistance, please feel free to contact us.</p>
                                <p>Thank you!</p>
                            </body>
                            </html>";

            return emailContent;
        }

        //Application status update 
        public string GetApplicationStatusUpdateEmailTemplate(Application application, string applicationStatus, string staffComment)
        {
            string emailContent = $@"
                            <!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Application Status Update</title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <h2>Application Status Update</h2>
                                <p>Dear {application.FirstName},</p>
                                <p>Your application status has been updated. Please see the details below:</p>
                                <ul>
                                    <li><strong>Plan ID:</strong> {application.PlanNo}</li>
                                    <li><strong>Submitted Date:</strong> {application.Payment.PaymentDate:yyyy-MM-dd}</li>
                                    <li><strong>Application Status:</strong> {applicationStatus}</li>
                                </ul>
                                <p><strong>Staff Comment:</strong></p>
                                <p>{staffComment}</p>
                                <p>If you have any questions or need further assistance, please feel free to contact us.</p>
                                <p>Thank you!</p>
                            </body>
                            </html>";

            return emailContent;
        }

        public string GetEnquiryEmailTemplate(string name, string email, string phone, string subject, string message)
        {
            string emailContent = $@"
                            <!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>General Inquiry</title>
                            </head>
                            <body style=""font-family: Arial, sans-serif;"">
                                <h2>General Enquiry</h2>
                                <p>Dear {name},</p>
                                <p>Thank you for reaching out to us at InHomePlan.</p>
                                <p>Here are the details you provided:</p>
                                <ul>
                                    <li><strong>Name:</strong> {name}</li>
                                    <li><strong>Email:</strong> {email}</li>
                                    <li><strong>Phone:</strong> {phone}</li>
                                    <li><strong>Subject:</strong> {subject}</li>
                                    <li><strong>Message:</strong> {message}</li>
                                </ul>
                                <p>We will review your inquiry and respond to you as soon as possible.</p>
                                <p>Thank you!</p>
                            </body>
                            </html>";

            return emailContent;
        }

        public string GetEnquiryEmailTemplateInternal(string name, string email, string phone, string subject, string message)
        {
            string emailContent = $@"
                            <h2>New Enquiry</h2>
                            <p>A new enquiry has been received from a customer at InHomePlan.</p>
                            <p>Here are the details provided:</p>
                            <ul>
                                <li><strong>Name:</strong> {name}</li>
                                <li><strong>Email:</strong> {email}</li>
                                <li><strong>Phone:</strong> {phone}</li>
                                <li><strong>Subject:</strong> {subject}</li>
                                <li><strong>Message:</strong> {message}</li>
                            </ul>
                            <p>Please review and take necessary actions.</p>
                            <p>Thank you!</p>";


            return emailContent;
        }
    }
}
