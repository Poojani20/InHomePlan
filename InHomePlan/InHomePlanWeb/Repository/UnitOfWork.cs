using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Repository.IRepository;

namespace InHomePlanWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        
        public IApplicationRepository Application { get; private set; }

        public IApplicationDetailRepository ApplicationDetails { get; private set; }

        public IApplicationHeaderRepository ApplicationHeader { get; private set; }

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Application = new ApplicationRepository(_db);
            ApplicationHeader = new ApplicationHeaderRepository(_db);
            ApplicationDetails = new ApplicationDetailRepository(_db);
        }
       
        public void save()
        {
            _db.SaveChanges();
        }
    }
}
