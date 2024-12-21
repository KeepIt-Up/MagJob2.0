namespace Organizations.Application.Features.Invitations.Get;

public sealed class GetInvitationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Invitation, GetInvitationResponse>();
    }
}