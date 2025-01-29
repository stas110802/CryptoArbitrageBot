using System.ComponentModel.DataAnnotations;
using CryptoArbitrageBot.Bot;
using CryptoArbitrageBot.Bot.Models.Configs;
using CryptoArbitrageBot.CI.Attributes;
using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.CI.Commands;

public class ConfigSettingsCommands : VoidCommandsObject
{
    private readonly ConfigManager _configManager;
    private readonly Exchanges _exchanges;

    public ConfigSettingsCommands()
    {
        _configManager = new ConfigManager();
        _exchanges = new Exchanges();
    }

    public override void PrintCommands()
    {
        Console.Clear();
        ConsoleHelper.Write("[1]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - установить smtp-настройки", ConsoleColor.Gray);

        ConsoleHelper.Write("[2]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - указать почту для уведомлений", ConsoleColor.Gray);

        ConsoleHelper.Write("[3]", ConsoleColor.Red);
        ConsoleHelper.Write(" - указать api-ключи для ", ConsoleColor.Gray);
        ConsoleHelper.WriteLine("Binance", ConsoleColor.Green);

        ConsoleHelper.Write("[4]", ConsoleColor.Red);
        ConsoleHelper.Write(" - указать api-ключи для ", ConsoleColor.Gray);
        ConsoleHelper.WriteLine("Bitfinex", ConsoleColor.Green);
        
        ConsoleHelper.Write("[5]", ConsoleColor.Red);
        ConsoleHelper.Write(" - указать api-ключи для ", ConsoleColor.Gray);
        ConsoleHelper.WriteLine("Binance-TestNet", ConsoleColor.Yellow);

        ConsoleHelper.Write("[Q]", ConsoleColor.Red);
        ConsoleHelper.WriteLine(" - назад", ConsoleColor.Gray);
    }

    [ConsoleCommand(ConsoleKey.D1)]
    public void SetSmtpSettings()
    {
        Console.Clear();

        ConsoleHelper.Write("Укажите почту: ", ConsoleColor.Gray);
        var email = Console.ReadLine();
        var emailValidator = new EmailAddressAttribute();
        var isValidEmail = emailValidator.IsValid(email);
        if (isValidEmail == false)
        {
            PrintErrorMessage("Ошибка! Неверный формат почты!");
            return;
        }

        Console.Write("Укажите порт: ");
        var isValidPort = int.TryParse(Console.ReadLine(), out int port);
        if (isValidPort == false)
        {
            PrintErrorMessage("Ошибка! Неверный формат почты!");
            return;
        }

        Console.Write("Укажите хост: ");
        var host = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(host))
        {
            PrintErrorMessage("Ошибка! Неверный формат хоста!");
            return;
        }


        Console.Write("Укажите пароль: ");
        var password = ConsoleHelper.ReadSecretString();
        Console.WriteLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            PrintErrorMessage("Ошибка! Неверный формат хоста!");
            return;
        }

        var smtp = new SmtpSettings
        {
            Host = host,
            Port = port,
            Password = password,
            Login = email!
        };

        _configManager.SetSmtpSettings(smtp);
        PrintDataSuccessfulUpdate();
    }

    [ConsoleCommand(ConsoleKey.D2)]
    public void SetEmail()
    {
        Console.Clear();

        ConsoleHelper.Write("Укажите почту для уведомлений: ", ConsoleColor.Gray);
        var email = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email))
        {
            PrintErrorMessage("Ошибка, указана пустая строка.");
            return;
        }

        var isValidEmail = _configManager.SetEmailAddress(email);
        if (isValidEmail is false)
        {
            PrintErrorMessage("Ошибка, неверный формат почты.");
            return;
        }

        PrintDataSuccessfulUpdate();
    }

    [ConsoleCommand(ConsoleKey.D3)]
    public void SetBinanceApiKey()
    {
        SetApiKeys(ExchangeType.Binance);
    }
    
    [ConsoleCommand(ConsoleKey.D4)]
    public void SetBitfinexApiKey()
    {
        SetApiKeys(ExchangeType.Bitfinex);
    }

    [ConsoleCommand(ConsoleKey.D5)]
    public void SetBinanceTestnetApiKey()
    {
        SetApiKeys(ExchangeType.BinanceTestnet);
    }

    private void SetApiKeys(ExchangeType type)
    {
        var apiKeys = GetApiKeys();
        _configManager.SetExchangeApiKeys(type, apiKeys.Item1, apiKeys.Item2);
        PrintDataSuccessfulUpdate();
    }
    
    private void PrintErrorMessage(string errorMessage)
    {
        Console.Clear();
        Console.WriteLine(errorMessage);
        Thread.Sleep(2000);
    }

    public static void PrintDataSuccessfulUpdate()
    {
        ConsoleHelper.Write("Данные успешно обновлены!", ConsoleColor.Green);
        Thread.Sleep(1500);
        Console.Clear();
    }

    private (string, string) GetApiKeys()
    {
        Console.Clear();
        Console.Write("Укажите публичный api-ключ: ");
        var key = ConsoleHelper.ReadSecretString();
        Console.WriteLine();
        Console.Write("Укажите секретный api-ключ:");
        var secret = ConsoleHelper.ReadSecretString();
        Console.WriteLine();

        if (string.IsNullOrWhiteSpace(key)) key = string.Empty;
        if (string.IsNullOrWhiteSpace(secret)) secret = string.Empty;

        return (key, secret);
    }
}