using Organizations.Application.Features.Members.Get;
using Organizations.Application.Features.Permissions.Get;

namespace Organizations.Application.Features.Roles.Get;

public sealed class GetRoleResponse
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public Guid OrganizationID { get; set; }
    public List<GetMemberResponse> Members { get; set; } = new List<GetMemberResponse>();
    public List<GetPermissionResponse> Permissions { get; set; } = new List<GetPermissionResponse>();
}