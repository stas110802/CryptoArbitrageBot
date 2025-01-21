using CryptoArbitrageBot.Bot.Types;
using CryptoArbitrageBot.ExchangeClients;

namespace CryptoArbitrageBot.Bot.Models;

public class ArbitrageInfo
{
    public ArbitrageInfo(CandleType arbitrageCandleType = CandleType.FifteenMin)
    {
        Type = arbitrageCandleType;
    }
    
    public string FirstCoin { get; set; }
    public string SecondCoin { get; set; }
    public decimal Amount { get; set; }
    public CandleType Type { get; set; }
    public IExchangeClient FirstClient { get; set; }
    public IExchangeClient SecondClient { get; set; }

    public string FirstExchangeName => nameof(FirstClient);
    public string SecondExchangeName => nameof(SecondClient);
}