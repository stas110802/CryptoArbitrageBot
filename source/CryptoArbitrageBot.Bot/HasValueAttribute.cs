namespace CryptoArbitrageBot.Bot;

public class HasValueAttribute() : Attribute
{
    public HasValueAttribute(object value) : this()
    {
        Value = value;
    }
    
    public object Value { get; set; } 
}
