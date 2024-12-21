using System.ComponentModel.DataAnnotations;

namespace Organizations.Application.Features.Organizations.Create
{
    public sealed record CreateOrganizationRequest(
        [Required]
        string name,
        [MaxLength(1000)]
        string? description,
        [Url]
        string? website,
        [EmailAddress]
        string? email,
        [Phone]
        string? phone
    ) : IRequest<CreateOrganizationResponse>;
}