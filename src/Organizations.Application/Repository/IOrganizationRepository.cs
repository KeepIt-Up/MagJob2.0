namespace Organizations.Application.Repository;

public interface IOrganizationRepository : IBaseRepository<Organization>
{
    Task<(byte[]? profileImage, byte[]? bannerImage)> GetOrganizationImagesAsync(Guid organizationId);
    Task UpdateOrganizationImagesAsync(Guid organizationId, byte[]? profileImage, byte[]? bannerImage);
}
