using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Rejection_Detail
    {
        [Key]
        public int RejectionID { get; set; }
        public string Reason { get; set;}
        public string RejectedBy { get; set;}

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }
    }
}
