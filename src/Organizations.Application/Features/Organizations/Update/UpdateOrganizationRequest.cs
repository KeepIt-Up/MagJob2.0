using System.ComponentModel.DataAnnotations;

namespace Organizations.Application.Features.Organizations.Update;

public record UpdateOrganizationRequest(
    [Required]
    Guid OrganizationId,
    [MaxLength(50)]
    string? Name,
    [MaxLength(1000)]
    string? Description,
    [MaxLength(5 * 1024 * 1024)]
    byte[]? ProfileImage,
    [MaxLength(5 * 1024 * 1024)]
    byte[]? BannerImage
    ) : IRequest<UpdateOrganizationResponse>;

