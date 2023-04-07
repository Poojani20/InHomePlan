using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string Review_Comment { get; set;}
        public string Review_Status { get; set; }
        public DateTime Review_Date { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }
    }
}
