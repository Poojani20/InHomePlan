using InHomePlanWeb.Models;

namespace InHomePlanWeb.Repository.IRepository
{
    public interface IApplicationHeaderRepository : IRepository<ApplicationHeader>
    {
        void Update(ApplicationHeader obj);
        void UpdateStatus(int id, string ApplicationStatus, string? paymentStatus = null);
        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
    }
}
