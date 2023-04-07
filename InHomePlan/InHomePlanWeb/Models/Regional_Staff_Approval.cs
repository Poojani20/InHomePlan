using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Regional_Staff_Approval
    {
        [Key]
        public int RegionalStaffApprovalId { get; set; }

        [ForeignKey("Approval")]
        public int ApprovalID { get; set; }

        [ForeignKey("RegionalStaff")]
        public int RegionalStaffID { get; set; }

        // Navigation properties
        public virtual Approval Approval { get; set; }
        public virtual RegionalStaff RegionalStaff { get; set; }
    }
}
