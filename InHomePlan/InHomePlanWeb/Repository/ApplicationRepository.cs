using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Repository.IRepository;
using System.Linq.Expressions;

namespace InHomePlanWeb.Repository
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository 
    {
        private ApplicationDbContext _db;
        public ApplicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Application obj)
        {
            _db.Application.Update(obj);
        }
    }
}
