using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace InHomePlanWeb.Repository
{
    public class ApplicationHeaderRepository : Repository<ApplicationHeader>, IApplicationHeaderRepository 
    {
        private ApplicationDbContext _db;
        public ApplicationHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ApplicationHeader obj)
        {
            _db.ApplicationHeader.Update(obj);
        }
    }
}
