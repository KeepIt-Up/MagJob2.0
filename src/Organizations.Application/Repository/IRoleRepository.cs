

namespace Organizations.Application.Repository;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<List<Role>> GetRolesByOrganizationId(Guid organizationId, CancellationToken cancellationToken = default);
}
