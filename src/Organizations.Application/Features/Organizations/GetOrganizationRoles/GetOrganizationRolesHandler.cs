namespace Organizations.Application.Features.Organizations.GetOrganizationRoles;

public sealed class GetOrganizationRolesHandler(
    IOrganizationRepository organizationRepository
    ) : IRequestHandler<GetOrganizationRolesRequest, GetOrganizationRolesResponse>
{
    public async Task<GetOrganizationRolesResponse> Handle(GetOrganizationRolesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}