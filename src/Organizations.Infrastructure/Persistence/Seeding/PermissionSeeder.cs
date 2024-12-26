using Organizations.Infrastructure.Persistence;

public static class PermissionSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Permissions.Any())
            return;

        var permissions = new List<Permission>
        {
           new Permission { Name = "Create Organization" },
           new Permission { Name = "Edit Organization" },
           new Permission { Name = "Delete Organization" },
           new Permission { Name = "Create Role" },
           new Permission { Name = "Edit Role" },
           new Permission { Name = "Delete Role" },
           new Permission { Name = "Create Member" },
           new Permission { Name = "Edit Member" },
           new Permission { Name = "Delete Member" },
           new Permission { Name = "Create Invitation" },
           new Permission { Name = "Edit Invitation" },
           new Permission { Name = "Delete Invitation" }
        };

        context.Permissions.AddRange(permissions);
    }
}