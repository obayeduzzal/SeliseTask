namespace TTM.Core.Shared.DTOs;
public class ApplicationSystemRoleDTO
{
    public required string Name { get; set; }
    public required string NormalizedName { get; set; }
}

public class RoleDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string NormalizedName { get; set; } = default!;
    public string? Description { get; set; }
}

public class RoleDetailsDTO : RoleDTO
{
    public List<string> Permissions { get; set; } = new();
}

public class RoleClaimsDTO
{
    public string Name { get; set; } = default!;
    public List<RoleClaimDTO> Claims { get; set; } = new();
}

public class RoleClaimDTO
{
    public string Action { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Permission { get; set; } = default!;
}