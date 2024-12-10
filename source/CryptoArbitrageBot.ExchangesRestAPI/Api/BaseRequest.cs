using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using static System.String;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Api;

public abstract class BaseRequest
{
    public RestRequest Request { get; set; }
    public RestClient Client { get; set; }
    public IApiOptions Options { get; set; }
    public BaseEndpoint Endpoint { get; set; }
    public string FullPath { get; set; } 
    public string? Payload { get; set; }
    
    public abstract BaseRequest Authorize(bool isAdditionalLogic = false);

    public abstract BaseRequest WithPayload(string payload);

    public virtual string Execute()
    {
        var result = Client.Execute(Request, Request.Method).Content;
        if (IsNullOrEmpty(result))
            throw new Exception("[REST-API] Request fetch error.");
        
        return result;
    }
}