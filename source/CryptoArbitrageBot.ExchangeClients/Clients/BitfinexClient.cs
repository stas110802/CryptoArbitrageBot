using CryptoArbitrageBot.ExchangeClients.Models;
using CryptoArbitrageBot.ExchangeClients.Utilities;
using CryptoArbitrageBot.ExchangesRestAPI.Api;
using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.String;

namespace CryptoArbitrageBot.ExchangeClients.Clients;

public sealed class BitfinexClient : IExchangeClient
{
    private readonly BaseRestApi<BitfinexRequest> _api;

    private readonly string[] WithdrawalTypes =
    [
        "bitcoin", "litecoin", "ethereum", "ethereumc", "tetheruso",
        "tetheruse", "tetherusl", "tetherusx", "tetheruss", "wire", "zcash", "monero", "dash", "ripple", "iota",
        "eos", "santiment", "omisego", "bcash", "neo", "metaverse", "qtum", "aventus", "eidoo", "bgold", "datacoin",
        "qash", "yoyow", "golem", "status", "bat", "mna", "fun", "zrx", "tnb", "spk", "trx", "rcn", "rlc", "aid",
        "sng", "rep", "elf", "nec", "ios", "aio", "req", "rdn", "lrc", "wax", "dai", "cfi", "agi", "bft", "mtn",
        "ode", "ant", "dth", "mit", "stj", "xlm", "xvg", "bci", "mkr", "ven", "knc", "poa", "lym", "utk", "vee",
        "dad", "ors", "auc", "poy", "fsn", "cbt", "zcn", "sen", "nca", "cnd", "ctx", "pai", "see", "ess", "atd",
        "add", "mto", "atm", "hot", "dta", "iqx", "wpr", "zil", "bnt", "abs", "xra", "man", "xtz"
    ];

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

        var isPriceValid = decimal.TryParse(response[6]?.ToString(), out var price);
        var isVolumeValid = decimal.TryParse(response[7]?.ToString(), out var volume);

        if (!isPriceValid || !isVolumeValid)
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
        var query = @"\t" + currency.ToUpper().Replace("USDT", "USD");
        var response = _api
            .CreateRequest(Method.Get, BitfinexEndpoint.Ticker, query)
            .Execute()
            .FromJson<JToken>();

        var result = decimal.TryParse(response[6]?.ToString(), out var price);
        if (!result)
            throw new ArgumentException($"[Bitfinex Client] Currency - ({currency}) is not valid");
        return price;
    }

    public IEnumerable<CurrencyBalance> GetAccountBalance()
    {
        var response = _api
            .CreateRequest(Method.Post, BitfinexEndpoint.Balance)
            .Authorize()
            .Execute()
            .FromJson<JToken>();

        var result = new List<CurrencyBalance>();
        foreach (var balance in response)
        {
            if (balance["type"]?.ToString() == "deposit") continue;

            var currency = balance["currency"]?.ToString();
            var isAmountValid = decimal.TryParse(balance["amount"]?.ToString(), out var amount);
            var isAvailableValid = decimal.TryParse(balance["available"]?.ToString(), out var available);
            if (!isAmountValid || !isAvailableValid || IsNullOrEmpty(currency)) continue;

            result.Add(new CurrencyBalance
            {
                Currency = currency,
                AvailableBalance = available,
                LockedBalance = amount - available
            });
        }

        return result;
    }

    public CurrencyBalance GetCurrencyBalance(string currency)
    {
        var balances = GetAccountBalance();
        foreach (var item in balances)
        {
            if (item.Currency == currency)
                return item;
        }

        return new CurrencyBalance { Currency = currency, AvailableBalance = 0 };
    }

    public bool CreateSellOrder(string currency, decimal amount, decimal price)
    {
        return CreateNewOrder(currency, amount, price);
    }

    public bool CreateSellOrder(string currency, decimal amount)
    {
        return CreateNewOrder(currency, amount, 0);
    }

    public bool CancelAllOrders()
    {
        try
        {
            var response = _api
                .CreateRequest(Method.Post, BitfinexEndpoint.CancelAllOrders)
                .Authorize()
                .Execute()
                .FromJson<JToken>()["result"];

            return response.ToString() == "All orders cancelled";
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool WithdrawalCurrency(string coin, decimal amount, string address)
    {
        var index = -1;

        for (var i = 0; i < WithdrawalTypes.Length; i++)
        {
            if (WithdrawalTypes[i].Equals(coin, StringComparison.CurrentCultureIgnoreCase))
                index = i;
        }

        if (coin == "BTC") index = 0;
        if (coin == "LTC") index = 1;
        if (coin == "ETH") index = 2;
        if (coin == "USDT") index = 5;
        if (index == -1) return false;

        var query = new Dictionary<string, object>
        {
            { "withdraw_type", WithdrawalTypes[index] },
            { "walletselected", "exchange" },
            { "amount", amount },
            { "currency", coin },
            { "address", address }
        };
        var status = _api
            .CreateRequest(Method.Post, BitfinexEndpoint.AllActiveOrders)
            .Authorize(query)
            .Execute()
            .FromJson<JToken>()["status"];

        return status?.ToString() == "success";
    }

    public IEnumerable<Order> GetMyOrders()
    {
        var result = new List<Order>();
        var orders = _api
            .CreateRequest(Method.Post, BitfinexEndpoint.AllActiveOrders)
            .Authorize()
            .Execute()
            .FromJson<JToken>();
        foreach (var order in orders)
        {
            var isValid = decimal.TryParse(order["price"]?.ToString(), out var id);
            var symbol = order["symbol"]?.ToString();
            if (isValid is false || IsNullOrEmpty(symbol)) continue;

            result.Add(new Order
            {
                Currency = symbol,
                OrderId = id
            });
        }

        return result;
    }

    private bool CreateNewOrder(string currency, decimal amount, decimal price)
    {
        var query = new Dictionary<string, object>
        {
            { "price", price }, // for market order can be random number [off api docs]
            { "side", "sell" },
            { "amount", amount },
            { "symbol", currency },
            { "type", "market" }
        };
        
        var orderId = _api
            .CreateRequest(Method.Post, BitfinexEndpoint.NewOrder)
            .Authorize(query)
            .Execute()
            .FromJson<JToken>()["id"]
            ?.ToString();
        
        return IsNullOrEmpty(orderId);
    }
}