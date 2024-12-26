using Organizations.Application.Features.Roles.Get;

namespace Organizations.Application.Features.Organizations.GetOrganizationRoles;

public sealed class GetOrganizationRolesHandler(
    IOrganizationRepository organizationRepository
    ) : IRequestHandler<GetOrganizationRolesRequest, PaginatedList<Role, GetRoleResponse>>
{
    public async Task<PaginatedList<Role, GetRoleResponse>> Handle(GetOrganizationRolesRequest request, CancellationToken cancellationToken)
    {
        return await organizationRepository.GetRoles<GetRoleResponse>(request.Id, request.Options);
    }
}