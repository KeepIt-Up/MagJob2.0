using Organizations.API.Common.Repositories;
using Organizations.API.Data;
using Organizations.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Organizations.API.Repositories;

public interface IOrganizationRepository : IAsyncRepository<Organization>
{
    Task<(byte[]? profileImage, byte[]? bannerImage)> GetOrganizationImagesAsync(int organizationId);
    Task UpdateOrganizationImagesAsync(int organizationId, byte[]? profileImage, byte[]? bannerImage);
}

public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
{
    private readonly OrganizationDbContext _context;

    public OrganizationRepository(OrganizationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(byte[]? profileImage, byte[]? bannerImage)> GetOrganizationImagesAsync(int organizationId)
    {
        var organization = await _context.Set<Organization>()
            .Where(o => o.Id == organizationId)
            .Select(o => new { o.ProfileImage, o.BannerImage })
            .FirstOrDefaultAsync();

        return organization == null
            ? (null, null)
            : (organization.ProfileImage, organization.BannerImage);
    }

    public async Task UpdateOrganizationImagesAsync(int organizationId, byte[]? profileImage, byte[]? bannerImage)
    {
        var organization = await _context.Set<Organization>()
            .FirstOrDefaultAsync(o => o.Id == organizationId);

        if (organization == null)
            throw new KeyNotFoundException($"Organization with ID {organizationId} not found.");

        organization.ProfileImage = profileImage;
        organization.BannerImage = bannerImage;

        await _context.SaveChangesAsync();
    }
}