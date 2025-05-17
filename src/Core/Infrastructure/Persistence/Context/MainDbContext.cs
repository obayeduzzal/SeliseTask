using TTM.Core.Domain.Team;
using TTM.Core.Infrastructure.Persistence.Configuration;

namespace TTM.Core.Infrastructure.Persistence.Context;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
{
    // Users
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    // Roles
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RoleClaim> RoleClaims => Set<RoleClaim>();

    // Teams
    public DbSet<Team> Teams => Set<Team>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Users
        modelBuilder
            .Entity<User>()
            .ToTable(nameof(Users), SchemaNames.Identity);
        modelBuilder
            .Entity<UserRole>()
            .ToTable(nameof(UserRoles), SchemaNames.Identity);

        // Roles
        modelBuilder
            .Entity<Role>()
            .ToTable(nameof(Roles), SchemaNames.Identity);
        modelBuilder
            .Entity<RoleClaim>()
            .ToTable(nameof(RoleClaims), SchemaNames.Identity);

        // Team
        modelBuilder
            .Entity<Team>()
            .ToTable(nameof(Teams), SchemaNames.Identity);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await base.SaveChangesAsync(ct);
    }
}
