using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Options;

public class RequestOptions
{
    public RestRequest Request { get; set; }
    public BaseEndpoint Endpoint { get; set; }
    public string FullPath { get; set; } 
    public string? Payload { get; set; }
}