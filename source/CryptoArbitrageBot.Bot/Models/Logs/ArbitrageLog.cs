using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Types;
using CryptoArbitrageBot.Bot.Utilities;

namespace CryptoArbitrageBot.Bot.Models.Logs;

public class ArbitrageLog : ILog
{
    public ArbitrageLog(ArbitrageInfo info)
    {
        ArbitrageInfo = info;
        Type = SubjectType.SellActivate;
    }
    
    public ArbitrageInfo ArbitrageInfo { get; set; }
    public string FilePath => $"{FolderPathList.OrdersFolder}{DateTime.Now:dd.MM.yyyy}.json";
    public SubjectType Type { get; init; }
}