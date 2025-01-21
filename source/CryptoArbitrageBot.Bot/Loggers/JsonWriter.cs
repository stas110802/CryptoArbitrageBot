using Newtonsoft.Json;

namespace CryptoArbitrageBot.Bot.Loggers;

public class JsonWriter
{
    /// <summary>
    /// Write log info to json file
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="logInfo"></param>
    public void LogInfoAtJsonArray<T>(string filePath, T logInfo)
    {
        var isFileExists = FileManager.IsFileExists(filePath);
        if(isFileExists == false)
            FileManager.CreateEmptyFile(filePath);
        
        var jsonData = File.ReadAllText(filePath);
        
        
        var logList = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? [];
        logList.Add(logInfo);
        
        jsonData = JsonConvert.SerializeObject(logList, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
}