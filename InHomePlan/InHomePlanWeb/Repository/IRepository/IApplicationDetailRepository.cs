using InHomePlanWeb.Models;

namespace InHomePlanWeb.Repository.IRepository
{
    public interface IApplicationDetailRepository : IRepository<ApplicationStatus>
    {
        void Update(ApplicationStatus obj);
        void Save();
    }
}
