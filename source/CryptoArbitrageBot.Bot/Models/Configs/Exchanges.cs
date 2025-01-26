using CryptoArbitrageBot.ExchangesRestAPI.Options;
using Newtonsoft.Json;

namespace CryptoArbitrageBot.Bot.Models.Configs;

public sealed class Exchanges
{
    [HasValue(ExchangeType.BinanceTestnet)]
    [JsonProperty("binance-testnet")]
    public ExchangeApiOptions? BinanceTestnetConfig { get; set; } = new()
    {
        BaseUri = "https://testnet.binance.vision"
    };

    [HasValue(ExchangeType.Binance)]
    [JsonProperty("binance")]
    public ExchangeApiOptions? BinanceConfig { get; set; } = new()
    {
        BaseUri = "https://api.binance.com"
    };
    
    [HasValue(ExchangeType.Bitfinex)]
    [JsonProperty("bitfinex")]
    public ExchangeApiOptions? BitfinexConfig { get; set; } = new()
    {
        BaseUri = "https://api.bitfinex.com"
    };
}