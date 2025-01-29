using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Loggers;
using CryptoArbitrageBot.Bot.Models;
using CryptoArbitrageBot.Bot.Models.Logs;
using CryptoArbitrageBot.Bot.Types;

namespace CryptoArbitrageBot.Bot.Core;

public sealed class CryptoBot
{
    private ArbitrageBot _arbitrageBot;
    private SLTPBot _sltpBot;
    private BotLogger _botLogger;

    public void SetSettingsFromConfig()
    {
        var configManager = new ConfigManager();
        var smtpSettings = configManager.GetSmtpSettings();
        if (smtpSettings == null) throw new NullReferenceException("smtpSettings in config is null");
        var smtp = new SmtpSender(smtpSettings);
        var email = configManager.GetEmailAddress();

        _botLogger = new BotLogger(smtp, email);
    }

    public void CreateSltpBot(SLTPInfo info)
    {
        _sltpBot = new SLTPBot(info, _botLogger);
    }

    public void CreateArbitrageBot()
    {
        _arbitrageBot = new ArbitrageBot(_botLogger);
    }

    public ILog RunSltpBot()
    {
        return _sltpBot.StartSLTP();
    }

    public void RunArbitrageBot(ArbitrageInfo info)
    {
        _arbitrageBot.StartArbitrage(info);
    }
}