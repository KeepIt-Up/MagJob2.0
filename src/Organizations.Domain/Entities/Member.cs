namespace Organizations.Domain.Entities;

public class Member : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Notes { get; set; }
    public bool Archived { get; set; } = false;
    public string FullName => $"{FirstName} {LastName}";
    public Guid UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();

    private Member() { }
    public static Member Create(Guid userId, Guid organizationId, string firstName, string lastName, Role role)
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

    // public void Update(UpdateMemberRequest request)
    // {
    //     if (!string.IsNullOrWhiteSpace(request.FirstName))
    //         FirstName = request.FirstName;
    //     if (!string.IsNullOrWhiteSpace(request.LastName))
    //         LastName = request.LastName;
    //     if (!string.IsNullOrWhiteSpace(request.Notes))
    //         Notes = request.Notes;
    // }
}