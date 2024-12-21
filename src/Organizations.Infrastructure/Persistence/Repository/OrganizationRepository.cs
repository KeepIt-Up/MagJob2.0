

namespace Organizations.Infrastructure.Persistence.Repository;

internal class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Organization?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await Context.Set<Organization>().FirstOrDefaultAsync(o => o.Name == name, cancellationToken);
    }

    public async Task<(byte[]? profileImage, byte[]? bannerImage)> GetOrganizationImagesAsync(Guid organizationId)
    {
        var organization = await Context.Set<Organization>()
            .Where(o => o.ID == organizationId)
            .Select(o => new { o.ProfileImage, o.BannerImage })
            .FirstOrDefaultAsync();

        return organization == null
            ? (null, null)
            : (organization.ProfileImage, organization.BannerImage);
    }

    public async Task UpdateOrganizationImagesAsync(Guid organizationId, byte[]? profileImage, byte[]? bannerImage)
    {
        var organization = await Context.Set<Organization>()
            .FirstOrDefaultAsync(o => o.ID == organizationId);

        if (organization == null)
            throw new KeyNotFoundException($"Organization with ID {organizationId} not found.");

        organization.ProfileImage = profileImage;
        organization.BannerImage = bannerImage;

        await Context.SaveChangesAsync();
    }
}