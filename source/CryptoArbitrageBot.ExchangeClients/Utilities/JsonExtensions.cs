using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoArbitrageBot.ExchangeClients.Utilities;

public static class JsonExtensions
{
    public static T? FromJson<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}