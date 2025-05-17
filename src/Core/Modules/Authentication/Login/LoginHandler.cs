using TTM.Core.Infrastructure.Services.Authentication;
using TTM.Core.Infrastructure.Services.Users;

namespace TTM.Core.Modules.Authentication.Login;

#region Handler
public class LoginHandler(
    IRepository<User> userRepository,
    IUserService userService,
    IAuthenticationService authenticationService) : IRequestHandler<LoginCommand, AuthDTO>
{
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly IUserService _userService = userService;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<AuthDTO> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpec(command.Email, includeRoles: true), cancellationToken);
        ValidateUser(user, command);

        return new AuthDTO
        {
            TokenInfo = await _authenticationService.GetAndStoreUserTokens(user!, cancellationToken),
            UserInfo = _userService.GetUserDTO(user!)
        };
    }

    #region Private
    private static void ValidateUser(User? user, LoginCommand request)
    {
        if (user is null)
            ErrorHelper.ThrowBadRequestException("Auth", "Incorrect email address.");
        if (!user!.IsActive)
            ErrorHelper.ThrowBadRequestException("Auth", "User not active.");

        if (!request.Password.IsPasswordMatch(user!.Password))
            ErrorHelper.ThrowBadRequestException("Auth", "Incorrect email or password.");
    }
    #endregion
}
#endregion
