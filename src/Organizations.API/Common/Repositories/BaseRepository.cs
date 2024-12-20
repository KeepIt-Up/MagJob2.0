using Organizations.API.Common.Models;
using Organizations.API.Data;
namespace Organizations.API.Common.Repositories;

public interface IAsyncRepository<T> where T : BaseEntity<int>
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    IQueryable<T> AsQueryable(CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity<int>
{
    private readonly OrganizationDbContext _context;

    public BaseRepository(OrganizationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    public IQueryable<T> AsQueryable(CancellationToken cancellationToken = default)
    {
        return _context.Set<T>().AsQueryable();
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
