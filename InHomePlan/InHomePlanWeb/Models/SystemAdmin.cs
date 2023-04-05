using System.ComponentModel.DataAnnotations;

namespace InHomePlanWeb.Models
{
    public class SystemAdmin
    {
        [Key]
        public int SystemAdminID { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }


    }
}
