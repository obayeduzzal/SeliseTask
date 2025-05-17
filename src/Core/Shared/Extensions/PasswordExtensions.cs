using TTM.Core.Shared.Helpers;

namespace TTM.Core.Shared.Extensions;
public static class PasswordExtensions
{
    public static string HashPassword(this string password)
        => PasswordHashHelper.CreateHash(password);

    public static bool IsPasswordMatch(this string password, string hashedPassword) =>
        PasswordHashHelper.VerifyHashed(hashedPassword, password);
}