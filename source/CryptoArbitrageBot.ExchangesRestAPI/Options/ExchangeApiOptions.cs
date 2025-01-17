using Newtonsoft.Json;

namespace CryptoArbitrageBot.ExchangesRestAPI.Options;

public class ExchangeApiOptions
{
    [JsonIgnore]
    public string BaseUri { get; set; } = string.Empty;

    [JsonProperty("key")] 
    public string PublicKey { get; set; } = string.Empty;
    
    [JsonProperty("secret-key")]
    public string SecretKey { get; set; } = string.Empty;
}