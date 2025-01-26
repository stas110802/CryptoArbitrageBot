using System.Security.Cryptography;
using System.Text;
using CryptoArbitrageBot.Utilities.Types;
using static System.String;

namespace CryptoArbitrageBot.ExchangesRestAPI.Utilities;

public static class HashCalculator
{
    public static string CalculateHMACSHA256Hash(string text, string salt)
    {
        var encoding = Encoding.Default;

        var baText2BeHashed = encoding.GetBytes(text);
        var baSalt = encoding.GetBytes(salt);
        using var hasher = new HMACSHA256(baSalt);
        var baHashedText = hasher.ComputeHash(baText2BeHashed);

        var result = Join("", baHashedText.ToList()
            .Select(b => b.ToString("x2")).ToArray());

        return result;
    }

    public static string CalculateHMACSHA384(string text, string salt, SignOutputType? outputType = null)
    {
        var sBytes = Encoding.UTF8.GetBytes(salt);
        var data = Encoding.UTF8.GetBytes(text);

        using var encryptor = new HMACSHA384(sBytes);
        
        var resultBytes = encryptor.ComputeHash(data);
        
        return outputType == SignOutputType.Base64 ? BytesToBase64String(resultBytes) : BytesToHexString(resultBytes);
    }
    
    public static string SignBinance(string payload, string salt)
    {
        var secret = Encoding.UTF8.GetBytes(salt);
        using var hmacsha256 = new HMACSHA256(secret);
        var payloadBytes = Encoding.UTF8.GetBytes(payload);
        var hash = hmacsha256.ComputeHash(payloadBytes);

        return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
    }
    
    private static string BytesToBase64String(byte[] buff)
    {
        return Convert.ToBase64String(buff);
    }
    
    private static string BytesToHexString(byte[] buff)
    {
        return Convert.ToHexString(buff);
    }
}