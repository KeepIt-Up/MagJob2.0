namespace Organizations.Application.Features.Roles.Update;

public sealed class UpdateRoleMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateRoleRequest, UpdateRoleResponse>();
    }
}