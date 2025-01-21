using System.Runtime.Serialization;
using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Types;
using CryptoArbitrageBot.Bot.Utilities;

namespace CryptoArbitrageBot.Bot.Models.Logs;

public class ErrorLog : ILog
{
    public ErrorLog() { }

    public ErrorLog(string message, SubjectType subjectType)
    {
        Message = message;
        Type = subjectType;
        ErrorDate = DateTime.UtcNow;
    }
    
    [DataMember]
    public string Message { get; set; }
    
    [DataMember]
    public DateTime ErrorDate { get; set; }

    public string FilePath => $"{FolderPathList.ErrorsFolder}{ErrorDate:dd.MM.yyyy}.json";
    public SubjectType? Type { get; init; }

    public override string ToString()
    {
        return $"Дата возникновения ошибки: {ErrorDate}\n" +
               $"{Type}: {Message}";
    }
}