namespace Organizations.Application.Features.Invitations.Get;

public sealed class GetInvitationRequest(Guid invitationId) : IRequest<GetInvitationResponse>;