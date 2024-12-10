using System.Globalization;
using CryptoArbitrageBot.ExchangesRestAPI.Api;
using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CryptoArbitrage.ExchangeRestAPI.UnitTest;

public class BinancePublicApiTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestPublicApi()
    {
        var options = new BinanceOptions
        {
            PublicKey = "",
            SecretKey = ""
        };
        var server = new BaseRestApi<BinanceRequest>(options);
        var query = $"?symbol=BTCUSDT";
        var response = server
            .CreateRequest(Method.Get, BinanceEndpoint.CurrentPrice, query)
            .Execute();
        var token = JObject.Parse(response).SelectToken("price");
        var result = decimal.TryParse(token.ToString(), CultureInfo.InvariantCulture, out var price);
        
        Assert.IsTrue(result);
    }
}