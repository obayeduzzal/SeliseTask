namespace TTM.Core.Infrastructure.Services.Authentication;

public interface IAuthenticationService : ITransientDependency
{
    Task<AccessTokenDTO> GetAndStoreUserTokens(User user, CancellationToken ct = default);
}