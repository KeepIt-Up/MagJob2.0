using Organizations.API.Models;

namespace Organizations.API.Data.Seeding
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(OrganizationDbContext context)
        {
            // Ensure database is created
            //await context.Database.EnsureCreatedAsync();

            // Check if there's already data
            if (context.Organizations.Any())
            {
                return; // Database has been seeded
            }
            await SeedPermissions(context);
            await SeedOrganizations(context);

            await context.SaveChangesAsync();
        }

        private static async Task SeedOrganizations(OrganizationDbContext context)
        {
            // Sample base64 image data (you can replace these with actual image data)
            var sampleProfileImage = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+ip1sAAAAASUVORK5CYII="); // Minimal base64 image data
            var sampleBannerImage = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+ip1sAAAAASUVORK5CYII="); // Minimal base64 image data

            List<Organization> organizations = new List<Organization>
            {
                Organization.Create("Organization 1", "Description 1", "7b0abc9e-087e-4e82-a7b7-50c3fadd73dc", sampleProfileImage, sampleBannerImage),
                Organization.Create("Organization 2", "Description 2", "2", sampleProfileImage, sampleBannerImage),
                Organization.Create("Organization 3", "Description 3", "3", sampleProfileImage, sampleBannerImage),
                Organization.Create("Organization 4", "Description 4", "4", sampleProfileImage, sampleBannerImage),
                Organization.Create("Organization 5", "Description 5", "5", sampleProfileImage, sampleBannerImage)
            };

            //add invitations
            organizations[0].InviteUser("1");
            organizations[0].InviteUser("2");
            organizations[1].InviteUser("7b0abc9e-087e-4e82-a7b7-50c3fadd73dc");
            organizations[1].InviteUser("4");
            organizations[2].InviteUser("7b0abc9e-087e-4e82-a7b7-50c3fadd73dc");
            organizations[2].InviteUser("6");

            //add members
            organizations[0].AddMember("7b0abc9e-087e-4e82-a7b7-50c3fadd73dc", "James", "Dean");
            organizations[0].AddMember("2", "Johnny", "Cash1");
            organizations[0].AddMember("3", "Johnny", "Cash2");
            organizations[0].AddMember("4", "Johnny", "Cash3");
            organizations[0].AddMember("5", "Johnny", "Cash4");
            organizations[0].AddMember("6", "Johnny", "Cash5");
            organizations[0].AddMember("7", "Johnny", "Cash6");
            organizations[0].AddMember("8", "Johnny", "Cash7");
            organizations[0].AddMember("9", "Johnny", "Cash8");
            organizations[0].AddMember("10", "Johnny", "Cash9");
            organizations[0].AddMember("11", "Johnny", "Cash10");
            organizations[0].AddMember("12", "Johnny", "Cash11");
            organizations[0].AddMember("13", "Johnny", "Cash12");
            organizations[0].AddMember("14", "Johnny", "Cash13");
            organizations[0].AddMember("15", "Johnny", "Cash14");
            organizations[0].AddMember("16", "Johnny", "Cash15");
            organizations[0].AddMember("17", "Johnny", "Cash16");
            organizations[0].AddMember("18", "Johnny", "Cash17");
            organizations[1].AddMember("3", "Jimmie", "Hendrix");
            organizations[1].AddMember("4", "Janis", "Joplin");

            //add roles
            organizations[0].AddRole("Admin", "Administrator role for the organization", "#000000");
            organizations[0].AddRole("Member", "Member role for the organization", "#000000");
            organizations[0].AddRole("Guest", "Guest role for the organization", "#000000");
            organizations[0].AddRole("Moderator", "Moderator role for the organization", "#000000");
            organizations[0].AddRole("Artist", "Artist role for the organization", "#000000");
            organizations[0].AddRole("Composer", "Composer role for the organization", "#000000");

            await context.Organizations.AddRangeAsync(organizations);
        }

        private static async Task SeedPermissions(OrganizationDbContext context)
        {
            List<Permission> permissions = new List<Permission>
            {
                new Permission { Id = 1, Name = "Create Organization", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { Id = 2, Name = "Edit Organization", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { Id = 3, Name = "Delete Organization", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };

            await context.Permissions.AddRangeAsync(permissions);
        }

    }
}