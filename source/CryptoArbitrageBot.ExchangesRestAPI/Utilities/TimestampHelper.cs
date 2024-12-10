namespace CryptoArbitrageBot.ExchangesRestAPI.Utilities;

public static class TimestampHelper
{
    /// <summary>
    /// Returns timestamp(UtcNow)
    /// </summary>
    /// <returns></returns>
    public static string GetUtcTimestamp()
    {
        var unixEpoch = new DateTime(1970, 1, 1);
        var elapsedTime = DateTime.UtcNow.Subtract(unixEpoch);
        var unixTimestamp = (long)elapsedTime.TotalMilliseconds;  

        return unixTimestamp.ToString();
    }
}