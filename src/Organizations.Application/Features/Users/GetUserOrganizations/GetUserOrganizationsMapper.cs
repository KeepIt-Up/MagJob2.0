namespace Organizations.Application.Features.Users.GetUserOrganizations;

public sealed class GetUserOrganizationsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, GetUserOrganizationsResponse>();
    }
}