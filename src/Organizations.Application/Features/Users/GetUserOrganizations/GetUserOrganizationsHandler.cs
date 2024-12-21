namespace Organizations.Application.Features.Users.GetUserOrganizations;

public sealed class GetUserOrganizationsHandler(
    IUserRepository userRepository,
    IMapper mapper
    ) : IRequestHandler<GetUserOrganizationsRequest, GetUserOrganizationsResponse>
{
    public async Task<GetUserOrganizationsResponse> Handle(GetUserOrganizationsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}