using CryptoArbitrageBot.Bot.Types;

namespace CryptoArbitrageBot.Bot.Interfaces;

public interface ILog 
{
    public string FilePath { get; }
    public SubjectType? Type { get; init; }
}