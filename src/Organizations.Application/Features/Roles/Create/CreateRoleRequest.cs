namespace Organizations.Application.Features.Roles.Create;

public sealed record CreateRoleRequest : IRequest<CreateRoleResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int OrganizationId { get; set; }
}