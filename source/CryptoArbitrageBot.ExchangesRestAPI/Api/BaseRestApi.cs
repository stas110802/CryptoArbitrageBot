using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using RestSharp;
using static System.String;

namespace CryptoArbitrageBot.ExchangesRestAPI.Api;

public sealed class BaseRestApi<T> 
    where T : BaseRequest, new()
{
    private readonly IApiOptions _apiOptions;

    public BaseRestApi(IApiOptions apiOptions)
    {
        _apiOptions = apiOptions;
    }
    
    public T CreateRequest(Method method, BaseEndpoint endpoint,
        string? query = null, string? payload = null)
    {
        var full = endpoint.Value + query;
        var result = new T
        {
            ApiOptions = _apiOptions,
            Client = new RestClient(_apiOptions.BaseUri),
            RequestOptions = new RequestOptions
            {
                FullPath = full,
                Endpoint = endpoint,
                Payload = payload,
                Request = new RestRequest(full)
                {
                    Method = method
                }
            }
        };
        
        return result;
    }
}