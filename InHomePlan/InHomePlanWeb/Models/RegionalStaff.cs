using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class RegionalStaff
    {
        public int RegionalStaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Address { get; set; }
        public string City { get; set; }    
        public string State { get; set; }   
        public string ZipCode { get; set; }
        // Foreign key property
        public int UserID { get; set; }

        // Navigation property
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        // Navigation property for Reviews
        public virtual ICollection<Review> Review { get; set; }

    }
}
