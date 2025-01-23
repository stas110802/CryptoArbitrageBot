using CryptoArbitrageBot.CI.Attributes;
using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.CI.Commands;

public class BotSettingsCommands : VoidCommandsObject
{
    private ConfigSettingsCommands _configSettingsCommands;

    public BotSettingsCommands()
    {
        _configSettingsCommands = new ConfigSettingsCommands();
    }
    
    public override void PrintCommands()
    {
        Console.Clear();
        ConsoleHelper.Write("[1]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - настроить конфиг", ConsoleColor.Gray);

        ConsoleHelper.Write("[2]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - скрывать консоль при работе бота", ConsoleColor.Gray);

        ConsoleHelper.Write("[3]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - стилистика приложения", ConsoleColor.Gray);

        ConsoleHelper.Write("[Q]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - назад", ConsoleColor.Gray);
    }
    
    [ConsoleCommand(ConsoleKey.D1)]
    public void BotSettings()
    {
        _configSettingsCommands.PrintCommands();
        _configSettingsCommands.ReadActionCommandKey();
    }
}