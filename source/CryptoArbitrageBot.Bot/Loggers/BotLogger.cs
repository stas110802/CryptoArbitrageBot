using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Models.Logs;
using CryptoArbitrageBot.Bot.Types;

namespace CryptoArbitrageBot.Bot.Loggers;

public sealed class BotLogger
{
    private readonly SmtpSender _smtpSender;
    private readonly JsonWriter _jsonWriter;
    private readonly string _recipientEmail;
    
    public BotLogger(SmtpSender sender, string email)
    {
        _jsonWriter = new JsonWriter();
        _smtpSender = sender;
        _recipientEmail = email;
    }
    
    public void AddLog<T>(T log)
        where T : ILog
    {
        AddLog(log, log.FilePath, log.Type, true);
    }
    
    private void AddLog<T>(T log, string filePath, SubjectType? subjectTheme = null, bool sendMailMessage = false)
        where T : ILog
    {
        if (log == null) throw new ArgumentNullException(nameof(log));
        
        _jsonWriter.LogInfoAtJsonArray(filePath, log);
        
        if(sendMailMessage is false) return;
        
        var msgTheme = subjectTheme?.Value;
        var emRes = _smtpSender.SendMailMessage(
            $"Akira-Bot [{msgTheme}]", log.ToString(), _recipientEmail);

        if (emRes) return;
        
        var mailErrorLog = new ErrorLog("Не удалось отправить сообщение на почту", SubjectType.Error);
        _jsonWriter.LogInfoAtJsonArray(mailErrorLog.FilePath, mailErrorLog);
    }
    
    public ErrorLog WriteErrorLog(string message, SubjectType? errorType = null)
    {
        errorType ??= SubjectType.Error;
        var errorLog = new ErrorLog(message, errorType);
        AddLog(errorLog);

        return errorLog;
    }
}