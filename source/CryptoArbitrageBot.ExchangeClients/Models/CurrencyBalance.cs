namespace CryptoArbitrageBot.ExchangeClients.Models;

public sealed class CurrencyBalance
{
    public bool IsActive { get; set; }

    public string Currency { get; set; } = string.Empty;

    public decimal TotalBalance { get; set; }

    public decimal AvailableBalance { get; set; }

    public decimal Debt { get; set; }

    public decimal Pending { get; set; }
}