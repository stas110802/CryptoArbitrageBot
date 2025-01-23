using CryptoArbitrageBot.ExchangeClients;
using Newtonsoft.Json;

namespace CryptoArbitrageBot.Bot.Models;

public class SLTPInfo
{
    public string FirstCoin { get; set; }
    public string SecondCoin { get; set; }
    public decimal UpperPrice { get; set; }
    public decimal BottomPrice { get; set; }
    public decimal BalanceLimit { get; set; }
    
    [JsonIgnore]
    public IExchangeClient Client { get; set; }
}