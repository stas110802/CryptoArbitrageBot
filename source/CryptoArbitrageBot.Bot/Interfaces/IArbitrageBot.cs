using CryptoArbitrageBot.Bot.Models;

namespace CryptoArbitrageBot.Bot.Interfaces;

public interface IArbitrageBot
{
    public ILog StartArbitrage(ArbitrageInfo info);
    public Task<ILog> StartArbitrageAsync(ArbitrageInfo info);
}