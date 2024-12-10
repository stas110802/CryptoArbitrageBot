using CryptoArbitrageBot.ExchangeClients.Models;
using CryptoArbitrageBot.ExchangeClients.Utilities;
using CryptoArbitrageBot.ExchangesRestAPI.Api;
using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CryptoArbitrageBot.ExchangeClients.Clients;

public class NiceHashClient(IExchangeRestApi restApi) : IExchangeClient
{
    public CurrencyPair GetCurrencyInfo(string currency)
    {
        var response1 = restApi
            .CreateRequest(Method.Get, NiceHashEndpoint.CurrentPrices)
            .Execute();
        
        var response = restApi
            .CreateRequest(Method.Get, NiceHashEndpoint.CurrentPrices)
            .Execute()
            .FromJson<JToken>();
        if (response == null)
            throw new NullReferenceException("[NH Client] : GetCurrencyInfo response is null");
        
        foreach (var item in response)
        {
            if(currency != item.Path) 
                continue;
            
            var currencyPair = new CurrencyPair
            {
                Currency = currency
            };
            
            var isSuccessfully = decimal.TryParse(item.First.ToString(), out var price);
            if (isSuccessfully == false)
                throw new Exception("[NH Client, Parse ERROR] : Cannot parse the selling price");
            
            currencyPair.SellingPrice = price;

            return currencyPair;
        }
        
        throw new ArgumentException("[Argument ERROR] : Cannot find the specified currency");
    }

    public decimal GetCurrencyPrice(string currency)
    {
        return GetCurrencyInfo(currency).SellingPrice;
    }

    public IEnumerable<CurrencyBalance> GetAccountBalance()
    {
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

    public bool WithdrawalCurrency(string currency, decimal amount, string address)
    {
        throw new NotImplementedException();
    }

    public void GetMyOrders()
    {
        throw new NotImplementedException();
    }
}