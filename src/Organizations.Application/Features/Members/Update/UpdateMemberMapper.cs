namespace Organizations.Application.Features.Members.Update;

public sealed class UpdateMemberMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateMemberRequest, UpdateMemberResponse>();
    }
}