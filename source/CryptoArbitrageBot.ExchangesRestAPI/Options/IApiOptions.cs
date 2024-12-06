namespace CryptoArbitrageBot.ExchangesRestAPI.Options;

public interface IApiOptions
{
    public string BaseUri { get; set; }
    public string? PublicKey { get; set; }
    public string? SecretKey { get; set; }
}