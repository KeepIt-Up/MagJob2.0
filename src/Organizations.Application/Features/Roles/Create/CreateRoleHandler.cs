namespace Organizations.Application.Features.Roles.Create;

public sealed class CreateRoleHandler(
    IOrganizationRepository organizationRepository
    ) : IRequestHandler<CreateRoleRequest, CreateRoleResponse>
{
    public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}