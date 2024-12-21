namespace Organizations.Application.Features.Roles.Create;

public sealed class CreateRoleResponse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int OrganizationId { get; set; }
}