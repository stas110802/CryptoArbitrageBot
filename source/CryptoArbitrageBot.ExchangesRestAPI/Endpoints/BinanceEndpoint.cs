namespace CryptoArbitrageBot.ExchangesRestAPI.Endpoints;

public sealed class BinanceEndpoint : BaseEndpoint
{
    public static readonly BinanceEndpoint AccountInfo = new("/api/v3/account");
    public static readonly BinanceEndpoint CurrentPrice = new ("/api/v3/ticker/price");
    private BinanceEndpoint(string value) : base(value) { }
}