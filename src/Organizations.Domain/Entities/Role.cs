using System.Text.Json.Serialization;


namespace Organizations.Domain.Entities;
public class Role : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public List<Member> Members { get; set; } = new List<Member>();
    public List<Permission> Permissions { get; set; } = new List<Permission>();

    private Role() { }
    public static Role Create(string name, Guid organizationId, string? description, string? color)
    {
        return new Role
        {
            Name = name,
            OrganizationId = organizationId,
            Description = description,
            Color = color
        };
    }

    public static Role[] GetDefaultRoles(Guid organizationId)
    {
        return [
            Create("@everyone", organizationId, "All members of the organization", "#000000")
        ];
    }

    public void AddPermissions(List<Permission> permissions)
    {
        Permissions.AddRange(permissions);
    }

    public void RemovePermissions(List<Permission> permissions)
    {
        Permissions.RemoveAll(p => permissions.Contains(p));
    }

    public void AddMembers(List<Member> members)
    {
        Members.AddRange(members);
    }

    public void RemoveMembers(List<Member> members)
    {
        Members.RemoveAll(m => members.Equals(m));
    }
}

public class Permission : BaseEntity
{
    public string Name { get; set; }
    [JsonIgnore]
    public List<Role> Roles { get; set; } = new List<Role>();
}

public class CreateRolePayload
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
}
