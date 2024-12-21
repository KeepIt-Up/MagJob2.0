using Organizations.Infrastructure.Persistence;

public static class OrganizationSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        var sampleProfileImage = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+ip1sAAAAASUVORK5CYII=");
        var sampleBannerImage = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+ip1sAAAAASUVORK5CYII=");

        var organizations = new List<Organization>
        {
            //Organization.Create("Organization 1", "7b0abc9e-087e-4e82-a7b7-50c3fadd73dc", "Description 1", sampleProfileImage, sampleBannerImage),
            // ... other organizations
        };

        SeedMembersAndRoles(organizations);
        context.Organizations.AddRange(organizations);
    }

    private static void SeedMembersAndRoles(List<Organization> organizations)
    {
        // Add members and roles to organizations
        var org1 = organizations[0];
        //org1.AddMember("7b0abc9e-087e-4e82-a7b7-50c3fadd73dc", "James", "Dean");
        // ... other members and roles
    }
}