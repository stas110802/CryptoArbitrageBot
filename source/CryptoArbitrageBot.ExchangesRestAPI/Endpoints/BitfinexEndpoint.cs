using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.ExchangesRestAPI.Endpoints;

public sealed class BitfinexEndpoint : BaseType
{
    private BitfinexEndpoint(string value) : base(value) { }

    public static readonly BitfinexEndpoint Ticker = new("/v2/ticker");

    public static readonly BitfinexEndpoint Balance = new("/v1/balances");
    public static readonly BitfinexEndpoint AccountFees = new("/v1/account_fees");
    public static readonly BitfinexEndpoint CancelAllOrders = new("/v1/order/cancel/all");
    public static readonly BitfinexEndpoint AllActiveOrders = new("/v1/orders");
    public static readonly BitfinexEndpoint NewOrder = new("/v1/order/new");
}