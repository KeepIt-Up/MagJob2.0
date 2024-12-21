using Organizations.Infrastructure.Common;

namespace Organizations.Application.Features.Organizations.GetOrganizationMembers;

public sealed record GetOrganizationMembersRequest(Guid Id) : QueryWithPaginationOptions, IRequest<GetOrganizationMembersResponse>;
