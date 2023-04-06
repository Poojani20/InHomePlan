using System.ComponentModel.DataAnnotations.Schema;

namespace InHomePlanWeb.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public string Token { get; set; } 
        public DateTime Expiration { get; set; }

        // Foreign key property
        public int UserID { get; set; }

        // Navigation property
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

    }
}
