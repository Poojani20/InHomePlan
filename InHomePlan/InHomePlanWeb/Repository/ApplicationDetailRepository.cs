using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace InHomePlanWeb.Repository
{
    public class ApplicationDetailRepository : Repository<ApplicationDetails>, IApplicationDetailRepository 
    {
        private ApplicationDbContext _db;
        public ApplicationDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ApplicationDetails obj)
        {
            _db.ApplicationDetails.Update(obj);
        }
    }
}
