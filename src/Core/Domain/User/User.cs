namespace TTM.Core.Domain;

public class User : IAggregateRoot
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public string UserName { get; private set; } = default!;
    public bool IsActive { get; private set; }

    public virtual ICollection<UserRole> UserRoles { get; private set; } = [];

    public static User CreateUser(
        string fullName,
        string email,
        string passWord,
        bool isActive,
        List<UserRole> roles)
    {
        return new User
        {
            FullName = fullName,
            Email = email.ToLower(),
            UserName = email.ToLower(),
            Password = passWord,
            IsActive = isActive,
            UserRoles = roles,
        };
    }

    public static User CreateUser(
        Guid id,
        string fullName,
        string email,
        string passWord,
        bool isActive,
        List<UserRole> roles)
    {
        return new User
        {
            Id = id,
            FullName = fullName,
            Email = email.ToLower(),
            UserName = email.ToLower(),
            Password = passWord,
            IsActive = isActive,
            UserRoles = roles,
        };
    }

    public User Update(string fullName)
    {
        FullName = fullName;

        return this;
    }

    public User Update(
        string fullName,
        List<UserRole> roles)
    {
        FullName = fullName;
        UserRoles = roles;

        return this;
    }
}