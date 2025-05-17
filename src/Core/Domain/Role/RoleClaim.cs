namespace TTM.Core.Domain;

public class RoleClaim : IAggregateRoot
{
    public Guid Id { get; set; }
    public required string ClaimType { get; set; }
    public required string ClaimName { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}