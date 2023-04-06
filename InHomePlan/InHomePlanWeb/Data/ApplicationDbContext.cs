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




    }
}
