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
        PrintAllExchanges();
    }

    public void PrintCustomTextCommands(string text)
    {
        Console.Clear();
        Console.WriteLine(text);
        PrintAllExchanges();
    }
    
    [ConsoleCommand(ConsoleKey.D1)]
    public BinanceClient GetBinanceClient()
    {
        var options = _configManager.GetExchangeConfig(ExchangeType.Binance);
        return new BinanceClient(options);
    }
    
    [ConsoleCommand(ConsoleKey.D2)]
    public BitfinexClient GetBitfinexClient()
    {
        var options = _configManager.GetExchangeConfig(ExchangeType.Bitfinex);
        return new BitfinexClient(options);
    }
    
    [ConsoleCommand(ConsoleKey.D3)]
    public BinanceClient GetBinanceTestnetClient()
    {
        var options = _configManager.GetExchangeConfig(ExchangeType.BinanceTestnet);
        return new BinanceClient(options);
    }
    
    private static void PrintAllExchanges()
    {
        ConsoleHelper.Write("[1]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - Binance", ConsoleColor.Gray);
        
        ConsoleHelper.Write("[2]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - Bitfinex", ConsoleColor.Gray);
        
        ConsoleHelper.Write("[3]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - Binance (TESTNET)", ConsoleColor.Yellow);
    }
}