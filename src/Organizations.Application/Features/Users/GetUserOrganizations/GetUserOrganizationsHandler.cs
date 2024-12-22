using Organizations.Application.Common.Interfaces;

namespace Organizations.Application.Features.Users.GetUserOrganizations;

public sealed class GetUserOrganizationsHandler(
    IOrganizationRepository organizationRepository,
    IUserAccessor userAccessor,
    IMapper mapper
    ) : IRequestHandler<GetUserOrganizationsRequest, PaginatedList<Organization, GetOrganizationResponse>>
{
    public async Task<PaginatedList<Organization, GetOrganizationResponse>> Handle(GetUserOrganizationsRequest request, CancellationToken cancellationToken)
    {
        return await organizationRepository.GetOrganizationsByUserIdAsync<GetOrganizationResponse>(request.id, request.Options);
    }
}