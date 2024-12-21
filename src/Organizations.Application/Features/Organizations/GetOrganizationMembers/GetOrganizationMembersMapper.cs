namespace Organizations.Application.Features.Organizations.GetOrganizationMembers;

public sealed class GetOrganizationMembersMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Member, GetOrganizationMembersResponse>();
    }
}