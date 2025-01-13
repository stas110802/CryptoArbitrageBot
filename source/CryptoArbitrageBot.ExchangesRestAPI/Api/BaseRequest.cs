using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Api;

public abstract class BaseRequest
{
    public RestClient? Client;
    public RequestOptions? RequestOptions { get; set; }
    public IApiOptions? ApiOptions { get; set; }
    public abstract BaseRequest Authorize();
    
    public string Execute()
    {
        if (RequestOptions == null || Client == null)
            throw new NullReferenceException("[Request error] : First you need to execute 'Create' method.");
        
        var result = Client
            .Execute(RequestOptions.Request, RequestOptions.Request.Method)
            .Content;
        
        if (string.IsNullOrEmpty(result))
            throw new Exception("[Request error] Request fetch error.");
        
        return result;
    }
}