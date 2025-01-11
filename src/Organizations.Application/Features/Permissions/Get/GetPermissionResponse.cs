namespace Organizations.Application.Features.Permissions.Get;

public sealed record GetPermissionResponse
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
