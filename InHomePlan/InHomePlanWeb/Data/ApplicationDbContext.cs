using InHomePlanWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace InHomePlanWeb.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) 
        {

        }
        //to create the table in the database
        public DbSet<User> User { get; set; }
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
        public DbSet<Regional_Staff_Approval> Regional_Staff_Approval { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Regional_Staff_Approval>()
                .HasKey(ra => new { ra.ApprovalID, ra.RegionalStaffID });

            modelBuilder.Entity<Regional_Staff_Approval>()
                .HasOne(ra => ra.Approval)
                .WithMany(a => a.Regional_Staff_Approval)
                .HasForeignKey(ra => ra.ApprovalID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Regional_Staff_Approval>()
                .HasOne(ra => ra.RegionalStaff)
                .WithMany(rs => rs.Regional_Staff_Approval)
                .HasForeignKey(ra => ra.RegionalStaffID)
                .OnDelete(DeleteBehavior.Cascade);
        }





    }
}
