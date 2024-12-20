public class RoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public int OrganizationId { get; set; }
    public List<MemberResponse> Members { get; set; } = new List<MemberResponse>();
    public List<PermissionResponse> Permissions { get; set; } = new List<PermissionResponse>();
}

public class PermissionResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
