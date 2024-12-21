namespace Organizations.Application.Features.Members.Update;

public sealed class UpdateMemberHandler(
    IMemberRepository memberRepository
    ) : IRequestHandler<UpdateMemberRequest, UpdateMemberResponse>
{
    public async Task<UpdateMemberResponse> Handle(UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}