namespace Organizations.Infrastructure.Persistence.Repository;

internal class RoleRepository(
    ApplicationDbContext context
    ) : BaseRepository<Role>(context), IRoleRepository
{

    public async Task<PaginatedList<Role, T>> GetRolesByOrganizationId<T>(Guid organizationId, PaginationOptions paginationOptions)
    {
        return await PaginatedList<Role, T>.CreateAsync(GetAll().Where(r => r.OrganizationId == organizationId), paginationOptions);
    }

}
