using Organizations.Application.Features.Members.Get;

namespace Organizations.Application.Repository;

public interface IOrganizationRepository : IBaseRepository<Organization>
{
    Task<Organization?> GetByName(string name, CancellationToken cancellationToken);
    Task<(byte[]? profileImage, byte[]? bannerImage)> GetOrganizationImagesAsync(Guid organizationId);
    Task UpdateOrganizationImagesAsync(Guid organizationId, byte[]? profileImage, byte[]? bannerImage);
    Task<PaginatedList<Member, T>> GetMembers<T>(Guid organizationId, PaginationOptions paginationOptions);
    Task<PaginatedList<Invitation, T>> GetInvitations<T>(Guid organizationId, PaginationOptions paginationOptions);
}
