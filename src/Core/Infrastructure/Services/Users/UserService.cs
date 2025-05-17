using Microsoft.Extensions.Configuration;

namespace TTM.Core.Infrastructure.Services.Users;

public class UserService(IRepository<UserRole> userRoleRepository) : IUserService
{
    private readonly IRepository<UserRole> _userRoleRepository = userRoleRepository;
    public async Task<bool> HasPermissionAsync(
        Guid userId,
        string permission,
        CancellationToken ct = default)
    {
        var spec = new GetUserRolesByUserIdSpec(userId, isTracking: false);
        var permissions = (await _userRoleRepository.ListAsync(spec, ct))
                         .SelectMany(s => s.Role!.RoleClaims.Select(p => p.ClaimName))
                         .ToList();

        return permissions?.Contains(permission) ?? false;
    }

    public List<RoleDTO> GetUserRoles(List<UserRole> userRoles)
    => userRoles?.Select(userRole => userRole.Role.Adapt<RoleDTO>()).ToList() ?? new();

    public UserDTO GetUserDTO(User user)
    {
        var dto = user.Adapt<UserDTO>();
        dto.Roles = GetUserRoles(user.UserRoles.ToList());

        return dto;
    }
}