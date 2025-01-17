namespace CryptoArbitrageBot.Bot;

public class ExchangeAttribute() : Attribute
{
    public ExchangeAttribute(ExchangeType value) : this()
    {
        Value = value;
    }
    
    public ExchangeType Value { get; set; } 
}
