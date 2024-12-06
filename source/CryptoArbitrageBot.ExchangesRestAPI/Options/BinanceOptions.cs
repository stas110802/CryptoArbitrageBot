namespace CryptoArbitrageBot.ExchangesRestAPI.Options;

public class BinanceOptions : IApiOptions
{
    public string BaseUri { get; set; } = "https://api.binance.com";
    public string? PublicKey { get; set; } = string.Empty;
    public string? SecretKey { get; set; } = string.Empty;
}