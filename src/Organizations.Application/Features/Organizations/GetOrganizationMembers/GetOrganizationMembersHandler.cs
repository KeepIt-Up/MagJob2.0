namespace Organizations.Application.Features.Organizations.GetOrganizationMembers;

public sealed class GetOrganizationMembersHandler(
    IOrganizationRepository organizationRepository
    ) : IRequestHandler<GetOrganizationMembersRequest, GetOrganizationMembersResponse>
{
    public async Task<GetOrganizationMembersResponse> Handle(GetOrganizationMembersRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}