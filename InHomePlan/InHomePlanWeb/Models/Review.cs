using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string Review_Comment { get; set;}
        public string Review_Status { get; set; }
        public DateTime ReviewDate { get; set; }

        // Foreign key properties
        public int ApplicationID { get; set; }
        public int RegionalStaffID { get; set; }

        // Navigation properties
        [ForeignKey("ApplicationID")]
        public virtual Application Application { get; set; }

        [ForeignKey("RegionalStaffID")]
        public virtual RegionalStaff RegionalStaff { get; set; }

    }
}
