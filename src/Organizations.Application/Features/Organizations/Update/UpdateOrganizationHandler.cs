namespace Organizations.Application.Features.Organizations.Update;

public sealed class UpdateOrganizationHandler(
    IOrganizationRepository organizationRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<UpdateOrganizationRequest, UpdateOrganizationResponse>
{
    public async Task<UpdateOrganizationResponse> Handle(UpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        var organizationWithSameName = await organizationRepository.GetByName(request.Name, cancellationToken);
        if (organizationWithSameName != null)
        {
            throw new ConflictException("Organization name must be unique");
        }

        var organization = await organizationRepository.Get(request.OrganizationId, cancellationToken);
        if (organization == null)
        {
            throw new NotFoundException("Organization not found");
        }

        organization.Update(request.Name, request.Description, request.ProfileImage, request.BannerImage);
        organizationRepository.Update(organization);
        await unitOfWork.Save(cancellationToken);

        return mapper.Map<UpdateOrganizationResponse>(organization);
    }
}