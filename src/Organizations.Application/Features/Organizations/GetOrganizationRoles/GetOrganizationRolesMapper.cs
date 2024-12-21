namespace Organizations.Application.Features.Organizations.GetOrganizationRoles;

public sealed class GetOrganizationRolesMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, GetOrganizationRolesResponse>();
    }
}