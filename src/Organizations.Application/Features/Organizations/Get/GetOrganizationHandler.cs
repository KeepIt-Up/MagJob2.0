namespace Organizations.Application.Features.Organizations.Get;

public sealed class GetOrganizationHandler(
    IOrganizationRepository organizationRepository
    ) : IRequestHandler<GetOrganizationRequest, GetOrganizationResponse>
{
    public async Task<GetOrganizationResponse> Handle(GetOrganizationRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}