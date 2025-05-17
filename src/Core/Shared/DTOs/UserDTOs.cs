namespace TTM.Core.Shared.DTOs;

public class UserSearchDTO
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UserDTO
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
    public List<RoleDTO> Roles { get; set; } = new();
}
