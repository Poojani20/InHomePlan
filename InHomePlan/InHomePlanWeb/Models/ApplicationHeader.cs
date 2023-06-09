﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InHomePlanWeb.Models
{
    public class ApplicationHeader
    {
        [Key]
        public int Id { get; set; }
       
        [ValidateNever]
        public DateTime SumbitDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public double Total { get; set; }

        public string? ApplicationStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }

        public DateTime PayementDate { get; set; }
        //public DateOnly PaymentDueDate { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntendId { get; set; }

        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String NIC { get; set; }
        [Required]
        public String Phone { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public String AssessmentNo { get; set; }
        [Required]
        public String PostalCode { get; set; }
        [Required]
        public String StreetName { get; set; }
        [Required]
        public int No_Of_Rooms { get; set; }
        [Required]
        public int No_of_perches { get; set; }
        [Required]
        public String BuildingArea { get; set; }
        [Required]
        public String PlanNo { get; set; }



        // public string? Carrier { get; set; }
    }
}
