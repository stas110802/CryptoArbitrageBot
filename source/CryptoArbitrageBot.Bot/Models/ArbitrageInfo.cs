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
    public string FirstClientAddress { get; set; }
    public string SecondClientAddress { get; set; }

    public string FirstExchangeName => FirstClient.GetType().Name;
    public string SecondExchangeName => SecondClient.GetType().Name;
    
}