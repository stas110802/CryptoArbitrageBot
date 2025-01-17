using System.Globalization;
using CryptoArbitrageBot.ExchangesRestAPI.Api;
using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using static NUnit.Framework.Assert;

namespace CryptoArbitrage.ExchangeRestAPI.UnitTest;

public class BinancePublicApiTest
{
    private BaseRestApi<BinanceRequest> _publicRestApi; 
    [SetUp]
    public void Setup()
    {
        _publicRestApi = new BaseRestApi<BinanceRequest>(new ExchangeApiOptions
        {
            BaseUri = "https://api.binance.com",
        });
    }

    [Test]
    public void TestPublicApi()
    {
        var query = $"?symbol=BTCUSDT";
        var response = _publicRestApi
            .CreateRequest(Method.Get, BinanceEndpoint.CurrentPrice, query)
            .Execute();
        var token = JObject.Parse(response).SelectToken("price");
        var result = decimal.TryParse(token.ToString(), CultureInfo.InvariantCulture, out var price);
        That(result, Is.True);
    }
}