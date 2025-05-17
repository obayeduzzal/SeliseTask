namespace TTM.Core.Domain;

public class UserRole : IAggregateRoot
{
    public Guid Id { get; set; }
    public Guid UserId { get; private set; }
    public virtual User? User { get; private set; }
    public Guid RoleId { get; private set; }
    public virtual Role? Role { get; private set; }

    public static UserRole Create(Role role)
    {
        return new UserRole
        {
            Id = Guid.NewGuid(),
            Role = role
        };
    }
}