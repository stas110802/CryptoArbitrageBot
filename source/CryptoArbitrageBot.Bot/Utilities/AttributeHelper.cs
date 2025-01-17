using CryptoArbitrageBot.Bot.Models.Configs;

namespace CryptoArbitrageBot.Bot.Utilities;

public static class AttributeHelper
{
    public static T? GetValueOf<T>(Exchanges exchanges, ExchangeType description)
    {

        var props = typeof(Exchanges).GetProperties().ToArray();
        foreach (var prop in props)
        {
            var atts = prop.GetCustomAttributes(false);
            foreach (var att in atts)
            {
                if (att is ExchangeAttribute)
                {
                    var value = (att as ExchangeAttribute).Value;
                    if (description.Equals(value))
                    {
                        return (T)prop.GetValue(exchanges, null);
                    }
                }
            }
        }

        return default;
    }
}