using CryptoArbitrageBot.Bot.Models.Configs;

namespace CryptoArbitrageBot.Bot.Utilities;

public static class AttributeHelper
{
    public static TKey? GetValueOf<TKey, TValue>(TValue exchanges, object description)
        where TValue : class
    {

        var props = typeof(Exchanges).GetProperties().ToArray();
        foreach (var prop in props)
        {
            var attributes = prop.GetCustomAttributes(false);
            foreach (var att in attributes)
            {
                if (att is HasValueAttribute attribute)
                {
                    var value = attribute.Value;
                    if (description.Equals(value))
                    {
                        return (TKey)prop.GetValue(exchanges, null);
                    }
                }
            }
        }

        return default;
    }
}