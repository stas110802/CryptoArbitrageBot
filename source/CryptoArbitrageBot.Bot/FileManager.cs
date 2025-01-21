using CryptoArbitrageBot.Bot.Utilities;
using Newtonsoft.Json;

namespace CryptoArbitrageBot.Bot;

public class FileManager
{
    public static void InitializeBaseFolders()
    {
        var pl = new FolderPathList();
        var projectFolders = StringHelper.GetStringValues(pl);
        CheckForFolderExists(projectFolders);
    }
    
    /// <summary>
    /// Checks whether the directory exists, if not, creates it
    /// </summary>
    /// <param name="folders"></param>
    public static void CheckForFolderExists(params string[] folders)
    {
        foreach (var path in folders)
        {
            if (Directory.Exists(path)) 
                continue;

            Directory.CreateDirectory(path);
        }
    }
    
    /// <summary>
    /// Checks whether the file exists, if not, creates it
    /// </summary>
    /// <param name="filePath"></param>
    public static void CreateEmptyFile(string filePath)
    {
        using var file = File.CreateText(filePath);
        file.Close();
    }
    
    public static void CreateReadyFile<T>(string filePath, T data)
    {
        var isFileExists = IsFileExists(filePath);
        if(isFileExists == false)
            CreateEmptyFile(filePath);
        
        var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
    
    public static bool IsFileExists(string filePath)
    {
        return File.Exists(filePath);
    }
}