using InHomePlanWeb.Data;
using InHomePlanWeb.Models;
using InHomePlanWeb.Repository.IRepository;

namespace InHomePlanWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            ApplicationHeader = new ApplicationHeaderRepository(_db);
            //ApplicationHeader = new ApplicationHeaderRepository(_db);
            //ApplicationStatus = new ApplicationDetailRepository(_db);
        }
       
        public IApplicationRepository Application { get; private set; }

        public IApplicationDetailRepository ApplicationDetails { get; private set; }

        public IApplicationHeaderRepository ApplicationHeader { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
