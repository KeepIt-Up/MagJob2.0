namespace Organizations.Application.Features.Roles.Update;

public sealed class UpdateRoleHandler(
    IRoleRepository roleRepository
    ) : IRequestHandler<UpdateRoleRequest, UpdateRoleResponse>
{
    public async Task<UpdateRoleResponse> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}