using TTM.Core.Infrastructure.Services.Token;

namespace TTM.Core.Infrastructure.Services.Authentication;

public class AuthenticationService(
    IRepository<User> userRepository,
    ITokenService tokenService) : IAuthenticationService
{
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<AccessTokenDTO> GetAndStoreUserTokens(User user, CancellationToken ct = default)
    {
        var tokenDTO = _tokenService.GetAccessToken(user);
        await _userRepository.UpdateAsync(user, ct);

        return new AccessTokenDTO
        {
            AccessToken = tokenDTO.AccessToken,
            AccessTokenExpiryTime = tokenDTO.AccessTokenExpiryTime
        };
    }
}