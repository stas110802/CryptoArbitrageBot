using Newtonsoft.Json;

namespace CryptoArbitrageBot.Bot.Utilities;

public static class PathHelper
{
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
    public static void CreateFile(string filePath, object? data = null)
    {
            using var file = File.CreateText(filePath);
            if(data == null)
                return;
            
            var serializer = new JsonSerializer();
            serializer.Serialize(file, data);
            file.Close();
    }

    public static bool IsFileExists(string filePath)
    {
        return File.Exists(filePath);
    }
}