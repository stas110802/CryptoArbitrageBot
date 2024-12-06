using CryptoArbitrageBot.ExchangesRestAPI.Utilities;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Api;

public class BinanceRequest : BaseRequest
{
    public override BaseRequest Authorize(bool isAdditionalLogic = false)
    {
        var query = GetQuery(FullPath);
        if (string.IsNullOrEmpty(query))
            throw new NullReferenceException("[Binance Request Error]\nRequired query parameters cant be nullable");
        
        var signature = HashCalculator.CalculateHMACSHA256Hash(query, Options.SecretKey);
        FullPath += $"&signature={signature}";
        Request = new RestRequest(FullPath);
        Request.AddHeader("X-MBX-APIKEY", Options.PublicKey);
        
        return this;
    }

    public override BaseRequest WithPayload(string payload)
    {
        throw new NotImplementedException();
    }
    
    private static string? GetQuery(string url)
    {
        var arrSplit = url.Split('?');

        return arrSplit.Length == 1 ? null : arrSplit[1];
    }
}