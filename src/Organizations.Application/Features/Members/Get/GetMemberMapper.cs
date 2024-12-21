namespace Organizations.Application.Features.Members.Get;

public sealed class GetMemberMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Member, GetMemberResponse>()
            .Map(dest => dest.ID, src => src.ID)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Roles, src => src.Roles);
    }
}