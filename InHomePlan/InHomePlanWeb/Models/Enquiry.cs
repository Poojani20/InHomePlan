using System.ComponentModel.DataAnnotations;

namespace InHomePlanWeb.Models
{
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
