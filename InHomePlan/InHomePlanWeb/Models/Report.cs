using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }
        public string ReportName { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }


    }
}
