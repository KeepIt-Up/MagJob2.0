namespace Organizations.Application.Features.Organizations.Get;

public sealed record GetOrganizationResponse(
    Guid ID,
    string Name,
    string? Description,
    Guid OwnerId,
    byte[]? ProfileImage,
    byte[]? BannerImage
);