using MediatR;

namespace Organizations.Application.Features.Members.Update;

public record UpdateMemberRequest(
    Guid id,
    string? firstName,
    string? lastName,
    string? notes
) : IRequest<UpdateMemberResponse>;