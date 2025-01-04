namespace Organizations.Application.Repository;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<PaginatedList<Role, T>> GetRolesByOrganizationId<T>(Guid organizationId, PaginationOptions paginationOptions);
}
