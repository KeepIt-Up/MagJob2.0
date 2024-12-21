using Organizations.Application.Features.Roles.Get;

namespace Organizations.Application.Features.Members.Get;

public sealed class GetMemberResponse
{
    public Guid ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<GetRoleResponse> Roles { get; set; } = new List<GetRoleResponse>();
}