namespace CryptoArbitrageBot.ExchangeClients.Models;

public sealed class CurrencyBalance
{
    public string Currency { get; set; } = string.Empty;
    public decimal AvailableBalance { get; set; }
    public decimal LockedBalance { get; set; }
    public decimal TotalBalance => AvailableBalance + LockedBalance;
}