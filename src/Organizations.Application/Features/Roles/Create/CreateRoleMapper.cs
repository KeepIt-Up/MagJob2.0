namespace Organizations.Application.Features.Roles.Create;

public sealed class CreateRoleMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRoleRequest, CreateRoleResponse>();
    }
}