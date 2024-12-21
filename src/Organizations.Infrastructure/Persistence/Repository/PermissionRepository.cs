

namespace Organizations.Infrastructure.Persistence.Repository;

public interface IPermissionRepository : IBaseRepository<Permission>
{
    Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
}

public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    private readonly ApplicationDbContext _context;
    public PermissionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.ToListAsync(cancellationToken);
    }
}
