using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Inspection
    {
        [Key]
        public int InspectionID { get; set; }
        public string InspectionStatus { get; set; }  
        public string InspectionComment { get; set;}
        public DateTime InspectionDate { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }

    }
}
