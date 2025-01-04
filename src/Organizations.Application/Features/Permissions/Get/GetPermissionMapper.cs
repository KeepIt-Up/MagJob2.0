namespace Organizations.Application.Features.Permissions.Get;

public sealed class GetPermissionMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Permission, GetPermissionResponse>()
            .Map(dest => dest.ID, src => src.ID)
            .Map(dest => dest.Name, src => src.Name);
    }
}