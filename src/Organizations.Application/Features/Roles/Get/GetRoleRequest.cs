namespace Organizations.Application.Features.Roles.Get;

public sealed record GetRoleRequest : IRequest<GetRoleResponse>
{
    public int Id { get; set; }
}