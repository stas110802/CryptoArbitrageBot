﻿namespace CryptoArbitrageBot.Bot.Models.Configs;

public sealed class SmtpSettings
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 0;
}