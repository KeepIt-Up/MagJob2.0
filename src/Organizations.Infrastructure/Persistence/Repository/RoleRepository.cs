namespace Organizations.Infrastructure.Persistence.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Role>> GetRolesByOrganizationId(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await Context.Roles.Where(r => r.OrganizationId == organizationId).Include(r => r.Permissions).Include(r => r.Members).ToListAsync(cancellationToken);
    }

}
