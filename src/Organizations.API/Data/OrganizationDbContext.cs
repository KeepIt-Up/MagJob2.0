using Microsoft.EntityFrameworkCore;
using Organizations.API.Common.Models;
using Organizations.API.Models;

namespace Organizations.API.Data
{
    public class OrganizationDbContext : DbContext
    {
        private readonly IConfiguration Configuration;

        public OrganizationDbContext(IConfiguration configuration, DbContextOptions<OrganizationDbContext> options)
            : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.ProfileImage)
                    .HasColumnType("bytea");

                entity.Property(e => e.BannerImage)
                    .HasColumnType("bytea");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration["OrganizationConnection"]);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity<int> && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity<int>)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                entity.UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity<int> && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity<int>)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                entity.UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}