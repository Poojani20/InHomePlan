using InHomePlanWeb.Models;

namespace InHomePlanWeb.Utility
{
    public class EmailTemplateProvider
    {
        public string GetHomePlanApprovalEmailTemplate(string firstName, string planId, DateTime submittedDate)
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

        public string GetApplicationStatusUpdateEmailTemplate(Application application, string applicationStatus)
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
                <li><strong>Submitted Date:</strong> {application.PaymentDate:yyyy-MM-dd}</li>
                <li><strong>Application Status:</strong> {applicationStatus}</li>
            </ul>
            <p>If you have any questions or need further assistance, please feel free to contact us.</p>
            <p>Thank you!</p>
        </body>
        </html>";

            return emailContent;
        }


    }
}
