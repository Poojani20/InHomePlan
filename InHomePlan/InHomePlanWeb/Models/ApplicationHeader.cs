using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class ApplicationHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime SumbitDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public double Total { get; set; }

        public string? ApplicationStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? PaymentIntendId { get; set; }



        // public string? Carrier { get; set; }
    }
}
