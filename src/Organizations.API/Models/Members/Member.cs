using Organizations.API.Common.Models;

namespace Organizations.API.Models;

public class Member : BaseEntity<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Notes { get; set; }
    public bool Archived { get; set; } = false;
    public string FullName => $"{FirstName} {LastName}";
    public string UserId { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();

    private Member() { }
    public static Member Create(string userId, int organizationId, string firstName, string lastName, Role role)
    {
        return new Member
        {
            UserId = userId,
            OrganizationId = organizationId,
            FirstName = firstName,
            LastName = lastName,
            Roles = [role]
        };
    }

    public void Update(UpdateMemberRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.FirstName))
            FirstName = request.FirstName;
        if (!string.IsNullOrWhiteSpace(request.LastName))
            LastName = request.LastName;
        if (!string.IsNullOrWhiteSpace(request.Notes))
            Notes = request.Notes;
    }
}