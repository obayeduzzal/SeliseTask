namespace TTM.Core.Infrastructure.Auth;

public class SecuritySettings
{
    public required string Provider { get; set; }
    public bool RequireEmailConfirmedAccount { get; set; }
    public int EmailVerificationCodeExpiredInMinutes { get; set; }
    public int PasswordVerificationCodeExpiredInMinutes { get; set; }
    public string? TwoFactorSecurityKey { get; set; }
    public int TwoFactorCodeExpiredInMinutes { get; set; }
    public bool RequireLockout { get; set; }
    public int LockoutTimeSpanInMinutes { get; set; }
    public int MaxFailedAccessAttempts { get; set; }
}
