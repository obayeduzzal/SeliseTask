using TTM.Core.Infrastructure.Services.Roles;
using TTM.Core.Infrastructure.Services.Users;

namespace TTM.Core.Modules.Users.BasicInfo.Update;

public class UpdateUserRequestHandler(
   IRepository<User> userRepository,
   IRoleService roleService,
   IUserService userService) : IRequestHandler<UpdateUserCommand, UserDTO>
{
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly IRoleService _roleService = roleService;
    private readonly IUserService _userService = userService;

    public async Task<UserDTO> Handle(
      UpdateUserCommand command,
      CancellationToken ct)
    {
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdSpec(command.Id, includeRoles: true), ct);
        if (user == null)
            ErrorHelper.ThrowNotFoundException("User", "User not found!");

        user!.Update(command.FullName!, await GetValidUserRoles(command.RoleIds, ct));

        await _userRepository.UpdateAsync(user, ct);

        return _userService.GetUserDTO(user);
    }

    private async Task<List<UserRole>> GetValidUserRoles(
        List<Guid> roleIds,
        CancellationToken ct)
    {
        var distinctRoleIds = roleIds.Distinct().ToList();
        var roles = await _roleService.GetRolesByIdsAsync(distinctRoleIds, ct);

        if (distinctRoleIds.Count != roles.Count)
            ErrorHelper.ThrowBadRequestException("Invalid roles", "One or more role Ids are invalid!");

        return roles.ConvertAll(role => UserRole.Create(role));
    }
}
