using Microsoft.EntityFrameworkCore;
using Organizations.API.Common.Repositories;
using Organizations.API.Data;

public interface IRoleRepository : IAsyncRepository<Role>
{
    Task<List<Role>> GetRolesByOrganizationId(int organizationId, CancellationToken cancellationToken = default);
}

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly OrganizationDbContext _context;
    public RoleRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetRolesByOrganizationId(int organizationId, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.Where(r => r.OrganizationId == organizationId).Include(r => r.Permissions).Include(r => r.Members).ToListAsync(cancellationToken);
    }

}
