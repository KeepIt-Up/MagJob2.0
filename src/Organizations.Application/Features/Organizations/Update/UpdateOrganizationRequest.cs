using MediatR;

namespace Organizations.Application.Features.Organizations.Update;

public record UpdateOrganizationRequest : IRequest<UpdateOrganizationResponse>
{

}
