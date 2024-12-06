using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Api;

public interface IExchangeRestApi
{
    public BaseRequest CreateRequest(Method method, BaseEndpoint niceHashEndpoint, string? query = null, string? payload = null);
}