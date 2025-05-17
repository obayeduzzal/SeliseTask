namespace TTM.Core.Domain;

public class Role : IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = default!;
    public string NormalizedName { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsSystem { get; private set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; private set; } = [];

    public static Role Create(
        string name,
        string normalizedName,
        bool isSystem,
        List<RoleClaim>? permissions = null,
        string? description = null)
    {
        return new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsSystem = isSystem,
            NormalizedName = normalizedName.ToUpperInvariant(),
            Description = description?.Trim(),
            RoleClaims = permissions ?? [],
        };
    }

    public static Role Create(
        Guid id,
        string name,
        string normalizedName,
        bool isSystem,
        List<RoleClaim>? permissions = null,
        string? description = null)
    {
        return new Role
        {
            Id = id,
            Name = name,
            IsSystem = isSystem,
            NormalizedName = normalizedName.ToUpperInvariant(),
            Description = description?.Trim(),
            RoleClaims = permissions ?? [],
        };
    }

    public Role Update(
        string name,
        List<RoleClaim> permissions,
        string? description = null)
    {
        Name = name;
        NormalizedName = name.ToUpperInvariant();
        Description = description?.Trim();
        RoleClaims = permissions;

        return this;
    }
}