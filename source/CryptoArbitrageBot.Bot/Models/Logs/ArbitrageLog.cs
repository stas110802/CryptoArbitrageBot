using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Types;
using CryptoArbitrageBot.Bot.Utilities;

namespace CryptoArbitrageBot.Bot.Models.Logs;

public class ArbitrageLog : ILog
{
    public ArbitrageLog(ArbitrageInfo info, decimal firstCandle, decimal secondCandle)
    {
        ArbitrageInfo = info;
        Type = SubjectType.SellActivate;
        ParsingDate = DateTime.UtcNow;
        FirstTotalPrice = firstCandle;
        SecondTotalPrice = secondCandle;
    }
    
    public ArbitrageInfo ArbitrageInfo { get; set; }
    public string FilePath => $"{FolderPathList.OrdersFolder}{DateTime.Now:dd.MM.yyyy}.json";
    public SubjectType Type { get; init; }
    public DateTime ParsingDate { get; set; }
    public decimal FirstTotalPrice { get; set; }
    public decimal SecondTotalPrice { get; set; }
    
    public override string ToString()
    {
        return $"Продаем: {ArbitrageInfo.FirstCoin} за {ArbitrageInfo.SecondCoin}\n" +
               $"Дата: {ParsingDate}\n" +
               $"Текущий курс на {ArbitrageInfo.FirstExchangeName}: {FirstTotalPrice} {ArbitrageInfo.SecondCoin}\n" +
               $"Текущий курс на {ArbitrageInfo.SecondExchangeName}: {SecondTotalPrice} {ArbitrageInfo.SecondCoin}\n" +
               $"Планируем продать: {ArbitrageInfo.Amount} {ArbitrageInfo.FirstCoin}";
    }
}