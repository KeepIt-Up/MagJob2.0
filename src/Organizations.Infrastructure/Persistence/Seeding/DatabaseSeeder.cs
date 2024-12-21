using Organizations.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static void SeedRequiredData(ApplicationDbContext context)
    {

        context.SaveChanges();
    }

    public static void SeedTestData(ApplicationDbContext context)
    {
        context.SaveChanges();
    }
}