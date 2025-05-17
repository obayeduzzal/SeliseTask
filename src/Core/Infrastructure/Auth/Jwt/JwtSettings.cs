namespace TTM.Core.Infrastructure.Auth;

public class JwtSettings
{
    public string Key { get; set; } = default!;
    public int TokenExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }
}