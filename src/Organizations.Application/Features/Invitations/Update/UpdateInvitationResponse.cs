namespace Organizations.Application.Features.Invitations.Update;

public sealed class UpdateInvitationResponse
{
    public Guid Id { get; init; }
    public InvitationStatus Status { get; init; }
}