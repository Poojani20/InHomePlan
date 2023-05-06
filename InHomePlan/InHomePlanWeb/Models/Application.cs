using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }   
        public String Address { get; set; }   
        public String NIC { get; set; } 
        public String Phone { get; set; }   
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String AssessmentNo { get; set; }
        public String PostalCode { get; set;}
        public String StreetName { get; set; } 
        public int No_Of_Rooms { get; set; } 
        public int No_of_perches { get; set; }
        public String BuildingArea { get; set; }
        public String PlanNo { get; set; }
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
