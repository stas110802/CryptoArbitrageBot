namespace CryptoArbitrageBot.Bot.Interfaces;

public interface ISLTPBot
{
    public ILog StartSLTP();
    public Task<ILog> StartSLTPAsync();
}