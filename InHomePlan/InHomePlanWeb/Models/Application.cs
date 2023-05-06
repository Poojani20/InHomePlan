using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        [Required]
        public String Address { get; set; }
        
        [Required]
        public String NIC { get; set; }
        
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        public String Phone { get; set; }
       
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public String AssessmentNo { get; set; }

        [Required]
        public String PostalCode { get; set;}

        [Required]
        public String StreetName { get; set; }
        public int No_Of_Rooms { get; set; } 
        public int No_of_perches { get; set; }
        public String BuildingArea { get; set; }
        public String PlanNo { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        public String? Payment_Method { get; set; }
        public DateTime? Payment_Date { get; set; }

        // Foreign key property
        //public int UserID { get; set; }

        // Navigation property
        //[ForeignKey("UserID")]
        //public virtual User User { get; set; }

        // Navigation property for Reviews
        //public virtual ICollection<Review> Review { get; set; }

    }
}
