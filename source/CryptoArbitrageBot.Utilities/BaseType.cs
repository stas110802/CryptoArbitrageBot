namespace CryptoArbitrageBot.Utilities;

public abstract class BaseType(string value)
{
    public string Value { get; init; } = value;
}