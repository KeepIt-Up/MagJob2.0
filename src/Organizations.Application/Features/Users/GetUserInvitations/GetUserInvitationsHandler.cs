

namespace Organizations.Application.Features.Users.GetUserInvitations;

public sealed class GetUserInvitationsHandler(
    IUserRepository userRepository,
    IMapper mapper
    ) : IRequestHandler<GetUserInvitationsRequest, GetUserInvitationsResponse>
{
    public async Task<GetUserInvitationsResponse> Handle(GetUserInvitationsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var identity = await _identityRepository.GetById(request.Id, cancellationToken);
        // if (identity == null)
        // {
        //     throw new NotFoundException("Identity not found");
        // }

        // return _mapper.Map<GetIdentityResponse>(identity);
    }
}