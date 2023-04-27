using InHomePlanWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InHomePlanWeb.Repository.IRepository;

public class Repository<T> : IRepository<T> where T : class

{
    public readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
       // _db.Application == dbSet
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        throw new NotImplementedException();
    }
}
