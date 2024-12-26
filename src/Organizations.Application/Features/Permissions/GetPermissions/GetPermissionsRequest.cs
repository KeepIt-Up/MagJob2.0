using Organizations.Application.Features.Permissions.Get;

namespace Organizations.Application.Features.Permissions.GetPermissions;

public record GetPermissionsRequest : IRequest<List<GetPermissionResponse>>;