using Organizations.Domain.Common;
using Organizations.Application.Repository;

namespace Organizations.Infrastructure.Persistence.Common;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext Context;

    public BaseRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public void Create(T entity)
    {
        Context.Add(entity);
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.DateCreated = DateTimeOffset.UtcNow;
        Context.Update(entity);
    }

    public Task<T> Get(Guid id, CancellationToken cancellationToken)
    {
        return Context.Set<T>().FirstOrDefaultAsync(x => x.ID == id, cancellationToken);
    }

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return Context.Set<T>().ToListAsync(cancellationToken);
    }
}
