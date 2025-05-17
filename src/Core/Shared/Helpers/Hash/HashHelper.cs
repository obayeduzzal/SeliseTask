using System.Security.Cryptography;
using System.Text;
namespace TTM.Core.Shared.Helpers;
public static class HashHelper
{
    public static string CreateHMACSHA256Hash(string data, string secret)
    {
        ASCIIEncoding encoding = new ASCIIEncoding();
        HMACSHA256 cryptographer = new HMACSHA256(encoding.GetBytes(secret));
        byte[] bytes = cryptographer.ComputeHash(encoding.GetBytes(data));

        return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
    }
}
