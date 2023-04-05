using InHomePlanWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace InHomePlanWeb.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) 
        {

        }
        public DbSet<User> User { get; set; } //to create the table in the database
    }
}
