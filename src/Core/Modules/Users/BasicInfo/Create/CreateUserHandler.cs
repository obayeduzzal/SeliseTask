using TTM.Core.Infrastructure.Services.Roles;


namespace TTM.Core.Modules.Users.BasicInfo.Create;

public class CreateUserHandler(
    IRepository<User> userRepository,
    IRoleService roleService) : IRequestHandler<CreateUserCommand, ResponseMetaDTO>
{
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly IRoleService _roleService = roleService;

    public async Task<ResponseMetaDTO> Handle(
        CreateUserCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpec(command.Email), ct);
        if (user != null)
            ErrorHelper.ThrowBadRequestException("User", "Email Already in Use");

        user = User.CreateUser(
            fullName: command.FullName,
            email: command.Email,
            passWord: command.Password.HashPassword(),
            isActive: false,
            roles: await GetValidUserRoles(command.RoleIds, ct));

        await _userRepository.AddAsync(user, ct);

        return new ResponseMetaDTO
        {
            Message = "New User created successfully."
        };
    }

    private async Task<List<UserRole>> GetValidUserRoles(
        List<Guid> roleIds,
        CancellationToken ct)
    {
        var distinctRoleIds = roleIds.Distinct().ToList();
        var roles = await _roleService.GetRolesByIdsAsync(distinctRoleIds, ct);

        if (distinctRoleIds.Count != roles.Count)
            ErrorHelper.ThrowBadRequestException("Invalid roles", "One or more role IDs are invalid.");

        return roles.ConvertAll(role => UserRole.Create(role));
    }
}
