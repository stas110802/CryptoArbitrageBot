using System.Runtime.Serialization;
using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Types;
using CryptoArbitrageBot.Bot.Utilities;

namespace CryptoArbitrageBot.Bot.Models.Logs;

public class OrderLog : ILog
{
    public OrderLog() { }

    public OrderLog(SLTPInfo options, decimal sellPrice, decimal amount)
    {
        OrderDate = DateTime.UtcNow;
        Info = options;
        SellPrice = sellPrice;
        Amount = amount;
        Type = SubjectType.Sell;
    }

    [DataMember] 
    public decimal SellPrice { get; set; }

    [DataMember] 
    public decimal Amount { get; set; }

    [DataMember] 
    public SLTPInfo Info { get; set; }

    [DataMember] 
    public DateTime OrderDate { get; set; }
    
    public string FilePath => $"{FolderPathList.OrdersFolder}{DateTime.Now:dd.MM.yyyy}.json";
    public SubjectType Type { get; init; }

    public override string ToString()
    {
        return $"Продаем: {Info.FirstCoin} за {Info.SecondCoin}\n" +
               $"Дата: {OrderDate}\n" +
               $"Рекомендуемая цена: {Info.UpperPrice} {Info.SecondCoin}\n" +
               $"Критическая цена: {Info.BottomPrice} {Info.SecondCoin}\n" +
               $"Цена продажи: {SellPrice} {Info.SecondCoin}\n" +
               $"Количество: {Amount} {Info.FirstCoin}";
    }
}