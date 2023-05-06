namespace InHomePlanWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationRepository Application{ get; }
        IApplicationDetailRepository ApplicationDetails { get; }
        IApplicationHeaderRepository ApplicationHeader{ get; }

        void Save();
    }
}
