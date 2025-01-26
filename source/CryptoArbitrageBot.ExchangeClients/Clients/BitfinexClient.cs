using System.Globalization;
using CryptoArbitrageBot.ExchangeClients.Models;
using CryptoArbitrageBot.ExchangeClients.Utilities;
using CryptoArbitrageBot.ExchangesRestAPI.Api;
using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CryptoArbitrageBot.ExchangeClients.Clients;

public sealed class BitfinexClient : IExchangeClient
{
    private readonly BaseRestApi<BitfinexRequest> _api;
    
    public BitfinexClient(ExchangeApiOptions options)
    {
        _api = new BaseRestApi<BitfinexRequest>(options);
    }
    
    public CurrencyPair GetCurrencyInfo(string currency)
    {
        var query = @"\t" + currency.Replace("USDT", "USD");
        var response = _api
            .CreateRequest(Method.Get, BitfinexEndpoint.Ticker, query)
            .Execute()
            .FromJson<JToken>();
        
        var isPriceValid = decimal.TryParse(response[6].ToString(), out var price);
        var isVolumeValid = decimal.TryParse(response[7].ToString(), out var volume);
        
        if(!isPriceValid || !isVolumeValid)
            throw new ArgumentException($"[Bitfinex Client] Currency - ({currency}) is not valid");

        return new CurrencyPair
        {
            Currency = currency,
            TradingVolume = volume,
            SellingPrice = price
        };
    }

    public decimal GetCurrencyPrice(string currency)
    {
        var query = @"\t" + currency.Replace("USDT", "USD");
        var response = _api
            .CreateRequest(Method.Get, BitfinexEndpoint.Ticker, query)
            .Execute()
            .FromJson<JToken>();
        
        var result = decimal.TryParse(response[6].ToString(), out var price);
        if(!result)
            throw new ArgumentException($"[Bitfinex Client] Currency - ({currency}) is not valid");
        return price;
    }

    public IEnumerable<CurrencyBalance> GetAccountBalance()
    {
        var response = _api
            .CreateRequest(Method.Post, BitfinexEndpoint.BalanceV1)
            .Authorize()
            .Execute()
            .FromJson<JToken>();
        
        throw new NotImplementedException();
    }

    public CurrencyBalance GetCurrencyBalance(string currency)
    {
        throw new NotImplementedException();
    }

    public bool CreateSellOrder(string currency, decimal amount, decimal price)
    {
        throw new NotImplementedException();
    }

    public bool CreateSellOrder(string currency, decimal amount)
    {
        throw new NotImplementedException();
    }

    public bool CancelAllOrders()
    {
        throw new NotImplementedException();
    }

    public bool WithdrawalCurrency(string coin, decimal amount, string address)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetMyOrders()
    {
        throw new NotImplementedException();
    }
}