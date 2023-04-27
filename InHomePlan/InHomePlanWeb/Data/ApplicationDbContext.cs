using InHomePlanWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InHomePlanWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //to create the table in the database
        public DbSet<User> User { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationHeader> ApplicationHeader { get; set; }
        public DbSet<ApplicationDetails> ApplicationDetails { get; set; }
        public DbSet<SystemAdmin> SystemAdmin { get; set; }
        public DbSet<Surveyor> Surveyor { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Status> Status { get; set; }   
        public DbSet<Session> Session { get; set; }
        public DbSet<RegionalStaff> RegionalStaff { get; set; } 
        public DbSet<Review> Review { get; set; }  
        public DbSet<Report> Report { get; set; } 
        public DbSet<Architect> Architect { get; set; }
        public DbSet<Approval> Approval { get; set; }
        public DbSet<Inspection> Inspection { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Rejection_Detail> Rejection_Detail { get; set; }

    }
}










