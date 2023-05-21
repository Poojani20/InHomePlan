using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static InHomePlanWeb.Utility.Enums;

namespace InHomePlanWeb.Models
{
    public class ApplicationStatus
    {
        // PK for the table
        [Key]
        public int ApplicationStatusId { get; set; }

        // FK for ApplicationStatus
        // 1 to m relationship - 1 Application can have many application status records

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }
        public Application Application { get; set; }

        // other fields

        //[Display(Name = "Approval of Plan")]
        //public BoolOptions HasPlanStatusChanged { get; set; }

        //[Display(Name = "Completion of Inspection")]
        //public BoolOptions HasInspectionStatusChanged { get; set; }

        //[Display(Name = "Grant of Final Approval")]
        //public BoolOptions HasFinalStatusChanged { get; set; }


        //Add properties of the enum type to represent the bool value

        [Display(Name = "Approval of Plan")]
        public BoolOptions IsPlanApproved { get; set; }

        [Display(Name = "Completion of Inspection")]
        public BoolOptions IsInspectionCompleted { get; set; }

        [Display(Name = "Grant of Final Approval")]
        public BoolOptions IsFinalApproved { get; set; }

        public string? PlanStatusComment { get; set; }
        public string? InspectionStatusComment { get; set; }
        public string? FinalStatusComment { get; set; }

        // auditing purposes
        public string? StatusChangedBy { get; set; }
        public DateTime StatusChangedOn { get; set; }

    }
}
