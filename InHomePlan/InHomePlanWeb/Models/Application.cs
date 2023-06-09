﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static InHomePlanWeb.Utility.Enums;

namespace InHomePlanWeb.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        public String FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; }
        
        [Required]
        public String NIC { get; set; }
        
        [Required]
        //[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        public String Phone { get; set; }
       
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Assessment number is required")]
        public String AssessmentNo { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        public String PostalCode { get; set;}

        [Required]
        public String StreetName { get; set; }
        public int No_Of_Rooms { get; set; } 
        public int No_of_perches { get; set; }
        public String BuildingArea { get; set; }
        public String PlanNo { get; set; }

        [DefaultValue("card")]
        public String? Payment_Method { get; set; }
        public DateTime? Payment_Date { get; set; }

        //Add properties of the enum type to represent the bool value

        [Display(Name = "Approval of Plan")]
        public BoolOptions IsPlanApproved { get; set; }

        [Display(Name = "Completion of Inspection")]
        public BoolOptions IsInspectionCompleted { get; set; }

        [Display(Name = "Grant of Final Approval")]
        public BoolOptions IsFinalApproved { get; set; }

        //Save files as byte array - This method is Not using
        //public byte[] FileHomePlan { get; set; }
        //public byte[] FileLandPlan { get; set; }

        //Save file Url
        [ValidateNever]
        public string HomePlanFileUrl { get; set; }
        [ValidateNever]
        public string LandPlanFileUrl { get; set; }

        //public string? SessionId { get; set; }

        //[Column(TypeName = "uniqueidentifier")]
        //public Guid PaymentId { get; set; }
        //public string? PaymentIntentId { get; set; }
        //public string? PaymentStatus { get; set; }
        //public DateTime PaymentDate { get; set; }

        public string? ApplicationStatus { get; set; }


        // Foreign key properties

        // get the application user
        [ForeignKey("ApplicationUser")]
        [ValidateNever]
        public string ApplicationUserID { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // 1 to 1 relationship with payments
        //1 application have only 1 payment
        [ValidateNever]
        public virtual Payment? Payment { get; set; }

        // 1 to m relationship with ApplicationStatus
        // ApplicationStatus table contains all history changes of the application status
        [ValidateNever]
        public ICollection<ApplicationStatus>? ApplicationStatusDetails { get; set; }

        [NotMapped]
        [ValidateNever]
        public ApplicationStatus LastStatus { get; set; }


    }
}
