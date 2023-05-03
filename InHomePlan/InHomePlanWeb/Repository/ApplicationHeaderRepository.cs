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
        //updating status
        public void UpdateStatus(int id, string ApplicationStatus, string? paymentStatus = null)
        {
            var ApplicationFromDb = _db.ApplicationHeader.FirstOrDefault(u => u.Id == id);
            if (ApplicationFromDb != null) {
                ApplicationFromDb.ApplicationStatus = ApplicationStatus;
                if(!string.IsNullOrEmpty(paymentStatus))
                {
                    ApplicationFromDb.PaymentStatus = paymentStatus;
                }
            }

        }
        //updating payment intent
        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var ApplicationFromDb = _db.ApplicationHeader.FirstOrDefault(u => u.Id == id);
            if(!string.IsNullOrEmpty(sessionId)) {
                ApplicationFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                ApplicationFromDb.PaymentIntendId = paymentIntentId;
                ApplicationFromDb.PayementDate = DateTime.Now;
            }
        }
    }
}
