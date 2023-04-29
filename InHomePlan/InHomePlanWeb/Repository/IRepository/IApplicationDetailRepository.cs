using InHomePlanWeb.Models;

namespace InHomePlanWeb.Repository.IRepository
{
    public interface IApplicationDetailRepository : IRepository<ApplicationDetails>
    {
        void Update(ApplicationDetails obj);
        void Save();
    }
}
