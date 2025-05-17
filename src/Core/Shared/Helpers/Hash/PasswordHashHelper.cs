using System.Security.Cryptography;
using System.Text;
namespace TTM.Core.Shared.Helpers;
public static class PasswordHashHelper
{
    private const int SaltByteSize = 24;
    private const int HashByteSize = 24;
    private const int HasingIterationsCount = 1010100;

    public static string CreateHash(string password)
    {
        if (password == null)
            ErrorHelper.ThrowBadRequestException("Password", "Password can not be null");

        byte[] salt;
        byte[] buffer2;

        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password!, SaltByteSize, HasingIterationsCount, HashAlgorithmName.SHA256))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(HashByteSize);
        }

        byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
        Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
        Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);

        return Convert.ToBase64String(dst);
    }

    public static bool VerifyHashed(string hashedValue, string value)
    {
        if (hashedValue == null)
            ErrorHelper.ThrowBadRequestException("HashedValue", "HashedValue can not be null");
        if (value == null)
            ErrorHelper.ThrowBadRequestException("Password", "Password can not be null");

        byte[] passwordHashBytes;

        int arrayLen = (SaltByteSize + HashByteSize) + 1;

        byte[] src = Convert.FromBase64String(hashedValue!);

        if ((src.Length != arrayLen) || (src[0] != 0))
        {
            return false;
        }

        byte[] currentSaltBytes = new byte[SaltByteSize];
        Buffer.BlockCopy(src, 1, currentSaltBytes, 0, SaltByteSize);

        byte[] currentHashBytes = new byte[HashByteSize];
        Buffer.BlockCopy(src, SaltByteSize + 1, currentHashBytes, 0, HashByteSize);

        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(value!, currentSaltBytes, HasingIterationsCount, HashAlgorithmName.SHA256))
        {
            passwordHashBytes = bytes.GetBytes(SaltByteSize);
        }

        return IsHashesEqual(currentHashBytes, passwordHashBytes);
    }

    public static string CreateHMACSHA256Hash(string data, string secret)
    {
        ASCIIEncoding encoding = new ASCIIEncoding();
        HMACSHA256 cryptographer = new HMACSHA256(encoding.GetBytes(secret));
        byte[] bytes = cryptographer.ComputeHash(encoding.GetBytes(data));

        return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
    }

    private static bool IsHashesEqual(byte[] firstHash, byte[] secondHash)
    {
        int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
        int xor = firstHash.Length ^ secondHash.Length;

        for (int i = 0; i < minHashLength; i++)
        {
            xor |= firstHash[i] ^ secondHash[i];
        }

        return xor == 0;
    }
}
