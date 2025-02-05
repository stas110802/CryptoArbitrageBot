using System.ComponentModel.DataAnnotations;
using CryptoArbitrageBot.Bot.Models.Configs;
using CryptoArbitrageBot.Bot.Utilities;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using CryptoArbitrageBot.Utilities;
using Newtonsoft.Json;
using static System.String;

namespace CryptoArbitrageBot.Bot;

public class ConfigManager
{
    private BotConfig _botConfig;

    public ConfigManager()
    {
        LoadConfig();
    }

    public SmtpSettings? GetSmtpSettings() => _botConfig.Smtp;
    
    public BotConfig GetCopyBotConfig()
    {
        return _botConfig.Copy();
    }

    public ExchangeApiOptions GetExchangeConfig(ExchangeType exchange)
    {
        var cfg = AttributeHelper.GetValueOf<ExchangeApiOptions, Exchanges>(_botConfig.Exchanges!, exchange);
        if(cfg == null) 
            throw new Exception($"Exchange '{exchange}' does not exist in configuration");
        if(IsNullOrWhiteSpace(cfg.PublicKey) || IsNullOrWhiteSpace(cfg.SecretKey))
            throw new Exception($"Public or secret key '{exchange}' is not configured");
        
        return cfg;
    }

    public string GetEmailAddress()
    {
        return _botConfig.ClientEmail;
    }

    public void SetExchangeApiKeys(ExchangeType exchange, string key, string secret)
    {
        var binanceTestnetConfig = AttributeHelper
            .GetValueOf<ExchangeApiOptions, Exchanges>(_botConfig.Exchanges!, exchange);
        if (binanceTestnetConfig == null)
            return;

        binanceTestnetConfig.PublicKey = key;
        binanceTestnetConfig.SecretKey = secret;
        UpdateConfig();
    }

    public void SetSmtpSettings(SmtpSettings smtpSettings)
    {
        _botConfig.Smtp = smtpSettings;
        UpdateConfig();
    }

    public bool SetEmailAddress(string emailAddress)
    {
        var emailValidator = new EmailAddressAttribute();
        var isValid = emailValidator.IsValid(emailAddress);
        _botConfig.ClientEmail = emailAddress;
        UpdateConfig();
        
        return isValid;
    }

    public void SetFullConfig(BotConfig cfg)
    {
        _botConfig = cfg;
        UpdateConfig();
    }

    private static string ConfigFilePath => $@"{FolderPathList.ProjectFolder}\cfg.json";

    private void UpdateConfig()
    {
        FileManager.CreateReadyFile(ConfigFilePath, _botConfig);
    }

    private void LoadConfig()
    {
        BotConfig? cfg = null;

        if (FileManager.IsFileExists(ConfigFilePath))
            cfg = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(ConfigFilePath));

        if (cfg == null)
        {
            _botConfig = new BotConfig();
            return;
        }

        _botConfig = cfg;
        UpdateConfig();
    }
}