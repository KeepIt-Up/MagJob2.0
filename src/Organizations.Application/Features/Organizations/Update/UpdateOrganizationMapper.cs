namespace Organizations.Application.Features.Organizations.Update;

public sealed class UpdateOrganizationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateOrganizationRequest, UpdateOrganizationResponse>();
    }
}