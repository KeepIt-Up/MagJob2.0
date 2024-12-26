namespace Organizations.Infrastructure.Persistence.Repository;

internal class RoleRepository(
    ApplicationDbContext context
    ) : BaseRepository<Role>(context), IRoleRepository
{

    public async Task<List<Role>> GetRolesByOrganizationId(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await Context.Roles.Where(r => r.OrganizationId == organizationId).Include(r => r.Permissions).Include(r => r.Members).ToListAsync(cancellationToken);
    }

}
