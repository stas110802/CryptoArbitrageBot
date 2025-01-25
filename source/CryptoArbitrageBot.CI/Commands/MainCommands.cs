using CryptoArbitrageBot.CI.Attributes;
using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.CI.Commands;

public class MainCommands : VoidCommandsObject
{
    private readonly BotSettingsCommands _botSettingsCommands;
    private readonly BotFunctionsCommands _botFunctionsCommands;

    public MainCommands()
    {
        _botSettingsCommands = new BotSettingsCommands();
        _botFunctionsCommands = new BotFunctionsCommands();
    }

    public override void PrintCommands()
    {
        Console.Clear();
        ConsoleHelper.Write("[1]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - функции бота", ConsoleColor.Gray);

        ConsoleHelper.Write("[2]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - настройки", ConsoleColor.Gray);

        ConsoleHelper.Write("[3]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - информация о боте", ConsoleColor.Gray);

        ConsoleHelper.Write("[Q]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - выход", ConsoleColor.Gray);
    }

    [ConsoleCommand(ConsoleKey.D1)]
    public void BotFunctions()
    {
        _botFunctionsCommands.PrintCommands();
        _botFunctionsCommands.ReadActionCommandKey();
    }

    [ConsoleCommand(ConsoleKey.D2)]
    public void BotSettings()
    {
        _botSettingsCommands.PrintCommands();
        _botSettingsCommands.ReadActionCommandKey();
    }

    [ConsoleCommand(ConsoleKey.D3)]
    public void PrintInfoAboutBot()
    {
        Console.Clear();
        ConsoleHelper.WriteLine("Небольшой крипто-бот. Умеет заниматся арбитражней торговлей,\n" +
                                "так же имеет функцию Стоп-Лосс и Тейк-Профит алгоритма.", ConsoleColor.Gray);
        Console.ReadKey();
        Console.Clear();
    }
}