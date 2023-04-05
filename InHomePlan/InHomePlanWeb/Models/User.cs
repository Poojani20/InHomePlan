using System.ComponentModel.DataAnnotations;

namespace InHomePlanWeb.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string address { get; set; }
        public string Usertype { get; set; }    

    }
}
