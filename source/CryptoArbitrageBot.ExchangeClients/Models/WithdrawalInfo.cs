using Newtonsoft.Json;

namespace CryptoArbitrageBot.ExchangeClients.Models;

public sealed class WithdrawalInfo
{
    [JsonProperty("coin")]
    public string Coin { get; set; }
    
    [JsonProperty("withdrawEnable")]
    public bool WithdrawEnable { get; set; }
    
    [JsonProperty("withdrawFee")]
    public decimal WithdrawFee { get; set; }
    
    [JsonProperty("withdrawMin")]
    public decimal WithdrawMin { get; set; }
    
    [JsonProperty("busy")]
    public bool IsBusy { get; set; }
    
    [JsonProperty("contractAddress")]
    public string Address { get; set; }
}