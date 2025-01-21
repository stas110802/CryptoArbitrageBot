using CryptoArbitrageBot.Utilities;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Options;

public class RequestOptions
{
    public RestRequest Request { get; set; }
    public BaseType Endpoint { get; set; }
    public string FullPath { get; set; } 
    public string? Payload { get; set; }
}