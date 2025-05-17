using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TTM.Core.Infrastructure.Auth;
using TTM.Core.Shared.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TTM.Core.Infrastructure.Services.Token;

public class TokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public AccessTokenDTO GetAccessToken(User user) => GenerateAndGetJWTToken(user);
    public string GetRefreshToken()
    {
        byte[] randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        if (string.IsNullOrEmpty(_jwtSettings.Key))
            ErrorHelper.ThrowBadRequestException("JWTKEY", "No Key defined in JwtSettings config.");

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            ErrorHelper.ThrowUnauthorizedException("AccessToken", "Invalid access token.");
        }

        return principal!;
    }

    #region Private
    private AccessTokenDTO GenerateAndGetJWTToken(User user) => GenerateEncryptedToken(user);

    private static IEnumerable<Claim> GetClaims(User user)
    {
        return
        [
            new(AppClaims.UserID, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(AppClaims.FullName, user.FullName)
        ];
    }

    private SigningCredentials GetSigningCredentials()
    {
        if (string.IsNullOrEmpty(_jwtSettings.Key))
            ErrorHelper.ThrowBadRequestException("JWT KEY", "No Key defined in JwtSettings config.");

        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }

    private AccessTokenDTO GenerateEncryptedToken(
        User user)
    {
        var accessTokenExpiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
           claims: GetClaims(user),
           expires: accessTokenExpiryTime,
           signingCredentials: GetSigningCredentials());

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AccessTokenDTO
        {
            AccessToken = tokenHandler.WriteToken(token),
            AccessTokenExpiryTime = accessTokenExpiryTime
        };
    }
    #endregion
}