using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public Guid PaymentId { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        public virtual Application Application { get; set; }

    }
}
