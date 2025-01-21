using CryptoArbitrageBot.Bot.Models.Configs;
using CryptoArbitrageBot.Bot.Utilities;
using static NUnit.Framework.Assert;

namespace CryptoArbitrageBot.Bot.UnitTest;

public class ConfigManagerTest
{
    private ConfigManager _manager;
    private BotConfig _oldBotConfigCopy;

    [SetUp]
    public void Setup()
    {
        _manager = new ConfigManager();
        _oldBotConfigCopy = _manager.GetCopyBotConfig();
    }

    [Test]
    public void GlobalTest()
    {
        var tests = new List<bool>
        {
            AssertSetExchangeApiKeys(),
            AssertSetEmailAddress(),
            AssertSetSmtpSettings()
        };

        _manager.SetFullConfig(_oldBotConfigCopy);

        tests.ForEach(IsTrue);
    }

    private bool AssertSetExchangeApiKeys()
    {
        _manager.SetExchangeApiKeys(ExchangeType.BinanceTestnet, "testnet-key", "testnet-key-secret");
        _manager.SetExchangeApiKeys(ExchangeType.Binance, "binance-key", "binance-key-secret");

        var binanceTestnetCfg = _manager.GetExchangeConfig(ExchangeType.BinanceTestnet);
        var binanceCfg = _manager.GetExchangeConfig(ExchangeType.Binance);

        return !StringHelper.IsAnyNullOrEmpty(binanceTestnetCfg)
               && !StringHelper.IsAnyNullOrEmpty(binanceCfg);
    }

    private bool AssertSetEmailAddress()
    {
        var testAddresses = new[]
        {
            "user-name@mail.ru", "test@test.com",
            "icansaveyou@google.com", "buanov@bruh.pizdec"
        };

        foreach (var totalAddress in testAddresses)
        {
            _manager.SetEmailAddress(totalAddress);
            var configEmail = _manager.GetEmailAddress();
            if (!string.IsNullOrEmpty(configEmail)
                && configEmail == totalAddress)
                continue;
            return false;
        }

        return true;
    }

    private bool AssertSetSmtpSettings()
    {
        var smtp = new SmtpSettings
        {
            Host = "smtp.gmail.com",
            Login = "test@test.com",
            Password = "******",
            Port = 5834
        };
        _manager.SetSmtpSettings(smtp);
        var configSmtp = _manager.GetSmtpSettings();

        return configSmtp == smtp;
    }
}