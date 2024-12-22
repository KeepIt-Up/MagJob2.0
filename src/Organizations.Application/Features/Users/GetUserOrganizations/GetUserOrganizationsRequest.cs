namespace Organizations.Application.Features.Users.GetUserOrganizations;

public sealed record GetUserOrganizationsRequest(Guid id) : QueryWithPaginationOptions, IRequest<PaginatedList<Organization, GetOrganizationResponse>>;