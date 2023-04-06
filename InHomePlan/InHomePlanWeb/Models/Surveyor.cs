using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Surveyor
    {
        [Key]
        public int SurveyorID { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}  
        public string Address { get; set;}
        public string LicenseNo { get; set;}

        // Foreign key property
        public int UserID { get; set; }

        // Navigation property
        [ForeignKey("UserID")]
        public virtual User User { get; set; }


    }
}
