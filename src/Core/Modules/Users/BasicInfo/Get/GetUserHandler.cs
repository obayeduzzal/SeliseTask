using TTM.Core.Infrastructure.Services.Users;

namespace TTM.Core.Modules.Users.BasicInfo.Get;

public sealed class GetUserRequestHandler(
    IRepository<User> userRepository,
    IUserService userService) : IRequestHandler<GetUserRequest, UserDTO>
{
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly IUserService _userService = userService;

    public async Task<UserDTO> Handle(
      GetUserRequest request,
      CancellationToken ct)
    {
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdSpec(request.Id, includeRoles: true), ct);
        if (user == null)
            ErrorHelper.ThrowNotFoundException("User", "User not found!");

        return _userService.GetUserDTO(user!);
    }
}