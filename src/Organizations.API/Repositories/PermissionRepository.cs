using Microsoft.EntityFrameworkCore;
using Organizations.API.Common.Repositories;
using Organizations.API.Data;

public interface IPermissionRepository : IAsyncRepository<Permission>
{
    Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
}

public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    private readonly OrganizationDbContext _context;
    public PermissionRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.ToListAsync(cancellationToken);
    }
}
