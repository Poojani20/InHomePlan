using InHomePlanWeb.Models;

namespace InHomePlanWeb.Repository.IRepository
{
    public interface IApplicationRepository : IRepository<Application>
    {
        void Update(Application obj);
        void Save();
    }
}
