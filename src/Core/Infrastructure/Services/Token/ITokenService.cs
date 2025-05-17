using System.Security.Claims;
using TTM.Core.Infrastructure.DependencyInjection;

namespace TTM.Core.Infrastructure.Services.Token;

public interface ITokenService : ITransientDependency
{
    AccessTokenDTO GetAccessToken(User user);
    string GetRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
}