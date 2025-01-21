using System.Globalization;
using CryptoArbitrageBot.ExchangeClients.Models;
using CryptoArbitrageBot.ExchangeClients.Utilities;
using CryptoArbitrageBot.ExchangesRestAPI.Api;
using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using CryptoArbitrageBot.ExchangesRestAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.Decimal;

namespace CryptoArbitrageBot.ExchangeClients.Clients;

public sealed class BinanceClient : IExchangeClient
{
    private readonly BaseRestApi<BinanceRequest> _api;
    
    public BinanceClient(ExchangeApiOptions options)
    {
        _api = new BaseRestApi<BinanceRequest>(options);
    }
    
    public CurrencyPair GetCurrencyInfo(string currency)
    {
        var query = GetSymbolQuery(currency);
        var response = _api
            .CreateRequest(Method.Get, BinanceEndpoint.CurrencyInfo, query)
            .Execute()
            .FromJson<JToken>();
        
        return new CurrencyPair
        {
            Currency = currency,
            SellingPrice = Parse(response["lastPrice"].ToString(), CultureInfo.InvariantCulture),
            TradingVolume = Parse(response["volume"].ToString(), CultureInfo.InvariantCulture)
        };
    }

    public decimal GetCurrencyPrice(string currency)
    {
        var query = GetSymbolQuery(currency);
        var response = _api
            .CreateRequest(Method.Get, BinanceEndpoint.CurrentPrice, query)
            .Execute()
            .FromJson<JToken>()["price"];
        
        if(response == null)
            throw new NullReferenceException("Binance.GetCurrencyPrice response is null");
        
        var price = Parse(response.ToString(), CultureInfo.InvariantCulture);
        
        return price;
    }

    public IEnumerable<CurrencyBalance> GetAccountBalance()
    {
        var balances = new List<CurrencyBalance>();
        var timestamp = TimestampHelper.GetUtcTimestamp();
        var query = $"?timestamp={timestamp}&omitZeroBalances=true";
        var response = _api
            .CreateRequest(Method.Get, BinanceEndpoint.AccountInfo, query)
            .Authorize()
            .Execute()
            .FromJson<JToken>();
        
        var accountBalances = response["balances"];
        if(accountBalances == null)
            return balances;
        
        balances.AddRange(from coin in accountBalances 
            let currency = coin["asset"].ToString() 
            let freeBalance = Parse(coin["free"].ToString(), CultureInfo.InvariantCulture) 
            let lockedBalance = Parse(coin["locked"].ToString(), CultureInfo.InvariantCulture) 
            select new CurrencyBalance { Currency = currency, AvailableBalance = freeBalance, LockedBalance = lockedBalance });
        
        return balances;
    }

    public CurrencyBalance GetCurrencyBalance(string currency)
    {
        var balances = GetAccountBalance();
        foreach (var item in balances)
        {
            if(item.Currency == currency)
                return item;
        }
        
        return new CurrencyBalance { Currency = currency, AvailableBalance = 0 };
    }

    public bool CreateSellOrder(string currency, decimal amount, decimal price)
    {
        var strAmount = amount.ToString(CultureInfo.InvariantCulture);
        var strPrice = price.ToString(CultureInfo.InvariantCulture);
        var query = $"?timestamp={GetTimestamp()}&symbol={currency}&quantity={strAmount}&price={strPrice}&side=SELL&type=LIMIT&timeInForce=GTC";
        
        return CreateSellOrder(query);
    }

    public bool CreateSellOrder(string currency, decimal amount)
    {
        var strAmount = amount.ToString(CultureInfo.InvariantCulture);
        var query = $"?timestamp={GetTimestamp()}&symbol={currency}&quantity={strAmount}&side=SELL&type=MARKET";
        
        return CreateSellOrder(query);
    }

    public bool CancelAllOrders()
    {
        var myOrders = GetMyOrders();
        foreach (var order in myOrders)
        {
            var query = $"{GetSymbolQuery(order.Currency)}&orderId={order.OrderId}";
            var orderStatus = _api
                .CreateRequest(Method.Delete, BinanceEndpoint.Order,query)
                .Authorize()
                .Execute()
                .FromJson<JToken>()["status"]
                ?.ToString();
            if(orderStatus == "CANCELED")
                continue;
            
            return false;
        }
        
        return true;
    }

    public bool WithdrawalCurrency(string coin, decimal amount, string address)
    {
        var timestamp = TimestampHelper.GetUtcTimestamp();
        var query = $"?timestamp={timestamp}&coin={coin}&address={address}&amount={amount}";
        
        var withdrawalResponse = _api
            .CreateRequest(Method.Post, BinanceEndpoint.Withdraw, query)
            .Authorize()
            .Execute()
            .FromJson<JToken>();
        
        var id = withdrawalResponse["id"];
        var isSuccessful = TryParse(id.ToString(), CultureInfo.InvariantCulture, out _);
        
        return isSuccessful;
    }

    public IEnumerable<Order> GetMyOrders()
    {
        var query = $"?timestamp={GetTimestamp()}";
        var result = new List<Order>();
        var orders = _api
            .CreateRequest(Method.Get, BinanceEndpoint.OrderList, query)
            .Authorize()
            .Execute()
            .FromJson<JToken>()["orders"];
        
        if(orders == null)
            return result;
        
        foreach (var order in orders)
        {
            result.Add(new Order
            {
                Currency = order["symbol"].ToString(),
                OrderId = Parse(order["orderId"].ToString(), CultureInfo.InvariantCulture),
                ClientOrderId = Parse(order["clientOrderId"].ToString(), CultureInfo.InvariantCulture),
            });
        }
        
        return result;
    }

    public IEnumerable<WithdrawalInfo> GetWithdrawalInfo()
    {
        var query = $"?timestamp={GetTimestamp()}";
        var result = new List<WithdrawalInfo>();
        
        var infoArray = _api
            .CreateRequest(Method.Get, BinanceEndpoint.AllCoinsInformation, query)
            .Authorize()
            .Execute()
            .FromJson<JToken>();
        
        foreach (var coinItem in infoArray)
        {
            var networkList = coinItem["networkList"].ToString();
            var items = JsonConvert.DeserializeObject<List<WithdrawalInfo>>(networkList) ?? [];
            result.AddRange(items);
        }
        
        return result;
    }

    private string GetTimestamp() => TimestampHelper.GetUtcTimestamp();
    
    private static string GetSymbolQuery(string currency)
    {
        return $"?symbol={currency.ToUpper()}";
    }

    private bool CreateSellOrder(string query)
    {
        var id = _api
            .CreateRequest(Method.Post, BinanceEndpoint.Order, query)
            .Authorize()
            .Execute()
            .FromJson<JToken>()["orderId"];

        return TryParse(id.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out _);
    }
}