using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.ExchangesRestAPI.Endpoints;

public sealed class BinanceEndpoint : BaseType
{
    private BinanceEndpoint(string value) : base(value) { }
    
    public static readonly BinanceEndpoint AccountInfo = new("/api/v3/account");
    public static readonly BinanceEndpoint CurrentPrice = new ("/api/v3/ticker/price");
    public static readonly BinanceEndpoint ServerTime = new ("/api/v3/time");
    public static readonly BinanceEndpoint CurrencyInfo = new ("/api/v3/ticker/24hr");
    public static readonly BinanceEndpoint OrderList = new ("/api/v3/orderList");
    public static readonly BinanceEndpoint Order= new ("/api/v3/order");
    
    public static readonly BinanceEndpoint Withdraw= new ("/sapi/v1/capital/withdraw/apply");
    public static readonly BinanceEndpoint AllCoinsInformation = new ("/sapi/v1/capital/config/getall");
}