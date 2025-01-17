using CryptoArbitrageBot.Bot.Models.Configs;
using CryptoArbitrageBot.Bot.Utilities;
using static NUnit.Framework.Assert;

namespace CryptoArbitrageBot.Bot.UnitTest;

public class ConfigInitializerTest
{
    private ConfigInitializer _initializer;
    private BotConfig _oldBotConfigCopy;
    
    [SetUp]
    public void Setup()
    {
        _initializer = new ConfigInitializer();
        _oldBotConfigCopy = _initializer.GetCopyBotConfig();
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

        _initializer.SetFullConfig(_oldBotConfigCopy);
        
        tests.ForEach(IsTrue);
    }
    
    private bool AssertSetExchangeApiKeys()
    {
        _initializer.SetExchangeApiKeys(ExchangeType.BinanceTestnet,"testnet-key", "testnet-key-secret");
        _initializer.SetExchangeApiKeys(ExchangeType.Binance,"binance-key", "binance-key-secret");

        var binanceTestnetCfg = _initializer.GetExchangeConfig(ExchangeType.BinanceTestnet);
        var binanceCfg = _initializer.GetExchangeConfig(ExchangeType.Binance);

        return !StringHelper.IsAnyNullOrEmpty(binanceTestnetCfg)
               && !StringHelper.IsAnyNullOrEmpty(binanceCfg);
    }
    
    private bool AssertSetEmailAddress()
    {
        var testAddresses = new []
        {
            "user-name@mail.ru", "test@test.com",
            "icansaveyou@google.com", "buanov@bruh.pizdec"
        };
        
        foreach (var totalAddress in testAddresses)
        {
            _initializer.SetEmailAddress(totalAddress);
            var configEmail = _initializer.GetEmailAddress();
            if(!string.IsNullOrEmpty(configEmail) 
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
        _initializer.SetSmtpSettings(smtp);
        var configSmtp = _initializer.GetSmtpConfig();
        
        return configSmtp == smtp;
    }

    
}