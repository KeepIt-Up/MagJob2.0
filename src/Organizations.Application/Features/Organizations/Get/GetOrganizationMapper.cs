namespace Organizations.Application.Features.Organizations.Get;

public sealed class GetOrganizationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Organization, GetOrganizationResponse>();
    }
}