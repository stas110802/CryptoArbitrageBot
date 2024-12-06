namespace CryptoArbitrageBot.ExchangesRestAPI.Endpoints;

public abstract class BaseEndpoint(string value)
{
    public string Value { get; init; } = value;
}