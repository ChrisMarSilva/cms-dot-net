using System.Security.Cryptography;
using System.Text;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;

public static class Uteis
{
    public static string HmacSHA256(string message, string secret)
    {
        using var hmac = new HMACSHA256(key: Encoding.Default.GetBytes(secret)); // Encoding.UTF8 // Encoding.Default;
        var hmBytes = hmac.ComputeHash(buffer: Encoding.Default.GetBytes(message));
        return ToHexString(hmBytes); // Convert.ToBase64String(hmBytes); // string.Join("", hashmessage.ToList().Select(b => b.ToString("x2")).ToArray());
    }

    public static string ToHexString(byte[] array)
    {
        StringBuilder hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}

