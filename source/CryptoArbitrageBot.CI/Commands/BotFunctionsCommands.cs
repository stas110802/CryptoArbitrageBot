using System.Globalization;
using CryptoArbitrageBot.Bot.Core;
using CryptoArbitrageBot.Bot.Models;
using CryptoArbitrageBot.CI.Attributes;
using CryptoArbitrageBot.ExchangeClients;
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
        Console.Clear();
        ConsoleHelper.Write("какую монету продаем: ", ConsoleColor.Gray);
        var firstCoin = Console.ReadLine();

        Console.Write("какую монету покупаем: ");
        var secondCoin = Console.ReadLine();

        Console.Write($"количество (указываем в {firstCoin})): ");
        var isAmountValid =
            decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var amount);

        if (isAmountValid is false)
        {
            PrintWrongNumberFormat();
            return;
        }

        Console.Clear();
        _clientSelectCommands.PrintCustomTextCommands("Первая биржа: ");
        var firstClient = _clientSelectCommands.ReadFuncCommandKey();

        Console.Clear();
        ConsoleHelper.Write($"адрес монеты {firstCoin} на первой бирже ({firstClient.GetType().Name}): ",
            ConsoleColor.Gray);
        var firstClientAddress = Console.ReadLine();

        _clientSelectCommands.PrintCustomTextCommands("Вторая биржа: ");
        var secondClient = _clientSelectCommands.ReadFuncCommandKey();

        Console.Clear();
        ConsoleHelper.Write($"адрес монеты {firstCoin} на второй бирже ({secondClient.GetType().Name}): ",
            ConsoleColor.Gray);
        var secondClientAddress = Console.ReadLine();

        if (firstClient?.GetType().Name == secondClient?.GetType().Name)
        {
            Console.Clear();
            Console.WriteLine("Нельзя выбрать 2 одинаковые биржи!");
            Thread.Sleep(2000);
            Console.Clear();

            return;
        }

        var info = new ArbitrageInfo
        {
            Amount = amount,
            FirstCoin = firstCoin,
            SecondCoin = secondCoin,
            FirstClient = firstClient,
            SecondClient = secondClient,
            FirstClientAddress = firstClientAddress,
            SecondClientAddress = secondClientAddress,
        };
        _bot.CreateArbitrageBot();
        _bot.RunArbitrageBot(info);
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
        var isBottomPriceValid =
            decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var bottomPrice);

        if (isBottomPriceValid is false)
        {
            PrintWrongNumberFormat();
            return;
        }

        Console.Write($"минимальный баланс на аккаунте, чтобы бот начал работу (указываем в {firstCoin})): ");
        var isMinAccBalanceValid =
            decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var minBalance);

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
        RunSltpBot(info);
    }

    private void PrintWrongNumberFormat()
    {
        Console.WriteLine("Неверный формат числа");
        Thread.Sleep(1500);
    }

    private void RunSltpBot(SLTPInfo info)
    {
        _bot.CreateSltpBot(info);
        var result = _bot.RunSltpBot();
        Console.WriteLine(result);
        Console.ReadKey();
    }
}