namespace Organizations.Application.Features.Organizations.GetOrganizationRoles;

public sealed record GetOrganizationRolesRequest(Guid Id) : QueryWithPaginationOptions, IRequest<GetOrganizationRolesResponse>;
