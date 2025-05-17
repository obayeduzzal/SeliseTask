namespace TTM.Core.Infrastructure.Services.Users;

public interface IUserService : ITransientDependency
{
    Task<bool> HasPermissionAsync(Guid userId, string permission, CancellationToken ct = default);
    List<RoleDTO> GetUserRoles(List<UserRole> userRoles);
    UserDTO GetUserDTO(User user);
}