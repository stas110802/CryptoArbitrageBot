using System.Globalization;
using CryptoArbitrageBot.Bot.Core;
using CryptoArbitrageBot.Bot.Models;
using CryptoArbitrageBot.CI.Attributes;
using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.CI.Commands;

public class BotFunctionsCommands : VoidCommandsObject
{
    private readonly CryptoBot _bot;
    private readonly ClientSelectCommands _clientSelectCommands;
    public BotFunctionsCommands()
    {
        _bot = new CryptoBot();
        _bot.SetSettingsFromConfig();
        _clientSelectCommands = new ClientSelectCommands();
    }

    public override void PrintCommands()
    {
        Console.Clear();
        ConsoleHelper.Write("[1]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - создать арбитражного бота (базовый алгоритм)", ConsoleColor.Gray);

        ConsoleHelper.Write("[2]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - создать Stop Loss и Take Profit отметку", ConsoleColor.Gray);
        
        ConsoleHelper.Write("[3]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - доступный функционал api биржи", ConsoleColor.Gray);

        ConsoleHelper.Write("[Q]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - назад", ConsoleColor.Gray);
    }

    [ConsoleCommand(ConsoleKey.D1)]
    public void CreateArbitrageBot()
    {
    }

    [ConsoleCommand(ConsoleKey.D2)]
    public void CreateStopLossTakProfit()
    {
        _clientSelectCommands.PrintCommands();
        var client = _clientSelectCommands.ReadFuncCommandKey();
        
        Console.Clear();
        ConsoleHelper.Write("какую монету продаем: ", ConsoleColor.Gray);
        var firstCoin = Console.ReadLine();
        

        Console.Write("какую монету покупаем: ");
        var secondCoin = Console.ReadLine();

        Console.Write($"верхняя цена (указываем в{secondCoin})): ");
        var isUpperPriceValid = decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var upperPrice);
        if (isUpperPriceValid is false)
        {
            PrintWrongNumberFormat();
            return;
        }

        

        Console.Write($"нижняя цена (указываем в {secondCoin})): ");
        var isBottomPriceValid = decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var bottomPrice);
        
        if (isBottomPriceValid is false)
        {
            PrintWrongNumberFormat();
            return;
        }
        
        Console.Write($"минимальный баланс на аккаунте, чтобы бот начал работу (указываем в {firstCoin})): ");
        var isMinAccBalanceValid = decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var minBalance);
        
        if (isMinAccBalanceValid is false)
        {
            PrintWrongNumberFormat();
            return;
        }
        
        var info = new SLTPInfo
        {
            FirstCoin = firstCoin.ToUpper(),
            SecondCoin = secondCoin.ToUpper(),
            UpperPrice = upperPrice,
            BottomPrice = bottomPrice,
            BalanceLimit = minBalance,
            Client = client
        };
        _bot.CreateSltpBot(info);
        _bot.RunSltpBot();
    }

    private void PrintWrongNumberFormat()
    {
        Console.WriteLine("Неверный формат числа");
        Thread.Sleep(1500);
    }
}