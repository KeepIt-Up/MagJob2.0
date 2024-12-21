using Organizations.Application.Features.Invitations.Get;
using Organizations.Infrastructure.Common;

namespace Organizations.Application.Features.Organizations.GetOrganizationInvitations;

public sealed record GetOrganizationInvitationsResponse(PaginatedList<GetInvitationResponse> Invitations);