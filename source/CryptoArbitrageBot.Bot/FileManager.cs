using CryptoArbitrageBot.Bot.Utilities;

namespace CryptoArbitrageBot.Bot;

public class FileManager
{
    public void InitializeBaseFolders()
    {
        var pl = new FolderPathList();
        var projectFolders = StringHelper.GetStringValues(pl);
        PathHelper.CheckForFolderExists(projectFolders);
    }
}