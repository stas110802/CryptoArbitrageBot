namespace CryptoArbitrageBot.ExchangeClients.Models;

public class Order
{
    public string Currency { get; set; }
    public decimal OrderId { get; set; }
    public decimal ClientOrderId { get; set; }
}