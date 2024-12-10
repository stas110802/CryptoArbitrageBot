using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Api;

public sealed class BaseRestApi<TRequest> : IExchangeRestApi
    where TRequest : BaseRequest, new()
{
    private readonly IApiOptions _options;

    public BaseRestApi(IApiOptions options)
    {
        _options = options;
    }

    public BaseRequest CreateRequest(Method method, BaseEndpoint endpoint, string? query = null, string? payload = null)
    {
        var full = endpoint.Value + query;
        
        var result = new TRequest
        {
            Request = new RestRequest(full),
            Client = new RestClient(_options.BaseUri),
            FullPath = full,
            Endpoint = endpoint,
            Payload = payload!,
            Options = _options
        };

        return result;
    }
}