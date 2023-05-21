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
        public void UpdateStatus(int id, string applicationStatus, string? paymentStatus = null)
        {
            var applicationFromDb = _db.Application.FirstOrDefault(u => u.ApplicationID == id);
            
            if (applicationFromDb != null) {

                applicationFromDb.ApplicationStatus = applicationStatus;

                if(!string.IsNullOrEmpty(paymentStatus))
                {
                    applicationFromDb.Payment.PaymentStatus = paymentStatus;
                }
            }

        }

        //updating payment intent
        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var ApplicationFromDb = _db.Application.FirstOrDefault(u => u.ApplicationID == id);
            if(!string.IsNullOrEmpty(sessionId)) {
                ApplicationFromDb.Payment.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                ApplicationFromDb.Payment.PaymentIntentId = paymentIntentId;
                ApplicationFromDb.Payment.PaymentDate = DateTime.Now;
            }
        }
    }
}
