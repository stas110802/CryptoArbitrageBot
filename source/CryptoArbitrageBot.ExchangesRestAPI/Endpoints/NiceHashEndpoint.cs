namespace CryptoArbitrageBot.ExchangesRestAPI.Endpoints;

public class NiceHashEndpoint : BaseEndpoint
{
    public static readonly NiceHashEndpoint ServerTime = new("/api/v2/time");
    public static readonly NiceHashEndpoint Balances = new("/main/api/v2/accounting/accounts2");
    public static readonly NiceHashEndpoint CurrentPrices = new("/exchange/api/v2/info/prices");
    public static readonly NiceHashEndpoint Order = new("/exchange/api/v2/order");
    public static readonly NiceHashEndpoint MyOrders = new("/exchange/api/v2/info/myOrders");
    public static readonly NiceHashEndpoint CancelAllOrders = new("/exchange/api/v2/info/cancelAllOrders");
    public static readonly NiceHashEndpoint Withdrawal = new("/main/api/v2/accounting/withdrawal");
    
    private NiceHashEndpoint(string value) : base(value) { }

}