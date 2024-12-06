namespace CryptoArbitrageBot.ExchangesRestAPI.Endpoints;

public sealed class BinanceEndpoint : BaseEndpoint
{
    public static readonly BinanceEndpoint AccountInfo = new("/api/v3/account");
    
    private BinanceEndpoint(string value) : base(value) { }
}