using System.Net;
using System.Net.Mail;
using CryptoArbitrageBot.Bot.Models.Configs;

namespace CryptoArbitrageBot.Bot.Loggers;

public sealed class SmtpSender
{
    private readonly SmtpSettings _settings;

    public SmtpSender(SmtpSettings settings)
    {
        _settings = settings;
    }
    
    public bool SendMailMessage(string subject, string content, string recipient)
    {
        if (recipient.Length == 0)
            throw new ArgumentException("recipient length == 0");

        using var smtpClient = new SmtpClient();
        smtpClient.Host = _settings.Host;
        smtpClient.Port = _settings.Port;
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(
            _settings.Login,
            _settings.Password);

        using var msg = new MailMessage(_settings.Login, recipient, subject, content);
        msg.To.Add(recipient);
        
        try
        {
            smtpClient.Send(msg);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}