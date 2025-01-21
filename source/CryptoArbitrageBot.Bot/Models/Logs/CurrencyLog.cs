using System.Runtime.Serialization;
using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Types;
using CryptoArbitrageBot.Bot.Utilities;

namespace CryptoArbitrageBot.Bot.Models.Logs;

public sealed class CurrencyLog : ILog
{
    public CurrencyLog() { }

    public CurrencyLog(SLTPInfo options, decimal totalPrice, decimal availableBalance)
    {
        ParsingDate = DateTime.UtcNow;
        Info = options;
        TotalPrice = totalPrice;
        AvailableBalance = availableBalance;
        Type = SubjectType.SellActivate;
    }
    
    [DataMember]
    public SLTPInfo Info { get; set; }
    
    [DataMember]
    public decimal AvailableBalance { get; set; }
    
    [DataMember]
    public decimal TotalPrice { get; set; }

    [DataMember]
    public DateTime ParsingDate { get; set; }

    public string FilePath => $"{FolderPathList.LaunchesFolder}{DateTime.Now:dd.MM.yyyy}.json";
    
    public SubjectType Type { get; init; }

    public override string ToString()
    {
        return $"Продаем: {Info.FirstCoin} за {Info.SecondCoin}\n" +
               $"Дата: {ParsingDate}\n" +
               $"Рекомендуемая цена: {Info.UpperPrice} {Info.SecondCoin}\n" +
               $"Критическая цена: {Info.BottomPrice} {Info.SecondCoin}\n" +
               $"Текущий курс: {TotalPrice} {Info.SecondCoin}\n" +
               $"Лимит баланса: {Info.BalanceLimit} {Info.FirstCoin}\n" +
               $"Баланс: {AvailableBalance} {Info.FirstCoin}";
    }
}