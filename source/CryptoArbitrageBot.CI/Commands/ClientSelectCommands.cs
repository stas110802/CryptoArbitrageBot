using CryptoArbitrageBot.Bot;
using CryptoArbitrageBot.CI.Attributes;
using CryptoArbitrageBot.ExchangeClients;
using CryptoArbitrageBot.ExchangeClients.Clients;
using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.CI.Commands;

public class ClientSelectCommands : MultiCommandsObject<IExchangeClient>
{
    private readonly ConfigManager _configManager;

    public ClientSelectCommands()
    {
        _configManager = new ConfigManager();
        InitFuncCommands(this);
    }
    
    public override void PrintCommands()
    {
        Console.Clear();
        Console.WriteLine("Какая биржа необходима? : ");
        
        ConsoleHelper.Write("[1]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - Binance", ConsoleColor.Gray);
        
        ConsoleHelper.Write("[2]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - Binance (TESTNET)", ConsoleColor.Yellow);
    }

    [ConsoleCommand(ConsoleKey.D1)]
    public BinanceClient GetBinanceClient()
    {
        var options = _configManager.GetExchangeConfig(ExchangeType.Binance);
        return new BinanceClient(options);
    }
    
    [ConsoleCommand(ConsoleKey.D2)]
    public BinanceClient GetBinanceTestnetClient()
    {
        var options = _configManager.GetExchangeConfig(ExchangeType.BinanceTestnet);
        return new BinanceClient(options);
    }
}