using System.Globalization;
using System.Text;
using CryptoArbitrageBot.ExchangesRestAPI.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoArbitrageBot.ExchangesRestAPI.Api;

public sealed class BitfinexRequest : BaseRequest
{
    private readonly BitfinexNonceProvider _nonceProvider = new();
    
    public override BaseRequest Authorize(Dictionary<string, object>? bodyParameters = null)
    {
        if (RequestOptions == null || ApiOptions == null)
            throw new NullReferenceException("");
        
        // var nonce = _nonceProvider.GetNonce();
        // var json = JsonConvert.SerializeObject(bodyParameters);
        //
        // var signature = $"/api{ApiOptions.BaseUri}{nonce}";
        // var signedData = HashCalculator.CalculateHMACSHA384(signature, ApiOptions.SecretKey);
        //
        // //RequestOptions.FullPath += $"&signature={signature}";
        // var method = RequestOptions.Request.Method;
        //
        // RequestOptions.Request = new RestRequest(RequestOptions.FullPath, method);
        // RequestOptions.Request.AddHeader("Content-Type", "application/json");
        // RequestOptions.Request.AddHeader("bfx-apikey", ApiOptions.PublicKey);
        // RequestOptions.Request.AddHeader("bfx-nonce", nonce);
        // RequestOptions.Request.AddHeader("bfx-signature", signedData);
        
        bodyParameters ??= new Dictionary<string, object>();
        bodyParameters.Add("request", RequestOptions.FullPath);
        bodyParameters.Add("nonce", _nonceProvider.GetNonce().ToString());

        var signature = JsonConvert.SerializeObject(bodyParameters);
        var payload = Convert.ToBase64String(Encoding.ASCII.GetBytes(signature));
        var signedData = HashCalculator.CalculateHMACSHA384(payload, ApiOptions.SecretKey);
        RequestOptions.Request.AddHeader("Content-Type", "application/json");
        RequestOptions.Request.AddHeader("X-BFX-APIKEY", ApiOptions.PublicKey);
        RequestOptions.Request.AddHeader("X-BFX-PAYLOAD", payload);
        RequestOptions.Request.AddHeader("X-BFX-SIGNATURE", signedData.ToLower(CultureInfo.InvariantCulture));
        
        return this;
    }
    
    private static string? GetQuery(string url)
    {
        var arrSplit = url.Split('?');

        return arrSplit.Length == 1 ? null : arrSplit[1];
    }
}