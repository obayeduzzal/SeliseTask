using TTM.Core.Shared.Authorization;

namespace TTM.Core.Infrastructure.Services.Roles;

public class RoleService(
    IRepository<Role> roleRepository,
    IRepository<UserRole> userRoleRepository) : IRoleService
{
    private readonly IRepository<Role> _roleRepository = roleRepository;
    private readonly IRepository<UserRole> _userRoleRepository = userRoleRepository;

    #region Role
    public async Task<bool> HasUsersInRoleAsync(Guid roleID, CancellationToken ct = default)
        => await _userRoleRepository.AnyAsync(new GetUserRoleByRoleIdSpec(roleID), ct);

    public async Task<bool> IsExistAsync(string name, CancellationToken ct = default)
        => await _roleRepository.AnyAsync(new GetRoleByNameSpec(name), ct);

    public async Task<List<Role>> GetRolesByIdsAsync(List<Guid> ids, CancellationToken ct = default)
        => await _roleRepository.ListAsync(new GetRolesByIdsSpec(ids), cancellationToken: ct);

    public RoleDetailsDTO GetRoleDetailsDTO(Role role)
    {
        var dto = role!.Adapt<RoleDetailsDTO>();
        dto.Permissions = [.. role.RoleClaims.Select(s => s.ClaimName)];

        return dto;
    }
    #endregion

    #region Role Claims
    public List<RoleClaim> GetRoleClaimsByPermissions(List<string> permissions)
    {
        var claimsList = AppPermissions.All
          .Select(s => $"{AppClaims.Permission}.{s.Resource}.{s.Action}".ToLower())
          .ToList();

        var validPermissions = permissions
            .Where(x => claimsList.Any(s => s.Equals(x, StringComparison.CurrentCultureIgnoreCase)))
            .Distinct()
            .ToList();

        if (validPermissions.Count == 0)
            ErrorHelper.ThrowBadRequestException("Permissions", "At least one valid permission is required.");

        var roleClaims = new List<RoleClaim>();

        validPermissions.ForEach(permission =>
        {
            roleClaims.Add(new RoleClaim
            {
                ClaimType = AppClaims.Permission,
                ClaimName = permission,
            });
        });

        return roleClaims;
    }
    #endregion
}