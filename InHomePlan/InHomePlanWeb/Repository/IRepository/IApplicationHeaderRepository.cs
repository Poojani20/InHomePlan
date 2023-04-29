using InHomePlanWeb.Models;

namespace InHomePlanWeb.Repository.IRepository
{
    public interface IApplicationHeaderRepository : IRepository<ApplicationHeader>
    {
        void Update(ApplicationHeader obj);
        void Save();
    }
}
