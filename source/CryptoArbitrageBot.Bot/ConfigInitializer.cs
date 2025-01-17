using CryptoArbitrageBot.Bot.Models.Configs;
using CryptoArbitrageBot.Bot.Utilities;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CryptoArbitrageBot.Bot;

public class ConfigInitializer
{
    private BotConfig _botConfig;
    
    public ConfigInitializer()
    {
        LoadConfig();
    }
    
    public SmtpSettings? GetSmtpConfig() => _botConfig.Smtp;

    public BotConfig GetCopyBotConfig()
    {
        return _botConfig.Copy();
    }
    
    public ExchangeApiOptions? GetExchangeConfig(ExchangeType exchange)
    {
        var cfg = AttributeHelper.GetValueOf<ExchangeApiOptions>(_botConfig.Exchanges!, exchange);
        
        return cfg;
    }

    public string GetEmailAddress()
    {
        return _botConfig.ClientEmail;
    }
    
    
    public void SetExchangeApiKeys(ExchangeType exchange, string key, string secret)
    {
        var binanceTestnetConfig = AttributeHelper.GetValueOf<ExchangeApiOptions>(_botConfig.Exchanges!, exchange);
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
    
    public void SetEmailAddress(string emailAddress)
    {
        var emailValidator = new EmailAddressAttribute();
        var isValid = emailValidator.IsValid(emailAddress);
        if(isValid == false)
            throw new ArgumentException("Invalid email address");
        
        _botConfig.ClientEmail = emailAddress;
        UpdateConfig();
    }

    public void SetFullConfig(BotConfig cfg)
    {
        _botConfig = cfg;
        UpdateConfig();
    }
    
    private static string ConfigFilePath => $@"{FolderPathList.ProjectFolder}\cfg.json";

    private void UpdateConfig()
    {
        PathHelper.CreateFile(ConfigFilePath, _botConfig);
    }

    private void LoadConfig()
    {
        BotConfig? cfg = null;
        
        if(PathHelper.IsFileExists(ConfigFilePath))
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