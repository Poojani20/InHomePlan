using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InHomePlanWeb.Models
{
    public class Architect
    {
        [Key]
        public int ArchitectID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string LicenseNo { get; set; }

        // Foreign key property
        public int UserID { get; set; }

        // Navigation property
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

    }
}
