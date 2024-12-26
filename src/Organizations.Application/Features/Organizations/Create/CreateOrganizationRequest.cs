using System.ComponentModel.DataAnnotations;

namespace Organizations.Application.Features.Organizations.Create;

public sealed record CreateOrganizationRequest(
    string name,
    string? description,
    string? website,
    string? email
) : IRequest<CreateOrganizationResponse>;