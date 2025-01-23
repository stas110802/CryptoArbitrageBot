using CryptoArbitrageBot.Bot.Types;
using Newtonsoft.Json;

namespace CryptoArbitrageBot.Bot.Interfaces;

public interface ILog 
{
    [JsonIgnore]
    public string FilePath { get; }
    [JsonIgnore]
    public SubjectType? Type { get; init; }
}