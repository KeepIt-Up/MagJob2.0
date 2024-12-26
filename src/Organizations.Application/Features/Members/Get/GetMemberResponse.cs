using Organizations.Application.Features.Roles.Get;

namespace Organizations.Application.Features.Members.Get;

public sealed record GetMemberResponse(
    Guid ID,
    string FirstName,
    string LastName,
    List<GetRoleResponse> Roles
);