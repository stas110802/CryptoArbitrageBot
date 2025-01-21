using CryptoArbitrageBot.Bot.Loggers;
using CryptoArbitrageBot.Bot.Models;
using CryptoArbitrageBot.Bot.Models.Logs;
using CryptoArbitrageBot.Bot.Types;

namespace CryptoArbitrageBot.Bot.Core;

public class CryptoBot
{
    private ArbitrageBot _arbitrageBot;
    private SLTPBot _sltpBot;
    private (ConfigManager, BotLogger) _botSettings;
    
    public CryptoBot()
    {
        SetDataFromConfig();
    }
    
    public void SetDataFromConfig()
    {
        var configManager = new ConfigManager();
        var smtpSettings = configManager.GetSmtpSettings();
        if (smtpSettings == null) throw new NullReferenceException("smtpSettings in config is null");
        var smtp = new SmtpSender(smtpSettings);
        var email = configManager.GetEmailAddress();

        var botLogger = new BotLogger(smtp, email);
        
        _botSettings = (configManager, botLogger);
    }

    public void CreateSltpBot(SLTPInfo info)
    {
        _sltpBot = new SLTPBot(info, _botSettings.Item1, _botSettings.Item2);
    }

    public void CreateArbitrageBot()
    {
        _arbitrageBot = new ArbitrageBot(_botSettings.Item1, _botSettings.Item2);
    }
}