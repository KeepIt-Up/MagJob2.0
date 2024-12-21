using Organizations.Application.Features.Roles.Get;

namespace Organizations.Application.Features.Members.Get;

public sealed class GetMemberResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<GetRoleResponse> Roles { get; set; } = new List<GetRoleResponse>();
    public string AllRolesNames { get; set; }
}