using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.ExchangesRestAPI.Endpoints;

public sealed class BitfinexEndpoint : BaseType
{
    private BitfinexEndpoint(string value) : base(value) { }
    
    public static readonly BitfinexEndpoint Ticker = new("/v2/ticker");
    public static readonly BitfinexEndpoint Balance = new("/v2/auth/calc/order/avail");
    public static readonly BitfinexEndpoint Wallets = new("/v2/auth/r/wallets");
    
    public static readonly BitfinexEndpoint BalanceV1 = new("/v1/balances");
    public static readonly BitfinexEndpoint AccountFeesV1 = new("/v1/account_fees");
}