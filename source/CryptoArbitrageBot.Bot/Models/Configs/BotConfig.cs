using Newtonsoft.Json;

namespace CryptoArbitrageBot.Bot.Models.Configs;

public sealed class BotConfig
{
    [JsonProperty("exchanges")]
    public Exchanges? Exchanges { get; set; } = new();

    [JsonProperty("smtpSettings")]
    public SmtpSettings? Smtp { get; set; } = new();

    [JsonProperty("clientMail")]
    public string ClientEmail {get; set;} = string.Empty;
}