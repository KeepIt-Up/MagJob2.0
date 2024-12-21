namespace Organizations.Application.Features.Roles.Get;

public sealed class GetRoleMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, GetRoleResponse>()
            .Map(dest => dest.ID, src => src.ID)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Color, src => src.Color)
            .Map(dest => dest.OrganizationId, src => src.OrganizationId)
            .Map(dest => dest.Members, src => src.Members)
            .Map(dest => dest.Permissions, src => src.Permissions);
    }
}