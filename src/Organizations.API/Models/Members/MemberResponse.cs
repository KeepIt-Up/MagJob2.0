public class MemberResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<RoleResponse> Roles { get; set; } = new List<RoleResponse>();
    public string AllRolesNames { get; set; }
}
