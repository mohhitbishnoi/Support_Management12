using Application.Interfaces.Repository;
using Domain.Commons;
using Persistence.DataContexts;

namespace Persistence.Extension.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private Dictionary<string, object> _repositories = new();
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
    {

        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = new GenericRepository<T>(_context);
            _repositories.Add(type, repositoryType);
           
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public Task<int> Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
