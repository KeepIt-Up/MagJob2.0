namespace Organizations.Application.Features.Organizations.Update;

public sealed class UpdateOrganizationHandler(
    IOrganizationRepository organizationRepository
    ) : IRequestHandler<UpdateOrganizationRequest, UpdateOrganizationResponse>
{
    public async Task<UpdateOrganizationResponse> Handle(UpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}