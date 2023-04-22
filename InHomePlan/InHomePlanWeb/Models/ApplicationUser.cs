using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace InHomePlanWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
       
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string StreetAddress { get; internal set; }
        public string City { get; internal set; }
        public string Name { get; internal set; }
        public string State { get; internal set; }
        public string PostalCode { get; internal set; }
    }
}
