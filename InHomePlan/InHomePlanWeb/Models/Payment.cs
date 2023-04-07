using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public string Amount { get; set; }
        public string PaymentMethod { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }

    }
}
